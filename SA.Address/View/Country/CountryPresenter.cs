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


using System.Windows.Input;
using SA.Repository.Domain;

namespace SA.Address.View.Country
{
    class CountryPresenter : ICountryPresenter, IDataErrorInfo
    {
        #region Properties
        private Pais _pais;
        private ValidationResults _validationResults;
        private readonly ICountryView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CountryPresenter(ICountryView view, Pais pais, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (pais == null)
            {
                throw new ArgumentNullException("país");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;
            //this._modelService = modelService;

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);

            this.Pais = pais;

            this._view = view;
            this._view.SetPresenter(this);
            
            OnPropertyChanged("Pais"); // para executar a validação dos dados
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region ICountryPresenter
        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public Pais Pais
        {
            get 
            {
                return _pais;
            }

            private set
            {
                if ((_pais != null) && _pais.Equals(value))
                    return;

                _pais = value;
            }
        }
        public string Nome
        {
            get
            {
                if (Pais == null)
                    return String.Empty;

                return Pais.Nome;
            }

            set
            {
                if (Pais == null)
                    return;

                if (Pais.Nome != null && Pais.Nome.Equals(value))
                    return;

                Pais.Nome = value;
                OnPropertyChanged("Nome");
            }
        }
        public string Sigla
        {
            get
            {
                if (Pais == null)
                    return String.Empty;

                return Pais.Sigla;
            }

            set
            {
                if (Pais == null)
                    return;

                if (Pais.Sigla != null && Pais.Sigla.Equals(value))
                    return;

                Pais.Sigla = value;
                OnPropertyChanged("Sigla");
            }
        }
        
        #endregion

        #region Methods

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
            _validationResults = Validation.Validate(_pais);
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
