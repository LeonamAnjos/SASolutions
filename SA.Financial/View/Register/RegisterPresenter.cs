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
using SA.Financial.View.FinancialAccount;

namespace SA.Financial.View.Register
{
    public class RegisterPresenter : IRegisterPresenter, IDataErrorInfo
    {
        #region Properties
        private Cadastro _cadastro;
        private ValidationResults _validationResults;
        private readonly IRegisterView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public RegisterPresenter(IRegisterView view, Cadastro cadastro, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (cadastro == null)
            {
                throw new ArgumentNullException("cadastro");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);

            this.SearchZipCodeMailCommand = new DelegateCommand<object>(this.SearchZipCodeMail);
            this.AddFinancialAccountCommand = new DelegateCommand<Cadastro>(this.AddFinancialAccount);
            this.UpdateFinancialAccountCommand = new DelegateCommand<Cadastro>(this.UpdateFinancialAccount, this.CanUpdateFinancialAccount);

            this._view = view;
            this._view.SetPresenter(this);
            this._container = container;

            this.Cadastro = cadastro;
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IRegisterViewModel

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
        /// Search Zip Code Mail command - called when searching a Zip Code Mail is requeired
        /// </summary>
        public ICommand SearchZipCodeMailCommand { get; private set; }
        /// <summary>
        /// Add Financial Account command - called when adding a Financial Account is requeired
        /// </summary>
        public ICommand AddFinancialAccountCommand { get; private set; }
        /// <summary>
        /// Update Financial Account command - called when updating a Financial Account is requeired
        /// </summary>
        public ICommand UpdateFinancialAccountCommand { get; private set; }
        
        #endregion

        public Cadastro Cadastro
        {
            get 
            {
                return _cadastro;
            }

            private set
            {
                if ((_cadastro != null) && _cadastro.Equals(value))
                    return;

                _cadastro = value;

                _numeroCorrespCep = (_cadastro.CorrespCep != null ? _cadastro.CorrespCep.CEP : String.Empty);
                OnPropertyChanged("Cadastro");
            }
        }

        public PersonType Tipo
        {
            get
            {
                return Cadastro.Tipo;
            }
            set
            {
                if (Cadastro.Tipo.Equals(value))
                    return;

                Cadastro.Tipo = value;
                OnPropertyChanged("Tipo");
                OnPropertyChanged("CPF");
            }
        }

        public ActiveInactiveType Situacao
        {
            get
            {
                return Cadastro.Situacao;
            }
            set
            {
                if (Cadastro.Situacao.Equals(value))
                    return;

                Cadastro.Situacao = value;
                OnPropertyChanged("Situacao");
            }
        }

        public string Nome
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.Nome;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.Nome != null && Cadastro.Nome.Equals(value))
                    return;

