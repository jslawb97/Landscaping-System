using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using DataObjects;
using DataAccessMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class MaintenanceRecordTests
    {
        private IMaintenanceRecordManager _maintenanceRecordManager;

        [TestInitialize]
        public void TestSetup()
        {
            _maintenanceRecordManager = new MaintenanceRecordManager(new MaintenanceRecordAccessorMock(), new EquipmentAccessorMock(), new EmployeeAccessorMock());
        }

        /// <summary>
        /// Noah Davison
        /// Created 04/27/2018
        /// 
        /// Test method for retrieve maintenance record detail list
        /// </summary>
        [TestMethod]
        public void TestRetrieveMaintenanceRecordDetailList()
        {
            //arrange
            List<MaintenanceRecordDetail> maintenanceRecordDetailList;

            //act
            maintenanceRecordDetailList = _maintenanceRecordManager.RetrieveMaintenanceRecordDetailList();

            //assert
            Assert.AreEqual(2, maintenanceRecordDetailList.Count());
        }
    }
}