using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Order
{
    public interface IOrderListView
    {
        void SetPresenter(IOrderListPresenter presenter);
    }
}
