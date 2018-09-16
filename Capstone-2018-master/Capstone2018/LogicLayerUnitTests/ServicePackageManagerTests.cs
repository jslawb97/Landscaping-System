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
    /// Created 2018/02/22
    /// 
    /// Test class for IServicePackageManager classes
    /// </summary>
    [TestClass]
    public class ServicePackageManagerTests
    {
        private IServicePackageManager _servicePackageManager;

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Test setup
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            //_servicePackageManager = new ServicePackageManager(new ServicePackageAccessorMock());
            _servicePackageManager = new ServicePackageManager(new ServicePackageAccessorMock());
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2/22/2018
        /// 
        /// Method that verifies retrieve service package returns correct number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveServiceList()
        {

            // arrange
            List<ServicePackage> serviceList;

            // act
            serviceList = _servicePackageManager.RetrieveServicePackageList();

            // assert
            Assert.AreEqual(3, serviceList.Count);
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing adding a service package to a data store
        /// </summary>
        [TestMethod]
        public void TestAddServicePackage()
        {
            // arrange
            var servicePackage = new ServicePackage { Name = "TestName", Description = "TestDescription", Active = true };

            // act
            var result = _servicePackageManager.AddServicePackage(servicePackage);

            // assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing adding a service package accross a data store: empty name
        /// </summary>
        [TestMethod]
        public void TestAddServicePackageNameEmpty()
        {
            // arrange
            var servicePackage = new ServicePackage { Name = "TestName", Description = "TestDescription", Active = true };

            try
            {
                // act
                var results = _servicePackageManager.AddServicePackage(servicePackage);
                Assert.Fail("Expected empty name error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing adding a service package accross a data store: name too long
        /// </summary>
        [TestMethod]
        public void TestAddServicePackageNameTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string name = new string(chars);
            var servicePackage = new ServicePackage { Name = name, Description = "TestDescription", Active = true };

            try
            {
                // act
                var results = _servicePackageManager.AddServicePackage(servicePackage);
                Assert.Fail("Expected empty name error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }



        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing adding a service package accross a data store: empty description
        /// </summary>
        [TestMethod]
        public void TestAddServicePackageDescriptionEmpty()
        {
            // arrange
            var servicePackage = new ServicePackage { Name = "TestName", Description = "", Active = true };

            try
            {
                // act
                var results = _servicePackageManager.AddServicePackage(servicePackage);
                Assert.Fail("Expected empty description error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing adding a service package accross a data store: description too long
        /// </summary>
        [TestMethod]
        public void TestAddServicePackageDescriptionTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXDESCRIPTIONLENGTH + 1];
            string description = new string(chars);
            var servicePackage = new ServicePackage { Name = "Test", Description = description, Active = true };

            try
            {
                // act
                var results = _servicePackageManager.AddServicePackage(servicePackage);
                Assert.Fail("Expected empty description error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing adding a service package accross a data store: null
        /// </summary>
        [TestMethod]
        public void TestAddServicePackageNull()
        {
            // arrange
            var chars = new char[Constants.MAXDESCRIPTIONLENGTH + 1];
            string description = new string(chars);
            ServicePackage servicePackage = null;

            try
            {
                // act
                var results = _servicePackageManager.AddServicePackage(servicePackage);
                Assert.Fail("Expected Package null error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }




        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing editing a service package accross a data store: empty name
        /// </summary>
        [TestMethod]
        public void TestEditServicePackageNameEmpty()
        {
            // arrange
            var oldServicePackage = new ServicePackage { ServicePackageID=1000000, Name = "TestName", Description = "TestDescription", Active = true };
            var newServicePackage = new ServicePackage { Name = "", Description = "TestDescriptionEDITED", Active = false };

            try
            {
                // act
                var results = _servicePackageManager.EditServicePackage(oldServicePackage, newServicePackage);
                Assert.Fail("Expected empty name error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
            

            
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing editing a service package accross a data store: name too long
        /// </summary>
        [TestMethod]
        public void TestEditServicePackageNameTooLong()
        {
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string name = new string(chars);
            // arrange
            var oldServicePackage = new ServicePackage { ServicePackageID = 1000000, Name = "TestName", Description = "TestDescription", Active = true };
            var newServicePackage = new ServicePackage { Name = name, Description = "TestDescriptionEDITED", Active = false };

            try
            {
                // act
                var results = _servicePackageManager.EditServicePackage(oldServicePackage, newServicePackage);
                Assert.Fail("Expected name too long error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }



        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing editing a service package accross a data store: description too long
        /// </summary>
        [TestMethod]
        public void TestEditServicePackageDescriptionTooLong()
        {
            var chars = new char[Constants.MAXDESCRIPTIONLENGTH + 1];
            string description = new string(chars);
            // arrange
            var oldServicePackage = new ServicePackage { ServicePackageID = 1000000, Name = "TestName", Description = "TestDescription", Active = true };
            var newServicePackage = new ServicePackage { Name = "Test", Description = description, Active = false };

            try
            {
                // act
                var results = _servicePackageManager.EditServicePackage(oldServicePackage, newServicePackage);
                Assert.Fail("Expected description too long error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing editing a service package accross a data store: empty name
        /// </summary>
        [TestMethod]
        public void TestEditServicePackageDescriptionEmpty()
        {
            // arrange
            var oldServicePackage = new ServicePackage { ServicePackageID = 1000000, Name = "TestName", Description = "TestDescription", Active = true };
            var newServicePackage = new ServicePackage { Name = "Test", Description = "", Active = false };

            try
            {
                // act
                var results = _servicePackageManager.EditServicePackage(oldServicePackage, newServicePackage);
                Assert.Fail("Expected empty description error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Testing editing a service package accross a data store: null
        /// </summary>
        [TestMethod]
        public void TestEditServicePackageNull()
        {
            // arrange
            var oldServicePackage = new ServicePackage { ServicePackageID = 1000000, Name = "TestName", Description = "TestDescription", Active = true };
            ServicePackage newServicePackage = null;

            try
            {
                // act
                var results = _servicePackageManager.EditServicePackage(oldServicePackage, newServicePackage);
                Assert.Fail("Expected Package null error");
            }
            catch (Exception)
            {

                // assert
                Assert.IsTrue(true);
            }



        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Testing the get all service package method. Should return not null list
        /// </summary>
        [TestMethod]
        public void TestRetrieveServicePackageList()
        {
            //arrange
            List<ServicePackage> list = null;
            //act
            try
            {
                list = _servicePackageManager.RetrieveServicePackageList();

            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
            //assert
            Assert.IsNotNull(list);

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/12
        /// 
        /// Method to verify that DeactivateServicePackage deactivates a ServicePackage
        /// </summary>
        [TestMethod]
        public void TestDeactivateServicePackage()
        {
            // Arrange
            bool result = false;

            // Act
            result = _servicePackageManager.DeactivateServicePackage(1000000);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
