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


namespace SA.Stock.View.SubGroup
{
    /// <summary>
    /// Interaction logic for SubGroupView.xaml
    /// </summary>
    public partial class SubGroupView : UserControl, ISubGroupView
    {
        public SubGroupView()
        {
            InitializeComponent();
        }

        public void SetPresenter(ISubGroupPresenter presenter) 
        {
            this.DataContext = presenter;  
        }
    }
}
