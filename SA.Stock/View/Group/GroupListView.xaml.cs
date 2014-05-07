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

namespace SA.Stock.View.Group
{
    /// <summary>
    /// Interaction logic for GroupListView.xaml
    /// </summary>
    public partial class GroupListView : UserControl, IGroupListView
    {
        public GroupListView()
        {
            InitializeComponent();
        }

        #region IGroupListView
        public void SetPresenter(IGroupListPresenter presenter) 
        {
            this.DataContext = presenter;
        }
        #endregion

        private void ListaGrupos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IGroupListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
