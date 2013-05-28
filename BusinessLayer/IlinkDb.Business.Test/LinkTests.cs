using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using IlinkDb.Entity;
using IlinkDb.Business;

namespace IlinkDb.Business.Test
{
    [TestClass]
    public class LinkTests
    {
        LinkManager _linkManager;

        [TestInitialize()]
        public void TestInitialize()
        {
            _linkManager = new LinkManager(true);
        }

        [TestMethod]
        public void ListLinkCountTest()
        {
            List<Link> list = _linkManager.List();

            System.Diagnostics.Debug.Print("---------");
            foreach (Link link in list)
            {
                System.Diagnostics.Debug.Print("One: " + link.Id + " " + link.Url);
            }
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void AddLinkTest()
        {
            Link one = new Link { Url = "wwww.google.com" };

            List<Link> list = _linkManager.List();
            int priorCount = list.Count;

            foreach (Link link in list)
            {
                System.Diagnostics.Debug.Print("Two: " + link.Id + " " + link.Url);
            }

            Link newOne = _linkManager.Save(one);

            Assert.IsTrue(newOne.Id > 0, "Id on new object was not set");
            Assert.IsTrue(_linkManager.List().Count == priorCount + 1, "Failed to add new object to collection");
        }

    }
}
