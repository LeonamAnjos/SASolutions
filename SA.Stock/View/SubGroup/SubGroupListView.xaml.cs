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

namespace SA.Stock.View.SubGroup
{
    /// <summary>
    /// Interaction logic for SubGroupListView.xaml
    /// </summary>
    public partial class SubGroupListView : UserControl, ISubGroupListView
    {
        public SubGroupListView()
        {
            InitializeComponent();
        }

        #region ISubGroupListView
        public void SetPresenter(ISubGroupListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaSubGrupos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as ISubGroupListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
