using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SA.Stock.View.Cashier
{
    /// <summary>
    /// Interaction logic for CashierListItemsView.xaml
    /// </summary>
    public partial class CashierListItemsView : UserControl, ICashierListItemsView
    {
        public CashierListItemsView()
        {
            InitializeComponent();
        }

        private ICashierListItemsPresenter _presenter;
        public void SetPresenter(ICashierListItemsPresenter presenter)
        {
            this._presenter = presenter;
            this.DataContext = this._presenter.OrderViewModel;
        }
    }
}
