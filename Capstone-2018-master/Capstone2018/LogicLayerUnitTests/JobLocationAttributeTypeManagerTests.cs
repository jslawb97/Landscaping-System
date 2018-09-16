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
    /// Created 2018/03/19
    /// 
    /// Test class for JobLocationAttributeType
    /// </summary>
    [TestClass]
    public class JobLocationAttributeTypeManagerTests
    {
        private IJobLocationAttributeTypeManager _jobLocationAttributeTypeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _jobLocationAttributeTypeManager = new JobLocationAttributeTypeManager(new JobLocationAttributeTypeAccessorMock());
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Tests the 'Create' method
        /// </summary>
        [TestMethod]
        public void TestCreateJobLocationAttributeType()
        {
            // arange
            JobLocationAttributeType jobLocationAttributeType = new JobLocationAttributeType()
            {
                JobLocationAttributeTypeID = "Test"
            };

            // act
            int rowsAffected = _jobLocationAttributeTypeManager.CreateJobLocationAttributeType(jobLocationAttributeType);

            // assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Tests the 'Edit' method
        /// </summary>
        [TestMethod]
        public void TestEditJobLocationAttributeType()
        {
            JobLocationAttributeType oldJobLocationAttributeType = new JobLocationAttributeType()
            {
                JobLocationAttributeTypeID = "old type"
            };
            JobLocationAttributeType newJobLocationAttributeType = new JobLocationAttributeType()
            {
                JobLocationAttributeTypeID = "new type"
            };

            // act
            int rowsAffected = _jobLocationAttributeTypeManager.EditJobLocationAttributeType(oldJobLocationAttributeType, newJobLocationAttributeType);

            // assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Tests the 'RetrieveByID' method
        /// </summary>
        [TestMethod]
        public void TestRetrieveJobLocationAttributeTypeByID()
        {
            string jobLocationAttributeTypeID = "thing 1";

            var c = this._jobLocationAttributeTypeManager.RetrieveJobLocationAttributeTypeByID(jobLocationAttributeTypeID);

            Assert.AreEqual(jobLocationAttributeTypeID, c.JobLocationAttributeTypeID);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Tests the 'RetrieveList' method
        /// </summary>
        [TestMethod]
        public void TestRetrieveJobLocationAttributeTypeList()
        {
            // arrange
            List<JobLocationAttributeType> jobLocationAttributeTypeList;

            // act
            jobLocationAttributeTypeList = _jobLocationAttributeTypeManager.RetrieveJobLocationAttributeTypeList();

            // assert
            Assert.AreEqual(2, jobLocationAttributeTypeList.Count);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _jobLocationAttributeTypeManager = null;
        }
    }
}
