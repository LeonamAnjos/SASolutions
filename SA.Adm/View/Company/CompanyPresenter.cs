using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SA.Infrastructure;
using System.Windows;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections.ObjectModel;
using SA.Adm.View.UserGroup;
using SA.BreadCrumb.View;
using Microsoft.Practices.Unity;
using SA.Repository.Domain;
using SA.Repository.Enums;
using SA.Repository.Repositories;
using System.Windows.Input;
using SA.Address.View.ZipCode;

namespace SA.Adm.View.Company
{
    class CompanyPresenter : ICompanyPresenter, IDataErrorInfo
    {
        #region Properties
        private Empresa _empresa;
        private ValidationResults _validationResults;
        private readonly ICompanyView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CompanyPresenter(ICompanyView view, Empresa empresa, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (empresa == null)
            {
                throw new ArgumentNullException("empresa");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);
            this.SearchZipCodeCommand = new DelegateCommand<object>(this.SearchZipCode);

            this._view = view;
            this._view.SetPresenter(this);
            this._container = container;

            this.Empresa = empresa;
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IUserPresenter
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        public ICommand SubmitCommand { get; private set; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        public ICommand CancelCommand { get; private set; }
        /// <summary>
        /// Search State command - called when searching a Zip Code is requeired
        /// </summary>
        public ICommand SearchZipCodeCommand { get; private set; }
        

        public Empresa Empresa
        {
            get 
            {
                return _empresa;
            }

            private set
            {
                if ((_empresa != null) && _empresa.Equals(value))
                    return;

                _empresa = value;
            }
        }
        public string Nome
        {
            get
            {
                if (Empresa == null)
                    return String.Empty;

                return Empresa.Nome;
            }

            set
            {
                if (Empresa == null)
                    return;

                if (Empresa.Nome != null && Empresa.Nome.Equals(value))
                    return;

                Empresa.Nome = value;
                OnPropertyChanged("Nome");
            }
        }
        public string CNPJ
        {
            get
            {
                if (Empresa == null)
                    return String.Empty;

                return Empresa.CNPJ;
            }

            set
            {
                if (Empresa == null)
                    return;

                if (Empresa.CNPJ != null && Empresa.CNPJ.Equals(value))
                    return;

                Empresa.CNPJ = value;
                OnPropertyChanged("CNPJ");
            }
        }
        public string InscricaoEstadual
        {
            get
            {
                if (Empresa == null)
                    return String.Empty;

                return Empresa.InscricaoEstadual;
            }

            set
            {
                if (Empresa == null)
                    return;

                if (Empresa.InscricaoEstadual != null && Empresa.InscricaoEstadual.Equals(value))
                    return;

                Empresa.InscricaoEstadual = value;
                OnPropertyChanged("InscricaoEstadual");
            }
        }
        public string Complemento
        {
            get
            {
                if (Empresa == null)
                    return String.Empty;

                return Empresa.Complemento;
            }

            set
            {
                if (Empresa == null)
                    return;

                if (Empresa.Complemento != null && Empresa.Complemento.Equals(value))
                    return;

                Empresa.Complemento = value;
                OnPropertyChanged("Complemento");
            }
        }
        public int Numero
        {
            get
            {
                if (Empresa == null)
                    return 0;

                return Empresa.Numero;
            }

            set
            {
                if (Empresa == null)
                    return;

                if (Empresa.Numero.Equals(value))
                    return;

                Empresa.Numero = value;
                OnPropertyChanged("Numero");
            }
        }
        private string _numeroCep;
        public string NumeroCep
        {
            get
            {
                if (Empresa.Cep != null)
                    return Empresa.Cep.CEP;
                return this._numeroCep;
            }

            set
            {
                if (this._numeroCep != null)
                    if (this._numeroCep.Equals(value))
                        return;

                try
                {
                    var repository = _container.Resolve<IZipCodeRepository>();
                    var reg = repository.GetByZipCode(value);
                    this.Cep = reg;
                    _numeroCep = value;
                }
                catch (InvalidOperationException)
                {
                    this.Cep = null;
                }
                OnPropertyChanged("NumeroCep");
            }
        }
        public Cep Cep
        {
            get
            {
                return Empresa.Cep;
            }

            set
            {
                if (Empresa.Cep != null)
                    if (Empresa.Cep.Equals(value))
                        return;

                Empresa.Cep = value;
                if (value != null)
                {
                    _numeroCep = value.CEP;
                    OnPropertyChanged("NumeroCep");
                }

                OnPropertyChanged("Cep");
            }
        }
        #endregion

        #region Methods
        private void SearchZipCode(object param)
        {
            IZipCodeListPresenter presenter = this._container.Resolve<IZipCodeListPresenter>("IZipCodeListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;
                
                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Cep = presenter.Cep;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("CEPs", presenter);
        }

        private bool CanSubmit(object parameter)
        {
            return (_validationResults == null ? true : _validationResults.IsValid);
        }
        private void Submit(object parameter)
        {
            CloseViewRequested(this, new CloseViewEventArgs(CloseViewType.Submit));
        }
        private void Cancel(object parameter)
        {
            CloseViewRequested(this, new CloseViewEventArgs(CloseViewType.Cancel));
        }
        
        private void Validar()
        {
            _validationResults = Validation.Validate(_empresa);
            if((this.NumeroCep != null) && (this.Cep == null)) {
                if (_validationResults == null)
                    _validationResults = new ValidationResults();
                _validationResults.AddResult(new ValidationResult("CEP inválido!", this, "NumeroCep", null, null));
            }
                
                
        }
        #endregion

        #region IDataErrorInfo
        public string Error
        {
            get
            {
                // Not implemented because we are not consuming it. 
                // Instead, we are displaying error messages at the item level. 
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (_validationResults == null)
                    return null;

                string resultado = Common.GetColumnValidationMessage(columnName, _validationResults);
                return resultado;
            }

            set
            {
                // Not implemented because we are using validation results
                throw new NotImplementedException();
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.Validar();
            (this.SubmitCommand as DelegateCommand<object>).RaiseCanExecuteChanged();

            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


    }
}
