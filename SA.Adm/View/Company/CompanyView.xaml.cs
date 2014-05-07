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
using System.Security;

namespace SA.Adm.View.Company
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class CompanyView : UserControl, ICompanyView
    {
        public CompanyView()
        {
            InitializeComponent();
        }

        public void SetPresenter(ICompanyPresenter presenter)
        {
            DataContext = presenter;
        }
    }
}
