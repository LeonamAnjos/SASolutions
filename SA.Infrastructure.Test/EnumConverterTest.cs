using SA.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.Repository.Enums;

namespace SA.Infrastructure.Test
{
    
    
    /// <summary>
    ///This is a test class for EnumConverterTest and is intended
    ///to contain all EnumConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EnumConverterTest
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
        public void FromIntTest()
        {
            ActiveInactiveType target = EnumConvert<ActiveInactiveType>.FromInt(Convert.ToInt32('A'));
            Assert.IsTrue(target == ActiveInactiveType.Active);

            target = EnumConvert<ActiveInactiveType>.FromInt(Convert.ToInt32('I'));
            Assert.IsTrue(target == ActiveInactiveType.Inactive);
        }

        [TestMethod()]
        public void FromCharTest()
        {
            ActiveInactiveType target = EnumConvert<ActiveInactiveType>.FromChar('A');
            Assert.IsTrue(target == ActiveInactiveType.Active);

            target = EnumConvert<ActiveInactiveType>.FromChar('I');
            Assert.IsTrue(target == ActiveInactiveType.Inactive);
        }

        [TestMethod()]
        public void ToEnumTest()
        {
            char target = 'A';
            ActiveInactiveType result = EnumConvert<ActiveInactiveType>.ToEnum(target);
            Assert.IsTrue(result == ActiveInactiveType.Active);

            result = EnumConvert<ActiveInactiveType>.ToEnum(Convert.ToInt32(target));
            Assert.IsTrue(result == ActiveInactiveType.Active);

            target = 'I';
            result = EnumConvert<ActiveInactiveType>.ToEnum(target);
            Assert.IsTrue(result == ActiveInactiveType.Inactive);

            result = EnumConvert<ActiveInactiveType>.ToEnum(Convert.ToInt32(target));
            Assert.IsTrue(result == ActiveInactiveType.Inactive);


        }
    }
}
