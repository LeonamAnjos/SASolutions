using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Windows;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace SA.Adm.View.Company
{
    public interface ICompanyView
    {
        void SetPresenter(ICompanyPresenter presenter);
    }
}
