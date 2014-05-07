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

namespace SA.Adm.View.UserGroup
{
    /// <summary>
    /// Interaction logic for UserGroupListView.xaml
    /// </summary>
    public partial class UserGroupListView : UserControl, IUserGroupListView
    {
        public UserGroupListView()
        {
            InitializeComponent();
        }

        public void SetPresenter(IUserGroupListPresenter presenter)
        {
            DataContext = presenter;
        }

        private void ListaGrupoUsuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IUserGroupListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
