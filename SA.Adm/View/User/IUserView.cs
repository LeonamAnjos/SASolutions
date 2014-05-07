using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Windows;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace SA.Adm.View.User
{
    public interface IUserView
    {
        SecureString Password { get; }
        SecureString PasswordConfirm { get; }

        event RoutedEventHandler PasswordChanged;

        void SetPresenter(IUserPresenter presenter);
    }
}
