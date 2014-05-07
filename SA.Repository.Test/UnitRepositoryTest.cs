using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for UnitRepositoryTest and is intended
    ///to contain all UnitRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UnitRepositoryTest
    {

        private static Unidade item;
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
            item = new Unidade() { Descricao = "Unidade01", Simbolo = "Un" };
            IUnitRepository target = new UnitRepository();
            target.Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            IUnitRepository target = new UnitRepository();
            target.Remove(item);
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
            Unidade item = new Unidade() { Descricao = "Unidade01", Simbolo = "Un1" };
            IUnitRepository target = new UnitRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Unidade>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Descricao, fromDb.Descricao);
                    Assert.AreEqual(item.Simbolo, fromDb.Simbolo);
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
            IUnitRepository target = new UnitRepository();

            item.Descricao = "Unidade02";
            item.Simbolo = "Un2";
            target.Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Unidade>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Descricao, fromDb.Descricao);
                Assert.AreEqual(item.Simbolo, fromDb.Simbolo);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            Unidade item = new Unidade() { Descricao = "Unidade00", Simbolo = "Un0" };
            IUnitRepository target = new UnitRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Unidade>(item.Id);

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
            IUnitRepository target = new UnitRepository();
            var fromDb = target.GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Descricao, fromDb.Descricao);
            Assert.AreEqual(item.Simbolo, fromDb.Simbolo);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetBySymbolTest()
        {
            IUnitRepository target = new UnitRepository();

            var fromDb = target.GetBySymbol(item.Simbolo);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Descricao, fromDb.Descricao);
            Assert.AreEqual(item.Simbolo, fromDb.Simbolo);
        }
    }
}
