using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using DataAccessMocks;
using DataObjects;

namespace LogicLayerUnitTests
{
    /// <summary>
    /// Reuben Cassell
    /// Created 4/30/2017
    /// 
    /// 
    /// </summary>
    [TestClass]
    public class PersonalEquipmentTests
    {
        private PersonalEquipmentManager _peqManager;

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Sets up the manager to test
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _peqManager = new PersonalEquipmentManager(new PersonalEquipmentAccessorMock());
        }

        // RETRIEVAL TESTS -------------------------->

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests RetrievePersonalEquipmentByAssigned works
        /// with assigned is false
        /// </summary>
        [TestMethod]
        public void TestRetrievePersonalEquipmentByUnassigned()
        {
            // Arrange
            bool isAssigned = false;
            int expectedListCount = 1;
            List<PersonalEquipment> unassignedItems = new List<PersonalEquipment>();

            // Act
            unassignedItems = _peqManager.RetrievePersonalEquipmentByAssigned(isAssigned);

            // Assert
            Assert.AreEqual(expectedListCount, unassignedItems.Count());
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests RetrievePersonalEquipmentByAssigned works
        /// with assigned is true
        /// </summary>
        [TestMethod]
        public void TestRetrievePersonalEquipmentByAssigned()
        {
            // Arrange
            bool isAssigned = true;
            int expectedListCount = 2;
            List<PersonalEquipment> unassignedItems = new List<PersonalEquipment>();

            // Act
            unassignedItems = _peqManager.RetrievePersonalEquipmentByAssigned(isAssigned);

            // Assert
            Assert.AreEqual(expectedListCount, unassignedItems.Count());
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests RetrieveAssignedPersonalEquipmentByEmployeeID works
        /// as intended
        /// </summary>
        [TestMethod]
        public void TestRetrieveAssignedPersonalEquipmentByEmployeeID()
        {
            // Arrange
            int employeeID = Constants.IDSTARTVALUE;
            int expectedListCount = 1;
            List<PersonalEquipment> eqList = new List<PersonalEquipment>();

            // Act
            eqList = _peqManager.RetrieveAssignedPersonalEquipmentByEmployeeID(employeeID);

            // Assert
            Assert.AreEqual(expectedListCount, eqList.Count());
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests RetrieveAssignedPersonalEquipmentByEmployeeID throws an
        /// exception when given an invalid employeeID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveAssignedPersonalEquipmentByBadEmployeeID()
        {
            // Arrange
            int badEmployeeID = Constants.IDSTARTVALUE - 1;
            List<PersonalEquipment> eqList = new List<PersonalEquipment>();

            // Act
            eqList = _peqManager.RetrieveAssignedPersonalEquipmentByEmployeeID(badEmployeeID);

        }


        // CREATE TESTS ----------------------------->

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests CreatePersonalEquipmentAssignment works as intended
        /// </summary>
        [TestMethod]
        public void TestCreatePersonalEquipmentAssignment()
        {
            // Arrange
            int expectedRowCount = 2;
            int employeeID = Constants.IDSTARTVALUE;
            int personalEquipmentID = Constants.IDSTARTVALUE;

            // Act
            int rowCount = _peqManager.CreatePersonalEquipmentAssignment(employeeID, personalEquipmentID);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests CreatePersonalEquipmentAssignment throws an exception
        /// when given an invalid employeeID 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreatePersonalEquipmentAssignmentBadEmployeeID()
        {
            // Arrange
            int badEmployeeID = Constants.IDSTARTVALUE - 1;
            int personalEquipmentID = Constants.IDSTARTVALUE;

            // Act
            int rowCount = _peqManager.CreatePersonalEquipmentAssignment(badEmployeeID, personalEquipmentID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests CreatePersonalEquipmentAssignment throws an exception
        /// when given an invalid personalEquipmentID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreatePersonalEquipmentAssignmentBadPersonalEquipmentID()
        {
            // Arrange
            int employeeID = Constants.IDSTARTVALUE;
            int personalEquipmentID = Constants.IDSTARTVALUE - 1;

            // Act
            int rowCount = _peqManager.CreatePersonalEquipmentAssignment(employeeID, personalEquipmentID);
        }

        // DELETE TESTS ---------------------------------------->

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests DeletePersonalEquimentAssignment works as intended
        /// </summary>
        [TestMethod]
        public void TestDeletePersonalEquimentAssignment()
        {
            // Arrange
            int expectedRowCount = 2;
            int employeeID = Constants.IDSTARTVALUE;
            int personalEquipmentID = Constants.IDSTARTVALUE;

            // Act
            int rowCount = _peqManager.DeletePersonalEquipmentAssignment(employeeID, personalEquipmentID);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests DeletePersonalEquimentAssignment when given an invalid
        /// employeeID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeletePersonalEquipmentAssignmentBadEmployeeID()
        {
            // Arrange
            int badEmployeeID = Constants.IDSTARTVALUE - 1;
            int personalEquipmentID = Constants.IDSTARTVALUE;

            // Act
            int rowCount = _peqManager.DeletePersonalEquipmentAssignment(badEmployeeID, personalEquipmentID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// Tests DeletePersonalEquimentAssignment when given an invalid
        /// personalEquipmentID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeletePersonalEquipmentAssignmentBadPersonalEquipmentID()
        {
            // Arrange
            int employeeID = Constants.IDSTARTVALUE;
            int personalEquipmentID = Constants.IDSTARTVALUE - 1;

            // Act
            int rowCount = _peqManager.DeletePersonalEquipmentAssignment(employeeID, personalEquipmentID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2017
        /// 
        /// sets the tested manager to null
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            _peqManager = null;
        }
    }
}
