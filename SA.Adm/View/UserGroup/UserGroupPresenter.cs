using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Windows;
using System.Runtime.InteropServices;
using System.Security;
using SA.Repository.Domain;
using SA.Repository.Enums;
using SA.Infrastructure;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace SA.Adm.View.UserGroup
{
    class UserGroupPresenter : IUserGroupPresenter, IDataErrorInfo
    {
        #region Properties
        private UsuarioGrupo _grupo;
        private ValidationResults _validationResults;
        private IUserGroupView _view;
        #endregion

        #region Constructors
        public UserGroupPresenter(IUserGroupView view, UsuarioGrupo grupo)
        {
            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);
            this.Grupo = grupo;

            this._view = view;
            this._view.SetPresenter(this);
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IUserGroupPresenter
        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public UsuarioGrupo Grupo
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

        public UserGroupType Tipo
        {
            get
            {
                return Grupo.Tipo;
            }
            set
            {
                if (Grupo.Tipo.Equals(value))
                    return;

                Grupo.Tipo = value;
                OnPropertyChanged("Tipo");
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
            _validationResults = Validation.Validate(_grupo);
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
