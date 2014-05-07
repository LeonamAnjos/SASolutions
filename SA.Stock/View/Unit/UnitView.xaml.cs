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


namespace SA.Stock.View.Unit
{
    /// <summary>
    /// Interaction logic for UnidadeView.xaml
    /// </summary>
    public partial class UnitView : UserControl, IUnitView
    {
        public UnitView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IUnitPresenter presenter) 
        {
            this.DataContext = presenter;  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
