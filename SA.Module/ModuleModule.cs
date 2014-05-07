using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.View;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using SA.Module.View.Module;
using SA.Infrastructure;
using SA.General.View.MenuXml;
using System.Windows.Data;

namespace SA.Module
{
    public class ModuleModule: IModule, ISAModuleControler
    {
        private readonly IUnityContainer _container;
        private readonly IBreadCrumbPresenter _breadCrumb;

        public ModuleModule(IUnityContainer container, IBreadCrumbPresenter breadCrumb)
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
            this.Run();
        }

        public void Run()
        {
            XmlDataProvider xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.Source = new Uri("/SA.Module;component/Resource/Modules.xml", UriKind.Relative);
            xmlDataProvider.XPath = "/Modules/Module";


            _breadCrumb.AddCrumb("Módulos", _container.Resolve<IMenuXmlPresenter>("IMenuXmlPresenter", new ParameterOverrides { { "xmlDataProvider", xmlDataProvider }, { "controler", _container.Resolve<IMenuXmlControler>("ModuleControler") } }));
            //_breadCrumb.AddCrumb("Módulos", _container.Resolve<IModulePresenter>(new ParameterOverrides { { "URL", "TESTE" }, { "CONTROLER", "NEW_CONTROLER" } }));
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterInstance<ISAModuleControler>("ModuleModule", this);

            _container.RegisterType<IMenuXmlPresenter, MenuXmlPresenter>("IMenuXmlPresenter");
            _container.RegisterType<IMenuXmlView, MenuXmlView>();

            _container.RegisterType<IMenuXmlControler, ModuleControler>("ModuleControler");

            

            
        }

    }
}
