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

namespace SA.Address.View.Country
{
    /// <summary>
    /// Interaction logic for CountryListView.xaml
    /// </summary>
    public partial class CountryListView : UserControl, ICountryListView
    {
        public CountryListView()
        {
            InitializeComponent();
        }

        public void SetPresenter(ICountryListPresenter presenter)
        {
            DataContext = presenter;
        }

        private void ListaPaises_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as ICountryListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
