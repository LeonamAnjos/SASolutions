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

namespace SA.Stock.View.Producer
{
    class ProducerListPresenter : IProducerListPresenter
    {
        #region Properties
        private Fabricante _fabricante;
        private ObservableCollection<Fabricante> _fabricantes;

        private readonly IProducerListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public ProducerListPresenter(IProducerListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Fabricante>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Fabricante>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Fabricante>(OnUpdateExecute, CanUpdate);
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

        #region IProducerListPresenter


        public Fabricante Fabricante
        {
            get
            {
                return _fabricante;
            }

            set
            {
                if ((_fabricante != null) && _fabricante.Equals(value))
                    return;

                _fabricante = value;
                OnPropertyChanged("Fabricante");

                (this.DeleteCommand as DelegateCommand<Fabricante>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Fabricante>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Fabricante>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Fabricante> Fabricantes
        {
            get
            {                
                return _fabricantes;
            }

            set
            {
                if ((_fabricantes != null) && _fabricantes.Equals(value))
                {
                    return;
                }
                _fabricantes = value;
                OnPropertyChanged("Fabricantes");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IProducerRepository>();
            this.Fabricantes = new ObservableCollection<Fabricante>(repository.GetAll());
        }

        private void OnCreateExecute(Fabricante target)
        {
            ShowView(CrudType.Create, new Fabricante());
        }

        private void OnUpdateExecute(Fabricante target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Fabricante target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Fabricante target)
        {
            _container.Resolve<IProducerRepository>().Remove(target);
            this.Fabricantes.Remove(target);
        }
        private bool CanDelete(Fabricante target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Fabricante target)
        {
            this._container.RegisterInstance<Fabricante>(target);
            IProducerPresenter presenter = this._container.Resolve<IProducerPresenter>("IProducerPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IProducerRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Fabricante);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Fabricante);
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
