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
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/21
    /// 
    /// Test class for Job Location Manager
    /// </summary>
    [TestClass]
    public class JobLocationManagerTests
    {
        private IJobLocationManager _jobLocationManager;

        [TestInitialize]
        public void TestSetup()
        {
            _jobLocationManager = new JobLocationManager(new JobLocationAccessorMock());
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing CreateJobLocation method
        /// </summary>
        [TestMethod]
        public void TestCreateJobLocation()
        {
            //arrange
            var jobLocation = new JobLocation
            {
                CustomerID = 1000000,
                Street = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                ZipCode = "52404",
                Comments = "Test comment",
                Active = true
            };

            int result = 0;

            //act
            try
            {
                result = _jobLocationManager.CreateJobLocation(jobLocation);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsTrue(Constants.IDSTARTVALUE <= result);

        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing UpdateJobLocation method
        /// </summary>
        [TestMethod]
        public void TestUpdateJobLocation()
        {
            //arrange
            var oldJobLocation = new JobLocation
            {
                CustomerID = 1000000,
                Street = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                ZipCode = "52404",
                Comments = "Test comment",
                Active = true
            };

            var newJobLocation = new JobLocation
            {
                Street = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                ZipCode = "52404",
                Comments = "Test comment editted",
                Active = false
            };

            bool result = false;

            //act
            try
            {
                result = _jobLocationManager.UpdateJobLocation(oldJobLocation, newJobLocation);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsTrue(result);
        }
        

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/21
        /// 
        /// Testing RetrieveJobLocationAttributeListByServicePackageID method
        /// </summary>
        [TestMethod]
        public void TestRetrieveJobLocationAttributeListByServicePackageID()
        {
            //arrange
            List<JobLocationAttribute> list = null;
            int id = Constants.IDSTARTVALUE;

            //act
            try
            {
                list = _jobLocationManager.RetrieveJobLocationAttributeListByServicePackageID(id);
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
        /// Testing RetrieveJobLocationListByCustomerID method
        /// </summary>
        [TestMethod]
        public void TestRetrieveJobLocationListByCustomerID()
        {
            //arrange
            List<JobLocation> list = null;
            int id = Constants.IDSTARTVALUE;

            //act
            try
            {
                list = _jobLocationManager.RetrieveJobLocationListByCustomerID(id);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.IsNotNull(list);
        }

    }
}
