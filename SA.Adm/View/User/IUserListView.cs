using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Adm.View.User
{
    public interface IUserListView
    {
        void SetPresenter(IUserListPresenter presenter);
    }
}
