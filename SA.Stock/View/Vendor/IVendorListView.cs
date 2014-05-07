using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Vendor
{
    public interface IVendorListView
    {
        void SetPresenter(IVendorListPresenter presenter);
    }
}
