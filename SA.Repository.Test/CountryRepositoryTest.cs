using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;
using System.Collections.Generic;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for CountryRepositoryTest and is intended
    ///to contain all CountryRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CountryRepositoryTest
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
            Pais item = new Pais() { Nome = "Pais01", Sigla = "01" };
            CountryRepository target = new CountryRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Pais>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.Sigla, fromDb.Sigla);
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
            Pais item = new Pais() { Nome = "Pais03", Sigla = "03" };
            CountryRepository target = new CountryRepository();
            target.Add(item);

            try
            {
                item.Nome = "Pais04";
                target = new CountryRepository();
                target.Update(item);

                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Pais>(item.Id);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
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
            Pais item = new Pais() { Nome = "Pais02", Sigla = "02" };
            CountryRepository target = new CountryRepository();
            target.Add(item);

            target = new CountryRepository();
            target.Remove(item);

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Pais>(item.Id);
                Assert.IsNull(fromDb);
            }
        }


        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            Pais item = new Pais() { Nome = "Pais05", Sigla = "05" };
            CountryRepository target = new CountryRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetById(item.Id);
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
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
            Pais item = new Pais() { Nome = "Pais04", Sigla = "04" };
            CountryRepository target = new CountryRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetByName(item.Nome);
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
            }
            finally
            {
                target.Remove(item);
            }
        }


        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            Pais item = new Pais() { Nome = "Pais06", Sigla = "06" };
            CountryRepository target = new CountryRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetAll(); ;
                Assert.IsNotNull(fromDb);

                bool found = false;
                foreach (var p in fromDb)
                {
                    found = p.Id.Equals(item.Id);
                    if(found)
                        break;
                }

                Assert.IsTrue(found);
            }
            finally
            {
                target.Remove(item);
            }
        }
    }
}
