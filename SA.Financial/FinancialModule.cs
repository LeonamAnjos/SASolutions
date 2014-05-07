using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using SA.BreadCrumb.View;
using SA.Financial.View.Register;
using SA.Financial.View.FinancialAccount;

namespace SA.Financial
{
    public class FinancialModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IBreadCrumbPresenter _breadCrumb;

        public FinancialModule(IUnityContainer container, IBreadCrumbPresenter breadCrumb)
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
            _container.RegisterInstance<IModule>("FinancialModule", this);

            #region Register
            _container.RegisterType<IRegisterListView, RegisterListView>();
            _container.RegisterType<IRegisterListPresenter, RegisterListPresenter>("IRegisterListPresenter");
            _container.RegisterType<IRegisterView, RegisterView>();
            _container.RegisterType<IRegisterPresenter, RegisterPresenter>("IRegisterPresenter");
            #endregion

            #region Financial Account
            _container.RegisterType<IFinancialAccountView, FinancialAccountView>();
            _container.RegisterType<IFinancialAccountPresenter, FinancialAccountPresenter>("IFinancialAccountPresenter");
            #endregion


        }

    }
}
