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
    public class ServiceOfferingManagerTests
    {
        private ServiceOfferingManager _serviceOfferingManager;
        private ServiceOfferingManager _noServiceOfferingManager;
        // A default service offering to be used to test if an item is invalid.
        ServiceOffering _serviceOffering = new ServiceOffering
        {
            ServiceOfferingID = 0,
            ServicePackageID = 0,
            Name = "Name",
            Description = "Description"
        };
        // A default service offering to be used when editing for invalid runs.
        ServiceOffering _newServiceOffering = new ServiceOffering
        {
            ServiceOfferingID = 0,
            ServicePackageID = 0,
            Name = "Name",
            Description = "Description2"
        };

        [TestInitialize]
        public void TestSetup()
        {
            _serviceOfferingManager = new ServiceOfferingManager(new ServiceOfferingAccessorMock());
            _noServiceOfferingManager = new ServiceOfferingManager(new ServiceOfferingAccessorMockFail());

        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/20
        /// 
        /// Method that verifies retrieve service offering list returns correct
        /// list of items.
        /// </summary>
        [TestMethod]
        public void TestRetrieveServiceOfferingListSuccess()
        {
            // arrange
            List<ServiceOffering> serviceOfferings;


            // act
            serviceOfferings = _serviceOfferingManager.RetrieveServiceOfferingList();


            // assert
            Assert.AreEqual(2, serviceOfferings.Count);
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/20
        /// 
        /// Method that verifies retrieve service offering list returns correct
        /// list of items.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveServiceOfferingListFail()
        {
            // arrange
            List<ServiceOffering> serviceOfferings = new List<ServiceOffering>();


            // act
            serviceOfferings = _noServiceOfferingManager.RetrieveServiceOfferingList();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// Tests to make sure CreateServiceOffering returns one
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/26
        /// 
        /// Changed test to actually add a service item to the list and check if it is there.
        /// </summary>
        [TestMethod]
        public void TestCreateServiceOfferingReturnsOne()
        {
            //arrange
            var newServiceOffering = new ServiceOffering()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE + 3,
                Name = "The Best Service Offering Ever™",
                Description = "All you need is in this offering"
            };

            //act
            int result = _serviceOfferingManager.CreateServiceOffering(newServiceOffering);

            //assert
            Assert.AreEqual(1, result);
        }
        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/26
        /// 
        /// Tests to see if item would be added if there is no access to the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateServiceOfferingNoDatabaseAccess()
        {
            //arrange
            var newServiceOffering = new ServiceOffering()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE + 3,
                Name = "The Best Service Offering Ever™",
                Description = "All you need is in this offering"
            };

            //act
            int result = _noServiceOfferingManager.CreateServiceOffering(newServiceOffering);
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/23
        /// 
        /// Tests to see if item would be edited
        /// </summary>
        [TestMethod]
        public void TestEditServiceOfferingReturnsOne()
        {
            //arrange

            //act
            int result = _serviceOfferingManager.EditServiceOffering(_serviceOffering, _newServiceOffering);

            //assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/04/27
        /// 
        /// Tests to see if item would be edited
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestEditServiceOfferingFail()
        {
            //arrange
            ServiceOffering _newBadServiceOffering = new ServiceOffering
            {
                ServiceOfferingID = 0,
                ServicePackageID = 0,
                Name = "",
                Description = ""
            };

            //act
            int result = _serviceOfferingManager.EditServiceOffering(_serviceOffering, _newBadServiceOffering);
        }

        /// <summary>
        /// Noah Davison
        /// Created on 2018/03/08
        /// 
        /// verifies that delete service offering by id deletes a service offering
        /// 
        /// Marshall Sejkora
        /// Updated: 2018/04/20
        /// Formatting to fit others and removal of duplicate
        /// </summary>
        [TestMethod]
        public void TestDeleteServiceOfferingID()
        {
            // arrange

            // act
            bool result = _serviceOfferingManager.DeleteServiceOfferingByID(1000000);

            // assert
            Assert.AreEqual(true, result);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _serviceOfferingManager = null;
        }
    }
}
