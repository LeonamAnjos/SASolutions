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


namespace SA.Stock.View.Vendor
{
    /// <summary>
    /// Interaction logic for VendorView.xaml
    /// </summary>
    public partial class VendorView : UserControl, IVendorView
    {
        public VendorView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IVendorPresenter presenter) 
        {
            this.DataContext = presenter;  
        }
    }
}
