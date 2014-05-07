using SA.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for VendorRepositoryTest and is intended
    ///to contain all VendorRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VendorRepositoryTest
    {

        private static IList<Vendedor> good = new List<Vendedor>();
        private static IList<Vendedor> bad = new List<Vendedor>();
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

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            good.Add(new Vendedor() { Nome = "Fulano" });
            good.Add(new Vendedor() { Nome = "1122334455" });
            good.Add(new Vendedor() { Nome = "  Fulano Teste  " });
            good.Add(new Vendedor() { Nome = "-" });
            good.Add(new Vendedor() { Nome = "1234567890123456789012345678901234567890" }); // 40

            bad.Add(new Vendedor() { Nome = "" });
            bad.Add(new Vendedor() { Nome = "    " });
            bad.Add(new Vendedor() { Nome = null });
            bad.Add(new Vendedor() { Nome = "12345678901234567890123456789012345678901" }); // 41
        }
        
        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        
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
        ///A test for Creat, Read, Update and Delete
        ///</summary>
        [TestMethod()]
        public void CrudTest()
        {
            var target = new VendorRepository();
            foreach (var v in good)
            {
                target.Add(v); // create
                Assert.IsTrue(v.Id > 0);

                v.Nome = "Updated";
                target.Update(v); // update

                target.Remove(v); // remove
            }

            foreach (var v in bad)
            {
                try
                {
                    target.Add(v); // create
                    target.Remove(v); // remove
                    Assert.Fail("Should had thrown a validation exception!");
                }
                catch (Exception e)
                {
                    Assert.IsInstanceOfType(e, typeof(ValidationException));
                }
            }

        }

        /// <summary>
        ///A test for Validation
        ///</summary>
        [TestMethod()]
        public void ValidationTest()
        {
            foreach (var v in good)
                Assert.IsTrue(v.Validar().IsValid);

            foreach (var v in bad)
                Assert.IsFalse(v.Validar().IsValid);
        }
    }
}
