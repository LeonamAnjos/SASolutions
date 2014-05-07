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
using SA.Repository.Repositories;
using SA.Repository.Domain;

namespace SA.Address.View.Country
{
    class CountryListPresenter : ICountryListPresenter
    {
        #region Properties
        private Pais pais;
        private ObservableCollection<Pais> paises;

        private readonly ICountryListView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CountryListPresenter(ICountryListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Pais>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Pais>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Pais>(OnUpdateExecute, CanUpdate);
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

        #region ICountryListPresenter
        public Pais Pais
        {
            get
            {
                return pais;
            }

            set
            {
                if ((pais != null) && pais.Equals(value))
                    return;

                pais = value;
                OnPropertyChanged("Pais");

                (this.DeleteCommand as DelegateCommand<Pais>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Pais>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Pais>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Pais> Paises
        {
            get
            {
                return paises;
            }

            set
            {
                if ((paises != null) && paises.Equals(value))
                {
                    return;
                }
                paises = value;
                OnPropertyChanged("Paises");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<ICountryRepository>();
            this.Paises = new ObservableCollection<Pais>(repository.GetAll());
        }

        private void OnCreateExecute(Pais target)
        {
            ShowView(CrudType.Create, new Pais());
        }

        private void OnUpdateExecute(Pais target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Pais target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Pais target)
        {
            var respository = _container.Resolve<ICountryRepository>();
            respository.Remove(target);
            this.Paises.Remove(target);
        }
        private bool CanDelete(Pais target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Pais target)
        {
            this._container.RegisterInstance<Pais>(target);
            ICountryPresenter presenter = this._container.Resolve<ICountryPresenter>("ICountryPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<ICountryRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Pais);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Pais);
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
