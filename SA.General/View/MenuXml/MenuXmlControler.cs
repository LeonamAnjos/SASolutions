using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SA.BreadCrumb.View;
using SA.Infrastructure;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Modularity;
using System.Windows.Data;

namespace SA.General.View.MenuXml
{
    public class MenuXmlControler : IMenuXmlControler
    {
        #region Properties
        private readonly IBreadCrumbPresenter _breadCrumb;
        private readonly IUnityContainer      _container;
        private readonly IModuleManager       _moduleManager;
        #endregion

        #region Constructor
        public MenuXmlControler(IBreadCrumbPresenter breadCrumb, IUnityContainer container, IModuleManager moduleManager)
        {
            this._breadCrumb    = breadCrumb;
            this._container     = container;
            this._moduleManager = moduleManager;

            ExecuteCommand = new DelegateCommand<XmlElement>(OnExecute, CanExecute);
        }
        #endregion

        #region IModelControler
        public DelegateCommand<XmlElement> ExecuteCommand { get; private set; }
        #endregion

        #region Methods
        private void OnExecute(XmlElement module) 
        {
            /*
            if (string.IsNullOrEmpty(module.InnerText))
                return;

            _moduleManager.LoadModule(module.InnerText);

            ISAModuleControler mc = _container.Resolve<ISAModuleControler>(module.InnerText);
            mc.Run();
             */ 
        }

        private bool CanExecute(XmlElement module)
        {
            return true;
        }
        #endregion
    }
}
