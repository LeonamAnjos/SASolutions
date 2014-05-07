using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Data;

namespace SA.General.View.MenuXml
{
    public interface IMenuXmlControler
    {
        DelegateCommand<XmlElement> ExecuteCommand { get; }
    }
}
