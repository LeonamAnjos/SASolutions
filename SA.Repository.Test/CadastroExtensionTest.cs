using SA.Repository.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Domain;
using System.Collections.Generic;

namespace SA.Repository.Test
{
    
    
    /// <summary>
    ///This is a test class for CadastroExtensionTest and is intended
    ///to contain all CadastroExtensionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CadastroExtensionTest
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
        ///A test for EhCliente
        ///</summary>
        [TestMethod()]
        public void EhClienteTest()
        {
            Cadastro cd = new Cadastro();
            Assert.IsFalse(cd.EhCliente());

            cd.ContasFinanceiras.Add(new ContaFinanceira() { Situacao = Enums.ActiveInactiveType.Active, Tipo = Enums.FinancialAccountType.Costumer });
            cd.ContasFinanceiras.Add(new ContaFinanceira() { Situacao = Enums.ActiveInactiveType.Active, Tipo = Enums.FinancialAccountType.Supplier });

            Assert.IsTrue(cd.EhCliente());

            foreach (var c in cd.ContasFinanceiras)
                c.Situacao = Enums.ActiveInactiveType.Inactive;

            Assert.IsFalse(cd.EhCliente());
        }

        /// <summary>
        ///A test for EhFornecedor
        ///</summary>
        [TestMethod()]
        public void EhFornecedorTest()
        {
            Cadastro cd = new Cadastro();
            Assert.IsFalse(cd.EhFornecedor());

            cd.ContasFinanceiras.Add(new ContaFinanceira() { Situacao = Enums.ActiveInactiveType.Active, Tipo = Enums.FinancialAccountType.Costumer });
            cd.ContasFinanceiras.Add(new ContaFinanceira() { Situacao = Enums.ActiveInactiveType.Active, Tipo = Enums.FinancialAccountType.Supplier });

            Assert.IsTrue(cd.EhFornecedor());

            foreach (var c in cd.ContasFinanceiras)
                c.Situacao = Enums.ActiveInactiveType.Inactive;

            Assert.IsFalse(cd.EhFornecedor());
        }
    }
}
