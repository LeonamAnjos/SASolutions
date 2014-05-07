using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using System.Collections.Generic;
using NHibernate;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for SubGroupRepositoryTest and is intended
    ///to contain all SubGroupRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SubGroupRepositoryTest
    {

        private static SubGrupo item;
        private static Grupo grupo;
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
                
            item = new SubGrupo() { Descricao = "SubGrupo01", Grupo = grupo };
            new SubGroupRepository().Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            new SubGroupRepository().Remove(item);
            new GroupRepository().Remove(grupo);
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
            SubGrupo item = new SubGrupo() { Descricao = "SubGrupo01", Grupo = grupo };
            ISubGroupRepository target = new SubGroupRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<SubGrupo>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Descricao, fromDb.Descricao);
                    Assert.IsNotNull(fromDb.Grupo);
                    Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
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
            item.Descricao = "SubGrupo02";
            new SubGroupRepository().Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<SubGrupo>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Descricao, fromDb.Descricao);
                Assert.IsNotNull(fromDb.Grupo);
                Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            SubGrupo item = new SubGrupo() { Descricao = "SubGrupo00", Grupo = grupo };
            ISubGroupRepository target = new SubGroupRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<SubGrupo>(item.Id);

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
            var fromDb = new SubGroupRepository().GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Descricao, fromDb.Descricao);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
        }

        /// <summary>
        ///A test for GetByDescription
        ///</summary>
        [TestMethod()]
        public void GetByDescriptionTest()
        {
            var fromDb = new SubGroupRepository().GetByDescription(item.Descricao);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Descricao, fromDb.Descricao);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Descricao, fromDb.Grupo.Descricao);
        }

        /// <summary>
        ///A test for GetByGroup
        ///</summary>
        [TestMethod()]
        public void GetByGroupTest()
        {
            var fromDb = new SubGroupRepository().GetByGroup(grupo);

            Assert.IsNotNull(fromDb);
            Assert.IsTrue(fromDb.Count > 0);
        }

    }
}
