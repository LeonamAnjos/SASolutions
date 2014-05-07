using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using SA.Repository.Repositories;
using Microsoft.Practices.Composite.Modularity;
using SA.Repository.Factories;

namespace SA.Repository
{
    public class RepositoryModule : IModule
    {

        private readonly IUnityContainer _container;

        public RepositoryModule(IUnityContainer container)
        {
            this._container = container;
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
            #region Factories
            _container.RegisterInstance<ISessionFactory>("SessionFactory", NHibernateHelper.SessionFactory, new ExternallyControlledLifetimeManager());
            _container.RegisterType<IOrderFactory, OrderFactory>();
            #endregion
            

            #region Repository

            #region Address
            _container.RegisterType<ICountryRepository, CountryRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IStateRepository, StateRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<ICityRepository, CityRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IZipCodeRepository, ZipCodeRepository>(new ExternallyControlledLifetimeManager());
            #endregion

            #region Adm
            _container.RegisterType<IUserGroupRepository, UserGroupRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IUserRepository, UserRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<ICompanyRepository, CompanyRepository>(new ExternallyControlledLifetimeManager());
            #endregion

            #region Stock
            _container.RegisterType<IProductRepository, ProductRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IProducerRepository, ProducerRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IGroupRepository, GroupRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<ISubGroupRepository, SubGroupRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IUnitRepository, UnitRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IVendorRepository, VendorRepository>(new ExternallyControlledLifetimeManager());
            _container.RegisterType<IOrderRepository, OrderRepository>(new ExternallyControlledLifetimeManager());
            #endregion

            #region Financial
            _container.RegisterType<IRegisterRepository, RegisterRepository>(new ExternallyControlledLifetimeManager());
            #endregion

            #endregion

        }
    }
}
