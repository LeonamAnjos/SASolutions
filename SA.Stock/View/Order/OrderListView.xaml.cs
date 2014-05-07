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
using Microsoft.Practices.Unity;
using SA.Infrastructure;

namespace SA.Stock.View.Order
{
    /// <summary>
    /// Interaction logic for OrderListView.xaml
    /// </summary>
    public partial class OrderListView : UserControl, IOrderListView
    {
        public OrderListView()
        {
            InitializeComponent();
        }

        #region IOrderListView
        public void SetPresenter(IOrderListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaPedidos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IOrderListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
