using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Cashier
{
    public interface ICashierListItemsView
    {
        void SetPresenter(ICashierListItemsPresenter presenter);
    }
}
