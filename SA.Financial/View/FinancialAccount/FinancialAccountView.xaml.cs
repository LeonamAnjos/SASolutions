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

namespace SA.Financial.View.FinancialAccount
{
    /// <summary>
    /// Interaction logic for FinancialAccountView.xaml
    /// </summary>
    public partial class FinancialAccountView : UserControl, IFinancialAccountView
    {
        public FinancialAccountView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IFinancialAccountPresenter presenter) 
        {
            this.DataContext = presenter;  
        }
    }
}
