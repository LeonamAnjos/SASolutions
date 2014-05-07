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
using SA.Repository.Enums;
using SA.Stock.View.Vendor;
using SA.Stock.ViewModel;
using SA.Stock.View.Product;



namespace SA.Stock.View.Cashier
{
    public class CashierOrderPresenter : ICashierOrderPresenter
    {
        #region Properties
        private IOrderViewModel _orderViewModel;
        private readonly ICashierOrderView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CashierOrderPresenter(ICashierOrderView view, IOrderViewModel orderViewModel, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (orderViewModel == null)
            {
                throw new ArgumentNullException("orderViewModel");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._orderViewModel = orderViewModel;
            this._container = container;
            this._view = view;
            this._view.SetPresenter(this);
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region ICashierOrderViewModel

        public IOrderViewModel OrderViewModel
        {
            get
            {
                return this._orderViewModel;
            }
        }

        #endregion

        #region Methods

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
