using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.Repository.Domain;
using System.Windows.Input;
using SA.Repository.Enums;
using SA.Stock.ViewModel;


namespace SA.Stock.View.Cashier
{
    public interface ICashierItemsPresenter : ICrumbViewContent, INotifyPropertyChanged
    {

        IOrderViewModel OrderViewModel { get; }

        #region Commands
        /// <summary>
        /// Search Product command - called when searching a Product is requeired
        /// </summary>
        ICommand SearchProductCommand { get; }
        #endregion
    }
}
