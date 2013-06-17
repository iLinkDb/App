using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IlinkDb.Entity;
using IlinkDb.Business;

namespace IlinkDb.Business.Test
{
    /// <summary>
    /// Summary description for TaskTests
    /// </summary>
    [TestClass]
    public class TaskTests
    {
        TaskManager _taskManager;
        public TaskTests()
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
            _taskManager= new TaskManager(true);
        }        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddTaskTest()
        {
            long projectId = 830205;

            StoryManager storyManager = new StoryManager(true);
            List<Story> storyList = storyManager.List(projectId);

            Task task = new Task();
            task.Description = "First task from API";
            Task newTask = _taskManager.Add(storyList[1], task);
            Assert.IsNotNull(newTask, "newTask should not have been null");
            Assert.IsTrue(newTask.Id > 0, "newTask ID should have been > 0");
        }

    }
}
