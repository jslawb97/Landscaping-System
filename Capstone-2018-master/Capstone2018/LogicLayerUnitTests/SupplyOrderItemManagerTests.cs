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
    public class SupplyOrderItemManagerTests
    {
        private ISupplyOrderItemManager _supplyOrderItemManager;

        [TestInitialize]
        public void TestSetup()
        {
            _supplyOrderItemManager = new SupplyOrderItemManager(new SupplyOrderItemAccessorMock());
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if order item is created with a valid order item
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyOrderItemSuccess()
        {
            // Arrange
            SupplyOrderItem orderItem = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000005,
                SupplyOrderLineID = 1000006,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.CreateSupplyOrderItem(orderItem);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Order ID is given
        /// when creating a supply order item
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSupplyOrderItemInvalidOrderID()
        {
            // Arrange
            SupplyOrderItem orderItem = new SupplyOrderItem()
            {
                SupplyOrderID = 1000,
                SupplyItemID = 1000005,
                SupplyOrderLineID = 1000006,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.CreateSupplyOrderItem(orderItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Supply Item ID is given
        /// when creating a supply order item
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSupplyOrderItemInvalidSupplyItemID()
        {
            // Arrange
            SupplyOrderItem orderItem = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000,
                SupplyOrderLineID = 1000006,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.CreateSupplyOrderItem(orderItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Order Line ID is given
        /// when creating a supply order item
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateSupplyOrderItemInvalidOrderLineID()
        {
            // Arrange
            SupplyOrderItem orderItem = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000005,
                SupplyOrderLineID = 100,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.CreateSupplyOrderItem(orderItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Test Method to retrieve supply order item by a supply order id
        /// </summary>
        [TestMethod]
        public void TestRetrieveOrderItemsSuccess()
        {
            //Arrange
            List<SupplyOrderItem> supplyOrderItems;
            int supplyOrderID = 1000000;

            //Act
            supplyOrderItems = _supplyOrderItemManager.RetrieveSupplyOrderItemsByID(supplyOrderID);

            //Assert
            Assert.AreEqual(1, supplyOrderItems.Count);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid ID is given
        /// when retrieving supply order items
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveOrderItemsInvalidID()
        {
            //Arrange
            List<SupplyOrderItem> supplyOrderItems;
            int supplyOrderID = 100;

            //Act
            supplyOrderItems = _supplyOrderItemManager.RetrieveSupplyOrderItemsByID(supplyOrderID);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if a supply order item is edited if given
        /// valid supply order items
        /// </summary>
        [TestMethod]
        public void TestEditSupplyOrderItemSuccess()
        {
            // Arrange
            SupplyOrderItem order = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 10000000,
                SupplyOrderLineID = 1000000,
                Quantity = 20
            };
            SupplyOrderItem newOrder = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000000,
                SupplyOrderLineID = 1000000,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.EditSupplyOrderItem(order, newOrder);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if an invalid Supply Order ID is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderItemInvalidSupplyOrderID()
        {
            // Arrange
            SupplyOrderItem order = new SupplyOrderItem()
            {
                SupplyOrderID = 10000,
                SupplyItemID = 10000000,
                SupplyOrderLineID = 1000000,
                Quantity = 20
            };
            SupplyOrderItem newOrder = new SupplyOrderItem()
            {
                SupplyOrderID = 10000,
                SupplyItemID = 1000000,
                SupplyOrderLineID = 1000000,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.EditSupplyOrderItem(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if an invalid Supply Item ID is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderItemInvalidSupplyItemID()
        {
            // Arrange
            SupplyOrderItem order = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 100000,
                SupplyOrderLineID = 1000000,
                Quantity = 20
            };
            SupplyOrderItem newOrder = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 100000,
                SupplyOrderLineID = 1000000,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.EditSupplyOrderItem(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if an invalid Supply Order Line ID is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderItemInvalidSupplyOrderLineID()
        {
            // Arrange
            SupplyOrderItem order = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000000,
                SupplyOrderLineID = 1000,
                Quantity = 20
            };
            SupplyOrderItem newOrder = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000000,
                SupplyOrderLineID = 1000,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.EditSupplyOrderItem(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if different Supply Order Line IDs are given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderItemDifferentSupplyOrderLineID()
        {
            // Arrange
            SupplyOrderItem order = new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 1000000,
                SupplyOrderLineID = 1000,
                Quantity = 20
            };
            SupplyOrderItem newOrder = new SupplyOrderItem()
            {
                SupplyOrderID = 1000001,
                SupplyItemID = 1000000,
                SupplyOrderLineID = 1000000,
                Quantity = 123
            };
            int result = 0;

            // Act
            result = _supplyOrderItemManager.EditSupplyOrderItem(order, newOrder);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if an invalid Supply Order ID is given
        /// to delete a supply order
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteSupplyOrderItemInvalidSupplyOrderID()
        {
            // Arrange
            int supplyOrderID = 100;
            int result = 0;

            // Act
            result = _supplyOrderItemManager.DeleteSupplyOrderItem(supplyOrderID);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if a Supply Order Item is deleted when 
        /// given a valid supply order id
        /// </summary>
        [TestMethod]
        public void TestDeleteSupplyOrderSuccess()
        {
            // Arrange
            int supplyOrderID = 1000000;
            int result = 0;

            // Act
            result = _supplyOrderItemManager.DeleteSupplyOrderItem(supplyOrderID);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Method to verify that TestEditSupplyOrderLineQuantityReceivedGood
        /// edits the QuantityReceived field when given good data
        /// </summary>
        [TestMethod]
        public void TestEditSupplyOrderLineQuantityReceivedGood()
        {
            // Arrange
            var result = false;
            var supplyOrder = new SupplyOrder { SupplyOrderID = 1000000 };
            var oldSupplyOrderItemList = new List<SupplyOrderItem>();
            var newSupplyOrderItemList = new List<SupplyOrderItem>();
            oldSupplyOrderItemList.Add(new SupplyOrderItem()
            {
                SupplyOrderLineID = 1000000,
                SupplyOrderID = 1000000,
                QuantityReceived = 0
            });
            newSupplyOrderItemList.Add(new SupplyOrderItem()
            {
                SupplyOrderLineID = 1000000,
                SupplyOrderID = 1000000,
                QuantityReceived = 4
            });

            // Act
            result = _supplyOrderItemManager
                .EditSupplyOrderLineQuantityReceived(supplyOrder
                , newSupplyOrderItemList, oldSupplyOrderItemList);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Method to verify that TestEditSupplyOrderLineQuantityReceivedGood
        /// edits the QuantityReceived field when given good data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditSupplyOrderLineQuantityReceivedBadData()
        {
            // Arrange
            var supplyOrder = new SupplyOrder { SupplyOrderID = 1000000 };
            var oldSupplyOrderItemList = new List<SupplyOrderItem>();
            var newSupplyOrderItemList = new List<SupplyOrderItem>();
            oldSupplyOrderItemList.Add(new SupplyOrderItem()
            {
                SupplyOrderLineID = 1000000,
                SupplyOrderID = 1000000,
                QuantityReceived = 0
            });
            newSupplyOrderItemList.Add(new SupplyOrderItem()
            {
                SupplyOrderLineID = 1000000,
                SupplyOrderID = 1000000,
                QuantityReceived = 0
            });

            // Act
            _supplyOrderItemManager
                .EditSupplyOrderLineQuantityReceived(supplyOrder
                , newSupplyOrderItemList, oldSupplyOrderItemList);

            // Assert
            Assert.Fail();
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _supplyOrderItemManager = null;
        }
    }
}
