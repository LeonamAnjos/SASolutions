using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.BreadCrumb.Model
{
    public interface ICrumb
    {
        string Title   { get; }
        string Description { get; set; }
        ICrumbViewContent Content { get; }
    }
}
