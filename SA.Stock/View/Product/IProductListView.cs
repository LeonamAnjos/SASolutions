using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Product
{
    public interface IProductListView
    {
        void SetPresenter(IProductListPresenter presenter);
    }
}
