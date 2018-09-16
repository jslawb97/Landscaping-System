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
    [TestClass]
    public class ResupplyOrderLineManagerTests
    {
        private ResupplyOrderLineManager _resupplyOrderLineManager;

        [TestInitialize]
        public void TestSetup()
        {
            _resupplyOrderLineManager = new ResupplyOrderLineManager(new ResupplyOrderLineAccessorMock());
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID value")]
        public void TestCreateResupplyOrderLineBadResupplyOrderIDValue()
        {
            // arrange
            bool returnedNewResupplyOrderLine;
            ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();
            resupplyOrderLine.Price = 19.95M;
            resupplyOrderLine.Quantity = 2;
            resupplyOrderLine.ResupplyOrderID = Constants.IDSTARTVALUE - 1;
            resupplyOrderLine.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            returnedNewResupplyOrderLine = _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID value")]
        public void TestCreateResupplyOrderLineBadSupplyItemIDValue()
        {
            // arrange
            bool returnedNewResupplyOrderLine;
            ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();
            resupplyOrderLine.Price = 19.95M;
            resupplyOrderLine.Quantity = 2;
            resupplyOrderLine.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrderLine.SupplyItemID = Constants.IDSTARTVALUE - 1;

            // act
            returnedNewResupplyOrderLine = _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad Quantity value")]
        public void TestCreateResupplyOrderLineBadQuantityValue()
        {
            // arrange
            bool returnedNewResupplyOrderLine;
            ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();
            resupplyOrderLine.Price = 19.95M;
            resupplyOrderLine.Quantity = -5;
            resupplyOrderLine.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrderLine.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            returnedNewResupplyOrderLine = _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);
        }

        [TestMethod]
        public void TestCreateResupplyOrderLineGood()
        {
            // arrange
            bool returnedNewResupplyOrderLine;
            ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();
            resupplyOrderLine.Price = 19.95M;
            resupplyOrderLine.Quantity = 2;
            resupplyOrderLine.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrderLine.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            returnedNewResupplyOrderLine = _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);

            // asssert
            Assert.AreEqual(true, returnedNewResupplyOrderLine);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "No data found")]
        public void TestCreateResupplyOrderLineNoDataFound()
        {
            // arrange
            bool returnedNewResupplyOrderLine;
            ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();
            resupplyOrderLine.Price = 19.95M;
            resupplyOrderLine.Quantity = 2;
            resupplyOrderLine.ResupplyOrderID = Constants.IDSTARTVALUE * 500;
            resupplyOrderLine.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            returnedNewResupplyOrderLine = _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);
        }

        [TestMethod]
        public void TestRetrieveResupplyOrderLineDetailListByResupplyOrderIDGood()
        {
            // arrange
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE;

            // act
            resupplyOrderLineDetailList = _resupplyOrderLineManager.RetrieveResupplyOrderLineDetailListByResupplyOrderID(resupplyOrder.ResupplyOrderID);

            // assert
            Assert.AreEqual(1, resupplyOrderLineDetailList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "No data found")]
        public void TestRetrieveResupplyOrderLineDetailListByResupplyOrderIDNoDataFound()
        {
            // arrange
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
            ResupplyOrder resupplyOrder = new ResupplyOrder();
            resupplyOrder.ResupplyOrderID = Constants.IDSTARTVALUE * 500;

            // act
            resupplyOrderLineDetailList = _resupplyOrderLineManager.RetrieveResupplyOrderLineDetailListByResupplyOrderID(resupplyOrder.ResupplyOrderID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID Value")]
        public void TestEditResupplyOrderLineBadResupplyOrderLineIDValue()
        {
            // arrange
            bool editSuccessful = false;
            var oldResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            oldResupplyOrderLineDetail.NameOfItem = "ValidItemName";
            oldResupplyOrderLineDetail.Price = 5M;
            oldResupplyOrderLineDetail.Quantity = 5;
            oldResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;
            var newResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            newResupplyOrderLineDetail.NameOfItem = "ValidEditedItemName";
            newResupplyOrderLineDetail.Price = 5M;
            newResupplyOrderLineDetail.Quantity = 10;
            newResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE - 1;
            newResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            editSuccessful = _resupplyOrderLineManager.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID Value")]
        public void TestEditResupplyOrderLineBadResupplyOrderIDValue()
        {
            // arrange
            bool editSuccessful = false;
            var oldResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            oldResupplyOrderLineDetail.NameOfItem = "ValidItemName";
            oldResupplyOrderLineDetail.Price = 5M;
            oldResupplyOrderLineDetail.Quantity = 5;
            oldResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;
            var newResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            newResupplyOrderLineDetail.NameOfItem = "ValidEditedItemName";
            newResupplyOrderLineDetail.Price = 5M;
            newResupplyOrderLineDetail.Quantity = 10;
            newResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE - 1;
            newResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            editSuccessful = _resupplyOrderLineManager.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID Value")]
        public void TestEditResupplyOrderLineBadSupplyItemIDValue()
        {
            // arrange
            bool editSuccessful = false;
            var oldResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            oldResupplyOrderLineDetail.NameOfItem = "ValidItemName";
            oldResupplyOrderLineDetail.Price = 5M;
            oldResupplyOrderLineDetail.Quantity = 5;
            oldResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;
            var newResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            newResupplyOrderLineDetail.NameOfItem = "ValidEditedItemName";
            newResupplyOrderLineDetail.Price = 5M;
            newResupplyOrderLineDetail.Quantity = 10;
            newResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE - 1;

            // act
            editSuccessful = _resupplyOrderLineManager.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Bad ID Value")]
        public void TestEditResupplyOrderLineBadQuantityValue()
        {
            // arrange
            bool editSuccessful = false;
            var oldResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            oldResupplyOrderLineDetail.NameOfItem = "ValidItemName";
            oldResupplyOrderLineDetail.Price = 5M;
            oldResupplyOrderLineDetail.Quantity = 5;
            oldResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;
            var newResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            newResupplyOrderLineDetail.NameOfItem = "ValidEditedItemName";
            newResupplyOrderLineDetail.Price = 5M;
            newResupplyOrderLineDetail.Quantity = -10;
            newResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            editSuccessful = _resupplyOrderLineManager.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestEditResupplyOrderLineNoEditInDatabase()
        {
            // arrange
            bool editSuccessful = false;
            var oldResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            oldResupplyOrderLineDetail.NameOfItem = "ValidItemName";
            oldResupplyOrderLineDetail.Price = 5M;
            oldResupplyOrderLineDetail.Quantity = 5;
            oldResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE + 1;
            oldResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;
            var newResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            newResupplyOrderLineDetail.NameOfItem = "ValidEditedItemName";
            newResupplyOrderLineDetail.Price = 5M;
            newResupplyOrderLineDetail.Quantity = 10;
            newResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            editSuccessful = _resupplyOrderLineManager.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail);
        }

        [TestMethod]
        public void TestEditResupplyOrderLineGood()
        {
            // arrange
            bool editSuccessful = false;
            var oldResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            oldResupplyOrderLineDetail.NameOfItem = "ValidItemName";
            oldResupplyOrderLineDetail.Price = 5M;
            oldResupplyOrderLineDetail.Quantity = 5;
            oldResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            oldResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;
            var newResupplyOrderLineDetail = new ResupplyOrderLineDetail();
            newResupplyOrderLineDetail.NameOfItem = "ValidEditedItemName";
            newResupplyOrderLineDetail.Price = 5M;
            newResupplyOrderLineDetail.Quantity = 10;
            newResupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            newResupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act
            editSuccessful = _resupplyOrderLineManager.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail);

            // assert
            Assert.AreEqual(true, editSuccessful);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestDeleteResupplyOrderLineNotFound()
        {
            // arrange
            bool deleteSuccessful = false;
            var resupplyOrderLineDetail = new ResupplyOrderLineDetail();
            resupplyOrderLineDetail.NameOfItem = "ValidItemName";
            resupplyOrderLineDetail.Price = 5M;
            resupplyOrderLineDetail.Quantity = 5;
            resupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE * 500;
            resupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act 
            deleteSuccessful = _resupplyOrderLineManager.DeleteResupplyOrderLineByResupplyOrderLineID(resupplyOrderLineDetail.ResupplyOrderLineID);
        }

        [TestMethod]
        public void TestDeleteResupplyOrderLineGood()
        {
            // arrange
            bool deleteSuccessful = false;
            var resupplyOrderLineDetail = new ResupplyOrderLineDetail();
            resupplyOrderLineDetail.NameOfItem = "ValidItemName";
            resupplyOrderLineDetail.Price = 5M;
            resupplyOrderLineDetail.Quantity = 5;
            resupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            resupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act 
            deleteSuccessful = _resupplyOrderLineManager.DeleteResupplyOrderLineByResupplyOrderLineID(resupplyOrderLineDetail.ResupplyOrderLineID);

            // assert
            Assert.AreEqual(true, deleteSuccessful);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error")]
        public void TestDeleteResupplyOrderLineByResupplyOrderIDNotFound()
        {
            // arrange
            bool deleteSuccessful = false;
            var resupplyOrderLineDetail = new ResupplyOrderLineDetail();
            resupplyOrderLineDetail.NameOfItem = "ValidItemName";
            resupplyOrderLineDetail.Price = 5M;
            resupplyOrderLineDetail.Quantity = 5;
            resupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE * 500;
            resupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            resupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act 
            deleteSuccessful = _resupplyOrderLineManager.DeleteResupplyOrderLineByResupplyOrderID(resupplyOrderLineDetail.ResupplyOrderID);
        }

        [TestMethod]
        public void TestDeleteResupplyOrderLineByResupplyOrderIDGood()
        {
            // arrange
            bool deleteSuccessful = false;
            var resupplyOrderLineDetail = new ResupplyOrderLineDetail();
            resupplyOrderLineDetail.NameOfItem = "ValidItemName";
            resupplyOrderLineDetail.Price = 5M;
            resupplyOrderLineDetail.Quantity = 5;
            resupplyOrderLineDetail.ResupplyOrderID = Constants.IDSTARTVALUE;
            resupplyOrderLineDetail.ResupplyOrderLineID = Constants.IDSTARTVALUE;
            resupplyOrderLineDetail.SupplyItemID = Constants.IDSTARTVALUE;

            // act 
            deleteSuccessful = _resupplyOrderLineManager.DeleteResupplyOrderLineByResupplyOrderID(resupplyOrderLineDetail.ResupplyOrderID);

            // assert
            Assert.AreEqual(true, deleteSuccessful);
        }
    }
}
