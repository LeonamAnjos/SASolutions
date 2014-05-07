using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Windows;

namespace SA.Adm.View.UserGroup
{
    public interface IUserGroupView
    {
        void SetPresenter(IUserGroupPresenter presenter);
    }
}
