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

namespace SA.Stock.View.Producer
{
    /// <summary>
    /// Interaction logic for ProducerListView.xaml
    /// </summary>
    public partial class ProducerListView : UserControl, IProducerListView
    {
        public ProducerListView()
        {
            InitializeComponent();
        }

        #region IProducerListView
        public void SetPresenter(IProducerListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaFabricantes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IProducerListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
