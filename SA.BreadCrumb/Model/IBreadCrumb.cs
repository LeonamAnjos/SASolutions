using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace SA.BreadCrumb.Model
{
    public interface IBreadCrumb
    {
        int AddCrumb(ICrumb crumb);
        int AddCrumb(string title, ICrumbViewContent content);

        int RemoveCrumb();
        int SetCrumbDescription(string description);
    }
}
