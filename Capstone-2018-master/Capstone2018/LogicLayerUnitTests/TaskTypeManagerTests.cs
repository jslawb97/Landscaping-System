using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerUnitTests;
using DataAccessMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class TaskTypeManagerTests
    {
        private ITaskTypeManager _taskTypeManager;
        [TestInitialize]
        public void TestSetup()
        {
            _taskTypeManager = new TaskTypeManager(new TaskTypeAccessorMock());
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing CreateTaskType method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestCreateTaskType()
        {
            var taskType = new TaskType
            {
                TaskTypeID = 1000000,
                Name = "Mow",
                Quantity = 1,
                JobLocationAttributeTypeID = "Acres to Mow",
                Active = true
            };

            bool result = false;

            // act
           result = _taskTypeManager.CreateTaskType(taskType); 
            
            // assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing DeactivateTaskTypeByID method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestDeactivateTaskType()
        {
            bool result;
            int taskTypeID = Constants.IDSTARTVALUE;
            TaskType taskType = new TaskType();
            taskType.TaskTypeID = taskTypeID;

            // act
            result = _taskTypeManager.DeactivateTaskType(taskType);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing EditTaskTypeByID method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestEditTaskTypeByID()
        {
            // arrange
            var oldTaskType = new TaskType
            {
                TaskTypeID = 1000000,
                Name = "Mow",
                Quantity = 1,
                JobLocationAttributeTypeID = "Acres to Mow",
                Active = true
            };

            var newTaskType = new TaskType
            {
                TaskTypeID = 1000001,
                Name = "Trim Trees",
                Quantity = 6,
                JobLocationAttributeTypeID = "Trees to Trim",
                Active = false
            };

            bool result = false;

            // act
            try
            {
                result = _taskTypeManager.EditTaskTypeByID(oldTaskType, newTaskType);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing RetrieveTaskTypeByID method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestRetrieveTaskTypeByID()
        {
            // arrange
            TaskType taskType = null;
            int id = Constants.IDSTARTVALUE;

            // act
            try
            {
                taskType = _taskTypeManager.RetrieveTaskTypeByID(id);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(taskType);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing RetrieveTaskTypeByName method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestRetrieveTaskTypeByName()
        {
            // arrange
            TaskType taskType = null;

            string name = "Trim Trees";

            // act
            try
            {
                taskType = _taskTypeManager.RetrieveTaskTypeByName(name);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(taskType);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing RetrieveTaskTypeList method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestRetrieveTaskTypeList()
        {
            // arrange
            List<TaskType> taskTypes = null;

            // act
            try
            {
                taskTypes = _taskTypeManager.RetrieveTaskTypeList();
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(taskTypes);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Testing RetrieveTaskTypeListByActive method
        /// </summary>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        [TestMethod]
        public void TestRetrieveTaskTypeListByActive()
        {
            // arrange
            List<TaskType> taskTypes = null;

            // act
            try
            {
                taskTypes = _taskTypeManager.RetrieveTaskTypeListByActive();
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(taskTypes);
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created 4/27/2018
        /// 
        /// End the tests by making the manager null
        /// </summary>
        /// <remarks>QA Added this method because it was missing Jayden Tollefson 4/27/2018</remarks>
        [TestCleanup]
        public void TestTearDown()
        {
            this._taskTypeManager = null;
        }
    }
}
