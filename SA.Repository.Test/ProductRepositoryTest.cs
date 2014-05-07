using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using System.Collections.Generic;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for ProductRepositoryTest and is intended
    ///to contain all ProductRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProductRepositoryTest
    {

        private static Produto item;
        private static Fabricante fabricante;
        private static Grupo grupo;
        private static SubGrupo subGrupo;
        private static Unidade unidade;

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
            grupo = new Grupo() { Descricao = "Descricao01" };
            new GroupRepository().Add(grupo);

            subGrupo = new SubGrupo() { Descricao = "Descricao01", Grupo = grupo };
            new SubGroupRepository().Add(subGrupo);

            fabricante = new Fabricante() { Nome = "Fabricante01" };
            new ProducerRepository().Add(fabricante);;
            
            unidade = new Unidade() { Descricao = "Unidade01", Simbolo = "Un01" };
            new UnitRepository().Add(unidade);

            item = new Produto() { Referencia = "001122", Nome = "Produto01", Grupo = grupo, SubGrupo = subGrupo, Unidade = unidade, Fabricante = fabricante };
            new ProductRepository().Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            new ProductRepository().Remove(item);
            new SubGroupRepository().Remove(subGrupo);
            new GroupRepository().Remove(grupo);
            new ProducerRepository().Remove(fabricante);
            new UnitRepository().Remove(unidade);
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
            Produto item = new Produto() { Referencia = "112233", Nome = "Produto02", Grupo = grupo, SubGrupo = subGrupo, Unidade = unidade, Fabricante = fabricante };
            IProductRepository target = new ProductRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Produto>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Referencia, fromDb.Referencia);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.IsNotNull(fromDb.Grupo);
                    Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
                    Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
                    Assert.IsNotNull(fromDb.SubGrupo);
                    Assert.AreEqual(item.SubGrupo.Id, fromDb.SubGrupo.Id);
                    Assert.AreEqual(item.SubGrupo.Descricao, fromDb.SubGrupo.Descricao);
                    Assert.IsNotNull(fromDb.Fabricante);
                    Assert.AreEqual(item.Fabricante.Id, fromDb.Fabricante.Id);
                    Assert.AreEqual(item.Fabricante.Nome, fromDb.Fabricante.Nome);
                    Assert.IsNotNull(fromDb.Unidade);
                    Assert.AreEqual(item.Unidade.Id, fromDb.Unidade.Id);
                    Assert.AreEqual(item.Unidade.Descricao, fromDb.Unidade.Descricao);
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
            item.Nome = "Produto02";
            item.Referencia = "3322211";
            new ProductRepository().Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Produto>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Referencia, fromDb.Referencia);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.IsNotNull(fromDb.Grupo);
                Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
                Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
                Assert.IsNotNull(fromDb.SubGrupo);
                Assert.AreEqual(item.SubGrupo.Id, fromDb.SubGrupo.Id);
                Assert.AreEqual(item.SubGrupo.Descricao, fromDb.SubGrupo.Descricao);
                Assert.IsNotNull(fromDb.Fabricante);
                Assert.AreEqual(item.Fabricante.Id, fromDb.Fabricante.Id);
                Assert.AreEqual(item.Fabricante.Nome, fromDb.Fabricante.Nome);
                Assert.IsNotNull(fromDb.Unidade);
                Assert.AreEqual(item.Unidade.Id, fromDb.Unidade.Id);
                Assert.AreEqual(item.Unidade.Descricao, fromDb.Unidade.Descricao);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            Produto item = new Produto() { Referencia = "112233", Nome = "Produto02", Grupo = grupo, SubGrupo = subGrupo, Unidade = unidade, Fabricante = fabricante };
            IProductRepository target = new ProductRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Produto>(item.Id);

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
            var fromDb = new ProductRepository().GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Referencia, fromDb.Referencia);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
            Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
            Assert.IsNotNull(fromDb.SubGrupo);
            Assert.AreEqual(item.SubGrupo.Id, fromDb.SubGrupo.Id);
            Assert.AreEqual(item.SubGrupo.Descricao, fromDb.SubGrupo.Descricao);
            Assert.IsNotNull(fromDb.Fabricante);
            Assert.AreEqual(item.Fabricante.Id, fromDb.Fabricante.Id);
            Assert.AreEqual(item.Fabricante.Nome, fromDb.Fabricante.Nome);
            Assert.IsNotNull(fromDb.Unidade);
            Assert.AreEqual(item.Unidade.Id, fromDb.Unidade.Id);
            Assert.AreEqual(item.Unidade.Descricao, fromDb.Unidade.Descricao);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            var fromDb = new ProductRepository().GetByName(item.Nome);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Referencia, fromDb.Referencia);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
            Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
            Assert.IsNotNull(fromDb.SubGrupo);
            Assert.AreEqual(item.SubGrupo.Id, fromDb.SubGrupo.Id);
            Assert.AreEqual(item.SubGrupo.Descricao, fromDb.SubGrupo.Descricao);
            Assert.IsNotNull(fromDb.Fabricante);
            Assert.AreEqual(item.Fabricante.Id, fromDb.Fabricante.Id);
            Assert.AreEqual(item.Fabricante.Nome, fromDb.Fabricante.Nome);
            Assert.IsNotNull(fromDb.Unidade);
            Assert.AreEqual(item.Unidade.Id, fromDb.Unidade.Id);
            Assert.AreEqual(item.Unidade.Descricao, fromDb.Unidade.Descricao);
        }

        /// <summary>
        ///A test for GetByReference
        ///</summary>
        [TestMethod()]
        public void GetByReferenceTest()
        {
            var fromDb = new ProductRepository().GetByReference(item.Referencia);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Referencia, fromDb.Referencia);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
            Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
            Assert.IsNotNull(fromDb.SubGrupo);
            Assert.AreEqual(item.SubGrupo.Id, fromDb.SubGrupo.Id);
            Assert.AreEqual(item.SubGrupo.Descricao, fromDb.SubGrupo.Descricao);
            Assert.IsNotNull(fromDb.Fabricante);
            Assert.AreEqual(item.Fabricante.Id, fromDb.Fabricante.Id);
            Assert.AreEqual(item.Fabricante.Nome, fromDb.Fabricante.Nome);
            Assert.IsNotNull(fromDb.Unidade);
            Assert.AreEqual(item.Unidade.Id, fromDb.Unidade.Id);
            Assert.AreEqual(item.Unidade.Descricao, fromDb.Unidade.Descricao);
        }


        /// <summary>
        ///A test for GetByProducer
        ///</summary>
        [TestMethod()]
        public void GetByProducerTest()
        {
            var fromDb = new ProductRepository().GetByProducer(fabricante);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }

        /// <summary>
        ///A test for GetByGroup
        ///</summary>
        [TestMethod()]
        public void GetByGroupTest()
        {
            var fromDb = new ProductRepository().GetByGroup(grupo);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }

        /// <summary>
        ///A test for GetBySubGroup
        ///</summary>
        [TestMethod()]
        public void GetBySubGroupTest()
        {
            var fromDb = new ProductRepository().GetBySubGroup(subGrupo);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }

    }
}
