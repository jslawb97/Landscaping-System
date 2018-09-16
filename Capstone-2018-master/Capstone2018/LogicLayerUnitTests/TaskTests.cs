using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using DataObjects;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class TaskTests
    {
        private ITaskManager _taskManager;

        public TaskTests()
        {
            this._taskManager = new TaskManager(new TaskAccessorMock());
        }

        [TestInitialize]
        public void TestSetup()
        {
            _taskManager = new TaskManager(new TaskAccessorMock());
        }

        [TestMethod]
        public void TestRetrieveTaskList()
        {
            Assert.AreEqual(3, this._taskManager.RetrieveTaskList().Count());
        }
    }
}
