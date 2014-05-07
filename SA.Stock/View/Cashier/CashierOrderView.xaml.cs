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
    /// Interaction logic for CashierOrderView.xaml
    /// </summary>
    public partial class CashierOrderView : UserControl, ICashierOrderView
    {
        public CashierOrderView()
        {
            InitializeComponent();
        }

        private ICashierOrderPresenter _presenter;
        public void SetPresenter(ICashierOrderPresenter presenter)
        {
            this._presenter = presenter;
            this.DataContext = this._presenter.OrderViewModel;
        }
    }
}
