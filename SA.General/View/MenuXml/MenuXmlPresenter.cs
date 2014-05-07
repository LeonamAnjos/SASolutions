using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SA.BreadCrumb.View;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Data;

namespace SA.General.View.MenuXml
{
    public class MenuXmlPresenter : IMenuXmlPresenter
    {
        #region Properties
        private readonly IMenuXmlView _view;
        private readonly IMenuXmlControler _controler;
        private XmlDataProvider _xmlDataProvider;
        #endregion

        #region Constructors
        public MenuXmlPresenter(IMenuXmlView view, XmlDataProvider xmlDataProvider)
        {
            this._view = view;
            this._view.SetPresenter(this);
        }

        public MenuXmlPresenter(IMenuXmlView view, XmlDataProvider xmlDataProvider, IMenuXmlControler controler)
            : this(view, xmlDataProvider)
        {
            this._controler = controler;
            ClickCommand = this._controler.ExecuteCommand;

            _xmlDataProvider = xmlDataProvider;
        }
        #endregion

        #region IModulePresenter

        public XmlDataProvider XmlDataProvider 
        {
            get 
            {
                return _xmlDataProvider; 
            }
            set { _xmlDataProvider = value; } 
        }
        public DelegateCommand<XmlElement> ClickCommand { get; private set; }
        #endregion

        #region ICrumbViewContent
        public event EventHandler CloseViewRequested = delegate { };
        public object View
        {
            get { return _view; }
        }
        #endregion
        
    }
}
