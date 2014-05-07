using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Adm.View.Company
{
    public interface ICompanyListView
    {
        void SetPresenter(ICompanyListPresenter presenter);
    }
}
