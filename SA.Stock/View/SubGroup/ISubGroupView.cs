using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.SubGroup
{
    public interface ISubGroupView
    {
        void SetPresenter(ISubGroupPresenter presenter);
    }
}
