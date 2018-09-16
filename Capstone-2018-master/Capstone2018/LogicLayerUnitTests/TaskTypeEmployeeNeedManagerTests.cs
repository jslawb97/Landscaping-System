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
    /// Created 2018/03/29
    /// 
    /// Test class for the TaskTypeEmployeeNeedManager class methods
    /// </summary>
    [TestClass]
    public class TaskTypeEmployeeNeedManagerTests
    {
        private ITaskTypeEmployeeNeedManager _taskTypeEmployeeNeedManager;

        [TestInitialize]
        public void TestStart()
        {
            _taskTypeEmployeeNeedManager = new TaskTypeEmployeeNeedManager(new TaskTypeEmployeeNeedAccessorMock());
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Testing the good path for deactivating a record
        /// </summary>
        [TestMethod]
        public void TestDeactivateTaskTypeEmployeeNeed()
        {
            // arrange
            int id = Constants.IDSTARTVALUE;

            //act
            try
            {
                int result = _taskTypeEmployeeNeedManager.DeactivateTaskTypeEmployeeNeedByID(id);
                //assert
                Assert.IsTrue(1 == result);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Testing where the given record has an invalid id
        /// </summary>
        [TestMethod]
        public void TestDeactivateTaskTypeEmployeeNeedBadID()
        {
            // arrange
            int id = 0;

            //act
            try
            {
                int result = _taskTypeEmployeeNeedManager.DeactivateTaskTypeEmployeeNeedByID(id);
                //assert
                Assert.Fail("Deactivate should have failed: Bad id");
            }
            catch (Exception ex)
            {

                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests getting the list of detail records
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskTypeEmployeeNeedDetailList()
        {
            // arrange
            List<TaskTypeEmployeeNeedDetail> detailList = null;


            // act 
            try
            {
                detailList = _taskTypeEmployeeNeedManager.RetrieveTaskTypeEmployeeDetailList();
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }


            // assert
            Assert.IsNotNull(detailList);
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests the good path for creating a record
        /// </summary>
        [TestMethod]
        public void TestCreateTaskTypeEmployeeNeed()
        {
            //arrange
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = Constants.IDSTARTVALUE + 100,
                HoursOfWork = 5,
                Active = true
            };
            int rowsAffected = 0;
            //act
            try
            {
                rowsAffected = _taskTypeEmployeeNeedManager.CreateTaskTypeEmployeeNeed(newTaskTypeEmployeeNeed);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
            
            //assert
            Assert.IsTrue(rowsAffected == 1);
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests where the given record has a bad id
        /// </summary>
        [TestMethod]
        public void TestCreateTaskTypeEmployeeNeedBadID()
        {
            //arrange
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = 0,
                HoursOfWork = 5,
                Active = true
            };
            //act
            try
            {
                _taskTypeEmployeeNeedManager.CreateTaskTypeEmployeeNeed(newTaskTypeEmployeeNeed);
                Assert.Fail("Bad ID should have thrown error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
                
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests creating a record that has an invalid HoursOfWork property
        /// </summary>
        [TestMethod]
        public void TestCreateTaskTypeEmployeeNeedBadNumber()
        {
            //arrange
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = Constants.IDSTARTVALUE + 1000,
                HoursOfWork = -100,
                Active = true
            };
            //act
            try
            {
                _taskTypeEmployeeNeedManager.CreateTaskTypeEmployeeNeed(newTaskTypeEmployeeNeed);
                Assert.Fail("Bad Hours Of Work should have thrown error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);

            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests the good path for updating an employee record
        /// </summary>
        [TestMethod]
        public void TestUpdateTaskTypeEmployeeNeed()
        {
            //arrange
            var oldTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = Constants.IDSTARTVALUE,
                HoursOfWork = 5,
                Active = true
            };
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = Constants.IDSTARTVALUE,
                HoursOfWork = 1,
                Active = false
            };

            int rowsAffected = 0;
            //act
            try
            {
                rowsAffected = _taskTypeEmployeeNeedManager.UpdateTaskTypeEmployeeNeed(oldTaskTypeEmployeeNeed, newTaskTypeEmployeeNeed);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsTrue(rowsAffected == 1);
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests updating a record where the oldRecord has a bad id
        /// </summary>
        [TestMethod]
        public void TestUpdateTaskTypeEmployeeNeedBadOldID()
        {
            //arrange
            var oldTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = 0,
                HoursOfWork = 5,
                Active = true
            };
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                HoursOfWork = 1,
                Active = false
            };
            
            //act
            try
            {
                _taskTypeEmployeeNeedManager.UpdateTaskTypeEmployeeNeed(oldTaskTypeEmployeeNeed, newTaskTypeEmployeeNeed);
                Assert.Fail("Bad ID should have thrown an error");
            }
            catch (Exception ex)
            {

                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests updating a record where the oldRecord has an invalid HoursToWork property
        /// </summary>
        [TestMethod]
        public void TestUpdateTaskTypeEmployeeNeedBadOldHoursToWork()
        {
            //arrange
            var oldTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = Constants.IDSTARTVALUE,
                HoursOfWork = -10,
                Active = true
            };
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                HoursOfWork = 1,
                Active = false
            };

            //act
            try
            {
                _taskTypeEmployeeNeedManager.UpdateTaskTypeEmployeeNeed(oldTaskTypeEmployeeNeed, newTaskTypeEmployeeNeed);
                Assert.Fail("Bad Old Number of Hours should have thrown an error");
            }
            catch (Exception ex)
            {

                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Tests updating a record where the new record has an invalid HoursToWork property
        /// </summary>
        [TestMethod]
        public void TestUpdateTaskTypeEmployeeNeedBadNewHoursToWork()
        {
            //arrange
            var oldTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                TaskTypeID = Constants.IDSTARTVALUE,
                HoursOfWork = 10,
                Active = true
            };
            var newTaskTypeEmployeeNeed = new TaskTypeEmployeeNeed()
            {
                HoursOfWork = -10,
                Active = false
            };

            //act
            try
            {
                _taskTypeEmployeeNeedManager.UpdateTaskTypeEmployeeNeed(oldTaskTypeEmployeeNeed, newTaskTypeEmployeeNeed);
                Assert.Fail("Bad New Number of Hours should have thrown an error");
            }
            catch (Exception ex)
            {

                Assert.IsTrue(true);
            }
        }


    }
}
