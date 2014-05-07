using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using SA.Repository.Extensions;
using System.Collections.Generic;
using NHibernate;
using SA.Repository.Enums;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for OrderRepositoryTest and is intended
    ///to contain all OrderRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OrderRepositoryTest
    {
        private static IList<Pedido> good = new List<Pedido>();
        private static IList<Pedido> bad  = new List<Pedido>();
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
            var registers = new RegisterRepository().GetAllClients().GetEnumerator();
            registers.MoveNext();
            var client = registers.Current;
            
            registers = new RegisterRepository().GetAllSuppliers().GetEnumerator();
            registers.MoveNext();
            var supplier = registers.Current;

            var vendors = new VendorRepository().GetAll().GetEnumerator();
            vendors.MoveNext();
            Vendedor vendor;
            if (vendors.Current != null)
                vendor = vendors.Current;
            else
                vendor = new Vendedor() { Nome = "Vendedor teste" };

            IList<PedidoItem> goodItems = new List<PedidoItem>();
            goodItems.Add(new PedidoItem() { Produto = new Produto() { Nome = "ProdTeste" }, ValorUnitario = 3.33, ValorDesconto = 1.66, Quantidade = 3, Valor = ((3 * 3.33) - 1.66) });
            goodItems.Add(new PedidoItem() { Produto = new Produto() { Nome = "ProdTeste" }, ValorUnitario = 133.66, ValorDesconto = 17.99, Quantidade = 6, Valor = ((133.66 * 6) - 17.99) });
            goodItems.Add(new PedidoItem() { Produto = new Produto() { Nome = "ProdTeste" }, ValorUnitario = 13.73, ValorDesconto = 2.11, Quantidade = 1.77, Valor = ((13.73 * 1.77) - 2.11) });

            good.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, DataValidade = DateTime.Today });
            good.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor,  DataValidade = DateTime.Today });
            good.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order,  Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor });
            good.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.BilledOrder, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor });

            good.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });
            good.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });
            good.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor });
            good.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.BilledOrder, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor });

            foreach (var pedido in good)
            {
                foreach (var item in goodItems)
                {
                    pedido.Itens.Add(item);
                }
                pedido.Atualizar();
            }


            IList<PedidoItem> badItems = new List<PedidoItem>();
            badItems.Add(new PedidoItem() { Produto = new Produto() { Nome = "ProdBadTeste" }, ValorUnitario = 3.34, ValorDesconto = 1.66, Quantidade = 3, Valor = ((3 * 3.33) - 1.66) });
            badItems.Add(new PedidoItem() { Produto = new Produto() { Nome = "ProdBadTeste" }, ValorUnitario = 133.66, ValorDesconto = 17.98, Quantidade = 6, Valor = ((133.66 * 6) - 17.99) });
            badItems.Add(new PedidoItem() { Produto = new Produto() { Nome = "ProdBadTeste" }, ValorUnitario = 13.73, ValorDesconto = 2.11, Quantidade = 1.76, Valor = ((13.73 * 1.77) - 2.11) });

            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.BilledOrder, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor });

            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.BilledOrder, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor });

            foreach (var pedido in bad)
            {
                foreach (var item in badItems)
                {
                    pedido.Atualizar();
                    pedido.Itens.Add(item);
                }
            }

            // Sales Order - Tender
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Cadastro = client, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, DataValidade = DateTime.Today });
            

            // Sales order - Order
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Vendedor = vendor });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Cadastro = client, Vendedor = vendor });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor });

            // Sales values checking
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, DataValidade = DateTime.Today, Valor = -1 });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.Order, Data  = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, ValorDesconto = -1 });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.SalesOrder, Fase = Enums.PhaseType.BilledOrder, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = client, Vendedor = vendor, ValorDescontoTotal = -1 });

            // Purchase order - Tender
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });            

            // Purchase order - Order
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today });

            // Purchase values checking
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Tender, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, DataValidade = DateTime.Today, Valor = 1 });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.Order, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, ValorDesconto = 1 });
            bad.Add(new Pedido() { Tipo = Enums.OrderType.PurchaseOrder, Fase = Enums.PhaseType.BilledOrder, Data = DateTime.Today, Hora = DateTime.Now, Cadastro = supplier, Vendedor = vendor, ValorDescontoTotal = 1 });

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
        ///A test for Validation
        ///</summary>
        [TestMethod()]
        public void ValidationTest()
        {
            foreach (var v in good)
                Assert.IsTrue(v.Validar().IsValid);

            foreach (var v in bad)
                Assert.IsFalse(v.Validar().IsValid);
        }
    }
}
