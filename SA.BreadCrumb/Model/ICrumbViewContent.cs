using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.BreadCrumb.Model
{
    public interface ICrumbViewContent
    {
        object View { get; }
        event EventHandler CloseViewRequested;
    }
}
