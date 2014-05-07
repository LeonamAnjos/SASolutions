using SA.Address.Model.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using SA.Address.Model;
using System.Transactions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;

namespace SA.Address.Test
{
    
    
    /// <summary>
    ///This is a test class for AddressModelServiceTest and is intended
    ///to contain all AddressModelServiceTest Unit Tests
    ///</summary>
    [TestClass(), System.Runtime.InteropServices.GuidAttribute("C39B9FB1-8E83-4027-A1A7-BB9B7532AC50")]
    public class AddressModelServiceTest
    {

        private static IUnityContainer container;
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
            container = new UnityContainer();
            //container.RegisterType(typeof(AddressEntities), new TransientLifetimeManager());
            //container.RegisterInstance(new AddressEntities(), new TransientLifetimeManager());
            container.RegisterType<IAddressModelService, AddressModelService>();
            container.RegisterType<IEventAggregator, EventAggregator>();
        }
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

        private void AddCountry(IAddressModelService service, int i)
        {
            Pais p = service.CreateObject<Pais>();
            p.Nome = "Teste " + i.ToString();
            p.Sigla = "T" + i.ToString();
            service.AddCountry(p);
            service.Save();
        }

        /// <summary>
        ///A test for AddCountry
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SA.Address.dll")]
        public void AddCountryTest()
        {
            IAddressModelService target = container.Resolve<IAddressModelService>();

            using(var ts = new TransactionScope()) 
            {
                for(int i = 1; i < 10; i++) 
                {
                    this.AddCountry(target, i);
                }    
            }
            Assert.IsTrue(true);
        }

        private Estado AddCity(IAddressModelService service, int i, Estado e)
        {
            Cidade p = service.CreateObject<Cidade>();
            p.Nome = "Teste " + i.ToString();
            p.DDD = "00";

            //var aux = container.Resolve<IAddressModelService>();


            //var est = aux.Context.Estados.Single<Estado>(g => g.ID == 1);
            //aux.Context.Detach(est);
            var est = service.Context.Estados.Single<Estado>(g => g.ID == 1);

            // var r = est.Equals(e);

            //service.Context.Estados.Detach(est);
            //est.StartTracking();

            p.Estado = est;

            //if (e == null) {
            //    service.Context.Estados.Attach(est);
            //    p.Estado = est;
            //}
            //else
            //    p.Estado = e;
            
            service.AddCity(p);
            service.Save();

            return est;
        }

        /// <summary>
        ///A test for AddCity
        ///</summary>
        [TestMethod()]
        public void AddCityTest()
        {
            IAddressModelService target = container.Resolve<IAddressModelService>();
            Estado e = null;
            using (var ts = new TransactionScope())
            {
                for (int i = 1; i < 10; i++)
                {
                    
                    e = this.AddCity(target, i, e);
                    //target.Save();
                }
                
            }
            
            Assert.IsTrue(true);
        }
    }
}
