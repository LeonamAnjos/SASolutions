using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using SA.Repository.Domain;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using SA.BreadCrumb.View;
using SA.Repository.Repositories;
using SA.Repository.Enums;
using SA.Address.View.ZipCode;

namespace SA.Financial.View.FinancialAccount
{
    public class FinancialAccountPresenter : IFinancialAccountPresenter, IDataErrorInfo
    {
        #region Properties
        private ContaFinanceira _contaFinanceira;
        private ValidationResults _validationResults;
        private readonly IFinancialAccountView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public FinancialAccountPresenter(IFinancialAccountView view, ContaFinanceira contaFinanceira, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (contaFinanceira == null)
            {
                throw new ArgumentNullException("ContaFinanceira");
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

            this.ContaFinanceira = contaFinanceira;
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IFinancialAccountViewModel

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        public ICommand SubmitCommand { get; private set; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        public ICommand CancelCommand { get; private set; }
        /// <summary>
        /// Search Zip Code command - called when searching a Zip Code is requeired
        /// </summary>
        public ICommand SearchZipCodeCommand { get; private set; }
        #endregion

        public ContaFinanceira ContaFinanceira
        {
            get 
            {
                return _contaFinanceira;
            }

            private set
            {
                if ((_contaFinanceira != null) && _contaFinanceira.Equals(value))
                    return;

                _contaFinanceira = value;

                _numeroCobrancaCep = (_contaFinanceira.CobrancaCep != null ? _contaFinanceira.CobrancaCep.CEP : String.Empty);
                OnPropertyChanged("ContaFinanceira");
            }
        }

        public FinancialAccountType Tipo
        {
            get
            {
                return ContaFinanceira.Tipo;
            }
            set
            {
                if (ContaFinanceira.Tipo.Equals(value))
                    return;

                ContaFinanceira.Tipo = value;
                OnPropertyChanged("Tipo");
            }
        }

        public ActiveInactiveType Situacao
        {
            get
            {
                return ContaFinanceira.Situacao;
            }
            set
            {
                if (ContaFinanceira.Situacao.Equals(value))
                    return;

                ContaFinanceira.Situacao = value;
                OnPropertyChanged("Situacao");
            }
        }

        #region CobrancaCep
        private string _numeroCobrancaCep;
        public string NumeroCobrancaCep
        {
            get
            {
                if (ContaFinanceira.CobrancaCep != null)
                    return ContaFinanceira.CobrancaCep.CEP;
                return this._numeroCobrancaCep;
            }

            set
            {
                if (this._numeroCobrancaCep != null)
                    if (this._numeroCobrancaCep.Equals(value))
                        return;

                _numeroCobrancaCep = value;

                if (String.IsNullOrEmpty(value))
                {
                    this.CobrancaCep = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IZipCodeRepository>();
                    var reg = repository.GetByZipCode(value);
                    this.CobrancaCep = reg;
                }
                catch (InvalidOperationException)
                {
                    this.CobrancaCep = null;
                }
            }
        }
        public Cep CobrancaCep
        {
            get
            {
                return ContaFinanceira.CobrancaCep;
            }

            set
            {
                 if (ContaFinanceira.CobrancaCep != null)
                    if (ContaFinanceira.CobrancaCep.Equals(value))
                        return;

                ContaFinanceira.CobrancaCep = value;
                if (value != null)
                    _numeroCobrancaCep = value.CEP;

                OnPropertyChanged("CobrancaCep");
                OnPropertyChanged("NumeroCobrancaCep");

            }
        }
        #endregion

        public int CobrancaNumero
        {
            get
            {
                if (ContaFinanceira == null)
                    return 0;

                return ContaFinanceira.CobrancaNumero;
            }

            set
            {
                if (ContaFinanceira == null)
                    return;

                if (ContaFinanceira.CobrancaNumero != null && ContaFinanceira.CobrancaNumero.Equals(value))
                    return;

                ContaFinanceira.CobrancaNumero = value;
                OnPropertyChanged("CobrancaNumero");
            }
        }

        public string CobrancaComplemento
        {
            get
            {
                if (ContaFinanceira == null)
                    return String.Empty;

                return ContaFinanceira.CobrancaComplemento;
            }

            set
            {
                if (ContaFinanceira == null)
                    return;

                if (ContaFinanceira.CobrancaComplemento != null && ContaFinanceira.CobrancaComplemento.Equals(value))
                    return;

                ContaFinanceira.CobrancaComplemento = value;
                OnPropertyChanged("CobrancaComplemento");
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
                    this.CobrancaCep = presenter.Cep;
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
            _validationResults = Validation.Validate(this.ContaFinanceira);

            /// ZipCodeMail Validation
            if ((!(String.IsNullOrEmpty(this.NumeroCobrancaCep))) && (this.CobrancaCep == null))
            {
                if (_validationResults == null)
                    _validationResults = new ValidationResults();
                _validationResults.AddResult(new ValidationResult("CEP inválido!", this, "NumeroCobrancaCep", null, null));
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
