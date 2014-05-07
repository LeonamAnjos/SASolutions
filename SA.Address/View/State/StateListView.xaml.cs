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

namespace SA.Address.View.State
{
    /// <summary>
    /// Interaction logic for StateListView.xaml
    /// </summary>
    public partial class StateListView : UserControl, IStateListView
    {
        public StateListView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IStateListPresenter presenter)
        {
            DataContext = presenter;
        }

        private void ListaPaises_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IStateListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
