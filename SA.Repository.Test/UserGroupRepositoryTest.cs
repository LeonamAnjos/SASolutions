using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using System.Collections.Generic;
using NHibernate;
using SA.Repository.Enums;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for UserGroupRepositoryTest and is intended
    ///to contain all UserGroupRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserGroupRepositoryTest
    {

        private static UsuarioGrupo item;
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
            item = new UsuarioGrupo() { Descricao = "UsuarioGrupo01", Tipo = UserGroupType.Administrator };
            IUserGroupRepository target = new UserGroupRepository();
            target.Add(item);
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            IUserGroupRepository target = new UserGroupRepository();
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
            UsuarioGrupo item = new UsuarioGrupo() { Descricao = "UsuarioGrupo01", Tipo = UserGroupType.Administrator };
            IUserGroupRepository target = new UserGroupRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<UsuarioGrupo>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Descricao, fromDb.Descricao);
                    Assert.AreEqual(item.Tipo, fromDb.Tipo);
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
            IUserGroupRepository target = new UserGroupRepository();

            item.Descricao = "UsuarioGrupo02";
            item.Tipo = UserGroupType.Employee;
            target.Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<UsuarioGrupo>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Descricao, fromDb.Descricao);
                Assert.AreEqual(item.Tipo, fromDb.Tipo);
            }
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            UsuarioGrupo item = new UsuarioGrupo() { Descricao = "UsuarioGrupo00", Tipo = UserGroupType.Employee };
            IUserGroupRepository target = new UserGroupRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<UsuarioGrupo>(item.Id);

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
            IUserGroupRepository target = new UserGroupRepository();
            var fromDb = target.GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Descricao, fromDb.Descricao);
            Assert.AreEqual(item.Tipo, fromDb.Tipo);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByDescriptionTest()
        {
            IUserGroupRepository target = new UserGroupRepository();

            var fromDb = target.GetByDescription(item.Descricao);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Descricao, fromDb.Descricao);
            Assert.AreEqual(item.Tipo, fromDb.Tipo);
        }
    }
}
