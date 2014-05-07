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

namespace SA.Adm.View.User
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl, IUserListView
    {
        public UserListView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IUserListPresenter presenter)
        {
            DataContext = presenter;
        }
    }
}
