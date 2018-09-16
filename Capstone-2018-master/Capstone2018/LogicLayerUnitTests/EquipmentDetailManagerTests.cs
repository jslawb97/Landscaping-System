using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessMocks;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class EquipmentDetailManagerTests
    {
        private IEquipmentDetailManager _equipmentDetailManager;

        [TestInitialize]
        public void TestSetup()
        {
            _equipmentDetailManager = new EquipmentDetailManager(new EquipmentAccessorMock(), new MakeModelAccessorMock());
        }

        [TestMethod]
        public void TestRetrieveEquipmentViewByActive()
        {
            // arange
            List<EquipmentDetail> equipmentViewList;

            // act
            equipmentViewList = _equipmentDetailManager.RetrieveEquipmentDetailByActive();

            // assert
            Assert.AreEqual(2, equipmentViewList.Count);

            //assert
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018-05-03
        /// 
        /// Test method for RetrieveEquipmentDetailList
        /// Check if it retrieves complete list
        /// </summary>
        [TestMethod]
        public void TestRetrieveEquipmentDetailList()
        {
            // arange
            List<EquipmentDetail> equipmentViewList;

            // act
            equipmentViewList = _equipmentDetailManager.RetrieveEquipmentDetailList();

            // assert
            Assert.AreEqual(3, equipmentViewList.Count);

            //assert
        }
    }
}

