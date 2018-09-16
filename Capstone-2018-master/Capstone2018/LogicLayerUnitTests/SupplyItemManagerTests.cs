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
    /// Tests for SupplyItemManager
    /// </summary>
    [TestClass]
    public class SupplyItemManagerTests
    {
        private SupplyItemManager _supplyItemManager;
        private SupplyItem _testItem;

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Setup for SupplyItem Manager Tests
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {

            _supplyItemManager = new SupplyItemManager();
            //_supplyItemManager = new SupplyItemManager(new SupplyAccessorMocks());
            _testItem = new SupplyItem
            {

                Name = "TestSupplyItem",
                Description = "Test Supply Description",
                Location = "Here",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };


        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Tests Retrieving SupplyItem lists
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplyItemList()
        {
            // arrange
            List<SupplyItem> supplyItemList;
            //act
            supplyItemList = _supplyItemManager.RetrieveSupplyItemList();

            //assert
            Assert.IsNotNull(supplyItemList);
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Test for creating a SupplyItem with good value
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItem()
        {
            // arrange
            _testItem = new SupplyItem
            {

                Name = "Test",
                Description = "Test Supply Description",
                Location = "Here",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);
                Assert.IsTrue(result);
            }
            catch (Exception)
            {
                // assert
                Assert.Fail("Add failed");
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Test for creating a SupplyItem with empty name
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemEmptyName()
        {
            // arrange
            _testItem = new SupplyItem
            {

                Name = "",
                Description = "Test Supply Description",
                Location = "Here",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Empty name should throw error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }
            
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/03/01
        /// 
        /// Test for creating a SupplyItem with name too long
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemNameTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string name = new string(chars);
            _testItem = new SupplyItem
            {

                Name = name,
                Description = "Test Supply Description",
                Location = "Here",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Too long name should throw error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Test for creating a SupplyItem with empty description
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemEmptyDescription()
        {
            // arrange
            _testItem = new SupplyItem
            {

                Name = "Test",
                Description = "",
                Location = "Here",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Empty description should throw error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/03/01
        /// 
        /// Test for creating a SupplyItem with description too long
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemDescriptionTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXDESCRIPTIONLENGTH + 1];
            string description = new string(chars);
            _testItem = new SupplyItem
            {

                Name = "Test",
                Description = description,
                Location = "Here",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Too long description should throw error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Test for creating a SupplyItem with empty location
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemEmptyLocation()
        {
            // arrange
            _testItem = new SupplyItem
            {

                Name = "Test",
                Description = "Test",
                Location = "",
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Empty location should throw error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/03/01
        /// 
        /// Test for creating a SupplyItem with location too long
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemLocationTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string location = new string(chars);
            _testItem = new SupplyItem
            {

                Name = "Test",
                Description = "test",
                Location = location,
                QuantityInStock = 10,
                ReorderLevel = 5,
                ReorderQuantity = 5,
                Active = false
            };

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Too long location should throw error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/03/01
        /// 
        /// Test for creating a SupplyItem with location too long
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        [TestMethod]
        public void TestCreateSupplyItemNullItem()
        {
            // arrange
            _testItem = null;

            // act
            try
            {
                var result = _supplyItemManager.CreateSupplyItem(_testItem);

                Assert.Fail("Null item should error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Tests the deactivate method for SupplyItems by id
        /// </summary>
        [TestMethod]
        public void TestDeactivateSupplyItemByID()
        {
            //Arrange
            _testItem.SupplyItemID = 1000000;
            // act
            var deactivateResult = _supplyItemManager.DeactivateSupplyItemByID(_testItem.SupplyItemID);

            //assert
            Assert.IsTrue(deactivateResult);
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Tests the deactivate method for SupplyItems by id
        /// </summary>
        [TestMethod]
        public void TestDeactivateSupplyItemByIDBadID()
        {
            //Arrange
            _testItem.SupplyItemID = 100000;
            bool deactivateResult = false;
            try
            {
                // act
                deactivateResult = _supplyItemManager.DeactivateSupplyItemByID(_testItem.SupplyItemID);
            }
            catch (Exception)
            {

                //assert
                Assert.IsTrue(true);
            }
            

           
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Tests edit functionality for Supply Item where name is too long
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemTooLongName()
        {
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string name = new string(chars);
            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            var newItem = new SupplyItem
            {

                Name = name,
                Description = items.ElementAt(0).Description,
                Location = items.ElementAt(0).Location,
                ReorderLevel = items.ElementAt(0).ReorderLevel,
                QuantityInStock = items.ElementAt(0).QuantityInStock,
                ReorderQuantity = items.ElementAt(0).ReorderQuantity

            };
            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Name too long error expected");
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
        /// Tests edit functionality for Supply Item where name is empty
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemEmptyName()
        {
            
            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            var newItem = new SupplyItem
            {

                Name = "",
                Description = items.ElementAt(0).Description,
                Location = items.ElementAt(0).Location,
                ReorderLevel = items.ElementAt(0).ReorderLevel,
                QuantityInStock = items.ElementAt(0).QuantityInStock,
                ReorderQuantity = items.ElementAt(0).ReorderQuantity

            };

            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Name empty error expected");
            }
            catch (Exception)
            {
                //assert
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Tests edit functionality for Supply Item where description is too long
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemTooLongDescription()
        {
            var chars = new char[Constants.MAXDESCRIPTIONLENGTH + 1];
            string description = new string(chars);
            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            var newItem = new SupplyItem
            {

                Name = "Test",
                Description = description,
                Location = items.ElementAt(0).Location,
                ReorderLevel = items.ElementAt(0).ReorderLevel,
                QuantityInStock = items.ElementAt(0).QuantityInStock,
                ReorderQuantity = items.ElementAt(0).ReorderQuantity

            };
            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Description too long error expected");
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
        /// Tests edit functionality for Supply Item where descripiton is empty
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemEmptyDescription()
        {

            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            var newItem = new SupplyItem
            {

                Name = "Test",
                Description = "",
                Location = items.ElementAt(0).Location,
                ReorderLevel = items.ElementAt(0).ReorderLevel,
                QuantityInStock = items.ElementAt(0).QuantityInStock,
                ReorderQuantity = items.ElementAt(0).ReorderQuantity

            };

            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Description empty error expected");
            }
            catch (Exception)
            {
                //assert
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Tests edit functionality for Supply Item where location is too long
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemTooLongLocation()
        {
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string location = new string(chars);
            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            var newItem = new SupplyItem
            {

                Name = "Test",
                Description = "Test",
                Location = location,
                ReorderLevel = items.ElementAt(0).ReorderLevel,
                QuantityInStock = items.ElementAt(0).QuantityInStock,
                ReorderQuantity = items.ElementAt(0).ReorderQuantity

            };
            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Location too long error expected");
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
        /// Tests edit functionality for Supply Item where name is empty
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemEmptyLocation()
        {

            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            var newItem = new SupplyItem
            {

                Name = "Test",
                Description = "Test",
                Location = "",
                ReorderLevel = items.ElementAt(0).ReorderLevel,
                QuantityInStock = items.ElementAt(0).QuantityInStock,
                ReorderQuantity = items.ElementAt(0).ReorderQuantity

            };

            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Description empty error expected");
            }
            catch (Exception)
            {
                //assert
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Tests edit functionality for Supply Item where name is empty
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemNullItem()
        {

            List<SupplyItem> items = _supplyItemManager.RetrieveSupplyItemList();
            SupplyItem newItem = null;

            try
            {
                // act
                var editResult = _supplyItemManager.EditSupplyItem(items.ElementAt(0), newItem);
                Assert.Fail("Null item error expected");
            }
            catch (Exception)
            {
                //assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestRetrieveAllSupplyItemDetail()
        {
            // arrange
            List<SupplyItemDetail> supplyItemDetails = new List<SupplyItemDetail>();

            // act
            supplyItemDetails = _supplyItemManager.RetrieveSupplyItemDetailList();

            // assert
            Assert.AreEqual(supplyItemDetails.Count, 10);
        }


        [TestCleanup]
        public void TestTearDown()
        {
            
        }

    }
}
