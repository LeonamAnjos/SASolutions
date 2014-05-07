using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Address.View.Country
{
    public interface ICountryListView
    {
        void SetPresenter(ICountryListPresenter presenter);
    }
}
