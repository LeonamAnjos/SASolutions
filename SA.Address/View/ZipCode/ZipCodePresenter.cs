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
using SA.BreadCrumb.View;
using Microsoft.Practices.Unity;


using SA.Address.View.City;
using System.Windows.Input;
using SA.Repository.Domain;
using SA.Repository.Repositories;

namespace SA.Address.View.ZipCode
{
    class ZipCodePresenter : IZipCodePresenter, IDataErrorInfo
    {
        #region Properties
        private Cep _cep;
        private ValidationResults _validationResults;
        private readonly IZipCodeView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public ZipCodePresenter(IZipCodeView view, Cep cep, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (cep == null)
            {
                throw new ArgumentNullException("cep");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);
            this.SearchCityCommand = new DelegateCommand<object>(this.SearchCity);

            this.Cep = cep;

            this._view = view;
            this._view.SetPresenter(this);
            
            OnPropertyChanged("Cep"); // para executar a validação dos dados
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IZipCodePresenter
        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        /// <summary>
        /// Search City command - called when searching a City is requeired
        /// </summary>
        public ICommand SearchCityCommand { get; private set; }

        public Cep Cep
        {
            get 
            {
                return _cep;
            }

            private set
            {
                if ((_cep != null) && _cep.Equals(value))
                    return;

                _cep = value;
            }
        }

        public string CEP
        {
            get
            {
                if (Cep == null)
                    return String.Empty;

                return Cep.CEP;
            }
            set
            {
                if (Cep == null)
                    return;

                if (Cep.CEP != null && Cep.CEP.Equals(value))
                    return;

                Cep.CEP = value;
                OnPropertyChanged("Cep");
            }
        }
        public string Logradouro
        {
            get
            {
                if (Cep == null)
                    return String.Empty;

                return Cep.Logradouro;
            }

            set
            {
                if (Cep == null)
                    return;

                if (Cep.Logradouro != null && Cep.Logradouro.Equals(value))
                    return;

                Cep.Logradouro = value;
                OnPropertyChanged("Logradouro");
            }
        }
        public string Bairro
        {
            get
            {
                if (Cep == null)
                    return String.Empty;

                return Cep.Bairro;
            }

            set
            {
                if (Cep == null)
                    return;

                if (Cep.Bairro != null && Cep.Bairro.Equals(value))
                    return;

                Cep.Bairro = value;
                OnPropertyChanged("Bairro");
            }
        }
        public int CidadeID
        {
            get
            {
                if (this.Cidade == null)
                    return 0;

                return this.Cidade.Id;
            }

            set
            {
                if (this.Cidade != null)
                    if (this.Cidade.Id.Equals(value))
                        return;

                try
                {
                    var repository = _container.Resolve<ICityRepository>();
                    var reg = repository.GetById(value);
                    this.Cidade = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Cidade = null;
                }
            }
        }
        public Cidade Cidade
        {
            get
            {
                return Cep.Cidade;
            }

            set
            {
                if (Cep.Cidade != null)
                    if (Cep.Cidade.Equals(value))
                        return;

                //Cep.CidadeReference.Value = value;
                Cep.Cidade = value;
                OnPropertyChanged("Cidade");
                OnPropertyChanged("CidadeID");
            }
        }
        
        #endregion

        #region Methods
        private void SearchCity(object param)
        {
            ICityListPresenter presenter = this._container.Resolve<ICityListPresenter>("ICityListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Cidade = presenter.Cidade;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Cidade", presenter);
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
        
        private string lastZipCodeValidated = string.Empty;
        private void Validar()
        {
            _validationResults = Validation.Validate(_cep);

            if (lastZipCodeValidated.Equals(_cep.CEP))
                return;

            lastZipCodeValidated = _cep.CEP;

            var repository = _container.Resolve<IZipCodeRepository>();
            var reg = repository.GetByZipCode(_cep.CEP);
            
            if ((reg != null) && (!reg.Id.Equals(_cep.Id)))
            {
                if (_validationResults == null)
                    _validationResults = new ValidationResults();
                _validationResults.AddResult(new ValidationResult("CEP já cadastrado!", _cep, "CEP", null, null));
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