                Cadastro.Nome = value;
                OnPropertyChanged("Nome");
            }
        }

        public string RazaoSocial
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.RazaoSocial;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.RazaoSocial != null && Cadastro.RazaoSocial.Equals(value))
                    return;

                Cadastro.RazaoSocial = value;
                OnPropertyChanged("RazaoSocial");
            }
        }

        public string Contato
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.Contato;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.Contato != null && Cadastro.Contato.Equals(value))
                    return;

                Cadastro.Contato = value;
                OnPropertyChanged("Contato");
            }
        }

        public string CPF
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.CPF;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.CPF != null && Cadastro.CPF.Equals(value))
                    return;

                if (value.Equals(String.Empty))
                    value = null;
                Cadastro.CPF = value;
                OnPropertyChanged("CPF");
            }
        }

        public string RG
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.RG;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.RG != null && Cadastro.RG.Equals(value))
                    return;

                Cadastro.RG = value;
                OnPropertyChanged("RG");
            }
        }

        #region CorrespCep
        private string _numeroCorrespCep;
        public string NumeroCorrespCep
        {
            get
            {
                if (Cadastro.CorrespCep != null)
                    return Cadastro.CorrespCep.CEP;
                return this._numeroCorrespCep;
            }

            set
            {
                if (this._numeroCorrespCep != null)
                    if (this._numeroCorrespCep.Equals(value))
                        return;

                _numeroCorrespCep = value;

                if (String.IsNullOrEmpty(value))
                {
                    this.CorrespCep = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IZipCodeRepository>();
                    var reg = repository.GetByZipCode(value);
                    this.CorrespCep = reg;
                }
                catch (InvalidOperationException)
                {
                    this.CorrespCep = null;
                }
            }
        }
        public Cep CorrespCep
        {
            get
            {
                return Cadastro.CorrespCep;
            }

            set
            {
                 if (Cadastro.CorrespCep != null)
                    if (Cadastro.CorrespCep.Equals(value))
                        return;

                Cadastro.CorrespCep = value;
                if (value != null)
                    _numeroCorrespCep = value.CEP;

                OnPropertyChanged("CorrespCep");
                OnPropertyChanged("NumeroCorrespCep");

            }
        }
        #endregion

        public int CorrespNumero
        {
            get
            {
                if (Cadastro == null)
                    return 0;

                return Cadastro.CorrespNumero;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.CorrespNumero != null && Cadastro.CorrespNumero.Equals(value))
                    return;

                Cadastro.CorrespNumero = value;
                OnPropertyChanged("CorrespNumero");
            }
        }

        public string CorrespComplemento
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.CorrespComplemento;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.CorrespComplemento != null && Cadastro.CorrespComplemento.Equals(value))
                    return;

                Cadastro.CorrespComplemento = value;
                OnPropertyChanged("CorrespComplemento");
            }
        }

        public string Telefone
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.Telefone;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.Telefone != null && Cadastro.Telefone.Equals(value))
                    return;

                Cadastro.Telefone = value;
                OnPropertyChanged("Telefone");
            }
        }

        public string Celular
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.Celular;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.Celular != null && Cadastro.Celular.Equals(value))
                    return;

                Cadastro.Celular = value;
                OnPropertyChanged("Celular");
            }
        }

        public string Fax
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.Fax;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.Fax != null && Cadastro.Fax.Equals(value))
                    return;

                Cadastro.Fax = value;
                OnPropertyChanged("Fax");
            }
        }

        public string EMail
        {
            get
            {
                if (Cadastro == null)
                    return String.Empty;

                return Cadastro.EMail;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.EMail != null && Cadastro.EMail.Equals(value))
                    return;

                Cadastro.EMail = value;
                OnPropertyChanged("EMail");
            }
        }

        public DateTime DataNascimento
        {
            get
            {
                if (Cadastro == null)
                    return DateTime.FromBinary(0);

                return Cadastro.DataNascimento;
            }

            set
            {
                if (Cadastro == null)
                    return;

                if (Cadastro.DataNascimento != null && Cadastro.DataNascimento.Equals(value))
                    return;

                Cadastro.DataNascimento = value;
                OnPropertyChanged("DataNascimento");
            }
        }

        public DateTime DataInclusao
        {
            get
            {
                if (Cadastro == null)
                    return DateTime.Now;

                return Cadastro.DataInclusao;
            }
        }

        public DateTime DataAlteracao
        {
            get
            {
                if (Cadastro == null)
                    return DateTime.Now;

                return Cadastro.DataAlteracao;
            }
        }

        public ObservableCollection<ContaFinanceira> ContasFinanceiras
        {
            get
            {
                if (Cadastro == null)
                    return null;
                return new ObservableCollection<ContaFinanceira>(Cadastro.ContasFinanceiras);
            }
        }
        private ContaFinanceira _contaFinanceira;
        public ContaFinanceira ContaFinanceira 
        {
            get
            {
                return _contaFinanceira;
            }
            set
            {
                if (_contaFinanceira != null)
                    if (_contaFinanceira.Equals(value))
                        return;

                _contaFinanceira = value;
                OnPropertyChanged("ContaFinanceira");
                (this.UpdateFinancialAccountCommand as DelegateCommand<Cadastro>).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Methods
        private void SearchZipCodeMail(object param)
        {
            IZipCodeListPresenter presenter = this._container.Resolve<IZipCodeListPresenter>("IZipCodeListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.CorrespCep = presenter.Cep;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("CEPs", presenter);
        }

        private void AddFinancialAccount(Cadastro target)
        {
            ShowFinancialAccountView(new ContaFinanceira() { Cadastro = this.Cadastro, Situacao = ActiveInactiveType.Active });
        }

        private void UpdateFinancialAccount(Cadastro target)
        {
            ShowFinancialAccountView(this.ContaFinanceira);
        }

        private bool CanUpdateFinancialAccount(Cadastro target)
        {
            return this.ContaFinanceira != null;
        }

        private void ShowFinancialAccountView(ContaFinanceira contaFinanceira)
        {
            IFinancialAccountPresenter presenter = this._container.Resolve<IFinancialAccountPresenter>("IFinancialAccountPresenter", new ParameterOverrides { { "contaFinanceira", contaFinanceira } });
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit))
                {
                    if (!this.Cadastro.ContasFinanceiras.Contains<ContaFinanceira>(presenter.ContaFinanceira))
                        this.Cadastro.ContasFinanceiras.Add(presenter.ContaFinanceira);
                    OnPropertyChanged("ContasFinanceiras");
                }
                OnPropertyChanged("Tipo");
                OnPropertyChanged("Situacao");
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Conta Financeira", presenter);
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
            _validationResults = Validation.Validate(this.Cadastro);

            /// ZipCodeMail Validation
            if ((!(String.IsNullOrEmpty(this.NumeroCorrespCep))) && (this.CorrespCep == null))
            {
                if (_validationResults == null)
                    _validationResults = new ValidationResults();
                _validationResults.AddResult(new ValidationResult("CEP inválido!", this, "NumeroCorrespCep", null, null));
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
