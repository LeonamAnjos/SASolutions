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

namespace SA.Financial.View.Register
{
    /// <summary>
    /// Interaction logic for RegisterListView.xaml
    /// </summary>
    public partial class RegisterListView : UserControl, IRegisterListView
    {
        public RegisterListView()
        {
            InitializeComponent();
        }

        #region IRegisterListView
        public void SetPresenter(IRegisterListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaCadastros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IRegisterListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
