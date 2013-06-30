using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IlinkDb.Entity;
using IlinkDb.Business;

namespace IlinkDb.Business.Test
{
    /// <summary>
    /// Summary description for NoteTests
    /// </summary>
    [TestClass]
    public class NoteTests
    {
        NoteManager _noteManager;

        public NoteTests()
        {
            //
        }

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
        [TestInitialize()]
        public void TestInitialize()
        {
            _noteManager = new NoteManager(true);
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //[TestMethod]
        //public void AddNoteTest()
        //{
        //    Note note = new Note { NotePath = "/fred", Status = NoteStatusEnum.Open, Text = "AddNoteTest" };

        //    Note saveOne = _noteManager.Save(note);

        //    Assert.IsNotNull(saveOne, "Add failed to return a new object");
        //    Assert.IsTrue(saveOne.Id > 0, "New object Id was not > 0");
        //}

        [TestMethod]
        public void AddNoteTest()
        {
            StoryManager storyManager = new StoryManager(true);
            List<Story> storyList = storyManager.List();

            Note note = new Note();
            // note.User = "ilinkdb";
            note.Text = "from API test @ " + DateTime.Now;
            Note newNote = _noteManager.Save(storyList[1], note);
            Assert.IsNotNull(newNote, "newNote should not have been null");
            Assert.IsTrue(newNote.Id > 0, "newNote ID should have been > 0");
        }

    }
}
