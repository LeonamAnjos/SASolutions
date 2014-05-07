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

namespace SA.Adm.View.User
{
    class UserPresenter : IUserPresenter, IDataErrorInfo
    {
        #region Properties
        private Usuario _usuario;
        private ValidationResults _validationResults;
        private readonly IUserView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public UserPresenter(IUserView view, Usuario usuario, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);
            this.SearchUserGroupCommand = new DelegateCommand<object>(this.SearchUserGroup);

            this.Usuario = usuario;
            this.SenhaConfirmacao = usuario.Senha;

            this._view = view;
            this._view.SetPresenter(this);
            this._view.PasswordChanged += PasswordChangedHandler;

            OnPropertyChanged("Usuario"); // para executar a validação dos dados
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
        public DelegateCommand<object> SubmitCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }
        /// <summary>
        /// Search user group command - called when searching a user group is requeired
        /// </summary>
        public DelegateCommand<object> SearchUserGroupCommand { get; private set; }
        

        public Usuario Usuario
        {
            get 
            {
                return _usuario;
            }

            private set
            {
                if ((_usuario != null) && _usuario.Equals(value))
                    return;

                _usuario = value;
            }
        }
        public string Login
        {
            get
            {
                if (Usuario == null)
                    return String.Empty;

                return Usuario.Login;
            }

            set
            {
                if (Usuario == null)
                    return;

                if (Usuario.Login != null && Usuario.Login.Equals(value))
                    return;

                Usuario.Login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Senha
        {
            get
            {
                if (Usuario == null)
                    return String.Empty;

                return Usuario.Senha;
            }
            private set
            {
                if (Usuario == null)
                    return;

                if ((Usuario.Senha == null) && (value == null))
                    return;

                if ((Usuario.Senha != null) && (Usuario.Senha.Equals(value)))
                    return;

                Usuario.Senha = value;
                OnPropertyChanged("Senha");
            }
        }
        private string _senhaConfirmacao;
        public string SenhaConfirmacao
        {
            get
            {
                return _senhaConfirmacao;
            }
            private set
            {
                if ((_senhaConfirmacao == null) && (value == null))
                    return;

                if ((_senhaConfirmacao != null) && (_senhaConfirmacao.Equals(value)))
                    return;

                _senhaConfirmacao = value;
                OnPropertyChanged("Senha");
            }
        }
        public string Nome
        {
            get
            {
                if (Usuario == null)
                    return String.Empty;

                return Usuario.Nome;
            }

            set
            {
                if (Usuario == null)
                    return;

                if (Usuario.Nome != null && Usuario.Nome.Equals(value))
                    return;

                Usuario.Nome = value;
                OnPropertyChanged("Nome");
            }
        }
        public string Email
        {
            get
            {
                if (Usuario == null)
                    return String.Empty;

                return Usuario.Email;
            }

            set
            {
                if (Usuario == null)
                    return;

                if (Usuario.Email != null && Usuario.Email.Equals(value))
                    return;

                Usuario.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public int Grupo
        {
            get
            {
                if (this.UsuarioGrupo == null)
                    return 0;

                return this.UsuarioGrupo.Id;
            }

            set
            {
                if (this.UsuarioGrupo != null)
                    if (this.UsuarioGrupo.Id.Equals(value))
                        return;

                try
                {
                    var repository = _container.Resolve<IUserGroupRepository>();
                    var reg = repository.GetById(value);
                    this.UsuarioGrupo = reg;
                }
                catch (InvalidOperationException)
                {
                    this.UsuarioGrupo = null;
                }
            }
        }
        public UsuarioGrupo UsuarioGrupo
        {
            get
            {
                return Usuario.Grupo;
            }

            set
            {
                if (Usuario.Grupo != null)
                    if (Usuario.Grupo.Equals(value))
                        return;

                Usuario.Grupo = value;
                OnPropertyChanged("UsuarioGrupo");
                OnPropertyChanged("Grupo");
            }
        }
        public ActiveInactiveType Situacao
        {
            get
            {
                return Usuario.Situacao;
            }
            set
            {
                if (Usuario.Situacao.Equals(value))
                    return;

                Usuario.Situacao = value;
                OnPropertyChanged("Situacao");
            }
        }
        #endregion

        #region Methods
        private void SearchUserGroup(object param)
        {
            IUserGroupListPresenter userGroupPresenter = this._container.Resolve<IUserGroupListPresenter>("IUserGroupListPresenter");
            userGroupPresenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;
                
                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.UsuarioGrupo = userGroupPresenter.Grupo;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Grupo de usuários", userGroupPresenter);
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
            _validationResults = Validation.Validate(_usuario);
            ValidarPassword(_validationResults);
        }
        private void ValidarPassword(ValidationResults results)
        {
            if (this.Senha == null)
            {
                results.AddResult(new ValidationResult("Senha do usuário não pode ser vazio!", this, "Password1", null, null));
                return;
            }

            if (this.Senha.Length > 20)
                results.AddResult(new ValidationResult("Senha do usuário deve ter no máximo 20 caracteres!", this, "Password1", null, null));

            if ((this.Senha.Length > 0) && (this.Senha.Length <= 20))
                if(!this.Senha.Equals(this.SenhaConfirmacao))
                    results.AddResult(new ValidationResult("A senha de confirmação deve ser igual a senha informada.", this, "Password2", null, null));
        }

        /*
         * MÉTODO PARA ATUALIZAR O CAMPO SENHA DO USUÁRIO
         * PENDÊNCIAS - GRAVAR CRIPTOGRAFADO
         *              COLOCAR O MÉTODO NUM SERVIÇO QUE POSSA SER USADO EM DIVERSOS LOCAIS
         * */
        private void PasswordChangedHandler(Object sender, RoutedEventArgs args)
        {
            IntPtr bstr1 = Marshal.SecureStringToBSTR(_view.Password);
            IntPtr bstr2 = Marshal.SecureStringToBSTR(_view.PasswordConfirm);

            try
            {
                this.Senha = Marshal.PtrToStringBSTR(bstr1);
                this.SenhaConfirmacao = Marshal.PtrToStringBSTR(bstr2);
            }
 
            finally
            {
                Marshal.ZeroFreeBSTR(bstr1);
                Marshal.ZeroFreeBSTR(bstr2);
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
            this.SubmitCommand.RaiseCanExecuteChanged();

            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


    }
}
