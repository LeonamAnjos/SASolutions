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

namespace SA.Stock.View.Product
{
    class ProductListPresenter : IProductListPresenter
    {
        #region Properties
        private Produto _produto;
        private ObservableCollection<Produto> _produtos;

        private readonly IProductListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public ProductListPresenter(IProductListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Produto>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Produto>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Produto>(OnUpdateExecute, CanUpdate);
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

        #region IProductListPresenter


        public Produto Produto
        {
            get
            {
                return _produto;
            }

            set
            {
                if ((_produto != null) && _produto.Equals(value))
                    return;

                _produto = value;
                OnPropertyChanged("Produto");

                (this.DeleteCommand as DelegateCommand<Produto>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Produto>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Produto>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Produto> Produtos
        {
            get
            {                
                return _produtos;
            }

            set
            {
                if ((_produtos != null) && _produtos.Equals(value))
                {
                    return;
                }
                _produtos = value;
                OnPropertyChanged("Produtos");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IProductRepository>();
            this.Produtos = new ObservableCollection<Produto>(repository.GetAll());
        }

        private void OnCreateExecute(Produto target)
        {
            ShowView(CrudType.Create, new Produto());
        }

        private void OnUpdateExecute(Produto target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Produto target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Produto target)
        {
            _container.Resolve<IProductRepository>().Remove(target);
            this.Produtos.Remove(target);
        }
        private bool CanDelete(Produto target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Produto target)
        {
            this._container.RegisterInstance<Produto>(target);
            IProductPresenter presenter = this._container.Resolve<IProductPresenter>("IProductPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IProductRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Produto);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Produto);
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
