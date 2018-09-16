using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class ResupplyOrderManagerTests
    {
        private ResupplyOrderManager _resupplyOrderManager;

        [TestInitialize]
        public void TestSetup()
        {
            _resupplyOrderManager = new ResupplyOrderManager(new ResupplyOrderAccessorMock());
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID value")]
        public void TestCreateResupplyOrderBadEmployeeIDValue()
        {
            // arrange
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE - 1;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "ValidSupplyStatus";

            // act
            _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Supply Status ID too short")]
        public void TestCreateResupplyOrderSupplyStatusIDTooShort()
        {
            // arrange
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "";

            // act
            _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Supply Status ID too long")]
        public void TestCreateResupplyOrderSupplyStatusIDTooLong()
        {
            // arrange
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            string supplyStatusName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                supplyStatusName += "a";
            }
            resupplyOrder.SupplyStatusID = supplyStatusName;

            // act
            _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        public void TestCreateResupplyOrderGood()
        {
            // arrange
            int returnedNewResupplyOrder;
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "ValidSupplyStatus";

            // act
            returnedNewResupplyOrder = _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);

            // assert
            Assert.IsTrue(Constants.IDSTARTVALUE <= returnedNewResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestCreateResupplyOrderNotCreated()
        {
            // arrange
            int newResupplyOrderCreated;
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE * 500;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "ValidSupplyStatus";

            // act
            newResupplyOrderCreated = _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID value")]
        public void TestEditResupplyOrderBadResupplyOrderID()
        {
            // arrange
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE - 1;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            newResupplyOrder.Date = DateTime.Now;
            newResupplyOrder.SupplyStatusID = "Valid Supply Status";
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID value")]
        public void TestEditResupplyOrderBadEmployeeID()
        {
            // arrange
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE - 1;
            newResupplyOrder.Date = DateTime.Now;
            newResupplyOrder.SupplyStatusID = "Valid Supply Status";
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Resupply order null")]
        public void TestEditResupplyOrderNullSupplyStatus()
        {
            // arrange
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            newResupplyOrder.Date = DateTime.Now;
            newResupplyOrder.SupplyStatusID = null;
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Resupply order too short")]
        public void TestEditResupplyOrderSupplyStatusTooShort()
        {
            // arrange
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            newResupplyOrder.Date = DateTime.Now;
            newResupplyOrder.SupplyStatusID = "";
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Resupply order too long")]
        public void TestEditResupplyOrderSupplyStatusTooLong()
        {
            // arrange
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            newResupplyOrder.Date = DateTime.Now;
            string supplyStatusName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                supplyStatusName += "a";
            }
            newResupplyOrder.SupplyStatusID = supplyStatusName;
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestEditResupplyOrderDatabaseError()
        {
            // arrange
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE * 500;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            newResupplyOrder.Date = DateTime.Now;
            newResupplyOrder.SupplyStatusID = "Valid Supply Status";
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE * 500;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        public void TestEditResupplyOrderGood()
        {
            // arrange
            bool rowsAffected;
            var newResupplyOrder = new ResupplyOrder();
            var oldResuplyOrder = new ResupplyOrder();
            newResupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            newResupplyOrder.Date = DateTime.Now;
            newResupplyOrder.SupplyStatusID = "Valid Supply Status";
            oldResuplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResuplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            oldResuplyOrder.Date = DateTime.Now;
            oldResuplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            rowsAffected = _resupplyOrderManager.EditResupplyOrder(oldResuplyOrder, newResupplyOrder);

            // assert
            Assert.AreEqual(true, rowsAffected);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID value")]
        public void TestRetrieveResupplyOrderByIDBadIDValue()
        {
            // arrange
            var resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE - 1;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.RetrieveResupplyOrderByID(resupplyOrder.ResupplyOrderID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestRetrieveResupplyOrderByIDDatabaseError()
        {
            // arrange
            var resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE * 500;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            _resupplyOrderManager.RetrieveResupplyOrderByID(resupplyOrder.ResupplyOrderID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        public void TestRetrieveResupplyOrderByIDGood()
        {
            // arrange
            var returnedResupplyOrder = new ResupplyOrder();
            var resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrder.EmployeeID = Constants.IDSTARTVALUE;
            resupplyOrder.Date = DateTime.Now;
            resupplyOrder.SupplyStatusID = "Valid Supply Status";

            // act
            returnedResupplyOrder = _resupplyOrderManager.RetrieveResupplyOrderByID(resupplyOrder.ResupplyOrderID);

            // assert
            Assert.AreEqual("Mock supply status 1", returnedResupplyOrder.SupplyStatusID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to test resupply order manager functionality
        /// </summary>
        [TestMethod]
        public void TestRetrieveResupplyOrderListGood()
        {
            // arrange
            List<ResupplyOrder> resupplyList;

            // act
            resupplyList = _resupplyOrderManager.RetrieveResupplyOrderList();

            //assert
            Assert.AreEqual(2, resupplyList.Count);
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to test deleting resupply order by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestDeleteResupplyOrderByIDNoDataDeleted()
        {
            // arrange 
            var result = false;
            int resupplyOrderID = Constants.IDSTARTVALUE * 500;

            // act
            result = _resupplyOrderManager.DeleteResupplyOrderByID(resupplyOrderID);
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to test deleting resupply order by id
        /// </summary>
        [TestMethod]
        public void TestDeleteResupplyOrderByIDGood()
        {
            // arrange 
            var result = false;
            int resupplyOrderID = Constants.IDSTARTVALUE;

            // act
            result = _resupplyOrderManager.DeleteResupplyOrderByID(resupplyOrderID);

            // assert
            Assert.AreEqual(true, result);
        }



        [TestCleanup]
        public void TestTearDown()
        {
            _resupplyOrderManager = null;
        }


    }
}
