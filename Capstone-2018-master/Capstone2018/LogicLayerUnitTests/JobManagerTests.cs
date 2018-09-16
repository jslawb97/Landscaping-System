using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using Logic;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/21
    /// 
    /// Tests for Job Manager
    /// </summary>
    [TestClass]
    public class JobManagerTests
    {
        private IJobManager _jobManager;

        [TestInitialize]
        public void TestSetup()
        {
            _jobManager = new JobManager(new JobAccessorMock());
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing CreateJob functionality
        /// </summary>
        [TestMethod]
        public void TestCreateJob()
        {
            //arrange
            var job = new Job()
            {
                DateScheduled = new DateTime(2018, 3, 17, 10, 0, 0),
                DateCompleted = new DateTime(2018, 3, 17, 15, 0, 0),
                EmployeeID = 100000,
                JobLocationID = 1000000,
                Comments = "Test comments",
                CustomerID = 1000000,
                Active = true

            };

            //act 
            int newID = _jobManager.CreateJob(job);

            //assert
            Assert.IsTrue(Constants.IDSTARTVALUE <= newID);
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing retrieving JobDetail list
        /// </summary>
        [TestMethod]
        public void TestRetrieveJobDetailList()
        {
            //arrange
            List<JobDetail> list = null;

            //act
            try
            {
                list = _jobManager.RetrieveJobDetailList();
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(list);
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing updateing a job record
        /// </summary>
        [TestMethod]
        public void TestUpdateJob()
        {
            //arrange
            bool updated = false;
            var oldJob = new Job()
            {
                JobID = Constants.IDSTARTVALUE,
                DateScheduled = new DateTime(2018, 3, 17, 10, 0, 0),
                DateCompleted = new DateTime(2018, 3, 17, 15, 0, 0),
                EmployeeID = 100000,
                JobLocationID = 1000000,
                Comments = "Test comments",
                CustomerID = 1000000,
                Active = true

            };
            var newJob = new Job()
            {
                JobID = Constants.IDSTARTVALUE,
                DateScheduled = new DateTime(2018, 3, 17, 11, 0, 0),
                DateCompleted = new DateTime(2018, 3, 17, 17, 0, 0),
                EmployeeID = 100001,
                JobLocationID = 1000001,
                Comments = "Test comments editted",
                CustomerID = 1000001,
                Active = false
            };

            //act
            try
            {
                updated = _jobManager.UpdateJob(oldJob, newJob);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsTrue(updated);

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing deactivate job functionality
        /// </summary>
        [TestMethod]
        public void TestDeactivateJob()
        {
            //arrange
            var deactivated = false;

            //act
            try
            {
                deactivated = _jobManager.DeactivateJob(Constants.IDSTARTVALUE);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            // assert
            Assert.IsTrue(deactivated);
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Test RetrieveJobList method
        /// </summary>
        [TestMethod]
        public void TestRetrieveJobList()
        {
            //arrange
            List<Job> list = null;

            //act
            try
            {
               list =  _jobManager.RetrieveJobList();
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(list);
        }
        
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing RetrieveServicePackageListByJobID method
        /// </summary>
        [TestMethod]
        public void TestRetrieveServicePackageListByJobID()
        {
            //arrange
            List<ServicePackage> list = null;

            //act
            try
            {
                list = _jobManager.RetrieveServicePackageListByJobID(Constants.IDSTARTVALUE);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(list);
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing CreateUpdateJobServicePackage method
        /// </summary>
        [TestMethod]
        public void TestCreateUpdateJobServicePackage()
        {
            //arrange
            int affected = 0;

            var list = new List<ServicePackage>() {
                new ServicePackage(){
                    ServicePackageID = Constants.IDSTARTVALUE + 10,
                    Name = "Test Service Package",
                    Description = "Test Description",
                    Active = true
                },
                new ServicePackage(){
                    ServicePackageID = Constants.IDSTARTVALUE,
                    Name = "Test Service Package",
                    Description = "Test Description",
                    Active = true
                }

            };
            int jobID = Constants.IDSTARTVALUE;

            //act
            try
            {
                affected = _jobManager.CreateUpdateJobServicePackage(jobID, list);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.AreEqual(2, affected);
        }
    }
}
