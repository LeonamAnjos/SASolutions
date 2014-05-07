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
using SA.BreadCrumb.View;


using System.Windows.Input;
using SA.Repository.Domain;
using SA.Repository.Repositories;

namespace SA.Address.View.State
{
    class StateListPresenter : IStateListPresenter
    {
        #region Properties
        private Estado estado;
        private ObservableCollection<Estado> estados;

        private readonly IStateListView _view;
        //private readonly IAddressModelService _modelService;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public StateListPresenter(IStateListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
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

            CreateCommand = new DelegateCommand<Estado>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Estado>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Estado>(OnUpdateExecute, CanUpdate);
            SearchCommand = new DelegateCommand<object>(OnSearchExecute);

            CloseCommand = new DelegateCommand<CloseViewType>(OnCloseViewExecute);

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

        #region IStateListPresenter
        public Estado Estado
        {
            get
            {
                return estado;
            }

            set
            {
                if ((estado != null) && estado.Equals(value))
                    return;

                estado = value;
                OnPropertyChanged("Estado");

                (this.DeleteCommand as DelegateCommand<Estado>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Estado>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Estado>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Estado> Estados
        {
            get
            {
                return estados;
            }

            set
            {
                if ((estados != null) && estados.Equals(value))
                {
                    return;
                }
                estados = value;
                OnPropertyChanged("Estados");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IStateRepository>();
            this.Estados = new ObservableCollection<Estado>(repository.GetAll());
        }

        private void OnCreateExecute(Estado target)
        {
            ShowView(CrudType.Create, new Estado());
        }

        private void OnUpdateExecute(Estado target)
        {
            //ShowView(CrudType.Update, (Estado)target.Clone2());
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Estado target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Estado target)
        {
            var respository = _container.Resolve<IStateRepository>();
            respository.Remove(target);

            //using (TransactionScope ts = new TransactionScope())
            //{
            //    this._modelService.RemoveState(target);
            //    this._modelService.Save();
            //    ts.Complete();
            //}
            this.Estados.Remove(target);
        }
        private bool CanDelete(Estado target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Estado target)
        {
            this._container.RegisterInstance<Estado>(target);
            IStatePresenter presenter = this._container.Resolve<IStatePresenter>("IStatePresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs) 
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IStateRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Estado);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Estado);
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
