using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using SA.Infrastructure;
using Microsoft.Practices.Unity;
using SA.BreadCrumb.View;
using SA.Adm.View.User;

using SA.Adm.View.UserGroup;
using SA.Adm.View.Company;

namespace SA.Adm
{
    public class AdmModule : IModule, ISAModuleControler
    {
        private readonly IUnityContainer _container;
        private readonly IBreadCrumbPresenter _breadCrumb;

        public AdmModule(IUnityContainer container, IBreadCrumbPresenter breadCrumb)
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

        public void Run()
        {
            this._breadCrumb.AddCrumb("Empresas", _container.Resolve<ICompanyListPresenter>());

            //this._breadCrumb.AddCrumb("Grupo de usuários", _container.Resolve<IUserGroupListPresenter>());
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterInstance<ISAModuleControler>("AdmModule", this);

            #region User
            _container.RegisterType<IUserListView, UserListView>();
            _container.RegisterType<IUserListPresenter, UserListPresenter>("IUserListPresenter");

            _container.RegisterType<IUserView, UserView>();
            _container.RegisterType<IUserPresenter, UserPresenter>("IUserPresenter");
            #endregion

            #region User Group
            _container.RegisterType<IUserGroupListView, UserGroupListView>();
            _container.RegisterType<IUserGroupListPresenter, UserGroupListPresenter>("IUserGroupListPresenter");

            _container.RegisterType<IUserGroupView, UserGroupView>();
            _container.RegisterType<IUserGroupPresenter, UserGroupPresenter>("IUserGroupPresenter");
            #endregion

            #region Company
            _container.RegisterType<ICompanyListView, CompanyListView>();
            _container.RegisterType<ICompanyListPresenter, CompanyListPresenter>("ICompanyListPresenter");

            _container.RegisterType<ICompanyView, CompanyView>();
            _container.RegisterType<ICompanyPresenter, CompanyPresenter>("ICompanyPresenter");
            #endregion


        }

    }
}
