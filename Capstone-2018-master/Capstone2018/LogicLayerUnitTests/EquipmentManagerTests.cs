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
    public class EquipmentManagerTests
    {
        private IEquipmentManager _equipmentManager;

        [TestInitialize]
        public void TestSetup()
        {
            _equipmentManager = new EquipmentManager(new EquipmentAccessorMock());
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018-05-03
        /// 
        /// Test method to retrieve list of all equipment
        /// </summary>
        [TestMethod]
        public void TestRetrieveEquipmentList()
        {
            // arrange
            List<Equipment> equipmentList = new List<Equipment>();

            // act
            equipmentList = _equipmentManager.RetrieveEquipmentList();


            // assert
            Assert.AreEqual(3, equipmentList.Count());
        }

        [TestMethod]
        public void TestDeactivateEquipmentByID()
        {
            // arrange
            bool result = false;
            List<Equipment> equipmentList = new List<Equipment>();

            // act
            result = _equipmentManager.DeactivateEquipmentByID(1000001);


            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/03
        /// 
        /// Test method to reactivate equipment
        /// </summary>
        [TestMethod]
        public void TestReactivateEquipmentByID()
        {
            // arrange
            bool result = false;
            List<Equipment> equipmentList = new List<Equipment>();

            // act
            result = _equipmentManager.ReactivateEquipmentByID(1000002);


            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/03
        /// 
        /// Unit Test to test create equipment
        /// </summary>
        [TestMethod]
        public void TestCreateEquipment()
        {
            // arrange
            bool result = false;
            Equipment equipment = new Equipment();
            equipment.EquipmentTypeID = "Vehicle";
            equipment.Name = "Bulldozer";
            equipment.MakeModelID = 1000002;
            equipment.DatePurchased = new DateTime(2008, 12, 25);
            equipment.DateLastRepaired = null;
            equipment.PriceAtPurchase = 38000.00M;
            equipment.CurrentValue = 17000.00M;
            equipment.WarrantyUntil = new DateTime(2016, 12, 25);
            equipment.EquipmentStatusID = "normal";
            equipment.EquipmentDetails = "Fork lift for the warhouse";

            // act
            result = _equipmentManager.CreateEquipment(equipment);

            //assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/03
        /// 
        /// Unit Test to test edit equipment
        /// </summary>
        [TestMethod]
        public void TestEditEquipment()
        {
            //arrange
            bool result = false;
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000001;
            oldEquipment.EquipmentTypeID = "Vehicle";
            oldEquipment.Name = "Bulldozer";
            oldEquipment.MakeModelID = 1000002;
            oldEquipment.DatePurchased = new DateTime(2008, 12, 25);
            oldEquipment.DateLastRepaired = null;
            oldEquipment.PriceAtPurchase = 38000.00M;
            oldEquipment.CurrentValue = 17000.00M;
            oldEquipment.WarrantyUntil = new DateTime(2016, 12, 25);
            oldEquipment.EquipmentStatusID = "normal";
            oldEquipment.EquipmentDetails = "Fork lift for the warhouse";
            oldEquipment.Active = true;
            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000001;
            newEquipment.EquipmentTypeID = "Vehicle.";
            newEquipment.Name = "Bulldozer.";
            newEquipment.MakeModelID = 1000003;
            newEquipment.DatePurchased = new DateTime(2008, 12, 26);
            newEquipment.DateLastRepaired = null;
            newEquipment.PriceAtPurchase = 38001.00M;
            newEquipment.CurrentValue = 17001.00M;
            newEquipment.WarrantyUntil = new DateTime(2016, 12, 26);
            newEquipment.EquipmentStatusID = "normal.";
            newEquipment.EquipmentDetails = "Fork lift for the warhouse.";
            newEquipment.Active = true;

            //act
            result = _equipmentManager.EditEquipment(oldEquipment, newEquipment);

            //assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/06
        /// </summary>
        [TestMethod]
        public void TestRetrieveEquipmentListByTypeAndAvailability()
        {
            //arrange
            EquipmentType equipmentType = new EquipmentType { EquipmentTypeID = "tractor", Active = true};

            //act
            List<Equipment> equipmentList = _equipmentManager.RetrieveEquipmentListByTypeAndAvailability(equipmentType, new DateTime(1919, 1, 1), new DateTime(1919, 1, 1));

            //assert
            Assert.IsNotNull(equipmentList);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a null equipment type throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment type is null.")]
        public void TestCreateEquipmentEquipmentTypeNull()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = null;
            equipment.Name = "Best Tractor";
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018-04-04);
            equipment.DateLastRepaired = new DateTime(2018-04-04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = "Fixed";
            equipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a null equipment type throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment type is null.")]
        public void TestEditEquipmentEquipmentTypeNull()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = null;
            newEquipment.Name = "Best Tractor";
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = "Fixed";
            newEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a too long equipment type throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment type is longer than 100.")]
        public void TestCreateEquipmentEquipmentTypeLongerThan100()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = stringBuilder(101);
            equipment.Name = "Best Tractor";
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = "Fixed";
            equipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a too long equipment type throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment type is too long.")]
        public void TestEditEquipmentEquipmentTypeLongerThan100()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = stringBuilder(101);
            newEquipment.Name = "Best Tractor";
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = "Fixed";
            newEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a null name throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name is null.")]
        public void TestCreateEquipmentNameIsNull()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = "Tractor";
            equipment.Name = null;
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = "Fixed";
            equipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a null name throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name is null.")]
        public void TestEditEquipmentNameIsNull()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = "Tractor";
            newEquipment.Name = null;
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = "Fixed";
            newEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a too long name throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name is too long.")]
        public void TestCreateEquipmentNameGreaterThan100()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = "Tractor";
            equipment.Name = stringBuilder(101);
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = "Fixed";
            equipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a too long name throws an application exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name is too long.")]
        public void TestEditEquipmentNameGreaterThan100()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = "Tractor";
            newEquipment.Name = stringBuilder(101);
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = "Fixed";
            newEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a null equipment status throws an application
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment status is null.")]
        public void TestCreateEquipmentEquipmentStatusNull()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = "Tractor";
            equipment.Name = "Best Tractor";
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = null;
            equipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a null equipmenet status throws an exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment status is null.")]
        public void TestEditEquipmentEquipmentStatusNull()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = "Tractor";
            newEquipment.Name = "Best Tractor";
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = null;
            newEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a too long equipment status throws an application
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment status is too long.")]
        public void TestCreateEquipmentEquipmentStatusGreaterThan100()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = "Tractor";
            equipment.Name = "Best Tractor";
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = stringBuilder(101);
            equipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having a too long equipmenet status throws an exception
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Equipment Status is too long.")]
        public void TestEditEquipmentEquipmentStatusLongerThan100()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = "Tractor";
            newEquipment.Name = "Best Tractor";
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = stringBuilder(101);
            newEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having null details throws an application exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Details are null.")]
        public void TestCreateEquipmentDetailsNull()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = "Tractor";
            equipment.Name = "Best Tractor";
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = "Fixed";
            equipment.EquipmentDetails = null;
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having null details throws an application exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Details are null.")]
        public void TestEditEquipmentDetailsNull()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = "Tractor";
            newEquipment.Name = "Best Tractor";
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = "Fixed";
            newEquipment.EquipmentDetails = null;
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having too long details throws an application exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Details are too long.")]
        public void TestCreateEquipmentDetailsLongerThan1000()
        {
            Equipment equipment = new Equipment();
            equipment.EquipmentID = 1000004;
            equipment.EquipmentTypeID = "Tractor";
            equipment.Name = "Best Tractor";
            equipment.MakeModelID = 1000000;
            equipment.DatePurchased = new DateTime(2018 - 04 - 04);
            equipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            equipment.PriceAtPurchase = 10.00M;
            equipment.CurrentValue = 10.00M;
            equipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            equipment.EquipmentStatusID = "Fixed";
            equipment.EquipmentDetails = stringBuilder(1001);
            equipment.Active = true;

            _equipmentManager.CreateEquipment(equipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Test method to make sure having too long details throws an application exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Details are too long.")]
        public void TestEditEquipmentDetailsLongerThan1000()
        {
            Equipment oldEquipment = new Equipment();
            oldEquipment.EquipmentID = 1000004;
            oldEquipment.EquipmentTypeID = "Tractor";
            oldEquipment.Name = "Best Tractor";
            oldEquipment.MakeModelID = 1000000;
            oldEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            oldEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            oldEquipment.PriceAtPurchase = 10.00M;
            oldEquipment.CurrentValue = 10.00M;
            oldEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            oldEquipment.EquipmentStatusID = "Fixed";
            oldEquipment.EquipmentDetails = "We got this tractor broken for $10 and fixed it in 1 day";
            oldEquipment.Active = true;

            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = 1000004;
            newEquipment.EquipmentTypeID = "Tractor";
            newEquipment.Name = "Best Tractor";
            newEquipment.MakeModelID = 1000000;
            newEquipment.DatePurchased = new DateTime(2018 - 04 - 04);
            newEquipment.DateLastRepaired = new DateTime(2018 - 04 - 04);
            newEquipment.PriceAtPurchase = 10.00M;
            newEquipment.CurrentValue = 10.00M;
            newEquipment.WarrantyUntil = new DateTime(2018 - 04 - 04);
            newEquipment.EquipmentStatusID = "Fixed";
            newEquipment.EquipmentDetails = stringBuilder(1001);
            newEquipment.Active = true;

            _equipmentManager.EditEquipment(oldEquipment, newEquipment);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018-05-04
        /// 
        /// Helper method to build string
        /// </summary>
        /// <param name="stringLength"></param>
        /// <returns></returns>
        public string stringBuilder(int stringLength)
        {
            string stringToReturn = "";
            for(int i = 0; i < stringLength; i++)
            {
                stringToReturn += "X";
            }
            return stringToReturn;
        }
    }
}

    

