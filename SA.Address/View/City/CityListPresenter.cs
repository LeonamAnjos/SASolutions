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

namespace SA.Address.View.City
{
    class CityListPresenter : ICityListPresenter
    {
        #region Properties
        private Cidade cidade;
        private ObservableCollection<Cidade> cidades;

        private readonly ICityListView _view;
        //private readonly IAddressModelService _modelService = null;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CityListPresenter(ICityListView view, IUnityContainer container)
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

            CreateCommand = new DelegateCommand<Cidade>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Cidade>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Cidade>(OnUpdateExecute, CanUpdate);
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

        #region ICityListPresenter
        public Cidade Cidade
        {
            get
            {
                return cidade;
            }

            set
            {
                if ((cidade != null) && cidade.Equals(value))
                    return;

                cidade = value;
                OnPropertyChanged("Cidade");

                (this.DeleteCommand as DelegateCommand<Cidade>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Cidade>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Cidade>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Cidade> Cidades
        {
            get
            {
                return cidades;
            }

            set
            {
                if ((cidades != null) && cidades.Equals(value))
                {
                    return;
                }
                cidades = value;
                OnPropertyChanged("Cidades");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<ICityRepository>();
            this.Cidades = new ObservableCollection<Cidade>(repository.GetAll());
        }

        private void OnCreateExecute(Cidade target)
        {
            ShowView(CrudType.Create, new Cidade());
        }

        private void OnUpdateExecute(Cidade target)
        {
            //ShowView(CrudType.Update, (Cidade)target.Clone2());
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Cidade target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Cidade target)
        {
            var respository = _container.Resolve<ICityRepository>();
            respository.Remove(target);
            this.Cidades.Remove(target);
        }
        private bool CanDelete(Cidade target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Cidade target)
        {
            this._container.RegisterInstance<Cidade>(target);
            ICityPresenter presenter = this._container.Resolve<ICityPresenter>("ICityPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs) 
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<ICityRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Cidade);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Cidade);
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
