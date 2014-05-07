using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for StateRepositoryTest and is intended
    ///to contain all StateRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StateRepositoryTest
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
            CountryRepository cr = new CountryRepository();

            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Estado item = new Estado() { Nome = "Estado01", Sigla = "01", Pais = p.Current };
            IStateRepository target = new StateRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Estado>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.Sigla, fromDb.Sigla);
                    Assert.AreEqual(item.Pais.Id, fromDb.Pais.Id);
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
            CountryRepository cr = new CountryRepository();
            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Estado item = new Estado() { Nome = "Estado01", Sigla = "01", Pais = p.Current };
            IStateRepository target = new StateRepository();
            target.Add(item);

            try
            {
                p.MoveNext();

                item.Nome = "Estado02";
                item.Sigla = "02";
                item.Pais = p.Current;
                target.Update(item);


                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Estado>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.Sigla, fromDb.Sigla);
                    Assert.AreEqual(item.Pais.Id, fromDb.Pais.Id);
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
            CountryRepository cr = new CountryRepository();

            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Estado item = new Estado() { Nome = "Estado01", Sigla = "01", Pais = p.Current };
            IStateRepository target = new StateRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Estado>(item.Id);

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
            CountryRepository cr = new CountryRepository();
            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Estado item = new Estado() { Nome = "Estado01", Sigla = "01", Pais = p.Current };
            IStateRepository target = new StateRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetById(item.Id);
                
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.Sigla, fromDb.Sigla);
                Assert.AreEqual(item.Pais.Id, fromDb.Pais.Id);
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
            CountryRepository cr = new CountryRepository();
            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Estado item = new Estado() { Nome = "Estado01", Sigla = "01", Pais = p.Current };
            IStateRepository target = new StateRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetByName(item.Nome);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.Sigla, fromDb.Sigla);
                Assert.AreEqual(item.Pais.Id, fromDb.Pais.Id);
            }
            finally
            {
                target.Remove(item);
            }
        }

    }
}
