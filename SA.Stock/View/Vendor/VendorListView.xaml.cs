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

namespace SA.Stock.View.Vendor
{
    /// <summary>
    /// Interaction logic for VendorListView.xaml
    /// </summary>
    public partial class VendorListView : UserControl, IVendorListView
    {
        public VendorListView()
        {
            InitializeComponent();
        }

        #region IVendorListView
        public void SetPresenter(IVendorListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaVendedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IVendorListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
