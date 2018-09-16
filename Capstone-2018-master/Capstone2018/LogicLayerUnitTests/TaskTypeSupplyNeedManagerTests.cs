using DataAccessMocks;
using DataObjects;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class TaskTypeSupplyNeedManagerTests
    {
        private TaskTypeSupplyNeedManager _taskSupplyManager;
        
        [TestInitialize]
        public void TestSetup()
        {
            _taskSupplyManager = new TaskTypeSupplyNeedManager(new TaskTypeSupplyNeedAccessorMock());
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if Task Type Supply Need item is created with a valid supply item id and task id
        /// </summary>
        [TestMethod]
        public void TestCreateTaskTypeSupplyNeedItemSuccess()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                SupplyItemID = 1000005,
                TaskTypeID = 1000005,
                Quantity = 29
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.AddTaskTypeSupplyNeedItem(taskSupply);

            // Assert
            Assert.AreEqual(1, result);
        }
        
        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if invalid Task ID is given
        /// when creating a Task Type Supply Need item
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateTaskTypeSupplyNeedItemInvalidTaskID()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                SupplyItemID = 1000005,
                TaskTypeID = 1000,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.AddTaskTypeSupplyNeedItem(taskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if invalid Supply Item ID is given
        /// when creating a Task Supply item
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateTaskTypeSupplyNeedItemInvalidSupplyItemID()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                SupplyItemID = 1000,
                TaskTypeID = 1000005,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.AddTaskTypeSupplyNeedItem(taskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Test Method to retrieve task supply items
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskTypeSupplyNeedItemsSuccess()
        {
            //Arrange
            List<TaskTypeSupplyNeed> supplyOrderItems;

            //Act
            supplyOrderItems = _taskSupplyManager.RetrieveTaskTypeSupplyNeedList();

            //Assert
            Assert.AreEqual(2, supplyOrderItems.Count);
        }
        
        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if a task supply item is updated if given
        /// valid task supply items
        /// </summary>
        [TestMethod]
        public void TestEditTaskTypeSupplyNeedItemSuccess()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000000,
                Quantity = 20
            };
            TaskTypeSupplyNeed newtaskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000000,
                Quantity = 203
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.EditTaskTypeSupplyNeedItem(taskSupply, newtaskSupply);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if an invalid Task ID is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskTypeSupplyNeedInvalidTaskID()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000,
                SupplyItemID = 1000000,
                Quantity = 20
            };
            TaskTypeSupplyNeed newtaskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000,
                SupplyItemID = 1000000,
                Quantity = 203
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.EditTaskTypeSupplyNeedItem(taskSupply, newtaskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if an invalid Supply Item ID is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskTypeSupplyNeedInvalidSupplyItemID()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000,
                Quantity = 20
            };
            TaskTypeSupplyNeed newtaskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000,
                Quantity = 203
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.EditTaskTypeSupplyNeedItem(taskSupply, newtaskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if an mismatched Task IDs are given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskTypeSupplyNeedTaskIDMismatch()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000000,
                Quantity = 20
            };
            TaskTypeSupplyNeed newtaskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000005,
                SupplyItemID = 1000000,
                Quantity = 203
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.EditTaskTypeSupplyNeedItem(taskSupply, newtaskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if an mismatched Supply Item IDs are given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskTypeSupplyNeedSupplyItemIDMismatch()
        {
            // Arrange
            TaskTypeSupplyNeed taskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000000,
                Quantity = 20
            };
            TaskTypeSupplyNeed newtaskSupply = new TaskTypeSupplyNeed()
            {
                TaskTypeID = 1000000,
                SupplyItemID = 1000005,
                Quantity = 203
            };
            int result = 0;

            // Act
            result = _taskSupplyManager.EditTaskTypeSupplyNeedItem(taskSupply, newtaskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if a Task Supply Item is deactivated when 
        /// given a valid taskTypeSupplyNeedID
        /// </summary>
        [TestMethod]
        public void TestDeactivateTaskTypeSupplyNeedSuccess()
        {
            // Arrange
            int taskTypeSupplyNeedID = Constants.IDSTARTVALUE;
            int result = 0;

            // Act
            result = _taskSupplyManager.DeactivateTaskTypeSupplyNeedItem(taskTypeSupplyNeedID);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to test if exception is thrown if an invalid taskTypeSupplyNeedID is given
        /// to deactivate a task supply
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeactivateTaskTypeSupplyNeedItemInvalidTaskTypeSupplyNeedID()
        {
            // Arrange
            int taskTypeSupplyNeedID = Constants.IDSTARTVALUE - 1;
            int result = 0;

            // Act
            result = _taskSupplyManager.DeactivateTaskTypeSupplyNeedItem(taskTypeSupplyNeedID);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _taskSupplyManager = null;
        }

    }
}
