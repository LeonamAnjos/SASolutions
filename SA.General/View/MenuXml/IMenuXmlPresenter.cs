using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SA.BreadCrumb.Model;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Data;

namespace SA.General.View.MenuXml
{
    public interface IMenuXmlPresenter : ICrumbViewContent
    {
        XmlDataProvider XmlDataProvider { get; set; }
        DelegateCommand<XmlElement> ClickCommand { get; }
    }
}
