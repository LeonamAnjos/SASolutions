using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using SA.Stock.View.Unit;
using SA.Infrastructure;
using SA.BreadCrumb.View;
using SA.Stock.View.Product;
using SA.Stock.View.Producer;
using SA.Stock.View.Vendor;
using SA.Stock.View.Group;
using SA.Stock.View.SubGroup;
using SA.Stock.View.Order;
using SA.Stock.View.Cashier;
using SA.Stock.ViewModel;


namespace SA.Stock
{
    public class StockModule : IModule, ISAModuleControler
    {
        private readonly IUnityContainer _container;
        private readonly IBreadCrumbPresenter _breadCrumb;
        private readonly IRegionManager _regionManager;

        public StockModule(IUnityContainer container, IRegionManager regionManager, IBreadCrumbPresenter breadCrumb)
        {
            this._container = container;
            this._breadCrumb = breadCrumb;
            this._regionManager = regionManager;
        }

        /// <summary>
        /// Module - When a module is loaded, the Initialize method is called. Here you need to populate the global container (if necessary) and show views on shell regions.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            // register views to scoped regions
            _regionManager.RegisterViewWithRegion(CashierRegionNames.ItemsRegion, () => _container.Resolve<ICashierItemsPresenter>("ICashierItemsPresenter").View);
            _regionManager.RegisterViewWithRegion(CashierRegionNames.CouponRegion, () => _container.Resolve<ICashierListItemsPresenter>("ICashierListItemsPresenter").View);
            _regionManager.RegisterViewWithRegion(CashierRegionNames.OrderRegion, () => _container.Resolve<ICashierOrderPresenter>("ICashierOrderPresenter").View);
        }

        public void Run()
        {
            this._breadCrumb.AddCrumb("Produtos", _container.Resolve<IProductListPresenter>("IProductListPresenter"));
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterInstance<ISAModuleControler>("StockModule", this);


            #region Unit
            _container.RegisterType<IUnitListView, UnitListView>();
            _container.RegisterType<IUnitListPresenter, UnitListPresenter>("IUnitListPresenter");
            _container.RegisterType<IUnitView, UnitView>();
            _container.RegisterType<IUnitPresenter, UnitPresenter>("IUnitPresenter");
            #endregion

            #region Product
            _container.RegisterType<IProductListView, ProductListView>();
            _container.RegisterType<IProductListPresenter, ProductListPresenter>("IProductListPresenter");
            _container.RegisterType<IProductView, ProductView>();
            _container.RegisterType<IProductPresenter, ProductPresenter>("IProductPresenter");
            #endregion

            #region Producer
            _container.RegisterType<IProducerListView, ProducerListView>();
            _container.RegisterType<IProducerListPresenter, ProducerListPresenter>("IProducerListPresenter");
            _container.RegisterType<IProducerView, ProducerView>();
            _container.RegisterType<IProducerPresenter, ProducerPresenter>("IProducerPresenter");
            #endregion

            #region Group
            _container.RegisterType<IGroupListView, GroupListView>();
            _container.RegisterType<IGroupListPresenter, GroupListPresenter>("IGroupListPresenter");
            _container.RegisterType<IGroupView, GroupView>();
            _container.RegisterType<IGroupPresenter, GroupPresenter>("IGroupPresenter");
            #endregion

            #region SubGroup
            _container.RegisterType<ISubGroupListView, SubGroupListView>();
            _container.RegisterType<ISubGroupListPresenter, SubGroupListPresenter>("ISubGroupListPresenter");
            _container.RegisterType<ISubGroupView, SubGroupView>();
            _container.RegisterType<ISubGroupPresenter, SubGroupPresenter>("ISubGroupPresenter");
            #endregion

            #region Vendor
            _container.RegisterType<IVendorListView, VendorListView>();
            _container.RegisterType<IVendorListPresenter, VendorListPresenter>("IVendorListPresenter");
            _container.RegisterType<IVendorView, VendorView>();
            _container.RegisterType<IVendorPresenter, VendorPresenter>("IVendorPresenter");
            #endregion

            #region Order
            _container.RegisterType<IOrderListView, OrderListView>();
            _container.RegisterType<IOrderListPresenter, OrderListPresenter>("IOrderListPresenter");
            _container.RegisterType<IOrderView, OrderView>();
            _container.RegisterType<IOrderPresenter, OrderPresenter>("IOrderPresenter");
            #endregion

            #region Cashier
            _container.RegisterType<ICashierView, CashierView>();
            _container.RegisterType<ICashierPresenter, CashierPresenter>("ICashierPresenter");

            _container.RegisterType<ICashierItemsView, CashierItemsView>();
            _container.RegisterType<ICashierItemsPresenter, CashierItemsPresenter>("ICashierItemsPresenter");

            _container.RegisterType<ICashierListItemsView, CashierListItemsView>();
            _container.RegisterType<ICashierListItemsPresenter, CashierListItemsPresenter>("ICashierListItemsPresenter");

            _container.RegisterType<ICashierOrderView, CashierOrderView>();
            _container.RegisterType<ICashierOrderPresenter, CashierOrderPresenter>("ICashierOrderPresenter");
            #endregion


        }

    }
}
