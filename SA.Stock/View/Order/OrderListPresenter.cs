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

namespace SA.Stock.View.Order
{
    class OrderListPresenter : IOrderListPresenter
    {
        #region Properties
        private Pedido _pedido;
        private ObservableCollection<Pedido> _pedidos;

        private readonly IOrderListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public OrderListPresenter(IOrderListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Pedido>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Pedido>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Pedido>(OnUpdateExecute, CanUpdate);
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

        #region IOrderListPresenter


        public Pedido Pedido
        {
            get
            {
                return _pedido;
            }

            set
            {
                if ((_pedido != null) && _pedido.Equals(value))
                    return;

                _pedido = value;
                OnPropertyChanged("Pedido");

                (this.DeleteCommand as DelegateCommand<Pedido>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Pedido>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Pedido>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Pedido> Pedidos
        {
            get
            {                
                return _pedidos;
            }

            set
            {
                if ((_pedidos != null) && _pedidos.Equals(value))
                {
                    return;
                }
                _pedidos = value;
                OnPropertyChanged("Pedidos");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IOrderRepository>();
            this.Pedidos = new ObservableCollection<Pedido>(repository.GetAll());
        }

        private void OnCreateExecute(Pedido target)
        {
            ShowView(CrudType.Create, new Pedido() { Fase = Repository.Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now });
        }

        private void OnUpdateExecute(Pedido target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Pedido target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Pedido target)
        {
            _container.Resolve<IOrderRepository>().Remove(target);
            this.Pedidos.Remove(target);
        }
        private bool CanDelete(Pedido target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Pedido target)
        {
            this._container.RegisterInstance<Pedido>(target);
            IOrderPresenter presenter = this._container.Resolve<IOrderPresenter>("IOrderPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IOrderRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Pedido);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Pedido);
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
