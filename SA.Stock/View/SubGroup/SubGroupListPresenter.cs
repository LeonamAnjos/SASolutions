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

namespace SA.Stock.View.SubGroup
{
    class SubGroupListPresenter : ISubGroupListPresenter
    {
        #region Properties
        private Grupo _grupo;
        private SubGrupo _subGrupo;
        private ObservableCollection<SubGrupo> _subGrupos;

        private readonly ISubGroupListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public SubGroupListPresenter(ISubGroupListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<SubGrupo>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<SubGrupo>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<SubGrupo>(OnUpdateExecute, CanUpdate);
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

        #region ISubGroupListPresenter


        public SubGrupo SubGrupo
        {
            get
            {
                return _subGrupo;
            }

            set
            {
                if ((_subGrupo != null) && _subGrupo.Equals(value))
                    return;

                _subGrupo = value;
                OnPropertyChanged("SubGrupo");

                (this.DeleteCommand as DelegateCommand<SubGrupo>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<SubGrupo>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<SubGrupo>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<SubGrupo> SubGrupos
        {
            get
            {                
                return _subGrupos;
            }

            set
            {
                if ((_subGrupos != null) && _subGrupos.Equals(value))
                {
                    return;
                }
                _subGrupos = value;
                OnPropertyChanged("SubGrupos");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<ISubGroupRepository>();

            if (_container.IsRegistered<Grupo>("Grupo"))
                this.SubGrupos = new ObservableCollection<SubGrupo>(repository.GetByGroup(_container.Resolve<Grupo>("Grupo")));
            else
                this.SubGrupos = new ObservableCollection<SubGrupo>(repository.GetAll());
               
        }

        private void OnCreateExecute(SubGrupo target)
        {
            var group = _container.Resolve<Grupo>("Grupo");
            ShowView(CrudType.Create, new SubGrupo() { Grupo = group });
        }

        private void OnUpdateExecute(SubGrupo target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(SubGrupo target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(SubGrupo target)
        {
            _container.Resolve<ISubGroupRepository>().Remove(target);
            this.SubGrupos.Remove(target);
        }
        private bool CanDelete(SubGrupo target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, SubGrupo target)
        {
            this._container.RegisterInstance<SubGrupo>(target);
            ISubGroupPresenter presenter = this._container.Resolve<ISubGroupPresenter>("ISubGroupPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<ISubGroupRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.SubGrupo);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.SubGrupo);
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
