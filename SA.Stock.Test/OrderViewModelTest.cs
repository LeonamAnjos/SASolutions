using SA.Stock.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using Microsoft.Practices.Unity;
using SA.Repository.Factories;
using SA.Repository.Enums;

namespace SA.Stock.Test
{
    
    
    /// <summary>
    ///This is a test class for OrderViewModelTest and is intended
    ///to contain all OrderViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OrderViewModelTest
    {


        private static IUnityContainer _container;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);
            _container.RegisterType<IOrderFactory, OrderFactory>();
            _container.RegisterInstance(typeof(Cadastro), "Cliente", new Cadastro());
            _container.RegisterInstance(typeof(Cadastro), "Fornecedor", new Cadastro());
            _container.RegisterInstance(typeof(Vendedor), "Vendedor", new Vendedor());

            IOrderFactory orderFactory = _container.Resolve<IOrderFactory>();
            _container.RegisterInstance(typeof(Pedido), "PedidoVenda", orderFactory.CreateOrder(OrderType.SalesOrder));
            _container.RegisterInstance(typeof(Pedido), "PedidoCompra", orderFactory.CreateOrder(OrderType.PurchaseOrder));
            _container.RegisterInstance<IOrderViewModel>("VM_PedidoVenda", new OrderViewModel(_container, (Pedido)_container.Resolve(typeof(Pedido), "PedidoVenda")));
            _container.RegisterInstance<IOrderViewModel>("VM_PedidoCompra", new OrderViewModel(_container, (Pedido)_container.Resolve(typeof(Pedido), "PedidoCompra")));
        }


        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Cadastro
        ///</summary>
        [TestMethod()]
        public void CadastroTest()
        {

            IOrderViewModel targetSale = _container.Resolve<IOrderViewModel>("VM_PedidoVenda");
            IOrderViewModel targetPurchase = _container.Resolve<IOrderViewModel>("VM_PedidoCompra");
            AssertVMOrder(targetSale);
            AssertVMOrder(targetPurchase);

            Cadastro cadastro = targetSale.Cadastro;
            targetSale.Cadastro = new Cadastro();
            Assert.IsNotNull(targetSale.Cadastro);
            Assert.AreNotEqual<Cadastro>(targetSale.Cadastro, cadastro);

            cadastro = targetPurchase.Cadastro;
            targetPurchase.Cadastro = new Cadastro();
            Assert.IsNotNull(targetPurchase.Cadastro);
            Assert.AreNotEqual<Cadastro>(targetPurchase.Cadastro, cadastro);
        }

        private void AssertVMOrder(IOrderViewModel orderViewModel)
        {
            Assert.IsNotNull(orderViewModel);
            Assert.IsNotNull(orderViewModel.Cadastro);
        }
    }
}
