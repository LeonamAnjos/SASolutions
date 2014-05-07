using SA.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Enums;

namespace SA.Infrastructure.Test
{
    
    
    /// <summary>
    ///This is a test class for EnumGetDescriptionTest and is intended
    ///to contain all EnumGetDescriptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EnumGetDescriptionTest
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
        ///A test for GetDescription
        ///</summary>
        [TestMethod()]
        public void GetDescriptionTest()
        {
            string s = ActiveInactiveType.Active.GetDescription();
            Assert.IsTrue(s.Equals("Ativo"));

            s = ActiveInactiveType.Inactive.GetDescription();
            Assert.IsTrue(s.Equals("Inativo"));

            s = CrudType.Create.GetDescription();
            Assert.IsTrue(s.Equals("Inclusão"));

            s = CrudType.Read.GetDescription();
            Assert.IsTrue(s.Equals("Consulta"));

            s = CrudType.Update.GetDescription();
            Assert.IsTrue(s.Equals("Alteração"));

            s = CrudType.Delete.GetDescription();
            Assert.IsTrue(s.Equals("Exclusão"));
        }
    }
}
