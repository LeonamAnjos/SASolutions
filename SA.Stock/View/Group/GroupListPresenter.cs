using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Events;
using SA.Infrastructure.Event;
using Microsoft.Practices.Composite.Presentation.Events;
using SA.Repository.Domain;
using SA.Repository.Repositories;
using SA.Infrastructure;
using SA.BreadCrumb.View;

namespace SA.Stock.View.Group
{
    class GroupListPresenter : IGroupListPresenter
    {
        #region Properties
        private Grupo _grupo;
        private ObservableCollection<Grupo> _grupos;

        private readonly IGroupListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public GroupListPresenter(IGroupListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Grupo>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Grupo>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Grupo>(OnUpdateExecute, CanUpdate);
            SearchCommand = new DelegateCommand<object>(OnSearchExecute);

            CloseCommand = new DelegateCommand<CloseViewType>(OnCloseViewExecute);

            this._view = view; 
            this._view.SetPresenter(this);
            this._container = container;

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

        #region IGroupListPresenter


        public Grupo Grupo
        {
            get
            {
                return _grupo;
            }

            set
            {
                if ((_grupo != null) && _grupo.Equals(value))
                    return;

                _grupo = value;
                OnPropertyChanged("Grupo");

                (this.DeleteCommand as DelegateCommand<Grupo>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Grupo>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Grupo>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Grupo> Grupos
        {
            get
            {                
                return _grupos;
            }

            set
            {
                if ((_grupos != null) && _grupos.Equals(value))
                {
                    return;
                }
                _grupos = value;
                OnPropertyChanged("Grupos");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IGroupRepository>();
            this.Grupos = new ObservableCollection<Grupo>(repository.GetAll());
        }

        private void OnCreateExecute(Grupo target)
        {
            ShowView(CrudType.Create, new Grupo());
        }

        private void OnUpdateExecute(Grupo target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Grupo target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Grupo target)
        {
            _container.Resolve<IGroupRepository>().Remove(target);
            this.Grupos.Remove(target);
        }
        private bool CanDelete(Grupo target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Grupo target)
        {
            this._container.RegisterInstance<Grupo>(target);
            IGroupPresenter presenter = this._container.Resolve<IGroupPresenter>("IGroupPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IGroupRepository>();

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
