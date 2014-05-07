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


namespace SA.Stock.View.Producer
{
    /// <summary>
    /// Interaction logic for ProducerView.xaml
    /// </summary>
    public partial class ProducerView : UserControl, IProducerView
    {
        public ProducerView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IProducerPresenter presenter) 
        {
            this.DataContext = presenter;  
        }
    }
}
