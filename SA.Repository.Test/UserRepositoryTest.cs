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
    ///This is a test class for UserRepositoryTest and is intended
    ///to contain all UserRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserRepositoryTest
    {

        private static Usuario item;
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
            IUserGroupRepository r = new UserGroupRepository();
            var list = r.GetAll().GetEnumerator();
            list.MoveNext();

            item = new Usuario() { Login = "user", Senha = "senha", Nome = "Nome", Email = "e@mail.com", Situacao = ActiveInactiveType.Active, Grupo = list.Current };
            IUserRepository target = new UserRepository();
            target.Add(item);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            IUserRepository target = new UserRepository();
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
            IUserGroupRepository r = new UserGroupRepository();
            var list = r.GetAll().GetEnumerator();
            list.MoveNext();

            Usuario item = new Usuario() { Login = "user1", Senha = "senha1", Nome = "Nome1", Email = "e@mail.com", Situacao = ActiveInactiveType.Active, Grupo = list.Current };
            IUserRepository target = new UserRepository();
            target.Add(item);

            try
            {
                // use session to try to load the product
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var fromDb = session.Get<Usuario>(item.Id);

                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(item, fromDb);
                    Assert.AreEqual(item.Login, fromDb.Login);
                    Assert.AreEqual(item.Senha, fromDb.Senha);
                    Assert.AreEqual(item.Nome, fromDb.Nome);
                    Assert.AreEqual(item.Email, fromDb.Email);
                    Assert.AreEqual(item.Situacao, fromDb.Situacao);
                    Assert.AreEqual(item.Situacao, fromDb.Situacao);
                    Assert.IsNotNull(fromDb.Grupo);
                    Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
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
            IUserGroupRepository r = new UserGroupRepository();
            var list = r.GetAll().GetEnumerator();
            list.MoveNext();

            Usuario item = new Usuario() { Login = "user2", Senha = "senha2", Nome = "Nome2", Email = "e@mail.com", Situacao = ActiveInactiveType.Active, Grupo = list.Current };
            IUserRepository target = new UserRepository();
            target.Add(item);
            target.Remove(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Usuario>(item.Id);

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
            IUserRepository target = new UserRepository();

            item.Login = "NovoLogin";
            item.Senha = "Senha nova";
            item.Nome = "Novo nome";
            item.Email = "e@mail";
            item.Situacao = ActiveInactiveType.Inactive;
            target.Update(item);

            // use session to try to load the product
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<Usuario>(item.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(item, fromDb);
                Assert.AreEqual(item.Login, fromDb.Login);
                Assert.AreEqual(item.Senha, fromDb.Senha);
                Assert.AreEqual(item.Nome, fromDb.Nome);
                Assert.AreEqual(item.Email, fromDb.Email);
                Assert.AreEqual(item.Situacao, fromDb.Situacao);
                Assert.IsNotNull(fromDb.Grupo);
                Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
            }
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            IUserRepository target = new UserRepository();
            var fromDb = target.GetById(item.Id);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Login, fromDb.Login);
            Assert.AreEqual(item.Senha, fromDb.Senha);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.Email, fromDb.Email);
            Assert.AreEqual(item.Situacao, fromDb.Situacao);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
        }

        /// <summary>
        ///A test for GetByLogin
        ///</summary>
        [TestMethod()]
        public void GetByLoginTest()
        {
            IUserRepository target = new UserRepository();
            var fromDb = target.GetByLogin(item.Login);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Login, fromDb.Login);
            Assert.AreEqual(item.Senha, fromDb.Senha);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.Email, fromDb.Email);
            Assert.AreEqual(item.Situacao, fromDb.Situacao);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            IUserRepository target = new UserRepository();
            var fromDb = target.GetByName(item.Nome);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(item, fromDb);
            Assert.AreEqual(item.Login, fromDb.Login);
            Assert.AreEqual(item.Senha, fromDb.Senha);
            Assert.AreEqual(item.Nome, fromDb.Nome);
            Assert.AreEqual(item.Email, fromDb.Email);
            Assert.AreEqual(item.Situacao, fromDb.Situacao);
            Assert.IsNotNull(fromDb.Grupo);
            Assert.AreEqual(item.Grupo.Id, fromDb.Grupo.Id);
        }

    }
}
