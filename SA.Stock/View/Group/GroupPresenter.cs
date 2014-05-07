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

namespace SA.Stock.View.Group
{
    public class GroupPresenter : IGroupPresenter, IDataErrorInfo
    {
        #region Properties
        private Grupo _grupo;
        private ValidationResults _validationResults;
        private readonly IGroupView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public GroupPresenter(IGroupView view, Grupo grupo, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (grupo == null)
            {
                throw new ArgumentNullException("grupo");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);

            this._view = view;
            this._view.SetPresenter(this);
            this._container = container;

            this.Grupo = grupo;
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IGroupViewModel
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        public ICommand SubmitCommand { get; private set; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        public Grupo Grupo
        {
            get 
            {
                return _grupo;
            }

            private set
            {
                if ((_grupo != null) && _grupo.Equals(value))
                    return;

                _grupo = value;
                OnPropertyChanged("Grupo");
            }
        }
        public string Descricao
        {
            get
            {
                if (Grupo == null)
                    return String.Empty;

                return Grupo.Descricao;
            }

            set
            {
                if (Grupo == null)
                    return;

                if (Grupo.Descricao != null && Grupo.Descricao.Equals(value))
                    return;

                Grupo.Descricao = value;
                OnPropertyChanged("Descricao");
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
            _validationResults = Validation.Validate(this.Grupo);
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
