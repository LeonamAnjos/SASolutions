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

namespace SA.Adm.View.User
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl, IUserView
    {
        public UserView()
        {
            InitializeComponent();
        }

        #region IUserView

        public SecureString Password 
        { 
            get 
            {
                return this.PassWord1.SecurePassword.Copy();
            } 
        }
        public SecureString PasswordConfirm 
        {
            get
            {
                return this.PassWord2.SecurePassword.Copy();
            }
        }


        public void SetPresenter(IUserPresenter presenter)
        {
            DataContext = presenter;
            if (!string.IsNullOrEmpty(presenter.Senha))
            {
                this.PassWord1.Password = presenter.Senha;
                this.PassWord2.Password = presenter.Senha;
            }
        }

        public event RoutedEventHandler PasswordChanged
        {
            add
            {
                lock (this.PassWord1)
                {
                    this.PassWord1.PasswordChanged += value;
                }

                lock (this.PassWord2)
                {
                    this.PassWord2.PasswordChanged += value;
                }

            }
            remove
            {
                lock (this.PassWord1)
                {
                    this.PassWord1.PasswordChanged -= value;
                }

                lock (this.PassWord2)
                {
                    this.PassWord2.PasswordChanged -= value;
                }
            }
        }
        #endregion
    }
}
