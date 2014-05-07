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

using System.Windows.Input;
using SA.Infrastructure;
using SA.Repository.Domain;
using SA.Repository.Repositories;
using Microsoft.Practices.Unity;
using SA.BreadCrumb.View;

namespace SA.Adm.View.UserGroup
{
    class UserGroupListPresenter : IUserGroupListPresenter
    {
        #region Properties
        private UsuarioGrupo grupo;
        private ObservableCollection<UsuarioGrupo> grupos;

        private readonly IUserGroupListView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public UserGroupListPresenter(IUserGroupListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<UsuarioGrupo>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<UsuarioGrupo>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<UsuarioGrupo>(OnUpdateExecute, CanUpdate);
            SearchCommand = new DelegateCommand<object>(OnSearchExecute);

            CloseCommand = new DelegateCommand<CloseViewType>(OnCloseViewExecute);

            this._container = container;
            this._view = view; 
            this._view.SetPresenter(this);

            SearchCommand.Execute(this);
        }
        #endregion

        #region Commands
        public ICommand CreateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        #endregion

        #region ICrumbViewContent
        public event EventHandler CloseViewRequested = delegate { };
        public object View 
        {
            get { return _view; }
        }
        #endregion

        #region IUserGroupListPresenter
        public UsuarioGrupo Grupo
        {
            get
            {
                return grupo;
            }

            set
            {
                if ((grupo != null) && grupo.Equals(value))
                    return;

                grupo = value;
                OnPropertyChanged("Grupo");

                (this.DeleteCommand as DelegateCommand<UsuarioGrupo>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<UsuarioGrupo>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<UsuarioGrupo>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<UsuarioGrupo> Grupos
        {
            get
            {
                return grupos;
            }

            set
            {
                if ((grupos != null) && grupos.Equals(value))
                {
                    return;
                }
                grupos = value;
                OnPropertyChanged("Grupos");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IUserGroupRepository>();
            this.Grupos = new ObservableCollection<UsuarioGrupo>(repository.GetAll());
        }

        private void OnCreateExecute(UsuarioGrupo target)
        {
            ShowView(CrudType.Create, new UsuarioGrupo());
        }

        private void OnUpdateExecute(UsuarioGrupo target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(UsuarioGrupo target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(UsuarioGrupo target)
        {
            var respository = _container.Resolve<IUserGroupRepository>();
            respository.Remove(target);
            this.Grupos.Remove(target);
        }
        private bool CanDelete(UsuarioGrupo target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, UsuarioGrupo target)
        {
            this._container.RegisterInstance<UsuarioGrupo>(target);
            IUserGroupPresenter presenter = this._container.Resolve<IUserGroupPresenter>("IUserGroupPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IUserGroupRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Grupo);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Grupo);
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

        private void OnCloseViewExecute(CloseViewType closeViewType)
        {
            CloseViewRequested(this, new CloseViewEventArgs(closeViewType));
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
