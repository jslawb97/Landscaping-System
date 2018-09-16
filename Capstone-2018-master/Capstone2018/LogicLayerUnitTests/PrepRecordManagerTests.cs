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
    public class PrepRecordManagerTests
    {
        private IPrepRecordManager _prepRecordManager;

        [TestInitialize]
        public void TestSetup()
        {
            _prepRecordManager = new PrepRecordManager(new PrepRecordAccessorMock());
        }





        /// <summary>
        /// Badis Saidani
        /// Created on 2018/03/16
        /// 
        /// Method to ensure bad id values cannot be passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
            "Bad ID value.")]
        public void TestRetrievePrepRecordByIDTooSmall()
        {
            // arrange
            PrepRecord prepRecord = new PrepRecord();
            prepRecord.PrepRecordID = Constants.IDSTARTVALUE - 1;

            // act
            _prepRecordManager.RetrievePrepRecordByID(prepRecord.PrepRecordID);
        }

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/03/16
        /// 
        /// Method to ensure error returned if prepRecord not found
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
            "PrepRecord record not found.")]
        public void TestRetrievePrepRecordByIDNotFound()
        {
            // arrange
            PrepRecord prepRecord = new PrepRecord();
            prepRecord.PrepRecordID = Constants.IDSTARTVALUE - 1;

            // act
            prepRecord = _prepRecordManager.RetrievePrepRecordByID(prepRecord.PrepRecordID);
        }


        /// <summary>
        /// Badis Saidani
        /// Created on 2018/03/16
        /// 
        /// Method that verifies the adding of a new prepRecord
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreatePrepRecord()
        {
            // Arrange
            var prep = new PrepRecord
            {
                EquipmentID = 1000000,
                EmployeeID = 1000000,
                Description = "Updated Test Description",
                Date = new DateTime(2018, 3, 17, 10, 0, 0)
            };

            // Act
            int result = this._prepRecordManager.CreatePrepRecord(prep);

            // Assert
            Assert.AreEqual(1000004, result);
        }

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/03/16
        /// 
        /// verifies that delete Prep Record by ID deletes a PrepRecord
        /// </summary>
        [TestMethod]
        public void TestDeletePrepRecord()
        {
            // Arrange
            int startingRowCount = _prepRecordManager.RetrievePrepRecordList().Count;
            int expectedRowCount = startingRowCount - 1;
            int prepRecordID = Constants.IDSTARTVALUE;

            // Act
            int newRowCount = _prepRecordManager.DeletePrepRecordByID(prepRecordID);

            // Assert
            Assert.AreEqual(expectedRowCount, newRowCount);
        }

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/05/04
        /// 
        /// Method that verifies the editing aprepRecord
        /// 
        /// </summary>
        [TestMethod]
        public void TestEditPrepRecord()
        {
            // Arrange

            var oldPrep = _prepRecordManager.RetrievePrepRecordByID(1000000);

            var prep = new PrepRecord
            {
                EquipmentID = 1000001,
                EmployeeID = 1000001,
                Description = "Updated Test Description",
                Date = new DateTime(2018, 4, 22, 10, 0, 0)
            };

            // Act
            int result = this._prepRecordManager.EditPrepRecordItem(oldPrep, prep);

            // Assert
            Assert.AreEqual(1, result);
        }



        [TestCleanup]
        public void TestTearDown()
        {
            _prepRecordManager = null;
        }
    }
}
