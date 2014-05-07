using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Adm.View.UserGroup
{
    public interface IUserGroupListView
    {
        void SetPresenter(IUserGroupListPresenter presenter);
    }
}
