using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for CompanyRepositoryTest and is intended
    ///to contain all CompanyRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompanyRepositoryTest
    {

        private static Empresa item;
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
            IZipCodeRepository r = new ZipCodeRepository();
            var list = r.GetAll().GetEnumerator();
            list.MoveNext();

            item = new Empresa() { Nome = "company", CNPJ = "12345678901234", InscricaoEstadual = "Isento", Complemento = "Ao lado do mercado", Numero = 10, Cep = list.Current };
            ICompanyRepository target = new CompanyRepository();
            target.Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            ICompanyRepository target = new CompanyRepository();
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
            IZipCodeRepository r = new ZipCodeRepository();
            var list = r.GetAll().GetEnumerator();
            list.MoveNext();

            Empresa item = new Empresa() { Nome = "company01", CNPJ = "12345678901234", InscricaoEstadual = "Isento", Complemento = "Ao lado do mercado", Numero = 10, Cep = list.Current };
            ICompanyRepository target = new CompanyRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Empresa>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.CNPJ, fromDb.CNPJ);
                    Assert.AreEqual(item.InscricaoEstadual, fromDb.InscricaoEstadual);
                    Assert.AreEqual(item.Numero, fromDb.Numero);
                    Assert.AreEqual(item.Complemento, fromDb.Complemento);
                    Assert.IsNotNull(fromDb.Cep);
                    Assert.AreEqual(item.Cep.Id, fromDb.Cep.Id);
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
            IZipCodeRepository r = new ZipCodeRepository();
            var list = r.GetAll().GetEnumerator();
            list.MoveNext();

            Empresa item = new Empresa() { Nome = "company01", CNPJ = "12345678901234", InscricaoEstadual = "Isento", Complemento = "Ao lado do mercado", Numero = 10, Cep = list.Current };
            ICompanyRepository target = new CompanyRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Empresa>(item.Id);

                Assert.IsNull(fromDb);
                Assert.AreNotSame(item, fromDb);
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            ICompanyRepository target = new CompanyRepository();

            item.Nome = "NovoLogin";
            item.CNPJ = "43210987654321";
            item.InscricaoEstadual = "Isentasso";
            item.Complemento = "Longe";
            item.Numero = 200;
            target.Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Empresa>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.CNPJ, fromDb.CNPJ);
                Assert.AreEqual(item.InscricaoEstadual, fromDb.InscricaoEstadual);
                Assert.AreEqual(item.Numero, fromDb.Numero);
                Assert.AreEqual(item.Complemento, fromDb.Complemento);
                Assert.IsNotNull(fromDb.Cep);
                Assert.AreEqual(item.Cep.Id, fromDb.Cep.Id);
            }
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            ICompanyRepository target = new CompanyRepository();
            var fromDb = target.GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.CNPJ, fromDb.CNPJ);
            Assert.AreEqual(item.InscricaoEstadual, fromDb.InscricaoEstadual);
            Assert.AreEqual(item.Numero, fromDb.Numero);
            Assert.AreEqual(item.Complemento, fromDb.Complemento);
            Assert.IsNotNull(fromDb.Cep);
            Assert.AreEqual(item.Cep.Id, fromDb.Cep.Id);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            ICompanyRepository target = new CompanyRepository();
            var fromDb = target.GetByName(item.Nome);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.CNPJ, fromDb.CNPJ);
            Assert.AreEqual(item.InscricaoEstadual, fromDb.InscricaoEstadual);
            Assert.AreEqual(item.Numero, fromDb.Numero);
            Assert.AreEqual(item.Complemento, fromDb.Complemento);
            Assert.IsNotNull(fromDb.Cep);
            Assert.AreEqual(item.Cep.Id, fromDb.Cep.Id);
        }
    }
}
