using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using Logic;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class MaintenanceChecklistManagerTests
    {
        private IMaintenanceChecklistManager _maintenanceChecklistManager;

        [TestInitialize]
        public void TestSetup()
        {
            _maintenanceChecklistManager = new MaintenanceChecklistManager(new MaintenanceChecklistAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        /// 
        /// Method that verifies RetrieveMaintenanceChecklistList returns
        /// correct number of items
        /// </summary>
        /// <remarks>QA Jayden T 4/27/18</remarks>
        [TestMethod]
        public void TestRetrieveMaintenanceChecklistList()
        {
            // arrange
            List<MaintenanceChecklist> maintenanceChecklistList;

            // act
            maintenanceChecklistList = _maintenanceChecklistManager.RetrieveMaintenanceChecklistList();

            // assert
            Assert.AreEqual(3, maintenanceChecklistList.Count());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _maintenanceChecklistManager = null;
        }

    }
}
