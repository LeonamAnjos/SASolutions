using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using SA.Infrastructure;
using Microsoft.Practices.Unity;
using SA.BreadCrumb.View;
using SA.Address.View.Country;

using SA.Address.View.State;
using SA.Address.View.City;
using SA.Address.View.ZipCode;

namespace SA.Address
{
    public class AddressModule : IModule, ISAModuleControler
    {
        private readonly IUnityContainer _container;
        private readonly IBreadCrumbPresenter _breadCrumb;

        public AddressModule(IUnityContainer container, IBreadCrumbPresenter breadCrumb)
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
            this._breadCrumb.AddCrumb("Cep", _container.Resolve<IZipCodeListPresenter>("IZipCodeListPresenter"));
            //this._breadCrumb.AddCrumb("Cidade", _container.Resolve<ICityListPresenter>());
            //this._breadCrumb.AddCrumb("Estado", _container.Resolve<IStateListPresenter>());
            //this._breadCrumb.AddCrumb("País", _container.Resolve<ICountryListPresenter>());
        }


        protected void RegisterViewsAndServices()
        {
            _container.RegisterInstance<ISAModuleControler>("AddressModule", this);

            //_container.RegisterInstance(new Model.AddressEntities(), new TransientLifetimeManager());
            //_container.RegisterInstance(new Model.AddressEntities());
            //_container.RegisterType<IAddressModelService, AddressModelService>(new ExternallyControlledLifetimeManager());
            //_container.RegisterType<IAddressModelService, AddressModelService>(new TransientLifetimeManager()); // STE problem

            #region Country
            _container.RegisterType<ICountryListView, CountryListView>();
            _container.RegisterType<ICountryListPresenter, CountryListPresenter>("ICountryListPresenter");
            _container.RegisterType<ICountryView, CountryView>();
            _container.RegisterType<ICountryPresenter, CountryPresenter>("ICountryPresenter");
            #endregion

            #region State
            _container.RegisterType<IStateListView, StateListView>();
            _container.RegisterType<IStateListPresenter, StateListPresenter>("IStateListPresenter");
            _container.RegisterType<IStateView, StateView>();
            _container.RegisterType<IStatePresenter, StatePresenter>("IStatePresenter");
            #endregion

            #region City
            _container.RegisterType<ICityListView, CityListView>();
            _container.RegisterType<ICityListPresenter, CityListPresenter>("ICityListPresenter");
            _container.RegisterType<ICityView, CityView>();
            _container.RegisterType<ICityPresenter, CityPresenter>("ICityPresenter");
            #endregion

            #region ZipCode
            _container.RegisterType<IZipCodeListView, ZipCodeListView>();
            _container.RegisterType<IZipCodeListPresenter, ZipCodeListPresenter>("IZipCodeListPresenter");
            _container.RegisterType<IZipCodeView, ZipCodeView>();
            _container.RegisterType<IZipCodePresenter, ZipCodePresenter>("IZipCodePresenter");
            #endregion
        }
    }
}
