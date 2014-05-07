using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Financial.View.Register
{
    public interface IRegisterListView
    {
        void SetPresenter(IRegisterListPresenter presenter);
    }
}
