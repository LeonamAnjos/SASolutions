using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for ProducerRepositoryTest and is intended
    ///to contain all ProducerRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProducerRepositoryTest
    {

        private static Fabricante item;
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
            item = new Fabricante() { Nome = "Fabricante01" };
            IProducerRepository target = new ProducerRepository();
            target.Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            IProducerRepository target = new ProducerRepository();
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
            Fabricante item = new Fabricante() { Nome = "Fabricante01" };
            IProducerRepository target = new ProducerRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Fabricante>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
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
            IProducerRepository target = new ProducerRepository();

            item.Nome = "Fabricante02";
            target.Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Fabricante>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            Fabricante item = new Fabricante() { Nome = "Fabricante00" };
            IProducerRepository target = new ProducerRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Fabricante>(item.Id);

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
            IProducerRepository target = new ProducerRepository();
            var fromDb = target.GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Nome, fromDb.Nome);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            IProducerRepository target = new ProducerRepository();

            var fromDb = target.GetByName(item.Nome);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Nome, fromDb.Nome);
        }
    }
}
