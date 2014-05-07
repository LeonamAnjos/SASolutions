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

namespace SA.Address.View.ZipCode
{
    class ZipCodeListPresenter : IZipCodeListPresenter
    {
        #region Properties
        private Cep cep;
        private ObservableCollection<Cep> ceps;

        private readonly IZipCodeListView _view;
        //private readonly IAddressModelService _modelService;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public ZipCodeListPresenter(IZipCodeListView view, IUnityContainer container)
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

            CreateCommand = new DelegateCommand<Cep>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Cep>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Cep>(OnUpdateExecute, CanUpdate);
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

        #region IZipCodeListPresenter
        public Cep Cep
        {
            get
            {
                return cep;
            }

            set
            {
                if ((cep != null) && cep.Equals(value))
                    return;

                cep = value;
                OnPropertyChanged("Cep");

                (this.DeleteCommand as DelegateCommand<Cep>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Cep>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Cep>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Cep> Ceps
        {
            get
            {
                return ceps;
            }

            set
            {
                if ((ceps != null) && ceps.Equals(value))
                {
                    return;
                }
                ceps = value;
                OnPropertyChanged("Ceps");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            IZipCodeRepository repository = _container.Resolve<IZipCodeRepository>();

            this.Ceps = new ObservableCollection<Cep>(repository.GetAll());
        }

        private void OnCreateExecute(Cep target)
        {
            ShowView(CrudType.Create, new Cep());
        }

        private void OnUpdateExecute(Cep target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Cep target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Cep target)
        {
            var respository = _container.Resolve<IZipCodeRepository>();
            respository.Remove(target);

            //using (TransactionScope ts = new TransactionScope())
            //{
            //    this._modelService.RemoveZipCode(target);
            //    this._modelService.Save();
            //    ts.Complete();
            //}
            this.Ceps.Remove(target);
        }
        private bool CanDelete(Cep target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Cep target)
        {
            this._container.RegisterInstance<Cep>(target);
            IZipCodePresenter presenter = this._container.Resolve<IZipCodePresenter>("IZipCodePresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs) 
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                        {
                            var repository = _container.Resolve<IZipCodeRepository>();

                            switch (action)
                            {
                                case CrudType.Create:
                                    repository.Add(presenter.Cep);
                                    break;
                                case CrudType.Update:
                                    repository.Update(presenter.Cep);
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
