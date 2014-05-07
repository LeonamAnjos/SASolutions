using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Unit
{
    public interface IUnitView
    {
        void SetPresenter(IUnitPresenter presenter);
    }
}
