using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Address.View.ZipCode
{
    public interface IZipCodeListView
    {
        void SetPresenter(IZipCodeListPresenter presenter);
    }
}
