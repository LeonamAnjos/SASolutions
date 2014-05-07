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

namespace SA.Stock.View.Vendor
{
    class VendorListPresenter : IVendorListPresenter
    {
        #region Properties
        private Vendedor _vendedor;
        private ObservableCollection<Vendedor> _vendedores;

        private readonly IVendorListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public VendorListPresenter(IVendorListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Vendedor>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Vendedor>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Vendedor>(OnUpdateExecute, CanUpdate);
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

        #region IVendorListPresenter


        public Vendedor Vendedor
        {
            get
            {
                return _vendedor;
            }

            set
            {
                if ((_vendedor != null) && _vendedor.Equals(value))
                    return;

                _vendedor = value;
                OnPropertyChanged("Vendedor");

                (this.DeleteCommand as DelegateCommand<Vendedor>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Vendedor>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Vendedor>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Vendedor> Vendedores
        {
            get
            {                
                return _vendedores;
            }

            set
            {
                if ((_vendedores != null) && _vendedores.Equals(value))
                {
                    return;
                }
                _vendedores = value;
                OnPropertyChanged("Vendedores");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IVendorRepository>();
            this.Vendedores = new ObservableCollection<Vendedor>(repository.GetAll());
        }

        private void OnCreateExecute(Vendedor target)
        {
            ShowView(CrudType.Create, new Vendedor());
        }

        private void OnUpdateExecute(Vendedor target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Vendedor target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Vendedor target)
        {
            _container.Resolve<IVendorRepository>().Remove(target);
            this.Vendedores.Remove(target);
        }
        private bool CanDelete(Vendedor target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Vendedor target)
        {
            this._container.RegisterInstance<Vendedor>(target);
            IVendorPresenter presenter = this._container.Resolve<IVendorPresenter>("IVendorPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IVendorRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Vendedor);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Vendedor);
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
