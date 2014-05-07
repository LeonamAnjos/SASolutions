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


using SA.Address.View.Country;
using System.Windows.Input;
using SA.Repository.Domain;
using SA.Repository.Repositories;

namespace SA.Address.View.State
{
    class StatePresenter : IStatePresenter, IDataErrorInfo
    {
        #region Properties
        private Estado _estado;
        private ValidationResults _validationResults;
        private readonly IStateView _view;
        private readonly IUnityContainer _container;
        //private readonly IAddressModelService _modelService;
        #endregion

        #region Constructors
        public StatePresenter(IStateView view, Estado estado, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (estado == null)
            {
                throw new ArgumentNullException("estado");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            //if (modelService == null)
            //{
            //    throw new ArgumentNullException("modelService");
            //}

            this._container = container;
            //this._modelService = modelService;

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);
            this.SearchCountryCommand = new DelegateCommand<object>(this.SearchCountry);

            this.Estado = estado;

            this._view = view;
            this._view.SetPresenter(this);
            
            OnPropertyChanged("Estado"); // para executar a validação dos dados
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IStatePresenter
        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        /// <summary>
        /// Search country command - called when searching a country is requeired
        /// </summary>
        public ICommand SearchCountryCommand { get; private set; }

        public Estado Estado
        {
            get 
            {
                return _estado;
            }

            private set
            {
                if ((_estado != null) && _estado.Equals(value))
                    return;

                _estado = value;
            }
        }
        public string Nome
        {
            get
            {
                if (Estado == null)
                    return String.Empty;

                return Estado.Nome;
            }

            set
            {
                if (Estado == null)
                    return;

                if (Estado.Nome != null && Estado.Nome.Equals(value))
                    return;

                Estado.Nome = value;
                OnPropertyChanged("Nome");
            }
        }
        public string Sigla
        {
            get
            {
                if (Estado == null)
                    return String.Empty;

                return Estado.Sigla;
            }

            set
            {
                if (Estado == null)
                    return;

                if (Estado.Sigla != null && Estado.Sigla.Equals(value))
                    return;

                Estado.Sigla = value;
                OnPropertyChanged("Sigla");
            }
        }
        public int PaisID
        {
            get
            {
                if (this.Pais == null)
                    return 0;

                return this.Pais.Id;
            }

            set
            {
                if (this.Pais != null)
                    if (this.Pais.Id.Equals(value))
                        return;

                try
                {
                    var repository = _container.Resolve<ICountryRepository>();
                    var reg = repository.GetById(value);
                    this.Pais = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Pais = null;
                }
            }
        }
        public Pais Pais
        {
            get
            {
                return Estado.Pais;
            }

            set
            {
                if (Estado.Pais != null)
                    if (Estado.Pais.Equals(value))
                        return;

                //Estado.PaisReference.Value = value;
                Estado.Pais = value;
                OnPropertyChanged("Pais");
                OnPropertyChanged("PaisID");
            }
        }
        
        #endregion

        #region Methods
        private void SearchCountry(object param)
        {
            ICountryListPresenter presenter = this._container.Resolve<ICountryListPresenter>("ICountryListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Pais = presenter.Pais;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("País", presenter);
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
            _validationResults = Validation.Validate(_estado);
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
