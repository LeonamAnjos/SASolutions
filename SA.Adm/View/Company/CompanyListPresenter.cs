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
using SA.Repository.Domain;
using SA.Repository.Repositories;
using SA.Repository.Enums;
using System.Windows.Input;

namespace SA.Adm.View.Company
{
    class CompanyListPresenter : ICompanyListPresenter
    {
        #region Properties
        private Empresa empresa;
        private ObservableCollection<Empresa> empresas;

        private readonly ICompanyListView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CompanyListPresenter(ICompanyListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Empresa>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Empresa>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Empresa>(OnUpdateExecute, CanUpdate);
            SearchCommand = new DelegateCommand<object>(OnSearchExecute);

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

        #region ICompanyListPresenter
        public Empresa Empresa
        {
            get
            {
                return empresa;
            }

            set
            {
                if ((empresa != null) && empresa.Equals(value))
                    return;

                empresa = value;
                OnPropertyChanged("Empresa");

                (this.DeleteCommand as DelegateCommand<Empresa>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Empresa>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Empresa>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Empresa> Empresas
        {
            get
            {
                return empresas;
            }

            set
            {
                if ((empresas != null) && empresas.Equals(value))
                {
                    return;
                }
                empresas = value;
                OnPropertyChanged("Empresas");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<ICompanyRepository>();
            this.Empresas = new ObservableCollection<Empresa>(repository.GetAll());

        }

        private void OnCreateExecute(Empresa target)
        {
            ShowView(CrudType.Create, new Empresa());
        }

        private void OnUpdateExecute(Empresa target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Empresa target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Empresa target)
        {
            var respository = _container.Resolve<ICompanyRepository>();
            respository.Remove(target);
            this.Empresas.Remove(target);
        }
        private bool CanDelete(Empresa target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Empresa target)
        {
            this._container.RegisterInstance<Empresa>(target);
            ICompanyPresenter presenter = this._container.Resolve<ICompanyPresenter>("ICompanyPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<ICompanyRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Empresa);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Empresa);
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
