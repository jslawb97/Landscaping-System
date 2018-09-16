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
    public class ServiceOfferingItemManagerTests
    {

        private IServiceOfferingItemManager _serviceOfferingItemManager;

        [TestInitialize]
        public void TestSetup()
        {
            _serviceOfferingItemManager = new ServiceOfferingItemManager(new ServiceOfferingItemAccessorMock());
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/06
        /// 
        /// Method to test if service offering item is created with a valid service item and offering ids
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        public void TestCreateServiceOfferingItemSuccess()
        {
            // Arrange
            ServiceOfferingItem serviceOfferingItem = new ServiceOfferingItem()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE,
                ServiceItemID = Constants.IDSTARTVALUE
            };
            int result = 0;

            // Act
            result = _serviceOfferingItemManager.CreateServiceOfferingItem(serviceOfferingItem);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Service Item ID is given
        /// when creating a service offering item
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateServiceOfferingItemInvalidServiceItemID()
        {
            // Arrange
            ServiceOfferingItem serviceOfferingItem = new ServiceOfferingItem()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE,
                ServiceItemID = Constants.IDSTARTVALUE - 5
            };
            int result = 0;

            // Act
            result = _serviceOfferingItemManager.CreateServiceOfferingItem(serviceOfferingItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Service Offering ID is given
        /// when creating a service offering item
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateServiceOfferingItemInvalidServiceOfferingID()
        {
            // Arrange
            ServiceOfferingItem serviceOfferingItem = new ServiceOfferingItem()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE - 5,
                ServiceItemID = Constants.IDSTARTVALUE
            };
            int result = 0;

            // Act
            result = _serviceOfferingItemManager.CreateServiceOfferingItem(serviceOfferingItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if service offering item is deleted when given valid service item and offering ids
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        public void TestDeleteServiceOfferingItemSuccess()
        {
            // Arrange
            ServiceOfferingItem serviceOfferingItem = new ServiceOfferingItem()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE,
                ServiceItemID = Constants.IDSTARTVALUE
            };
            int result = 0;

            // Act
            result = _serviceOfferingItemManager.DeleteServiceOfferingItem(serviceOfferingItem);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Service Item ID is given
        /// when deleting a service offering item
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteServiceOfferingItemInvalidServiceItemID()
        {
            // Arrange
            ServiceOfferingItem serviceOfferingItem = new ServiceOfferingItem()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE,
                ServiceItemID = Constants.IDSTARTVALUE - 5
            };
            int result = 0;

            // Act
            result = _serviceOfferingItemManager.DeleteServiceOfferingItem(serviceOfferingItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Service Offering ID is given
        /// when deleting a service offering item
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestDeleteServiceOfferingItemInvalidServiceOfferingID()
        {
            // Arrange
            ServiceOfferingItem serviceOfferingItem = new ServiceOfferingItem()
            {
                ServiceOfferingID = Constants.IDSTARTVALUE - 5,
                ServiceItemID = Constants.IDSTARTVALUE
            };
            int result = 0;

            // Act
            result = _serviceOfferingItemManager.DeleteServiceOfferingItem(serviceOfferingItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if service offering item is found when given valid service offering id
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        public void TestRetrieveServiceOfferingItemByIDSuccess()
        {
            // Arrange
            int serviceOfferingID = Constants.IDSTARTVALUE;
            List<ServiceOfferingItem> result = null;
            List<ServiceOfferingItem> expected = new List<ServiceOfferingItem>();
            expected.Add(new ServiceOfferingItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE,
                ServiceOfferingID = Constants.IDSTARTVALUE
            });

            // Act
            result = _serviceOfferingItemManager.RetrieveServiceOfferingItemByID(serviceOfferingID);

            // Assert
            Assert.AreEqual(expected.Count, result.Count);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Service Offering ID is given
        /// when retrieving service offering items
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveServiceOfferingItemByIDInvalidServiceOfferingID()
        {
            // Arrange
            int serviceOfferingID = Constants.IDSTARTVALUE - 5;
            List<ServiceOfferingItem> result = null;
            List<ServiceOfferingItem> expected = new List<ServiceOfferingItem>();
            expected.Add(new ServiceOfferingItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE,
                ServiceOfferingID = Constants.IDSTARTVALUE
            });

            // Act
            result = _serviceOfferingItemManager.RetrieveServiceOfferingItemByID(serviceOfferingID);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if service offering item is found when given valid service Item id
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        public void TestRetrieveServiceOfferingItemByServiceItemIDSuccess()
        {
            // Arrange
            int serviceItemID = Constants.IDSTARTVALUE;
            List<ServiceOfferingItem> result = null;
            List<ServiceOfferingItem> expected = new List<ServiceOfferingItem>();
            expected.Add(new ServiceOfferingItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE,
                ServiceOfferingID = Constants.IDSTARTVALUE
            });

            // Act
            result = _serviceOfferingItemManager.RetrieveServiceOfferingItemByServiceItemID(serviceItemID);

            // Assert
            Assert.AreEqual(expected.Count, result.Count);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/23
        /// 
        /// Method to test if exception is thrown if invalid Service Item ID is given
        /// when retrieving service offering items
        /// </summary>
        /// <remarks> QA Jayden Tollefson 5/4/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRetrieveServiceOfferingItemByServiceItemIDInvalidServiceItemID()
        {
            // Arrange
            int serviceItemID = Constants.IDSTARTVALUE - 5;
            List<ServiceOfferingItem> result = null;
            List<ServiceOfferingItem> expected = new List<ServiceOfferingItem>();
            expected.Add(new ServiceOfferingItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE,
                ServiceOfferingID = Constants.IDSTARTVALUE
            });

            // Act
            result = _serviceOfferingItemManager.RetrieveServiceOfferingItemByServiceItemID(serviceItemID);
        }

        /// <summary>
        /// Jayden Tollefson QA
        /// Created 5/4/2018
        /// 
        /// Method to make _serviceOfferingItemManager null
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            _serviceOfferingItemManager = null;
        }
    }
}
