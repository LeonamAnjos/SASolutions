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
using System.Security;

namespace SA.Adm.View.UserGroup
{
    /// <summary>
    /// Interaction logic for UserGroupView.xaml
    /// </summary>
    public partial class UserGroupView : UserControl, IUserGroupView
    {
        public UserGroupView()
        {
            InitializeComponent();
        }

        #region IUserGroupView

        public void SetPresenter(IUserGroupPresenter presenter)
        {
            DataContext = presenter;
        }
        #endregion
    }
}
