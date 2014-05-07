using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Composite.Modularity;
using System.Windows;
using SA.BreadCrumb;
using SA.General;
using SA.Module;
using SA.Stock;
using SA.Adm;
using SA.Address;
using SA.Repository;
using SA.Financial;


namespace SASolutions
{
    class SASolutionsBootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Step 1 - Create Shell: returning a shell, the UnityBootstrapper base class will attach the default region manager to it.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            Shell shell = new Shell();
            shell.Show();
            return shell;
        }

        /// <summary>
        /// Step 2 - Create an instance of a module catalog, populate it with modules, and return it.
        ///          There are 4 ways of populating module catalog:
        ///          1) From code (used here);
        ///          2) From a XAML file;
        ///          3) From a Configuration file;
        ///          4) From a Directory.
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog GetModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog()
                .AddModule(typeof(BreadCrumbModule), InitializationMode.WhenAvailable)
                .AddModule(typeof(RepositoryModule), InitializationMode.WhenAvailable)
                .AddModule(typeof(AddressModule), InitializationMode.WhenAvailable, "BreadCrumbModule", "RepositoryModule")
                .AddModule(typeof(GeneralModule), InitializationMode.WhenAvailable, "BreadCrumbModule")
                .AddModule(typeof(ModuleModule), InitializationMode.WhenAvailable, "BreadCrumbModule", "GeneralModule")
                .AddModule(typeof(AdmModule), InitializationMode.OnDemand, "BreadCrumbModule", "GeneralModule", "AddressModule")
                .AddModule(typeof(FinancialModule), InitializationMode.OnDemand, "BreadCrumbModule", "GeneralModule", "AddressModule")
                .AddModule(typeof(StockModule), InitializationMode.OnDemand, "BreadCrumbModule", "GeneralModule", "AddressModule", "FinancialModule");
            

            return catalog;
        }
    }
}
