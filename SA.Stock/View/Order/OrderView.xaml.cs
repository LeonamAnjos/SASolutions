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


namespace SA.Stock.View.Order
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl, IOrderView
    {
        public OrderView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IOrderPresenter presenter) 
        {
            this.DataContext = presenter;  
        }
    }
}
