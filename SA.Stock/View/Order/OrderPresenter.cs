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
using SA.Financial.View.Register;
using SA.Repository.Enums;
using SA.Stock.View.Vendor;



namespace SA.Stock.View.Order
{
    public class OrderPresenter : IOrderPresenter, IDataErrorInfo
    {
        #region Properties
        private Pedido _pedido;
        private ValidationResults _validationResults;
        private readonly IOrderView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public OrderPresenter(IOrderView view, Pedido pedido, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (pedido == null)
            {
                throw new ArgumentNullException("pedido");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);

            this.SearchRegisterCommand = new DelegateCommand<object>(this.SearchRegister);
            this.SearchVendorCommand = new DelegateCommand<object>(this.SearchVendor);

            this._view = view;
            this._view.SetPresenter(this);
            this._container = container;

            this.Pedido = pedido;
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IOrderViewModel

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        public ICommand SubmitCommand { get; set; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        public ICommand CancelCommand { get; set; }
        /// <summary>
        /// Search Register command - called when searching a Register is requeired
        /// </summary>
        public ICommand SearchRegisterCommand { get; set; }
        /// <summary>
        /// Search Vendor command - called when searching a Vendor is requeired
        /// </summary>
        public ICommand SearchVendorCommand { get; set; }
        #endregion

        public Pedido Pedido
        {
            get 
            {
                return _pedido;
            }

            private set
            {
                if ((_pedido != null) && _pedido.Equals(value))
                    return;

                _pedido = value;

                _cadastroID = (_pedido.Cadastro != null ? _pedido.Cadastro.Id : 0);
                _vendedorID = (_pedido.Vendedor != null ? _pedido.Vendedor.Id : 0);

                OnPropertyChanged("Pedido");
            }
        }

        public DateTime Data
        {
            get
            {
                if (Pedido == null)
                    return DateTime.Today;

                return Pedido.Data;
            }

            set
            {
                if (Pedido == null)
                    return;

                if (Pedido.Data != null && Pedido.Data.Equals(value))
                    return;

                Pedido.Data = value;
                OnPropertyChanged("Data");
            }
        }

        public DateTime Hora
        {
            get
            {
                if (Pedido == null)
                    return DateTime.Now;

                return Pedido.Hora;
            }

            set
            {
                if (Pedido == null)
                    return;

                if (Pedido.Hora != null && Pedido.Hora.Equals(value))
                    return;

                Pedido.Hora = value;
                OnPropertyChanged("Hora");
            }
        }

        public OrderType Tipo 
        {
            get
            {
                return Pedido.Tipo;
            }

            set
            {
                if (Pedido.Tipo.Equals(value))
                    return;

                Pedido.Tipo = value;
                OnPropertyChanged("Tipo");
                OnPropertyChanged("CadastroID");

                if (Pedido.Tipo == OrderType.PurchaseOrder)
                    VendedorID = 0;
                else
                    OnPropertyChanged("VendedorID");
            }
        }

        public DateTime DataValidade
        {
            get
            {
                if (Pedido == null)
                    return DateTime.Today;

                return Pedido.DataValidade;
            }

            set
            {
                if (Pedido == null)
                    return;

                if (Pedido.DataValidade != null && Pedido.DataValidade.Equals(value))
                    return;

                Pedido.DataValidade = value;
                OnPropertyChanged("DataValidade");
            }
        }

        public PhaseType Fase
        {
            get
            {
                return Pedido.Fase;
            }

            private set
            {
                if (Pedido.Fase.Equals(value))
                    return;

                Pedido.Fase = value;
                OnPropertyChanged("Fase");
            }
        }


        public Double Valor 
        {
            get
            {
                return Pedido.Valor;
            }
        }

        public Double ValorDesconto 
        {
            get
            {
                return Pedido.ValorDesconto;
            }

            set
            {
                if (Pedido == null)
                    return;

                if (Pedido.ValorDesconto != null && Pedido.ValorDesconto.Equals(value))
                    return;

                Pedido.ValorDesconto = value;
                OnPropertyChanged("ValorDesconto");
            }
        }

        public Double ValorDescontoTotal 
        {
            get
            {
                return Pedido.ValorDescontoTotal;
            }
        }

        #region Cadastro
        private int _cadastroID;
        public int CadastroID
        {
            get
            {
                if (Pedido.Cadastro != null)
                    return Pedido.Cadastro.Id;
                return this._cadastroID;
            }

            set
            {
                if (this._cadastroID.Equals(value))
                    return;

                _cadastroID = value;

                if (value < 1)
                {
                    this.Cadastro = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IRegisterRepository>();
                    var reg = repository.GetById(value);
                    this.Cadastro = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Cadastro = null;
                }
            }
        }
        public Cadastro Cadastro
        {
            get
            {
                return Pedido.Cadastro;
            }

            set
            {
                if (Pedido.Cadastro != null)
                    if (Pedido.Cadastro.Equals(value))
                        return;

                Pedido.Cadastro = value;
                if (value != null)
                  _cadastroID = value.Id;

                OnPropertyChanged("Cadastro");
                OnPropertyChanged("CadastroID");
            }
        }
        #endregion

        #region Vendedor
        private int _vendedorID;
        public int VendedorID
        {
            get
            {
                if (Pedido.Vendedor != null)
                    return Pedido.Vendedor.Id;
                return this._vendedorID;
            }

            set
            {
                if (this._vendedorID.Equals(value))
                    return;

                _vendedorID = value;
                
                if (value < 1)
                {
                    Vendedor = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IVendorRepository>();
                    var reg = repository.GetById(value);
                    this.Vendedor = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Vendedor = null;
                }
            }
        }
        public Vendedor Vendedor
        {
            get
            {
                return Pedido.Vendedor;
            }

            set
            {
                if (Pedido.Vendedor != null)
                    if (Pedido.Vendedor.Equals(value))
                        return;

                Pedido.Vendedor = value;
                
                if (value != null)
                    _vendedorID = value.Id;

                OnPropertyChanged("Vendedor");
                OnPropertyChanged("VendedorID");
            }
        }
        #endregion


        #endregion

        #region Methods

        private void SearchRegister(object param)
        {
            IRegisterListPresenter presenter = this._container.Resolve<IRegisterListPresenter>("IRegisterListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Cadastro = presenter.Cadastro;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Cadastros", presenter);
        }

        private void SearchVendor(object param)
        {
            IVendorListPresenter presenter = this._container.Resolve<IVendorListPresenter>("IVendorListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Vendedor = presenter.Vendedor;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Vendedores", presenter);
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
                _validationResults = Validation.Validate(this.Pedido);

            /// Register Validation
            if (this.CadastroID != 0)
            {
                if (this.Cadastro == null)
                    _validationResults.AddResult(new ValidationResult("Cadastro inválido!", this, "CadastroID", null, null));
            }
            
            /// Vendor Validation
            if (this.VendedorID != 0)
            {
                if (this.Vendedor == null)
                    _validationResults.AddResult(new ValidationResult("Vendedor inválido!", this, "VendedorID", null, null));
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
