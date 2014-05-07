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

using SA.Address.View.State;
using System.Windows.Input;
using SA.Repository.Domain;
using SA.Repository.Repositories;

namespace SA.Address.View.City
{
    class CityPresenter : ICityPresenter, IDataErrorInfo
    {
        #region Properties
        private Cidade _cidade;
        private ValidationResults _validationResults;
        private readonly ICityView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CityPresenter(ICityView view, Cidade cidade, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (cidade == null)
            {
                throw new ArgumentNullException("cidade");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);
            this.SearchStateCommand = new DelegateCommand<object>(this.SearchState);

            this.Cidade = cidade;

            this._view = view;
            this._view.SetPresenter(this);
            
            OnPropertyChanged("Cidade"); // para executar a validação dos dados
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
        /// Search State command - called when searching a State is requeired
        /// </summary>
        public ICommand SearchStateCommand { get; private set; }

        public Cidade Cidade
        {
            get 
            {
                return _cidade;
            }

            private set
            {
                if ((_cidade != null) && _cidade.Equals(value))
                    return;

                _cidade = value;
            }
        }
        public string Nome
        {
            get
            {
                if (Cidade == null)
                    return String.Empty;

                return Cidade.Nome;
            }

            set
            {
                if (Cidade == null)
                    return;

                if (Cidade.Nome != null && Cidade.Nome.Equals(value))
                    return;

                Cidade.Nome = value;
                OnPropertyChanged("Nome");
            }
        }
        public string DDD
        {
            get
            {
                if (Cidade == null)
                    return String.Empty;

                return Cidade.DDD;
            }

            set
            {
                if (Cidade == null)
                    return;

                if (Cidade.DDD != null && Cidade.DDD.Equals(value))
                    return;

                Cidade.DDD = value;
                OnPropertyChanged("DDD");
            }
        }
        public int EstadoID
        {
            get
            {
                if (this.Estado == null)
                    return 0;

                return this.Estado.Id;
            }

            set
            {
                if (this.Estado != null)
                    if (this.Estado.Id.Equals(value))
                        return;

                try
                {
                    var repository = _container.Resolve<IStateRepository>();
                    var reg = repository.GetById(value);
                    this.Estado = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Estado = null;
                }
            }
        }
        public Estado Estado
        {
            get
            {
                return Cidade.Estado;
            }

            set
            {
                if (Cidade.Estado != null)
                    if (Cidade.Estado.Equals(value))
                        return;

                //Cidade.EstadoReference.Value = value;
                Cidade.Estado = value;
                OnPropertyChanged("Estado");
                OnPropertyChanged("EstadoID");
            }
        }
        
        #endregion

        #region Methods
        private void SearchState(object param)
        {
            IStateListPresenter presenter = this._container.Resolve<IStateListPresenter>("IStateListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Estado = presenter.Estado;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Estado", presenter);
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
            _validationResults = Validation.Validate(_cidade);
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
