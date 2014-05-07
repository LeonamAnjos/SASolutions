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

namespace SA.Stock.View.Unit
{
    /// <summary>
    /// Interaction logic for UnidadeSearchView.xaml
    /// </summary>
    public partial class UnitListView : UserControl, IUnitListView
    {
        public UnitListView()
        {
            InitializeComponent();
        }

        #region IUnidadeListView
        public void SetPresenter(IUnitListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaUnidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IUnitListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
