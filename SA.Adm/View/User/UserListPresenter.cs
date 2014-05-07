using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;

using Microsoft.Practices.Composite.Events;
using SA.Infrastructure.Event;
using Microsoft.Practices.Composite.Presentation.Events;

using Microsoft.Practices.Unity;
using SA.Infrastructure;
using System.Transactions;
using SA.Adm.View.UserGroup;
using SA.BreadCrumb.View;
using SA.Repository.Domain;
using SA.Repository.Repositories;
using SA.Repository.Enums;

namespace SA.Adm.View.User
{
    class UserListPresenter : IUserListPresenter
    {
        #region Properties
        private Usuario usuario;
        private ObservableCollection<Usuario> usuarios;

        private readonly IUserListView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public UserListPresenter(IUserListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;

            CreateCommand = new DelegateCommand<Usuario>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Usuario>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Usuario>(OnUpdateExecute, CanUpdate);
            SearchCommand = new DelegateCommand<object>(OnSearchExecute);

            this._view = view; 
            this._view.SetPresenter(this);

            SearchCommand.Execute(this);
        }
        #endregion

        #region Commands
        public DelegateCommand<Usuario> CreateCommand { get; private set; }
        public DelegateCommand<Usuario> DeleteCommand { get; private set; }
        public DelegateCommand<Usuario> UpdateCommand { get; private set; }
        public DelegateCommand<object> SearchCommand { get; private set; }
        #endregion

        #region ICrumbViewContent
        public event EventHandler CloseViewRequested = delegate { };
        public object View 
        {
            get { return _view; }
        }
        #endregion

        #region IUserListPresenter
        public Usuario Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                if ((usuario != null) && usuario.Equals(value))
                    return;

                usuario = value;
                OnPropertyChanged("Usuario");

                this.DeleteCommand.RaiseCanExecuteChanged();
                this.UpdateCommand.RaiseCanExecuteChanged();
                this.CreateCommand.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Usuario> Usuarios
        {
            get
            {
                return usuarios;
            }

            set
            {
                if ((usuarios != null) && usuarios.Equals(value))
                {
                    return;
                }
                usuarios = value;
                OnPropertyChanged("Usuarios");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IUserRepository>();
            this.Usuarios = new ObservableCollection<Usuario>(repository.GetAll());

        }

        private void OnCreateExecute(Usuario usuario)
        {
            ShowView(CrudType.Create, new Usuario() { Situacao = ActiveInactiveType.Active });
        }

        private void OnUpdateExecute(Usuario usuario)
        {
            ShowView(CrudType.Update, usuario);
        }
        private bool CanUpdate(Usuario usuario)
        {
            return ((usuario != null) && (usuario.Id > 0));
        }

        private void OnDeleteExecute(Usuario target)
        {
            var respository = _container.Resolve<IUserRepository>();
            respository.Remove(target);
            this.Usuarios.Remove(target);
        }
        private bool CanDelete(Usuario usuario)
        {
            return (usuario != null) && (usuario.Id > 0);
        }

        private void ShowView(CrudType action, Usuario usuario)
        {
            this._container.RegisterInstance<Usuario>(usuario);
            IUserPresenter presenter = this._container.Resolve<IUserPresenter>("IUserPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IUserRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Usuario);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Usuario);
                                break;
                        }
                        SearchCommand.Execute(this);
                    }
                    else
                    {
                        if (action == CrudType.Update)
                            SearchCommand.Execute(this);
                    }
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb(action.GetDescription(), presenter);
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
