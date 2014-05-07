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

namespace SA.Address.View.State
{
    /// <summary>
    /// Interaction logic for CountryView.xaml
    /// </summary>
    public partial class StateView : UserControl, IStateView
    {
        public StateView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IStatePresenter presenter)
        {
            DataContext = presenter;
        }

    }
}
