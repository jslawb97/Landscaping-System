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
    /// <summary>
    /// Brady Feller
    /// Created on 2018/04/05
    /// 
    /// Test class for the TaskEquipmentManager class
    /// </summary>
    [TestClass]
    public class TaskEquipmentManagerTests
    {
        private ITaskEquipmentManager _taskEquipmentManager;

        [TestInitialize]
        public void TestSetup()
        {
            _taskEquipmentManager = new TaskEquipmentManager(new TaskEquipmentAccessorMock());
        }

        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Testing RetrieveTaskEmployeeDetailByJobID to make sure it doesnt return null
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskEmployeeDetailByJobID()
        {
            // arrange
            List<TaskEquipmentDetail> details = null;
            int jobID = Constants.IDSTARTVALUE;

            // act
            try
            {
                details = _taskEquipmentManager.RetrieveTaskEquipmentDetailByJobID(jobID);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }

            // assert
            Assert.IsNotNull(details);
        }

        [TestMethod]
        public void TestRetrieveAssignedEquipmentByTaskID()
        {
            // arrange
            List<Equipment> equipmentList = new List<Equipment>();

            // act
            equipmentList = _taskEquipmentManager.RetrieveAssignedEquipmentByTaskID(1000000);

            // assert
            Assert.AreEqual(1, equipmentList.Count());
        }

        /// <summary>
        /// Noah Davison
        /// Created on 2018/04/19
        /// 
        /// Testing RetrieveAssignedEquipmentByTaskIDAndJobID to make sure it returns a list of equipment
        /// </summary>
        [TestMethod]
        public void TestRetrieveAssignedEquipmentByTaskIDAndJobID()
        {
            // arrange
            List<Equipment> equipmentList = new List<Equipment>();
            int taskID = 1000000;
            int jobID = 1000000;

            // act
            equipmentList = _taskEquipmentManager.RetrieveAssignedEquipmentByTaskIDAndJobID(taskID, jobID);

            // assert
            Assert.AreEqual(1, equipmentList.Count());
        }

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Testing method for adding equipment.
        /// </summary>
        [TestMethod]
        public void TestAddEquipmentByID()
        {
            //arrange
            int equipmentID = Constants.IDSTARTVALUE;
            int jobID = Constants.IDSTARTVALUE;
            int taskTypeEquipmentNeedID = Constants.IDSTARTVALUE;

            //act
            bool result = _taskEquipmentManager.AddEquipmentToTaskEquipment(equipmentID, jobID, taskTypeEquipmentNeedID);

            //assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Noah Davison
        /// 2018/04/13
        /// 
        /// Test method for deleting all assigned equipment from a task
        /// </summary>
        /// <param name="taskID"></param>
        [TestMethod]
        public void DeleteAssignedEquipmentByTaskID()
        {
            //arrange
            int taskID = 1000000;
            int jobID = 1000000;
            int rowsDeleted = 0;

            //act
            rowsDeleted = _taskEquipmentManager.DeleteAssignedEquipmentByTaskIDAndJobID(taskID, jobID);

            //assert
            Assert.AreEqual(1, rowsDeleted);
        }

        /// <summary>
        /// Noah Davison
        /// 2018/04/13
        /// 
        /// Test method for deleting all assigned equipment from a task, if no equipment is assigned
        /// </summary>
        /// <param name="taskID"></param>
        [TestMethod]
        public void DeleteAssignedEquipmentByTaskIDNoAssignedEquipment()
        {
            //arrange
            int taskID = 10000;
            int jobID = 1000000;
            int rowsDeleted = 0;

            try
            {
                //act 
                rowsDeleted = _taskEquipmentManager.DeleteAssignedEquipmentByTaskIDAndJobID(taskID, jobID);

                //assert
                Assert.Fail("Method did not throw an exception");
            }
            catch (Exception ex)
            {
                //assert
                Assert.AreEqual("There was no equipment assigned to that task to delete", ex.Message);
            }

        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/14
        /// 
        /// Test method for removing equipment with valid TaskEquipmentID
        /// </summary>
        [TestMethod]
        public void TestDeleteEquipmentFromTaskEquipmentSuccess()
        {
            // Arrange
            int jobID = Constants.IDSTARTVALUE;
            int equipmentID = Constants.IDSTARTVALUE;
            int expectedResult = 1;

            // Act
            int result = _taskEquipmentManager.DeleteEquipmentFromTaskEquipment(jobID, equipmentID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/14
        /// 
        /// Test method for removing equipment with invalid JobID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteEquipmentFromTaskEquipmentInvalidJobID()
        {
            // Arrange
            int jobID = Constants.IDSTARTVALUE - 5;
            int equipmentID = Constants.IDSTARTVALUE;

            // Act
            int result = _taskEquipmentManager.DeleteEquipmentFromTaskEquipment(jobID, equipmentID);
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/14
        /// 
        /// Test method for removing equipment with invalid EquipmentID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteEquipmentFromTaskEquipmentInvalidEquipmentID()
        {
            // Arrange
            int jobID = Constants.IDSTARTVALUE;
            int equipmentID = Constants.IDSTARTVALUE - 5;

            // Act
            int result = _taskEquipmentManager.DeleteEquipmentFromTaskEquipment(jobID, equipmentID);
        }
    }
}
