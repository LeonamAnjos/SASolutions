using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;
using System.Collections.Generic;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for ZipCodeRepositoryTest and is intended
    ///to contain all ZipCodeRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZipCodeRepositoryTest
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


        [TestMethod()]
        public void GetByIdTest()
        {
            CityRepository cr = new CityRepository();
            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Cep item = new Cep() { CEP = "00000001", Logradouro = "Logradouro01", Bairro = "Bairro01", Cidade = p.Current };
            IZipCodeRepository target = new ZipCodeRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetById(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.CEP, fromDb.CEP);
                Assert.AreEqual(item.Logradouro, fromDb.Logradouro);
                Assert.AreEqual(item.Bairro, fromDb.Bairro);
                Assert.IsNotNull(fromDb.Cidade);
                Assert.AreEqual(item.Cidade.Id, fromDb.Cidade.Id);
            }
            finally
            {
                target.Remove(item);
            }
        }

        /// <summary>
        ///A test for GetByZipCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SA.Repository.dll")]
        public void GetByZipCodeTest()
        {
            CityRepository cr = new CityRepository();
            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Cep item = new Cep() { CEP = "00000001", Logradouro = "Logradouro01", Bairro = "Bairro01", Cidade = p.Current };
            IZipCodeRepository target = new ZipCodeRepository();
            target.Add(item);

            try
            {
                var fromDb = target.GetByZipCode(item.CEP);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.CEP, fromDb.CEP);
                Assert.AreEqual(item.Logradouro, fromDb.Logradouro);
                Assert.AreEqual(item.Bairro, fromDb.Bairro);
                Assert.IsNotNull(fromDb.Cidade);
                Assert.AreEqual(item.Cidade.Id, fromDb.Cidade.Id);
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
        [DeploymentItem("SA.Repository.dll")]
        public void GetAllTest()
        {
            IZipCodeRepository target = new ZipCodeRepository();
            var list = target.GetAll();

            foreach (var fromDb in list)
            {
                Assert.IsNotNull(fromDb);
                Assert.IsNotNull(fromDb.Cidade);
                Assert.IsNotNull(fromDb.Cidade.Nome);
            }
        }


        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            CityRepository cr = new CityRepository();

            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Cep item = new Cep() { CEP = "00000001", Logradouro = "Logradouro01", Bairro = "Bairro01", Cidade = p.Current };
            IZipCodeRepository target = new ZipCodeRepository();
                                            
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Cep>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.CEP, fromDb.CEP);
                    Assert.AreEqual(item.Logradouro, fromDb.Logradouro);
                    Assert.AreEqual(item.Bairro, fromDb.Bairro);
                    Assert.IsNotNull(fromDb.Cidade);
                    Assert.AreEqual(item.Cidade.Id, fromDb.Cidade.Id);
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
            CityRepository cr = new CityRepository();

            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Cep item = new Cep() { CEP = "00000001", Logradouro = "Logradouro01", Bairro = "Bairro01", Cidade = p.Current };
            IZipCodeRepository target = new ZipCodeRepository();

            target.Add(item);

            try
            {
                p.MoveNext();
                item.CEP = "00000002";
                item.Logradouro = "Logradouro02";
                item.Bairro = "Bairro02";
                item.Cidade = p.Current;
                target.Update(item);

                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Cep>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.CEP, fromDb.CEP);
                    Assert.AreEqual(item.Logradouro, fromDb.Logradouro);
                    Assert.AreEqual(item.Bairro, fromDb.Bairro);
                    Assert.IsNotNull(fromDb.Cidade);
                    Assert.AreEqual(item.Cidade.Id, fromDb.Cidade.Id);
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
            CityRepository cr = new CityRepository();

            var p = cr.GetAll().GetEnumerator();
            p.MoveNext();

            Cep item = new Cep() { CEP = "00000001", Logradouro = "Logradouro01", Bairro = "Bairro01", Cidade = p.Current };
            IZipCodeRepository target = new ZipCodeRepository();

            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Cep>(item.Id);

                Assert.IsNull(fromDb);
                Assert.AreNotSame(item, fromDb);
            }
        }
    }
}
