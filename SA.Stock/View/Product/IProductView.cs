using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Product
{
    public interface IProductView
    {
        void SetPresenter(IProductPresenter presenter);
    }
}
