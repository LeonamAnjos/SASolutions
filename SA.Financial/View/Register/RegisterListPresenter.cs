using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Presentation.Commands;

using SA.Infrastructure.Event;
using Microsoft.Practices.Composite.Presentation.Events;
using SA.Repository.Domain;
using SA.Repository.Repositories;
using SA.Infrastructure;
using SA.BreadCrumb.View;
using SA.Repository.Enums;

namespace SA.Financial.View.Register
{
    class RegisterListPresenter : IRegisterListPresenter
    {
        #region Properties
        private Cadastro _cadastro;
        private ObservableCollection<Cadastro> _cadastros;

        private readonly IRegisterListView _view;
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors
        public RegisterListPresenter(IRegisterListView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CreateCommand = new DelegateCommand<Cadastro>(OnCreateExecute);
            DeleteCommand = new DelegateCommand<Cadastro>(OnDeleteExecute, CanDelete);
            UpdateCommand = new DelegateCommand<Cadastro>(OnUpdateExecute, CanUpdate);
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

        #region IRegisterListPresenter


        public Cadastro Cadastro
        {
            get
            {
                return _cadastro;
            }

            set
            {
                if ((_cadastro != null) && _cadastro.Equals(value))
                    return;

                _cadastro = value;
                OnPropertyChanged("Cadastro");

                (this.DeleteCommand as DelegateCommand<Cadastro>).RaiseCanExecuteChanged();
                (this.UpdateCommand as DelegateCommand<Cadastro>).RaiseCanExecuteChanged();
                (this.CreateCommand as DelegateCommand<Cadastro>).RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<Cadastro> Cadastros
        {
            get
            {                
                return _cadastros;
            }

            set
            {
                if ((_cadastros != null) && _cadastros.Equals(value))
                {
                    return;
                }
                _cadastros = value;
                OnPropertyChanged("Cadastros");
            }
        }
        #endregion

        #region Methods
        private void OnSearchExecute(object sender)
        {
            var repository = _container.Resolve<IRegisterRepository>();
            this.Cadastros = new ObservableCollection<Cadastro>(repository.GetAll());
        }

        private void OnCreateExecute(Cadastro target)
        {
            ShowView(CrudType.Create, new Cadastro() { Tipo = PersonType.Fisica, Situacao = ActiveInactiveType.Active });
        }

        private void OnUpdateExecute(Cadastro target)
        {
            ShowView(CrudType.Update, target);
        }
        private bool CanUpdate(Cadastro target)
        {
            return ((target != null) && (target.Id > 0));
        }

        private void OnDeleteExecute(Cadastro target)
        {
            _container.Resolve<IRegisterRepository>().Remove(target);
            this.Cadastros.Remove(target);
        }
        private bool CanDelete(Cadastro target)
        {
            return (target != null) && (target.Id > 0);
        }

        private void ShowView(CrudType action, Cadastro target)
        {
            this._container.RegisterInstance<Cadastro>(target);
            IRegisterPresenter presenter = this._container.Resolve<IRegisterPresenter>("IRegisterPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs is CloseViewEventArgs)
                {
                    if ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Submit)
                    {
                        var repository = _container.Resolve<IRegisterRepository>();

                        switch (action)
                        {
                            case CrudType.Create:
                                repository.Add(presenter.Cadastro);
                                break;
                            case CrudType.Update:
                                repository.Update(presenter.Cadastro);
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
