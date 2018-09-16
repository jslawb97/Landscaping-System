using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using DataObjects;
using DataAccessMocks;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class InspectionRecordManagerTests
    {
        private IInspectionRecordManager _inspectionRecordManager;

        [TestInitialize]
        public void TestSetup()
        {
            _inspectionRecordManager
                = new InspectionRecordManager(new InspectionRecordAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/09
        /// 
        /// Method to verify that CreateInspectionRecord creates an InspectionRecord
        /// </summary>
        [TestMethod]
        public void TestCreateInspectionRecordGood()
        {
            // Arrange
            bool result = false;
            InspectionRecord inspectionRecord = new InspectionRecord
            {
                EmployeeID = 1000000,
                EquipmentID = 1000000,
                Description = "TestDescription",
                Date = DateTime.Now
            };

            // Act
            result = _inspectionRecordManager.CreateInspectionRecord(inspectionRecord);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/09
        /// 
        /// Method to verify that CreateInspectionRecord fails to create an InspectionRecord
        /// when given bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateInspectionRecordBadData()
        {
            // Arrange
            InspectionRecord inspectionRecord = new InspectionRecord
            {
                EmployeeID = 1,
                EquipmentID = 1,
                Description = "",
                Date = DateTime.Now
            };

            // Act
            _inspectionRecordManager.CreateInspectionRecord(inspectionRecord);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to verify that RetrieveInspectionRecordByID returns an InspectionRecord
        /// </summary>
        [TestMethod]
        public void TestRetrieveInspectionRecordByIDGood()
        {
            // Arrange
            InspectionRecord inspectionRecord = null;
            int inspectionRecordID = 1000000;

            // Act
            inspectionRecord = _inspectionRecordManager.RetrieveInspectionRecordByID(inspectionRecordID);

            // Assert
            Assert.AreEqual(inspectionRecordID, inspectionRecord.InspectionRecordID);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to verify that RetrieveInspectionRecordByID throws an exception when
        /// given bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveInspectionRecordByIDBadData()
        {
            // Arrange
            int inspectionRecordID = 1;

            // Act
            _inspectionRecordManager.RetrieveInspectionRecordByID(inspectionRecordID);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to verify that RetrieveInspectionRecordByID throws access exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveInspectionRecordByIDException()
        {
            // Arrange
            int inspectionRecordID = 1000234;
            InspectionRecord inspectionRecord = null;

            // Act
            inspectionRecord = _inspectionRecordManager.RetrieveInspectionRecordByID(inspectionRecordID);

            // Assert
            Assert.IsNull(inspectionRecord);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to verify that RetrieveInspectionRecordListByEquipmentID returns
        /// a list of InspectionRecords
        /// </summary>
        [TestMethod]
        public void TestRetrieveInspectionRecordListByEquipmentIDGood()
        {
            // Arrange
            int equipmentID = 1000000;
            List<InspectionRecord> inspectionRecordList = null;

            // Act
            inspectionRecordList = _inspectionRecordManager.RetrieveInspectionRecordListByEquipmentID(equipmentID);

            // Assert
            Assert.AreEqual(2, inspectionRecordList.Count());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to verify that RetrieveInspectionRecordListByEquipmentID
        /// throws an exception when given bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveInspectionRecordListByEquipmentIDBadData()
        {
            // Arrange
            int equipmentID = 1;

            // Act
            _inspectionRecordManager.RetrieveInspectionRecordListByEquipmentID(equipmentID);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to verify that RetrieveInspectionRecordListByEquipmentID throws
        /// access exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveInspectionRecordListByEquipmentIDException()
        {
            // Arrange
            int equipmentID = 1000234;

            // Act
            _inspectionRecordManager.RetrieveInspectionRecordListByEquipmentID(equipmentID);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that EditInspectionRecord changes an
        /// InspectionRecord
        /// </summary>
        [TestMethod]
        public void TestEditInspectionRecord()
        {
            // Arrange
            bool result = false;
            InspectionRecord oldInspectionRecord = new InspectionRecord
            {
                InspectionRecordID = 1000001,
                EquipmentID = 1000001,
                EmployeeID = 1000001,
                Description = "TestDescription2",
                Date = DateTime.Parse("1/28/2010 9:01:26 PM")
            };
            InspectionRecord newInspectionRecord = new InspectionRecord {
                InspectionRecordID = 1000001,
                EquipmentID = 1000005,
                EmployeeID = 1000005,
                Description = "TestDescriptionNew",
                Date = DateTime.Parse("3/23/2018 9:13:00 AM")
            };

            // Act
            result = _inspectionRecordManager.EditInspectionRecord(oldInspectionRecord
                , newInspectionRecord);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that EditInspectionRecord throws an
        /// exception when given bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditInspectionRecordBadData()
        {
            // Arrange
            InspectionRecord oldInspectionRecord = new InspectionRecord
            {
                InspectionRecordID = 1,
                EquipmentID = 1000001,
                EmployeeID = 1000001,
                Description = "TestDescription2",
                Date = DateTime.Parse("1/28/2010 9:01:26 PM")
            };
            InspectionRecord newInspectionRecord = new InspectionRecord
            {
                InspectionRecordID = 1000001,
                EquipmentID = 1000005,
                EmployeeID = 1000005,
                Description = "TestDescriptionNew",
                Date = DateTime.Parse("3/23/2018 9:13:00 AM")
            };

            // Act
            _inspectionRecordManager.EditInspectionRecord(oldInspectionRecord
                , newInspectionRecord);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that EditInspectionRecord throws data
        /// access exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditInspectionRecordException()
        {
            // Arrange
            InspectionRecord oldInspectionRecord = new InspectionRecord
            {
                InspectionRecordID = 1000001,
                EquipmentID = 9999999,
                EmployeeID = 1000001,
                Description = "TestDescription2",
                Date = DateTime.Parse("1/28/2010 9:01:26 PM")
            };
            InspectionRecord newInspectionRecord = new InspectionRecord
            {
                InspectionRecordID = 1000001,
                EquipmentID = 1000005,
                EmployeeID = 1000005,
                Description = "TestDescriptionNew",
                Date = DateTime.Parse("3/23/2018 9:13:00 AM")
            };

            // Act
            _inspectionRecordManager.EditInspectionRecord(oldInspectionRecord
                , newInspectionRecord);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that DeleteInspectionRecord deletes
        /// an InspectionRecord
        /// </summary>
        [TestMethod]
        public void TestDeleteInspectionRecordGood()
        {
            // Act
            bool result = false;
            int inspectionRecordID = 1000000;

            // Arrange
            result =
                _inspectionRecordManager.DeleteInspectionRecord(inspectionRecordID);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that DeleteInspectionRecord throws an
        /// exception when given bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteInspectionRecordBadData()
        {
            // Act
            int inspectionRecordID = 1;

            // Arrange
            _inspectionRecordManager.DeleteInspectionRecord(inspectionRecordID);

            // Assert
            Assert.Fail();
        }


        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that DeleteInspectionRecord throws data
        /// access exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeleteInspectionRecordException()
        {
            // Act
            int inspectionRecordID = 9999999;

            // Arrange
            _inspectionRecordManager.DeleteInspectionRecord(inspectionRecordID);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to verify that RetrieveInspectionRecordList returns
        /// a list of InspectionRecords
        /// </summary>
        [TestMethod]
        public void TestRetrieveInspectionRecordList()
        {
            // Act
            List<InspectionRecord> inspectionRecordList = null;

            // Arrange
            inspectionRecordList =
                _inspectionRecordManager.RetrieveInspectionRecordList();

            // Assert
            Assert.AreEqual(4, inspectionRecordList.Count());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/03
        /// 
        /// Method to verify that RetrieveInspectionRecordDetailList returns
        /// a list of InspectionRecords
        /// </summary>
        [TestMethod]
        public void TestRetrieveInspectionRecordDetailList()
        {
            // Act
            List<InspectionRecordDetail> inspectionRecordDetailList = null;

            // Arrange
            inspectionRecordDetailList =
                _inspectionRecordManager.RetrieveInspectionRecordDetailList();

            // Assert
            Assert.AreEqual(4, inspectionRecordDetailList.Count());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _inspectionRecordManager = null;
        }
    }
}
