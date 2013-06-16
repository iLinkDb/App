using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IlinkDb.Entity;
using IlinkDb.Business;

namespace IlinkDb.Business.Test
{
    /// <summary>
    /// Summary description for TenantTests
    /// </summary>
    [TestClass]
    public class TenantTests
    {
        private TestContext testContextInstance;
        TenantManager _tenantManager;

        public TenantTests()
        {
            //
        }


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
        // You can use the following additional attributes as you write your tests:
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //

        [TestInitialize()]
        public void TestInitialize()
        {
            _tenantManager = new TenantManager(true);
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddTenantTest()
        {
            Tenant tenant = new Tenant();
            tenant.Domain = "testdomain";

            Tenant saveOne = _tenantManager.Save(tenant);

            Assert.IsNotNull(saveOne, "Add failed to return a new object");
            Assert.IsTrue(saveOne.Id > 0, "New object Id was not > 0");
        }

        [TestMethod]
        public void ListCountGreaterThanZeroTest()
        {
            List<Tenant> list = _tenantManager.List();

            System.Diagnostics.Debug.Print("---------");
            foreach (Tenant tenant in list)
            {
                System.Diagnostics.Debug.Print("One: " + tenant.Id + " " + tenant.Domain);
            }
            Assert.IsTrue(list.Count > 0);
        }
    }
}
