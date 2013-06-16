using System;
using System.Collections.Generic;
using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using IlinkDb.Entity;
using IlinkDb.Business;

namespace IlinkDb.Business.Test
{
    [TestClass]
    public class ProjectTests
    {
        private TestContext testContextInstance;
        ProjectManager _projectManager;

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

        [TestInitialize()]
        public void TestInitialize()
        {
            _projectManager = new ProjectManager(true);
        }

        [TestMethod]
        public void ProjectListCountGreaterThanZeroTest()
        {
            List<Project> list = _projectManager.List();
            Assert.IsTrue(list.Count > 0, "List method failed to return any items");
        }

        [TestMethod]
        public void ConvertPivotDateTimeTest()
        {
            string dateText = "2013/05/20 01:55:10 UTC";

            DateTimeFormatInfo dt = new DateTimeFormatInfo();

            DateTime work;
            DateTimeStyles style = DateTimeStyles.AdjustToUniversal;
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");

            bool success = DateTime.TryParse(dateText, cultureInfo, style, out work);
            Assert.IsTrue(success, "Failed to convert date");
        }
    }
}
