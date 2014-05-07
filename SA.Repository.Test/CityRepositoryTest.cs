using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for CityRepositoryTest and is intended
    ///to contain all CityRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CityRepositoryTest
    {


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
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            StateRepository sr = new StateRepository();

            var p = sr.GetAll().GetEnumerator();
            p.MoveNext();

            Cidade item = new Cidade() { Nome = "Cidade01", DDD="00", Estado = p.Current };
            ICityRepository target = new CityRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Cidade>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.DDD, fromDb.DDD);
                    Assert.AreEqual(item.Estado.Id, fromDb.Estado.Id);
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
            StateRepository sr = new StateRepository();

            var p = sr.GetAll().GetEnumerator();
            p.MoveNext();

            Cidade item = new Cidade() { Nome = "Cidade01", DDD = "00", Estado = p.Current };
            ICityRepository target = new CityRepository();
            target.Add(item);

            p.MoveNext();

            item.Nome = "Cidade02";
            item.DDD = "01";
            item.Estado = p.Current;
            target.Update(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Cidade>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.DDD, fromDb.DDD);
                    Assert.AreEqual(item.Estado.Id, fromDb.Estado.Id);
                }
            }
            finally
            {
                target.Remove(item);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            StateRepository sr = new StateRepository();

            var p = sr.GetAll().GetEnumerator();
            p.MoveNext();

            Cidade item = new Cidade() { Nome = "Cidade01", DDD = "00", Estado = p.Current };
            ICityRepository target = new CityRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Cidade>(item.Id);

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
            StateRepository sr = new StateRepository();

            var p = sr.GetAll().GetEnumerator();
            p.MoveNext();

            Cidade item = new Cidade() { Nome = "Cidade01", DDD = "00", Estado = p.Current };
            ICityRepository target = new CityRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetById(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.DDD, fromDb.DDD);
                Assert.AreEqual(item.Estado.Id, fromDb.Estado.Id);
            }
            finally
            {
                target.Remove(item);
            }
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            StateRepository sr = new StateRepository();

            var p = sr.GetAll().GetEnumerator();
            p.MoveNext();

            Cidade item = new Cidade() { Nome = "Cidade01", DDD = "00", Estado = p.Current };
            ICityRepository target = new CityRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetByName(item.Nome);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.DDD, fromDb.DDD);
                Assert.AreEqual(item.Estado.Id, fromDb.Estado.Id);
            }
            finally
            {
                target.Remove(item);
            }
        }
    }
}
