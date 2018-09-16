using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using Logic;
using DataAccessMocks;

namespace LogicLayerUnitTests
{

   // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
    [TestClass]
    public class ServiceItemManagerTests 
    {

        private ServiceItemManager _serviceItemManager;

        [TestInitialize]
        public void TestSetup()
        {
            _serviceItemManager = new ServiceItemManager(new ServiceItemAccessorMock());
        }

        [TestMethod]
        public void TestServiceItemList()
        {
            Assert.AreEqual(3, this._serviceItemManager.RetrieveServiceItemList().Count);
        }

        /// <summary>
        /// Amanda Tampir
        /// Created on 2018/02/22
        /// 
        /// Method that verifies deactivate service item by id deactivates an employee
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        [TestMethod]
        public void TestDeactivateServiceItemByID()
        {
            // arrange


            // act
            //_serviceItemManager.DeactivateServiceItemByID(Constants.IDStartValue);
            //serviceItemList = _serviceItem.RetrieveServiceItemByActive(true);

            // assert
            Assert.AreEqual(1, this._serviceItemManager.DeactivateServiceItemByID(Constants.IDSTARTVALUE));
        }
        
        /// <summary>
        /// Zach Murphy
        /// Created on 2018/03/8
        /// 
        /// Verifies the creation of given sample data
        /// </summary>
        [TestMethod]
        public void TestCreateServiceItem() {
            Assert.AreEqual(1, this._serviceItemManager.AddServiceItem(new ServiceItem {
                Name = "New service item Name",
                Description = "New test description."
            }));
        }

        /// <summary>
        /// Zach Murphy
        /// Created on 2018/03/8
        /// 
        /// Verifies that sample data can be edited
        /// </summary>
        /// 
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        [TestMethod]
        public void TestEditServiceItem()
        {
            Assert.AreEqual(1, this._serviceItemManager.EditServiceItemByID(
                this._serviceItemManager.RetrieveServiceItemList().Find(si => si.ServiceItemID.Equals(Constants.IDSTARTVALUE)),
                new ServiceItem {
                    Name = "New Name",
                    Description = "Updated test description."
                }));
        }

        //[TestMethod]
        // public void TestServiceItemByID(){            
        //    Assert.AreEqual(1, this._serviceItemManager.EditServiceItemByID(
        //        this._serviceItemManager.RetrieveServiceItemByID(Constants.IDStartValue),
        //        new ServiceItem
        //        {
        //              
        //              Name = "Upated test name",
        //            Description = "Updated test Description",
        //          ServiceOfferingID = "100000"
        //        }));
        //}


        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        /// Method tests that RetrieveServiceItemList returns the right number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveServiceItemList()
        {
            // arrange
            List<ServiceItem> servItemList;

            // act
            servItemList = _serviceItemManager.RetrieveServiceItemList();

            // assert
            Assert.AreEqual(3, servItemList.Count);
        }



        [TestCleanup]
        public void TestTearDown()
        {
            _serviceItemManager = null;
        }


    }
}
