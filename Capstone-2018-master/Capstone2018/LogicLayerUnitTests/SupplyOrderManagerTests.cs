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
    [TestClass]
    public class SupplyOrderManagerTests
    {
        private ISupplyOrderManager _supplyOrderManager;

        [TestInitialize]
        public void TestSetup()
        {
            _supplyOrderManager = new SupplyOrderManager(new SupplyOrderAccessorMock());
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Test method to retrieve list of supply orders
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplyOrderList()
        {
            //Arrange
            List<SupplyOrder> supplyOrders;

            //Act
            supplyOrders = _supplyOrderManager.RetrieveSupplyOrderList();

            //Assert
            Assert.AreEqual(2, supplyOrders.Count);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Test Method to retrieve supply order by an id
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplyOrderByID()
        {
            //Arrange
            SupplyOrder supplyOrder;
            SupplyOrder expectedOrder = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = null,
                SupplyStatusID = "Delivered",
                Date = new DateTime(2018, 3, 30)
            };
            int id = 1000000;


            //Act
            supplyOrder = _supplyOrderManager.RetrieveSupplyOrderByID(id);

            //Assert
            Assert.AreEqual(expectedOrder.SupplyOrderID, supplyOrder.SupplyOrderID);
            Assert.AreEqual(expectedOrder.EmployeeID, supplyOrder.EmployeeID);
            Assert.AreEqual(expectedOrder.JobID, supplyOrder.JobID);
            Assert.AreEqual(expectedOrder.SupplyStatusID, supplyOrder.SupplyStatusID);
            Assert.AreEqual(expectedOrder.Date, supplyOrder.Date);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid ID is given
        /// when retrieving supply orders
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveSupplyOrderByIDInvalidID()
        {
            //Arrange
            SupplyOrder supplyOrder;
            int id = 100;

            //Act
            supplyOrder = _supplyOrderManager.RetrieveSupplyOrderByID(id);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if order is created with a valid order
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyOrderSuccess()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000005,
                EmployeeID = 1000003,
                JobID = null,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.CreateSupplyOrderNoJob(order);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method to verify that AddSupplyOrder throws exception if
        /// an invalid SupplyOrderID is given.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSupplyOrderInvalidOrderID()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 100,
                EmployeeID = 1000003,
                JobID = null,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.CreateSupplyOrderNoJob(order);
        }


        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method to verify that AddSupplyOrder throws exception if
        /// an invalid EmployeeID is given.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSupplyOrderInvalidEmployeeID()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 10000,
                JobID = null,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.CreateSupplyOrderNoJob(order);
        }


        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Supply Order ID is given
        /// to edit a supply order
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderInvalidSupplyOrderID()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 100,
                EmployeeID = 1000000,
                JobID = 1000000,
                Date = DateTime.Now
            };
            SupplyOrder newOrder = new SupplyOrder()
            {
                SupplyOrderID = 100,
                EmployeeID = 1000001,
                JobID = 10000000,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.EditSupplyOrder(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Employee ID is given
        /// when editing an existing Supply Order ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderInvalidEmployeeID()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000,
                JobID = 1000000,
                Date = DateTime.Now
            };
            SupplyOrder newOrder = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000,
                JobID = 10000000,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.EditSupplyOrder(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Job ID is given
        /// when editing an existing Supply Order ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderInvalidJobID()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = 1000,
                Date = DateTime.Now
            };
            SupplyOrder newOrder = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = 1000,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.EditSupplyOrder(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if different Supply Order IDs are given
        /// </summary>
		[TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderDifferentSupplyOrderID()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = 1000000,
                Date = DateTime.Now
            };
            SupplyOrder newOrder = new SupplyOrder()
            {
                SupplyOrderID = 1000005,
                EmployeeID = 1000001,
                JobID = 10000000,
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.EditSupplyOrder(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if a supply order is edited if given
        /// valid supply orders
        /// </summary>
        [TestMethod]
        public void TestEditSupplyOrderSuccess()
        {
            // Arrange
            SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = 1000000,
                SupplyStatusID = "Delivered",
                Date = DateTime.Now
            };
            SupplyOrder newOrder = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000001,
                JobID = 1000000,
                SupplyStatusID = "Pending Delivery",
                Date = DateTime.Now
            };
            int result = 0;

            // Act
            result = _supplyOrderManager.EditSupplyOrderNoJob(order, newOrder);

            // Assert
            Assert.AreEqual(1, result);
        }


        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Supply Order ID is given
        /// to edit a supply order
        /// </summary>
        [TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestEditSupplyOrderNoJobInvalidSupplyOrderID()
		{
			// Arrange
			SupplyOrder order = new SupplyOrder() 
			{
				SupplyOrderID = 100,
				EmployeeID = 1000000,
				JobID = null,
				Date = DateTime.Now
			};
			SupplyOrder newOrder = new SupplyOrder() 
			{
				SupplyOrderID = 100,
				EmployeeID = 1000001,
				JobID = null,
				Date = DateTime.Now
			};
			int result = 0;
			
			// Act
			result = _supplyOrderManager.EditSupplyOrderNoJob(order, newOrder);
		}
		
        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Employee ID is given
        /// when editing an existing Supply Order ID
        /// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestEditSupplyOrderNoJobInvalidEmployeeID()
		{
			// Arrange
			SupplyOrder order = new SupplyOrder() 
			{
				SupplyOrderID = 1000000,
				EmployeeID = 1000,
				JobID = null,
				Date = DateTime.Now
			};
			SupplyOrder newOrder = new SupplyOrder() 
			{
				SupplyOrderID = 1000000,
				EmployeeID = 1000,
				JobID = null,
				Date = DateTime.Now
			};
			int result = 0;
			
			// Act
			result = _supplyOrderManager.EditSupplyOrderNoJob(order, newOrder);
		}

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if different Supply Order IDs are given
        /// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestEditSupplyOrderNoJobDifferentSupplyOrderID()
		{
			// Arrange
			SupplyOrder order = new SupplyOrder() 
			{
				SupplyOrderID = 1000000,
				EmployeeID = 1000000,
				JobID = null,
				Date = DateTime.Now
			};
			SupplyOrder newOrder = new SupplyOrder() 
			{
                SupplyOrderID = 1000005,
				EmployeeID = 1000001,
				JobID = null,
				Date = DateTime.Now
			};
			int result = 0;
			
			// Act
			result = _supplyOrderManager.EditSupplyOrderNoJob(order, newOrder);
		}
		
        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if a supply order is edited if given
        /// valid supply orders
        /// </summary>
		[TestMethod]
		public void TestEditSupplyOrderNoJobSuccess()
		{
			// Arrange
			SupplyOrder order = new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = null,
                SupplyStatusID = "Delivered",
                Date = DateTime.Now
			};
			SupplyOrder newOrder = new SupplyOrder() 
			{
                SupplyOrderID = 1000000,
				EmployeeID = 1000001,
                JobID = null,
                SupplyStatusID = "Pending Delivery",
				Date = DateTime.Now
			};
			int result = 0;
			
			// Act
			result = _supplyOrderManager.EditSupplyOrderNoJob(order, newOrder);
			
			// Assert
			Assert.AreEqual(1, result);
		}
		
        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Supply Order ID is given
        /// to delete a supply order
        /// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestDeleteSupplyOrderInvalidSupplyOrderID()
		{
			// Arrange
			SupplyOrder order = new SupplyOrder() 
			{
				SupplyOrderID = 100,
				EmployeeID = 1000000,
				JobID = 1000000,
				Date = DateTime.Now,
				SupplyStatusID = "Cancelled"
			};
			int result = 0;
			
			// Act
			result = _supplyOrderManager.DeleteSupplyOrder(order.SupplyOrderID, order.SupplyStatusID);
		}
		
        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if a Supply Order is deleted when 
        /// given a valid supply order id and the current status
        /// </summary>
		[TestMethod]
		public void TestDeleteSupplyOrderSuccess()
		{
			// Arrange
			SupplyOrder order = new SupplyOrder() 
			{
				SupplyOrderID = 1000000,
				EmployeeID = 1000000,
				JobID = 1000000,
				Date = DateTime.Now,
				SupplyStatusID = "Delivered"
			};
			int result = 0;
			
			// Act
			result = _supplyOrderManager.DeleteSupplyOrder(order.SupplyOrderID, order.SupplyStatusID);
			
			// Assert
            Assert.AreEqual(1, result);
		}

        [TestCleanup]
        public void TestTearDown()
        {
            _supplyOrderManager = null;
        }
    }
}
