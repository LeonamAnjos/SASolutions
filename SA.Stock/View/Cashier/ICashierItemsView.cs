using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Cashier
{
    public interface ICashierItemsView
    {
        void SetPresenter(ICashierItemsPresenter presenter);
    }
}
