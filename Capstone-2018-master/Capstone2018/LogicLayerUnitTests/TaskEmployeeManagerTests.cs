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
    /// <summary>
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Test class for the TaskEmployeeManager class
    /// </summary>
    [TestClass]
    public class TaskEmployeeManagerTests
    {
        private ITaskEmployeeManager _taskEmployeeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _taskEmployeeManager = new TaskEmployeeManager(new TaskEmployeeAccessorMock());
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
            List<TaskEmployeeDetail> details = null;
            int jobID = Constants.IDSTARTVALUE;

            // act
            try
            {
                details = _taskEmployeeManager.RetrieveTaskEmployeeDetailByJobID(jobID);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                
            }

            // assert
            Assert.IsNotNull(details);
        }


        /// <summary>
        /// Badis Saidani
        /// Created on 2018/04/05
        /// 
        /// Removes a TaskEmployee records from the database
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [TestMethod]
        public void RemoveTaskEmployeeByTaskTypeEmployeeNeedId()
        {
            // arrange
            int result = 0;
            int taskID = Constants.IDSTARTVALUE;



            // act
            try
            {
                result = _taskEmployeeManager.RemoveTaskEmployeeByTaskTypeEmployeeNeedId(taskID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }



            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CreateEmployeeTaskAssignment()
        {
            // arrange
            bool result = false;
            int jobID = Constants.IDSTARTVALUE;
            int employeeID = Constants.IDSTARTVALUE;
            int taskTypeEmployeeNeedID = Constants.IDSTARTVALUE;



            // act
            try
            {
                result = _taskEmployeeManager.CreateEmployeeTaskAssignment(employeeID, jobID, taskTypeEmployeeNeedID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }



            // assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteEmployeeTaskAssignment()
        {
            // arrange
            bool result = false;
            int jobID = Constants.IDSTARTVALUE;
            int employeeID = Constants.IDSTARTVALUE;



            // act
            try
            {
                result = _taskEmployeeManager.DeleteEmployeeTaskAssignment(employeeID, jobID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }



            // assert
            Assert.AreEqual(true, result);
        }
    }
}
