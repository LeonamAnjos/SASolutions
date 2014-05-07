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

namespace SA.Stock.View.Unit
{
    class UnitListPresenter : IUnitListPresenter
    {
        #region Properties
        private Unidade _unidade;
        private ObservableCollection<Unidade> _unidades;

        private readonly IUnitListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public UnitListPresenter(IUnitListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Unidade>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Unidade>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Unidade>(OnUpdateExecute, CanUpdate);
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

        #region IUnitListPresenter


        public Unidade Unidade
        {
            get
            {
                return _unidade;
            }

            set
            {
                if ((_unidade != null) && _unidade.Equals(value))
                    return;

                _unidade = value;
                OnPropertyChanged("Unidade");

                (this.DeleteCommand as DelegateCommand<Unidade>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Unidade>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Unidade>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Unidade> Unidades
        {
            get
            {                
                return _unidades;
            }

            set
            {
                if ((_unidades != null) && _unidades.Equals(value))
                {
                    return;
                }
                _unidades = value;
                OnPropertyChanged("Unidades");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IUnitRepository>();
            this.Unidades = new ObservableCollection<Unidade>(repository.GetAll());
        }

        private void OnCreateExecute(Unidade target)
        {
            ShowView(CrudType.Create, new Unidade());
        }

        private void OnUpdateExecute(Unidade target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Unidade target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Unidade target)
        {
            _container.Resolve<IUnitRepository>().Remove(target);
            this.Unidades.Remove(target);
        }
        private bool CanDelete(Unidade target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Unidade target)
        {
            this._container.RegisterInstance<Unidade>(target);
            IUnitPresenter presenter = this._container.Resolve<IUnitPresenter>("IUnitPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IUnitRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Unidade);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Unidade);
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
