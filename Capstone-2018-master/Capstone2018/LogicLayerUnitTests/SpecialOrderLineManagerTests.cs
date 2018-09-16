using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using DataAccessMocks;
using System.Collections.Generic;
using DataObjects;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class SpecialOrderLineManagerTests
    {
        private ISpecialOrderLineManager _specialOrderLineManager;

        [TestInitialize]
        public void TestSetup()
        {
            _specialOrderLineManager =
                new SpecialOrderLineManager(new SpecialOrderLineAccessorMock());
        }


        // RETRIEVAL TESTS ------------------>

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests for successful retrieval using SpecialOrderLineID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderLineByID()
        {
            // Arrange
            SpecialOrderLine line;
            int specialOrderLineID = Constants.IDSTARTVALUE;

            SpecialOrderLine expectedLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000000,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000000,
                Quantity = 1
            };

            // Act
            line = _specialOrderLineManager.RetrieveSpecialOrderLineByID(specialOrderLineID);

            // Assert
            Assert.AreEqual(line.SpecialOrderLineID, expectedLine.SpecialOrderLineID);
            Assert.AreEqual(line.SpecialOrderID, expectedLine.SpecialOrderID);
            Assert.AreEqual(line.SpecialOrderItemID, expectedLine.SpecialOrderItemID);
            Assert.AreEqual(line.Quantity, expectedLine.Quantity);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests for unsuccessful retrieval using an invalid SpecialOrderLineID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveSpecialOrderLineByBadID()
        {
            // Arrange
            SpecialOrderLine line;
            int badID = Constants.IDSTARTVALUE - 1;

            // Act
            line = _specialOrderLineManager.RetrieveSpecialOrderLineByID(badID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests for successful retrieval using SpecialOrderID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderLineBySpecialOrderID()
        {
            // Arrange
            List<SpecialOrderLine> lines;
            int specialOrderID = Constants.IDSTARTVALUE;
            int expectedFirstLineID = 1000000;
            int expectedSecondLineID = 1000001;
            int expectedRowCount = 2;

            // Act
            lines = _specialOrderLineManager.RetrieveSpecialOrderLineBySpecialOrderID(specialOrderID);

            // Assert
            Assert.AreEqual(expectedRowCount, lines.Count);
            Assert.AreEqual(expectedFirstLineID, lines[0].SpecialOrderLineID);
            Assert.AreEqual(expectedSecondLineID, lines[1].SpecialOrderLineID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests for unsuccessful retrieval using an invalid SpecialOrderID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveSpecialOrderLineByBadSpecialOrderID()
        {
            // Arrange
            SpecialOrderLine line;
            int badOrderID = Constants.IDSTARTVALUE - 1;

            // Act
            line = _specialOrderLineManager.RetrieveSpecialOrderLineByID(badOrderID);
        }


        // CREATE TESTS ------------------>

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests to see if CreateSpecialOrderLine successfully
        /// adds a new order
        /// </summary>
        [TestMethod]
        public void TestCreateSpecialOrderLine()
        {
            // Arrange
            int expectedRowCount = 1;
            int rowCount;
            SpecialOrderLine newLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = Constants.IDSTARTVALUE + 3,
                SpecialOrderID = Constants.IDSTARTVALUE,
                SpecialOrderItemID = Constants.IDSTARTVALUE,
                Quantity = 1
            };

            // Act
            rowCount = _specialOrderLineManager.CreateSpecialOrderLine(newLine);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests to see if CreateSpecialOrderLine successfully
        /// adds a new order
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSpecialOrderLineBadQuantity()
        {
            // Arrange
            int rowCount;
            SpecialOrderLine newLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = Constants.IDSTARTVALUE + 3,
                SpecialOrderID = Constants.IDSTARTVALUE,
                SpecialOrderItemID = Constants.IDSTARTVALUE,
                Quantity = 0
            };

            // Act
            rowCount = _specialOrderLineManager.CreateSpecialOrderLine(newLine);

        }


        // EDIT TESTS ------------------>

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Test to see if EditSpecialOrderLine successfully edits an 
        /// existing SpecialOrderLine
        /// </summary>
        [TestMethod]
        public void TestEditSpecialOrderLine()
        {
            // Arrange
            int expectedRowCount = 1;
            int rowCount;
            SpecialOrderLine oldLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000000,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000000,
                Quantity = 1
            };

            SpecialOrderLine newLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000000,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000000,
                Quantity = 5
            };

            // Act
            rowCount = _specialOrderLineManager.EditSpecialOrderLine(oldLine, newLine);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Test to see if EditSpecialOrderLine unsuccessfuly edits an 
        /// existing SpecialOrderLine if given a bad quantity
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSpecialOrderLineBadQuantity()
        {
            int rowCount;
            SpecialOrderLine oldLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000000,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000000,
                Quantity = 1
            };

            SpecialOrderLine newLine = new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000000,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000000,
                Quantity = 0
            };

            // Act
            rowCount = _specialOrderLineManager.EditSpecialOrderLine(oldLine, newLine);
        }


        //DELETE TESTS ------------------> 

        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests for successful deletion using DeleteSpecialOrderLineByID
        /// </summary>
        [TestMethod]
        public void TestDeleteSpecialOrderLine()
        {
            // Arrange
            int rowCount;
            int expectedRowCount = 1;
            int specialOrderLineID = Constants.IDSTARTVALUE;

            // Act
            rowCount = _specialOrderLineManager.DeleteSpecialOrderLine(specialOrderLineID);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }


        /// <summary>
        /// Reuben Cassell
        /// Created 3/31/2018
        /// 
        /// Tests for unsuccessful deletion using DeleteSpecialOrderLineByID
        /// with an invalid SpecialOrderLineID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteSpecialOrderLineBadID()
        {
            // Arrange
            int rowCount;
            int specialOrderLineID = Constants.IDSTARTVALUE - 1;

            // Act
            rowCount = _specialOrderLineManager.DeleteSpecialOrderLine(specialOrderLineID);

        }

        [TestCleanup]
        public void TestTearDown()
        {
            _specialOrderLineManager = null;
        }
    }
}