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
    [TestClass]
    public class TimeOffRequestManagerTests
    {
        private TimeOffRequestManager _timeOffRequestManager;

        [TestInitialize]
        public void TestSetup()
        {
            _timeOffRequestManager = new TimeOffRequestManager(new TimeOffRequestAccessorMock());
        }
        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method to test retrieve time off request list
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  time off request list is past.
        [TestMethod]
        public void TestRetrieveTimeOffRequestList()
        {
            // arrange 
            List<TimeOffRequest> timeOffRequestList;

            // act
            timeOffRequestList = _timeOffRequestManager.RetrieveTimeOffRequestList();

            // assert
            Assert.AreEqual(2, timeOffRequestList.Count);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method to test create time off request
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  test past 
        [TestMethod]
        public void TestCreateTimeOffRequestCreated()
        {
            // arrange
            bool returnedNewTimeOffRequestID;
            TimeOffRequest timeOffRequest = new TimeOffRequest();
            timeOffRequest.Approved = true;
            timeOffRequest.EmployeeID = Constants.IDSTARTVALUE;

            //act
            returnedNewTimeOffRequestID = _timeOffRequestManager.CreateTimeOffRequest(timeOffRequest);

            //assert
            Assert.AreEqual(true, returnedNewTimeOffRequestID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/27
        /// 
        /// Method to test exception is thrown with an invalid ID
        /// QA Shilin Xiong 4/27/2018  test past 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
            "Bad ID value.")]
        public void TestCreateTimeOffRequestEmployeeIDTooSmall()
        {
            // arrange
            TimeOffRequest timeOffRequest = new TimeOffRequest();
            timeOffRequest.EmployeeID = Constants.IDSTARTVALUE - 1;

            // act            
            _timeOffRequestManager.CreateTimeOffRequest(timeOffRequest);
        }


        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/27
        /// 
        /// Method to test false is returned when no record is created
        /// </summary>
        [TestMethod]
        public void TestCreateTimeOffRequestNotCreated()
        {
            // arrange
            TimeOffRequest timeOffRequest = new TimeOffRequest();
            timeOffRequest.EmployeeID = Constants.IDSTARTVALUE;
            bool returnedNewTimeOffRequestID;

            // act
            returnedNewTimeOffRequestID = _timeOffRequestManager.CreateTimeOffRequest(timeOffRequest);

            // assert
            Assert.AreEqual(false, returnedNewTimeOffRequestID);
        }


        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/01
        /// 
        /// Method that verifies that edit will not run with an TimeOffID
        /// less than the IDStartValue
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTimeOffInvalidID()
        {
            // arrange
            int timeOffEdited = 0;
            TimeOffRequest oldTimeOff = new TimeOffRequest()
            {
                TimeOffID = 105,
                EmployeeID = 1000005,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 3),
                Approved = false

            };
            TimeOffRequest newTimeOff = new TimeOffRequest()
            {
                TimeOffID = 105,
                EmployeeID = 1000005,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 8),
                Approved = true
            };

            // act
            timeOffEdited = _timeOffRequestManager.EditTimeOff(oldTimeOff, newTimeOff);
        }


        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/09
        /// 
        /// Method that verifies that edit will not run with an EmployeeID
        /// less than the IDStartValue
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditTimeOffInvalidEmployeeID()
        {
            // arrange
            int timeOffEdited = 0;
            TimeOffRequest oldTimeOff = new TimeOffRequest()
            {
                TimeOffID = 1000000,
                EmployeeID = 105,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 3),
                Approved = false

            };
            TimeOffRequest newTimeOff = new TimeOffRequest()
            {
                TimeOffID = 105,
                EmployeeID = 105,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 8),
                Approved = true
            };

            // act
            timeOffEdited = _timeOffRequestManager.EditTimeOff(oldTimeOff, newTimeOff);
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/01
        /// 
        /// Method that test whether the update will go through id
        /// the two TimeOffIDs do not match
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditTimeOffDifferentID()
        {
            // arrange
            int timeOffEdited = 0;
            TimeOffRequest oldTimeOff = new TimeOffRequest()
            {
                TimeOffID = 1000000,
                EmployeeID = 1000000,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 3),
                Approved = false
            };
            TimeOffRequest newTimeOff = new TimeOffRequest()
            {
                TimeOffID = 1000001,
                EmployeeID = 1000001,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 8),
                Approved = true
            };

            // act
            timeOffEdited = _timeOffRequestManager.EditTimeOff(oldTimeOff, newTimeOff);
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/01
        /// 
        /// Method that tests whether the update will succeed if
        /// the two ids being passed match 
        /// </summary>
        [TestMethod]
        public void TestEditTimeOffSuccess()
        {
            // arrange
            int timeOffEdited = 0;
            TimeOffRequest oldTimeOff = new TimeOffRequest()
            {
                TimeOffID = 1000000,
                EmployeeID = 1000000,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 3),
                Approved = false
            };
            TimeOffRequest newTimeOff = new TimeOffRequest()
            {
                TimeOffID = 1000000,
                EmployeeID = 1000000,
                StartTime = new DateTime(2008, 5, 1),
                EndTime = new DateTime(2008, 5, 8),
                Approved = true
            };

            // act
            timeOffEdited = _timeOffRequestManager.EditTimeOff(oldTimeOff, newTimeOff);

            // assert
            Assert.AreEqual(1, timeOffEdited);
        }

        [TestMethod]
        public void TestDeactivateTimeOffRequestByID()
        {
            this._timeOffRequestManager = new TimeOffRequestManager(new TimeOffRequestAccessorMock());
            Assert.AreEqual(true, this._timeOffRequestManager.DeactivateTimeOffRequestByID(10000000));
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _timeOffRequestManager = null;
        }
    }
}
