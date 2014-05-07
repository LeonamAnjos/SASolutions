using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.View;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using SA.General.View.MenuXml;
using SA.Infrastructure;

namespace SA.General
{
    public class GeneralModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IBreadCrumbPresenter _breadCrumb;

        public GeneralModule(IUnityContainer container, IBreadCrumbPresenter breadCrumb)
        {
            this._container = container;
            this._breadCrumb = breadCrumb;
        }

        /// <summary>
        /// Module - When a module is loaded, the Initialize method is called. Here you need to populate the global container (if necessary) and show views on shell regions.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();
        }


        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IMenuXmlPresenter, MenuXmlPresenter>("IMenuXmlPresenter");
            _container.RegisterType<IMenuXmlView, MenuXmlView>();
        }

    }
}
