using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.SubGroup
{
    public interface ISubGroupListView
    {
        void SetPresenter(ISubGroupListPresenter presenter);
    }
}
