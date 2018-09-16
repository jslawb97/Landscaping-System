using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using DataAccessMocks;
using System.Collections.Generic;
using DataObjects;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class SpecialOrderManagerTests
    {
        private SpecialOrderManager _specialOrderManager; 

        [TestInitialize]
        public void TestSetup()
        {
            _specialOrderManager = new SpecialOrderManager(new SpecialOrderAccessorMock());
        }

        // RETRIEVAL TESTS ------------------>

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for successful retrieval
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderList()
        {
            //Arrange
            List<SpecialOrder> specialOrderList;

            //Act
            specialOrderList = _specialOrderManager.RetrieveSpecialOrders();

            //Assert
            Assert.AreEqual(2, specialOrderList.Count);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for successful retrieval using employee id
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderListByEmployeeID()
        {
            // Arrange
            List<SpecialOrder> specialOrderList;
            int expectedRowCount = 1;
            int employeeID = 1000001;

            // Act
            specialOrderList = _specialOrderManager.RetrieveSpecialOrderByEmployeeID(employeeID);

            // Assert
            Assert.AreEqual(expectedRowCount, specialOrderList.Count);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for exception thrown when given bad or non existent employee id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveSpecialOrderListByBadEmployeeID()
        {
            // Arrange
            List<SpecialOrder> specialOrderList;
            int badEmployeeID = 1;

            // Act
            specialOrderList = _specialOrderManager.RetrieveSpecialOrderByEmployeeID(badEmployeeID);

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for successful retrieval using job id
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderListByJobID()
        {
            // Arrange
            List<SpecialOrder> specialOrderList;
            int expectedRowCount = 2;
            int jobID = 1000001;

            // Act
            specialOrderList = _specialOrderManager.RetrieveSpecialOrderByJobID(jobID);

            // Assert
            Assert.AreEqual(expectedRowCount, specialOrderList.Count);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for exception thrown when given bad or non existent job id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveSpecialOrderListByBadJobID()
        {
            // Arrange
            List<SpecialOrder> specialOrderList;
            int badJobID = 1;

            // Act
            specialOrderList = _specialOrderManager.RetrieveSpecialOrderByJobID(badJobID);

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for successful retrieval using special order id
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderByID()
        {
            // Arrange
            SpecialOrder specialOrder;
            int specialOrderID = 1000001;
            SpecialOrder expectedSpecialOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000001,
                EmployeeID = 1000001,
                JobID = 1000001,
                SupplyStatusID = "Shipped",
                Date = new DateTime(2018, 02, 27)
            };

            // Act
            specialOrder = _specialOrderManager.RetrieveSpecialOrderByID(specialOrderID);

            // Assert
            Assert.AreEqual(expectedSpecialOrder.SpecialOrderID, specialOrder.SpecialOrderID);
            Assert.AreEqual(expectedSpecialOrder.EmployeeID, specialOrder.EmployeeID);
            Assert.AreEqual(expectedSpecialOrder.JobID, specialOrder.JobID);
            Assert.AreEqual(expectedSpecialOrder.SupplyStatusID, specialOrder.SupplyStatusID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Tests for exception thrown when given bad or non existent employee id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveSpecialOrderListByBadSpecialOrderID()
        {
            // Arrange
            SpecialOrder specialOrder;
            int badSpecialOrderID = 1;

            // Act
            specialOrder = _specialOrderManager.RetrieveSpecialOrderByID(badSpecialOrderID);

        }


        //CREATE TESTS ------------------>

        /// <summary>
        /// Reuben Cassell
        /// Created 3/1/2018
        /// 
        /// Tests to see if CreateSpecialOrder successfully
        /// adds a new order
        /// </summary>
        [TestMethod]
        public void TestCreateSpecialOrder()
        {

            // Arrange
            int expectedRowCount = 1;
            SpecialOrder specialOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000003,
                JobID = 1000001,
                EmployeeID = 1000001,
                Date = DateTime.Now,
                SupplyStatusID = "Shipped",
            };

            // Act
            int rowCount = _specialOrderManager.CreateSpecialOrder(specialOrder);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/28/2018
        /// 
        /// Tests for exception when creating an order for a closed job
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateSupplyOrderForClosedJob()
        {
            // Arrange
            int rowCount;
            SpecialOrder specialOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000003,
                JobID = 1000003,
                EmployeeID = 1000001,
                Date = DateTime.Now,
                SupplyStatusID = "Shipped",
            };

            // Act
            rowCount = _specialOrderManager.CreateSpecialOrder(specialOrder);

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/28/2018
        /// 
        /// Tests for exception when creating an order for a job with
        /// an id less than the minimum
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSupplyOrderForJobWithTooSmallID()
        {
            // Arrange
            int rowCount;
            SpecialOrder specialOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000003,
                JobID = 1,
                EmployeeID = 1000001,
                Date = DateTime.Now,
                SupplyStatusID = "Shipped",
            };

            // Act
            rowCount = _specialOrderManager.CreateSpecialOrder(specialOrder);

        }


        // EDIT TESTS ------------------>
        

        /// <summary>
        /// Reuben Cassell
        /// Created 3/1/2018
        /// 
        /// Tests to see if EditSpecialOrder successfully
        /// edits an order
        /// </summary>
        [TestMethod]
        public void TestEditSpecialOrder()
        {
            // Arrange
            int rowCount;
            int expectedRowCount = 1;
            SpecialOrder oldOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000001,
                EmployeeID = 1000001,
                JobID = 1000001,
                SupplyStatusID = "Shipped",
                Date = new DateTime(2018, 02, 27)
            };

            SpecialOrder newOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000001,
                JobID = 1000001,
                EmployeeID = 1000001,
                Date = DateTime.Now,
                SupplyStatusID = "Delayed",
            };


            // Act
            rowCount = _specialOrderManager.EditSpecialOrder(newOrder, oldOrder);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/1/2018
        /// 
        /// Tests to see if EditSpecialOrder successfully
        /// edits an order
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSpecialOrderBadEdit()
        {
            // Arrange
            int rowCount;
            SpecialOrder oldOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000001,
                EmployeeID = 1000001,
                JobID = 1000001,
                SupplyStatusID = "Shipped",
                Date = new DateTime(2018, 02, 27)
            };

            SpecialOrder newOrder = new SpecialOrder()
            {
                SpecialOrderID = 1000001,
                JobID = 1,
                EmployeeID = 1000001,
                Date = DateTime.Now,
                SupplyStatusID = "Delayed",
            };


            // Act
            rowCount = _specialOrderManager.EditSpecialOrder(newOrder, oldOrder);

            
        }

        //DELETE TESTS ------------------> 

        /// <summary>
        /// Reuben Cassell
        /// Created 3/1/2018
        /// 
        /// Tests to see if DeleteSpecialOrder successfully
        /// deletes an order
        /// </summary>
        [TestMethod]
        public void TestDeleteSpecialOrder()
        {
            // Arrange
            int rowCount;
            int expectedRowCount = 1;
            int specialOrderID = 1000001;

            // Act
            rowCount = _specialOrderManager.DeleteSpecialOrderByID(specialOrderID);

            // Assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/1/2018
        /// 
        /// Tests to see if DeleteSpecialOrder throws an exception
        /// when given an order id below the minimum
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteSpecialOrderWithTooSmallSpecailOrderID()
        {
            // Arrange
            int rowCount;
            int specialOrderID = 1;

            // Act
            rowCount = _specialOrderManager.DeleteSpecialOrderByID(specialOrderID);

            
        }



        [TestCleanup]
        public void TestTearDown()
        {
            _specialOrderManager = null;
        }
    }
}
