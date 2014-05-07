using SA.Repository.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Practices.Unity;
using SA.Repository.Enums;
using SA.Repository.Domain;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for OrderFactoryTest and is intended
    ///to contain all OrderFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OrderFactoryTest
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
        ///A test for CreateOrder
        ///</summary>
        [TestMethod()]
        public void CreateOrderTest()
        {
            IOrderFactory target = _container.Resolve<IOrderFactory>();
            Pedido actual = target.CreateOrder(OrderType.SalesOrder);

            AssertOrder(target.CreateOrder(OrderType.PurchaseOrder), OrderType.PurchaseOrder);
            AssertOrder(target.CreateOrder(OrderType.SalesOrder), OrderType.SalesOrder);

            _container.RegisterInstance(typeof(Cadastro), "Cliente", new Cadastro());
            _container.RegisterInstance(typeof(Cadastro), "Fornecedor", new Cadastro());

            AssertOrder(target.CreateOrder(OrderType.PurchaseOrder), OrderType.PurchaseOrder);
            AssertOrder(target.CreateOrder(OrderType.SalesOrder), OrderType.SalesOrder);

            _container.RegisterInstance(typeof(Vendedor), "Vendedor", new Vendedor());
            AssertOrder(target.CreateOrder(OrderType.PurchaseOrder), OrderType.PurchaseOrder);
            AssertOrder(target.CreateOrder(OrderType.SalesOrder), OrderType.SalesOrder);
        }

        private void AssertOrder(Pedido pedido, OrderType tipo)
        {
            Assert.IsNotNull(pedido);
            Assert.AreEqual(pedido.Data, DateTime.Today);
            Assert.AreEqual(pedido.Tipo, tipo);
            Assert.AreEqual(pedido.Fase, PhaseType.Tender);
            Assert.IsTrue(pedido.DataValidade > pedido.Data);

            switch (tipo)
            {
                case OrderType.PurchaseOrder:
                    if (_container.IsRegistered(typeof(Cadastro), "Fornecedor"))
                    {
                        Assert.IsNotNull(pedido.Cadastro);
                        Assert.AreEqual(pedido.Cadastro, (Cadastro)_container.Resolve(typeof(Cadastro), "Fornecedor"));
                    }
                    else
                    {
                        Assert.IsNull(pedido.Cadastro);
                    }
                    break;
                case OrderType.SalesOrder:
                    if (_container.IsRegistered(typeof(Cadastro), "Cliente"))
                    {
                        Assert.IsNotNull(pedido.Cadastro);
                        Assert.AreEqual(pedido.Cadastro, (Cadastro)_container.Resolve(typeof(Cadastro), "Cliente"));
                    }
                    else
                    {
                        Assert.IsNull(pedido.Cadastro);
                    }

                    if (_container.IsRegistered(typeof(Vendedor), "Vendedor"))
                    {
                        Assert.IsNotNull(pedido.Vendedor);
                        Assert.AreEqual(pedido.Vendedor, (Vendedor)_container.Resolve(typeof(Vendedor), "Vendedor"));
                    }
                    else
                    {
                        Assert.IsNull(pedido.Vendedor);
                    }
                    break;
                default:
                    throw new AssertFailedException("Tipo de pedido inválido!");
            }

        }

    }
}
