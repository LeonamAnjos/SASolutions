using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Windows;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace SA.Address.View.Country
{
    public interface ICountryView
    {
        void SetPresenter(ICountryPresenter presenter);
    }
}
