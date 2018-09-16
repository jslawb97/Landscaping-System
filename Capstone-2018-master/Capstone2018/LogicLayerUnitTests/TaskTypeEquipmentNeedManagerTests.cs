using DataAccessMocks;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class TaskTypeEquipmentNeedManagerTests
    {
        private ITaskTypeEquipmentNeedManager _taskTypeEquipmentNeedManager;

        [TestInitialize]
        public void TestSetup()
        {
            _taskTypeEquipmentNeedManager = new TaskTypeEquipmentNeedManager(new TaskTypeEquipmentNeedAccessorMock(), new TaskTypeAccessorMock(), new EquipmentTypeAccessorMock());
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/29
        /// 
        /// Method to verify that the record is created
        /// </summary>
        [TestMethod]
        public void TestCreateTaskTypeEquipmentNeedGood()
        {
            TaskTypeEquipmentNeed taskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = 1000002,
                EquipmentTypeID = "equipTyp4",
                HoursOfWork = 2
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);

            // assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Method to verify that TaskTypeEquipmentNeed won't be created with a null EquipmentType
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateTaskTypeEquipmentNeedNullEquipmentType()
        {
            TaskTypeEquipmentNeed taskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = 1000002,
                EquipmentTypeID = null,
                HoursOfWork = 2
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Method to verify that TaskTypeEquipmentNeed won't be created with an empty EquipmentType
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateTaskTypeEquipmentNeedEmptyEquipmentType()
        {
            TaskTypeEquipmentNeed taskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = 1000002,
                EquipmentTypeID = "",
                HoursOfWork = 2
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Method to verify that TaskTypeEquipmentNeed won't be created with a bad tasktype id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateTaskTypeEquipmentNeedBadTaskType()
        {
            TaskTypeEquipmentNeed taskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = Constants.IDSTARTVALUE - 1,
                EquipmentTypeID = "Test",
                HoursOfWork = 2
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Method to verify that TaskTypeEquipmentNeed won't be created with negative hours
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateTaskTypeEquipmentNeedNegativeHours()
        {
            TaskTypeEquipmentNeed taskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = Constants.IDSTARTVALUE,
                EquipmentTypeID = "Test",
                HoursOfWork = -2
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/29
        /// 
        /// Method to verify that the record is edited
        /// </summary>
        [TestMethod]
        public void TestEditTaskTypeEquipmentNeedGood()
        {
            // arange
            TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = 1000002,
                EquipmentTypeID = "equipTyp4",
                HoursOfWork = 2
            };
            TaskTypeEquipmentNeed newTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000002,
                TaskTypeID = 1000004,
                EquipmentTypeID = "equipType3",
                HoursOfWork = 4
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.EditTaskTypeEquipmentNeed(oldTaskTypeEquipmentNeed, newTaskTypeEquipmentNeed);

            // assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Method to verify that a TaskTypeEquipmentNeed won't be edited with a bad ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskTypeEquipmentNeedBadID()
        {
            // arange
            TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE,
                TaskTypeID = 1000002,
                EquipmentTypeID = "equipTyp4",
                HoursOfWork = 2
            };
            TaskTypeEquipmentNeed newTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE - 1,
                TaskTypeID = 1000004,
                EquipmentTypeID = "equipType3",
                HoursOfWork = 4
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.EditTaskTypeEquipmentNeed(oldTaskTypeEquipmentNeed, newTaskTypeEquipmentNeed);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Method to verify that a TaskTypeEquipmentNeed won't be edited with a bad ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskTypeEquipmentNeedBadTaskType()
        {
            // arange
            TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE,
                TaskTypeID = 1000002,
                EquipmentTypeID = "equipTyp4",
                HoursOfWork = 2
            };
            TaskTypeEquipmentNeed newTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE,
                TaskTypeID = Constants.IDSTARTVALUE - 1,
                EquipmentTypeID = "equipType3",
                HoursOfWork = 4
            };

            // act
            int rowsAffected = _taskTypeEquipmentNeedManager.EditTaskTypeEquipmentNeed(oldTaskTypeEquipmentNeed, newTaskTypeEquipmentNeed);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/29
        /// 
        /// Method to verify that the record is retrieved its ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskTypeEquipmentNeedDetail()
        {
            TaskTypeEquipmentNeed t = new TaskTypeEquipmentNeed()
            {
                TaskTypeEquipmentNeedID = 1000000,
                TaskTypeID = 1000001,
                EquipmentTypeID = "type",
                HoursOfWork = 3

            };
            var c = this._taskTypeEquipmentNeedManager.RetrieveTaskTypeEquipmentNeedDetail(t);
            Assert.AreEqual(1000000, c.TaskTypeEquipmentNeed.TaskTypeEquipmentNeedID);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/29
        /// 
        /// Method to verify that the list of records are retrieved
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskTypeEquipmentNeedList()
        {
            // arrange
            List<TaskTypeEquipmentNeed> taskTypeEquipmentNeedList;

            // act
            taskTypeEquipmentNeedList = _taskTypeEquipmentNeedManager.RetrieveTaskTypeEquipmentNeedList();

            // assert
            Assert.AreEqual(2, taskTypeEquipmentNeedList.Count);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Tests that a TaskTypeEquipmentNeed can be deleted
        /// </summary>
        [TestMethod]
        public void TestDeleteTaskTypeEquipmentNeed()
        {
            // act
            int result = 0;

            int taskTypeEquipmentNeedID = 1000009;

            // arrange
            result = _taskTypeEquipmentNeedManager.DeleteTaskTypeEquipmentNeedItem(taskTypeEquipmentNeedID);

            // assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Tests that a TaskTypeEquipmentNeed won't be deleted with a bad ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeleteTaskTypeEquipmentNeedBadID()
        {
            // act
            int taskTypeEquipmentNeedID = 1;

            // arrange
            _taskTypeEquipmentNeedManager.DeleteTaskTypeEquipmentNeedItem(taskTypeEquipmentNeedID);

            // assert
            Assert.Fail();
        }
    }
}
