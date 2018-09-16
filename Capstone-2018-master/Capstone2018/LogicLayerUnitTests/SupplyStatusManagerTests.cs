using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using DataObjects;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class SupplyStatusManagerTests
    {
        private SupplyStatusManager _supplyStatusManager;

        [TestInitialize]
        public void TestSetup()
        {
            _supplyStatusManager = new SupplyStatusManager(new SupplyStatusAccessorMock());
        }
        
        /// <summary>
        /// Weston Olund
        /// 2018/05/04
        /// Method to test retrieve supply status list
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplyStatusList()
        {
            // arrange
            List<string> supplyStatusList;
            
            // act
            supplyStatusList = _supplyStatusManager.RetrieveSupplyStatusList();

            // assert
            Assert.AreEqual(2, supplyStatusList.Count);
        }
    }
}
