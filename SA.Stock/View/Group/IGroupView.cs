using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Group
{
    public interface IGroupView
    {
        void SetPresenter(IGroupPresenter presenter);
    }
}
