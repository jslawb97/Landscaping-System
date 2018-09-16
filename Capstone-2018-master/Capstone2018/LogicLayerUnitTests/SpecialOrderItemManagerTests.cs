using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using DataObjects;
using Logic;
using DataAccessMocks;

namespace LogicLayerUnitTests
{
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/02/08
    /// 
    /// Logic tests for the SpecialOrderItemManager
    /// </summary>
    [TestClass]
    public class SpecialOrderItemManagerTests
    {
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Initialize test 
        /// </summary>
        private ISpecialOrderItemManager _specialOrderItemManager;
        [TestInitialize]
        public void TestSetup()
        {
            //_specialOrderItemManager = new SpecialOrderItemManager();
            _specialOrderItemManager = new SpecialOrderItemManager(new SpecialOrderItemAccessorMocks());
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the RetrieveSpecialOrderItems method
        /// </summary>
        [TestMethod]
        public void TestRetrieveSpecialOrderItems()
        {
            // Arrange
            List<SpecialItem> specialItemList;

            // Act
            specialItemList = _specialOrderItemManager.RetrieveSpecialOrderItems();

            // Assert
            Assert.IsNotNull(specialItemList);
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the AddSpecialOrderItem method
        /// </summary>
        [TestMethod]
        public void TestAddSpecialOrderItem()
        {
            // Arrange
            var newItem = new SpecialItem() {
                Name = "TestSpecialItem",
                Active = true

            };

            // Act
            var result = _specialOrderItemManager.AddSpecialOrderItem(newItem);

            // Assert
            Assert.IsTrue(result);
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the AddSpecialOrderItem method name too long
        /// </summary>
        [TestMethod]
        public void TestAddSpecialOrderItemNameTooLong()
        {
            var chars = new char[Constants.MAX_SPECIAL_ITEM_NAME_LENGTH + 1];
            string name = new string(chars);
            // Arrange
            var newItem = new SpecialItem()
            {
                Name = name,
                Active = true

            };

            try
            {
                // Act
                var result = _specialOrderItemManager.AddSpecialOrderItem(newItem);
                Assert.Fail("Expected Name too long error");
            }
            catch (Exception)
            {

                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the AddSpecialOrderItem method name empty
        /// </summary>
        [TestMethod]
        public void TestAddSpecialOrderItemNameEmpty()
        {
            
            // Arrange
            var newItem = new SpecialItem()
            {
                Name = "",
                Active = true

            };

            try
            {
                // Act
                var result = _specialOrderItemManager.AddSpecialOrderItem(newItem);
                Assert.Fail("Expected Name empty error");
            }
            catch (Exception)
            {

                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the AddSpecialOrderItem method null
        /// </summary>
        [TestMethod]
        public void TestAddSpecialOrderItemNull()
        {

            // Arrange
            SpecialItem newItem = null;

            try
            {
                // Act
                var result = _specialOrderItemManager.AddSpecialOrderItem(newItem);
                Assert.Fail("Expected null item error");
            }
            catch (Exception)
            {

                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the DeactivateSpecialOrderItem method
        /// </summary>
        [TestMethod]
        public void TestDeactivateSpecialOrderItem()
        {
            // Arrange
            var item = new SpecialItem()
            {
                SpecialOrderItemID = 1000000,
                Name = "TestSpecialItem",
                Active = true

            };

            // Act
            var deactivateResult = _specialOrderItemManager.DeactivateSpecialOrderItem(item.SpecialOrderItemID);

            // Assert
            Assert.IsTrue(deactivateResult);
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the DeactivateSpecialOrderItem method for bad id
        /// </summary>
        [TestMethod]
        public void TestDeactivateSpecialOrderItemBadID()
        {
            // Arrange
            var item = new SpecialItem()
            {
                SpecialOrderItemID = 100000,
                Name = "TestSpecialItem",
                Active = true

            };

            try
            {
                // Act
                var deactivateResult = _specialOrderItemManager.DeactivateSpecialOrderItem(item.SpecialOrderItemID);
                Assert.Fail("Expected bad id error");
            }
            catch (Exception)
            {

                // Assert
                Assert.IsTrue(true);
            }
            

            
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the EditSpecialOrderItem method
        /// </summary>
        [TestMethod]
        public void TestEditSpecialOrderItem()
        {
            // Arrange
            List<SpecialItem> items = _specialOrderItemManager.RetrieveSpecialOrderItems();

            var newItem = new SpecialItem {
                SpecialOrderItemID = items.ElementAt(0).SpecialOrderItemID,
                Name = items.ElementAt(0).Name + "EDITING THIS FIELD",
                Active = items.ElementAt(0).Active
                    
            };
            
            // Act
            var editResult = _specialOrderItemManager.EditSpecialOrderItem(items.ElementAt(0), newItem);

            // Assert
            Assert.IsTrue(editResult);
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the EditSpecialOrderItem method name too long
        /// </summary>
        [TestMethod]
        public void TestEditSpecialOrderItemNameTooLong()
        {
            // Arrange
            List<SpecialItem> items = _specialOrderItemManager.RetrieveSpecialOrderItems();
            var chars = new char[Constants.MAX_SPECIAL_ITEM_NAME_LENGTH + 1];
            string name = new string(chars);
            var newItem = new SpecialItem
            {
                SpecialOrderItemID = items.ElementAt(0).SpecialOrderItemID,
                Name = name,
                Active = items.ElementAt(0).Active

            };
            try
            {
                // Act
                var editResult = _specialOrderItemManager.EditSpecialOrderItem(items.ElementAt(0), newItem);
                Assert.Fail("Expected name too long error");
            }
            catch (Exception)
            {

                // Assert
                Assert.IsTrue(true);
            }
            
           
        }


       /// <summary>
         /// Zachary Hall
         /// Created: 2018/02/08
         /// 
         /// Test for the EditSpecialOrderItem method name empty
         /// </summary>
        [TestMethod]
        public void TestEditSpecialOrderItemNameEmpty()
        {
            // Arrange
            List<SpecialItem> items = _specialOrderItemManager.RetrieveSpecialOrderItems();
            
            var newItem = new SpecialItem
            {
                SpecialOrderItemID = items.ElementAt(0).SpecialOrderItemID,
                Name = "",
                Active = items.ElementAt(0).Active

            };
            try
            {
                // Act
                var editResult = _specialOrderItemManager.EditSpecialOrderItem(items.ElementAt(0), newItem);
                Assert.Fail("Expected name empty error");
            }
            catch (Exception)
            {

                // Assert
                Assert.IsTrue(true);
            }


        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Test for the EditSpecialOrderItem method null
        /// </summary>
        [TestMethod]
        public void TestEditSpecialOrderItemNull()
        {
            // Arrange
            List<SpecialItem> items = _specialOrderItemManager.RetrieveSpecialOrderItems();
            SpecialItem newItem = null;
            try
            {
                // Act
                var editResult = _specialOrderItemManager.EditSpecialOrderItem(items.ElementAt(0), newItem);
                Assert.Fail("Expected item null error");
            }
            catch (Exception)
            {

                // Assert
                Assert.IsTrue(true);
            }


        }
    }
}
