using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using SA.Repository.Enums;
using SA.Repository.Extensions;
using System.Linq;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for RegisterRepositoryTest and is intended
    ///to contain all RegisterRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RegisterRepositoryTest
    {

        private static Cadastro[] itens = new Cadastro[2];
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


            itens[0] = new Cadastro() { Situacao = ActiveInactiveType.Active, Nome = "Nome01", CPF = "12345678901", RG = "70010010", Telefone = "4499778877", Celular = "4499778855", Fax = "4477884488", DataNascimento = DateTime.Parse("29/01/1970"), EMail = "a@b.c", CorrespCep = cep, Tipo = PersonType.Fisica };
            itens[1] = new Cadastro() { Situacao = ActiveInactiveType.Active, Nome = "Nome02", CPF = "55555678903333", RG = "ISENTO", Telefone = "4499778877", Celular = "4499778855", Fax = "4477884488", DataNascimento = DateTime.Parse("29/01/1970"), EMail = "a@b.c", Contato = "Fulano", CorrespCep = cep, Tipo = PersonType.Juridica };

            IRegisterRepository r = new RegisterRepository();
            foreach (var item in itens)
                r.Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            IRegisterRepository r = new RegisterRepository();
            foreach (var item in itens)
                r.Remove(item);
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
            Cadastro item = new Cadastro() { Situacao = ActiveInactiveType.Active, Nome = "Nome03", CPF = "12345678903", RG = "70010013", Telefone = "4499778877", Celular = "4499778855", Fax = "4477884488", DataNascimento = DateTime.Parse("29/01/1970"), EMail = "a@b.c", CorrespCep = cep, Tipo = PersonType.Fisica };
            IRegisterRepository target = new RegisterRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Cadastro>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Tipo, fromDb.Tipo);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.CPF, fromDb.CPF);
                    Assert.AreEqual(item.RG, fromDb.RG);
                    Assert.AreEqual(item.Telefone, fromDb.Telefone);
                    Assert.AreEqual(item.Celular, fromDb.Celular);

                    Assert.AreEqual(item.Tipo, fromDb.Tipo);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.RazaoSocial, fromDb.RazaoSocial);
                    Assert.AreEqual(item.Contato, fromDb.Contato);
                    Assert.AreEqual(item.CPF, fromDb.CPF);
                    Assert.AreEqual(item.RG, fromDb.RG);
        
                    Assert.IsNotNull(fromDb.CorrespCep);
                    Assert.AreEqual(item.CorrespCep.Id, fromDb.CorrespCep.Id);
                    Assert.AreEqual(item.CorrespNumero, fromDb.CorrespNumero);
                    Assert.AreEqual(item.CorrespComplemento, fromDb.CorrespComplemento);

                    Assert.AreEqual(item.Telefone, fromDb.Telefone);
                    Assert.AreEqual(item.Celular, fromDb.Celular);
                    Assert.AreEqual(item.Fax, fromDb.Fax);
                    Assert.AreEqual(item.EMail, fromDb.EMail);

                    Assert.AreEqual(item.DataNascimento, fromDb.DataNascimento);
                    Assert.AreEqual(item.DataInclusao, fromDb.DataInclusao);
                    Assert.AreEqual(item.DataAlteracao, fromDb.DataAlteracao);
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
            var item = RegisterRepositoryTest.itens[0];
            item.Tipo = PersonType.Juridica;
            item.CPF = "12345678901200";
            item.Situacao = ActiveInactiveType.Inactive;
            new RegisterRepository().Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Cadastro>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Tipo, fromDb.Tipo);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.CPF, fromDb.CPF);
                Assert.AreEqual(item.RG, fromDb.RG);
                Assert.AreEqual(item.Telefone, fromDb.Telefone);
                Assert.AreEqual(item.Celular, fromDb.Celular);

                Assert.AreEqual(item.Tipo, fromDb.Tipo);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.RazaoSocial, fromDb.RazaoSocial);
                Assert.AreEqual(item.Contato, fromDb.Contato);
                Assert.AreEqual(item.CPF, fromDb.CPF);
                Assert.AreEqual(item.RG, fromDb.RG);

                Assert.IsNotNull(fromDb.CorrespCep);
                Assert.AreEqual(item.CorrespCep.Id, fromDb.CorrespCep.Id);
                Assert.AreEqual(item.CorrespNumero, fromDb.CorrespNumero);
                Assert.AreEqual(item.CorrespComplemento, fromDb.CorrespComplemento);

                Assert.AreEqual(item.Telefone, fromDb.Telefone);
                Assert.AreEqual(item.Celular, fromDb.Celular);
                Assert.AreEqual(item.Fax, fromDb.Fax);
                Assert.AreEqual(item.EMail, fromDb.EMail);

                Assert.AreEqual(item.DataNascimento, fromDb.DataNascimento);
                Assert.AreEqual(item.DataInclusao, fromDb.DataInclusao);
                Assert.AreEqual(item.DataAlteracao, fromDb.DataAlteracao);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            Cadastro item = new Cadastro() { Situacao = ActiveInactiveType.Inactive, Nome = "Nome04", CPF = "12345678904", RG = "70010013", Telefone = "4499778877", Celular = "4499778855", Fax = "4477884488", DataNascimento = DateTime.Parse("29/01/1970"), EMail = "a@b.c", CorrespCep = cep, Tipo = PersonType.Fisica };
            IRegisterRepository target = new RegisterRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Cadastro>(item.Id);

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
            var item = RegisterRepositoryTest.itens[1];
            var fromDb = new RegisterRepository().GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Tipo, fromDb.Tipo);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.CPF, fromDb.CPF);
            Assert.AreEqual(item.RG, fromDb.RG);
            Assert.AreEqual(item.Telefone, fromDb.Telefone);
            Assert.AreEqual(item.Celular, fromDb.Celular);

            Assert.AreEqual(item.Tipo, fromDb.Tipo);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.RazaoSocial, fromDb.RazaoSocial);
            Assert.AreEqual(item.Contato, fromDb.Contato);
            Assert.AreEqual(item.CPF, fromDb.CPF);
            Assert.AreEqual(item.RG, fromDb.RG);

            Assert.IsNotNull(fromDb.CorrespCep);
            Assert.AreEqual(item.CorrespCep.Id, fromDb.CorrespCep.Id);
            Assert.AreEqual(item.CorrespNumero, fromDb.CorrespNumero);
            Assert.AreEqual(item.CorrespComplemento, fromDb.CorrespComplemento);

            Assert.AreEqual(item.Telefone, fromDb.Telefone);
            Assert.AreEqual(item.Celular, fromDb.Celular);
            Assert.AreEqual(item.Fax, fromDb.Fax);
            Assert.AreEqual(item.EMail, fromDb.EMail);

            Assert.AreEqual(item.DataNascimento, fromDb.DataNascimento);
            Assert.AreEqual(item.DataInclusao, fromDb.DataInclusao);
            Assert.AreEqual(item.DataAlteracao, fromDb.DataAlteracao);
        }

        /// <summary>
        ///A test for GetByType
        ///</summary>
        [TestMethod()]
        public void GetByName()
        {
            var item = RegisterRepositoryTest.itens[1];
            var fromDb = new RegisterRepository().GetByName(item.Nome);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }

        /// <summary>
        ///A test for GetByCPF
        ///</summary>
        [TestMethod()]
        public void GetByCPFTest()
        {
            var item = RegisterRepositoryTest.itens[1];
            var fromDb = new RegisterRepository().GetByCPF(item.CPF);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Tipo, fromDb.Tipo);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.CPF, fromDb.CPF);
            Assert.AreEqual(item.RG, fromDb.RG);
            Assert.AreEqual(item.Telefone, fromDb.Telefone);
            Assert.AreEqual(item.Celular, fromDb.Celular);

            Assert.AreEqual(item.Tipo, fromDb.Tipo);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.RazaoSocial, fromDb.RazaoSocial);
            Assert.AreEqual(item.Contato, fromDb.Contato);
            Assert.AreEqual(item.CPF, fromDb.CPF);
            Assert.AreEqual(item.RG, fromDb.RG);

            Assert.IsNotNull(fromDb.CorrespCep);
            Assert.AreEqual(item.CorrespCep.Id, fromDb.CorrespCep.Id);
            Assert.AreEqual(item.CorrespNumero, fromDb.CorrespNumero);
            Assert.AreEqual(item.CorrespComplemento, fromDb.CorrespComplemento);

            Assert.AreEqual(item.Telefone, fromDb.Telefone);
            Assert.AreEqual(item.Celular, fromDb.Celular);
            Assert.AreEqual(item.Fax, fromDb.Fax);
            Assert.AreEqual(item.EMail, fromDb.EMail);

            Assert.AreEqual(item.DataNascimento, fromDb.DataNascimento);
            Assert.AreEqual(item.DataInclusao, fromDb.DataInclusao);
            Assert.AreEqual(item.DataAlteracao, fromDb.DataAlteracao);
        }

        /// <summary>
        ///A test for GetAllClients
        ///</summary>
        [TestMethod()]
        public void GetAllClients()
        {
            var fromDb = new RegisterRepository().GetAllClients();

            Assert.IsNotNull(fromDb);
            foreach (var c in fromDb)
            {
                Assert.IsTrue(c.EhCliente());
            }
        }

        /// <summary>
        ///A test for GetAllSuppliers
        ///</summary>
        [TestMethod()]
        public void GetAllSuppliers()
        {
            var fromDb = new RegisterRepository().GetAllSuppliers();

            Assert.IsNotNull(fromDb);
            foreach (var c in fromDb)
            {
                Assert.IsTrue(c.EhFornecedor());
            }
        }


    }
}
