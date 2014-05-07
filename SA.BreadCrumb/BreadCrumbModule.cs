using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using SA.Infrastructure;
using SA.BreadCrumb.View;

namespace SA.BreadCrumb
{
    public class BreadCrumbModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public BreadCrumbModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        /// <summary>
        /// Module - When a module is loaded, the Initialize method is called. Here you need to populate the global container (if necessary) and show views on shell regions.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();
            this._regionManager.RegisterViewWithRegion(RegionNames.MainToolBarRegion, () => _container.Resolve<IBreadCrumbPresenter>().View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IBreadCrumbView, BreadCrumbView>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IBreadCrumbPresenter, BreadCrumbPresenter>(new ExternallyControlledLifetimeManager());
        }

    }
}
