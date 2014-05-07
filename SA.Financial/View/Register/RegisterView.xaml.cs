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

namespace SA.Financial.View.Register
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl, IRegisterView
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IRegisterPresenter presenter) 
        {
            this.DataContext = presenter;  
        }
    }
}
