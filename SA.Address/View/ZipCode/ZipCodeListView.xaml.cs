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
using SA.Infrastructure;

namespace SA.Address.View.ZipCode
{
    /// <summary>
    /// Interaction logic for StateListView.xaml
    /// </summary>
    public partial class ZipCodeListView : UserControl, IZipCodeListView
    {
        public ZipCodeListView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IZipCodeListPresenter presenter)
        {
            DataContext = presenter;
        }

        private void ListaCeps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IZipCodeListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
