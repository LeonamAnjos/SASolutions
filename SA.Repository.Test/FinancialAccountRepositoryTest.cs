using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using System.Collections.Generic;
using SA.Repository.Enums;
using System.Linq;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for FinancialAccountRepositoryTest and is intended
    ///to contain all FinancialAccountRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FinancialAccountRepositoryTest
    {

        private static ContaFinanceira item_costumer;
        private static ContaFinanceira item_supplier;
        private static Cadastro cadastro;
        private static Cep cep;
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            cep = new ZipCodeRepository().GetAll().First<Cep>();

            cadastro = new Cadastro() { Situacao = ActiveInactiveType.Active, Nome = "Nome01", CPF = "12345678912", CorrespCep = cep, Tipo = PersonType.Fisica };
            new RegisterRepository().Add(cadastro);

            item_costumer = new ContaFinanceira() { Tipo = FinancialAccountType.Costumer, Situacao = ActiveInactiveType.Active, Cadastro = cadastro, CobrancaCep = cep };
            item_supplier = new ContaFinanceira() { Tipo = FinancialAccountType.Supplier, Situacao = ActiveInactiveType.Active, Cadastro = cadastro };
            var r = new FinancialAccountRepository();
            r.Add(item_costumer);
            r.Add(item_supplier);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            new FinancialAccountRepository().Remove(item_costumer);
            new FinancialAccountRepository().Remove(item_supplier);
            new RegisterRepository().Remove(cadastro);
        }

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
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            ContaFinanceira item = new ContaFinanceira() { Tipo = FinancialAccountType.Bank, Situacao = ActiveInactiveType.Active, Cadastro = cadastro, CobrancaCep = cep };
            IFinancialAccountRepository target = new FinancialAccountRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<ContaFinanceira>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Tipo, fromDb.Tipo);
                    Assert.IsNotNull(fromDb.Cadastro);
                    Assert.AreEqual(item.Cadastro.Id, fromDb.Cadastro.Id);
                    Assert.IsNotNull(fromDb.CobrancaCep);
                    Assert.AreEqual(item.CobrancaCep.Id, fromDb.CobrancaCep.Id);
                }
            }
            finally
            {
                target.Remove(item);
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            var item = FinancialAccountRepositoryTest.item_costumer;
            item.Tipo = FinancialAccountType.Bank;
            item.Situacao = ActiveInactiveType.Inactive;
            new FinancialAccountRepository().Update(item_costumer);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                
                var fromDb = session.Get<ContaFinanceira>(item_costumer.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Tipo, fromDb.Tipo);
                Assert.IsNotNull(fromDb.Cadastro);
                Assert.AreEqual(item.Cadastro.Id, fromDb.Cadastro.Id);
                Assert.IsNotNull(fromDb.CobrancaCep);
                Assert.AreEqual(item.CobrancaCep.Id, fromDb.CobrancaCep.Id);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            ContaFinanceira item = new ContaFinanceira() { Tipo = FinancialAccountType.Costumer, Situacao = ActiveInactiveType.Active, Cadastro = cadastro, CobrancaCep = cep };
            IFinancialAccountRepository target = new FinancialAccountRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<ContaFinanceira>(item.Id);

                Assert.IsNull(fromDb);
                Assert.AreNotSame(item, fromDb);
            }
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            var item = FinancialAccountRepositoryTest.item_supplier;
            var fromDb = new FinancialAccountRepository().GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Tipo, fromDb.Tipo);
            Assert.IsNotNull(fromDb.Cadastro);
            Assert.AreEqual(item.Cadastro.Id, fromDb.Cadastro.Id);
            Assert.IsNull(fromDb.CobrancaCep);
        }


        /// <summary>
        ///A test for GetByRegister
        ///</summary>
        [TestMethod()]
        public void GetByRegisterTest()
        {
            var item = FinancialAccountRepositoryTest.item_supplier;
            var fromDb = new FinancialAccountRepository().GetByRegister(cadastro);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }

        /// <summary>
        ///A test for GetByRegister
        ///</summary>
        [TestMethod()]
        public void GetByRegisterTest1()
        {
            var item = FinancialAccountRepositoryTest.item_supplier;
            var fromDb = new FinancialAccountRepository().GetByRegister(cadastro, FinancialAccountType.Supplier);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Tipo, fromDb.Tipo);
            Assert.IsNotNull(fromDb.Cadastro);
            Assert.AreEqual(item.Cadastro.Id, fromDb.Cadastro.Id);
            Assert.IsNull(fromDb.CobrancaCep);
        }

        /// <summary>
        ///A test for GetByType
        ///</summary>
        [TestMethod()]
        public void GetByTypeTest()
        {
            var item = FinancialAccountRepositoryTest.item_supplier;
            var fromDb = new FinancialAccountRepository().GetByType(FinancialAccountType.Supplier);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }
    }
}
