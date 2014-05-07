using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using SA.Repository.Domain;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using SA.BreadCrumb.View;
using SA.Repository.Repositories;
using SA.Financial.View.Register;
using SA.Repository.Enums;
using SA.Stock.View.Vendor;
using SA.Stock.ViewModel;
using Microsoft.Practices.Composite.Regions;
using SA.BreadCrumb.Model;
using SA.Repository.Factories;



namespace SA.Stock.View.Cashier
{
    public class CashierPresenter : ICashierPresenter
    {
        #region Properties
        private IOrderViewModel _orderViewModel;
        private ValidationResults _validationResults;
        private readonly ICashierView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CashierPresenter(ICashierView view, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);

            this._view = view;
            this._view.SetPresenter(this);
            this._container = container;

            IOrderFactory orderFactory = this._container.Resolve<IOrderFactory>();
            this._orderViewModel = new OrderViewModel(container, orderFactory.CreateOrder(OrderType.SalesOrder));
            this._container.RegisterInstance<IOrderViewModel>(this._orderViewModel, new ExternallyControlledLifetimeManager());
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region ICashierViewModel

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        public ICommand SubmitCommand { get; set; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        public ICommand CancelCommand { get; set; }
        #endregion

        #endregion
        
        #region Methods

        private bool CanSubmit(object parameter)
        {
            return (_validationResults == null ? true : _validationResults.IsValid);
        }
        private void Submit(object parameter)
        {
            CloseViewRequested(this, new CloseViewEventArgs(CloseViewType.Submit));
        }
        private void Cancel(object parameter)
        {
            CloseViewRequested(this, new CloseViewEventArgs(CloseViewType.Cancel));
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
