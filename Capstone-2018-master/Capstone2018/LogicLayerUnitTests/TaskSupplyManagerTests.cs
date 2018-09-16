using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using Logic;
using DataAccessMocks;

namespace LogicLayerUnitTests
{

    /// <summary>
    /// Mike Mason
    /// Created on 2018/04/05
    /// 
    /// Test class for the TaskSupplyManager class
    /// </summary>
    [TestClass]
    public class TaskSupplyManagerTests
    {
        private ITaskSupplyManager _taskSupplyManager;

        [TestInitialize]
        public void TestSetup()
        {
            _taskSupplyManager = new TaskSupplyManager(new TaskSupplyAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/07
        /// 
        /// Method to verify that TaskEditSupplyQuantity changes a TaskSupply's quantity
        /// </summary>
        [TestMethod]
        public void TestEditTaskSupplyQuantityGood()
        {
            // Arrange
            var oldTaskSupply = new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000003,
                TaskSupplyQuantity = 0,
            };
            var newTaskSupply = new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000003,
                TaskSupplyQuantity = 5,
            };
            var result = false;

            // Act
            result = _taskSupplyManager.EditTaskSupplyQuantity(oldTaskSupply, newTaskSupply);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/07
        /// 
        /// Method to verify that TaskEditSupplyQuantity throws an exception when
        /// given bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTaskSupplyQuantityBadData()
        {
            // Arrange
            var oldTaskSupply = new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000000,
                TaskSupplyQuantity = 0,
            };
            var newTaskSupply = new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000001,
                TaskSupplyQuantity = 5,
            };

            // Act
            _taskSupplyManager.EditTaskSupplyQuantity(oldTaskSupply, newTaskSupply);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/07
        /// 
        /// Method to verify that TaskEditSupplyQuantity throws access exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditTaskSupplyQuantityAccessException()
        {
            // Arrange
            var oldTaskSupply = new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000000,
                TaskSupplyQuantity = 9999999,
            };
            var newTaskSupply = new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000000,
                TaskSupplyQuantity = 5,
            };

            // Act
            _taskSupplyManager.EditTaskSupplyQuantity(oldTaskSupply, newTaskSupply);

            // Assert
            Assert.Fail();
        }
		
        /// Mike Mason
        /// Created on 2018/04/05
        /// 
        /// Testing RetrieveTaskSupplyDetailList to make sure it doesnt return null
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskSupplyDetailList()
        {
            // arrange
            List<TaskSupplyDetail> details = null;
            int jobID = Constants.IDSTARTVALUE;

            // act
            try
            {
                details = _taskSupplyManager.RetrieveTaskSupplyDetailList(jobID);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                
            }

            // assert
            Assert.IsNotNull(details);
        }
    }
}