using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace SA.BreadCrumb.View
{
    public interface IBreadCrumbPresenter : IBreadCrumb, ICrumbViewContent
    {
        ObservableCollection<ICrumb> Crumbs { get; }
        DelegateCommand<ICrumb> CrumbAccessedCommand { get; }
    }
}
