﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IlinkDb.Entity;
using IlinkDb.Business;

namespace IlinkDb.Business.Test
{
    /// <summary>
    /// Summary description for StoryTests
    /// </summary>
    [TestClass]
    public class StoryTests
    {
        private TestContext testContextInstance;
        StoryManager _storyManager;

        public StoryTests()
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
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize()]
        public void TestInitialize()
        {
            _storyManager = new StoryManager(true);
        }

        [TestMethod]
        public void StoryListCountGreaterThanZeroTest()
        {
            long projectId = 830205;
            List<Story> list = _storyManager.List(projectId);
            Assert.IsTrue(list.Count > 0, "List method failed to return any items");
        }

        [TestMethod]
        public void AddNoteTest()
        {
            long projectId = 830205;
            Note note = new Note();
            note.User = "ilinkdb";
            note.Text = "Note added from API";
            Note newNote = _storyManager.AddNote(projectId, note);
            Assert.IsNotNull(newNote, "newNote should not have been null");
            Assert.IsTrue(newNote.Id > 0, "newNote ID should have been > 0");
        }

    }
}
