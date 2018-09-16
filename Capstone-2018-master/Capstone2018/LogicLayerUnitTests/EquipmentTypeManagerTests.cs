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
    public class EquipmentTypeManagerTests
    {
        private IEquipmentTypeManager _equipmentTypeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _equipmentTypeManager = new EquipmentTypeManager(new EquipmentTypeAccessorMock(), new PrepChecklistAccessorMock(), new InspectionChecklistAccessorMock());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestRetrieveEquipmentTypeList()
        {
            // arrange
            List<EquipmentType> equipTypeList;

            // act
            equipTypeList = _equipmentTypeManager.RetrieveEquipmentTypeList();

            // assert
            Assert.AreEqual(2, equipTypeList.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestEditEquipmentType()
        {
            // arangel
            EquipmentType oldEquipType = new EquipmentType()
            {
                EquipmentTypeID = "new record",
                InspectionChecklistID = 1000001,
                PrepChecklistID = 1000001
            };
            EquipmentType newEquipType = new EquipmentType()
            {
                EquipmentTypeID = "new record",
                InspectionChecklistID = 1000002,
                PrepChecklistID = 1000002
            };

            // act
            bool rowsAffected = _equipmentTypeManager.EditEquipmentType(oldEquipType, newEquipType);

            // assert
            Assert.AreEqual(true, rowsAffected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestCreateEquipmentType()
        {
            // arange
            EquipmentType equipType = new EquipmentType()
            {
                EquipmentTypeID = "Test Equipment Type",
                InspectionChecklistID = 1000001,
                PrepChecklistID = 1000001
            };

            // act
            int rowsAffected = _equipmentTypeManager.CreateEquipmentType(equipType);

            // assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestRetrieveEquipmentTypeDetail()
        {
            EquipmentType e = new EquipmentType()
            {
                EquipmentTypeID = "e",
                InspectionChecklistID = 1000001,
                PrepChecklistID = 1000001

            };
            var c = this._equipmentTypeManager.RetrieveEquipmentTypeDetail(e);
            Assert.AreEqual("e", c.EquipmentType.EquipmentTypeID);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method to verify that DeactivateEquipmentTypeByID deactivates an 
        /// equipment type
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestDeactivateEquipmentTypeByID()
        {
            // Arrange
            int rowcount = 0;

            // Act
            rowcount = _equipmentTypeManager.DeactivateEquipmentTypeByID("tractor");

            // Assert
            Assert.AreEqual(1, rowcount);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/27
        /// 
        /// Method to verify that DeactivateEquipmentTypeByID returns 0
        /// if deactivate fails
        /// </summary>
        [TestMethod]
        public void TestDeactivateEquipmentTypeByIDBad()
        {
            // Arrange
            int rowcount = -1;

            // Act
            rowcount = _equipmentTypeManager.DeactivateEquipmentTypeByID("dsagsdgadjlkgdflk");

            // Assert
            Assert.AreEqual(0, rowcount);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/09
        /// 
        /// Method to verify that RetrieveEquipmentTypeDetailList works
        /// </summary>
        [TestMethod]
        public void TestRetrieveEquipmentTypeDetailList()
        {
            // Arrange
            List<EquipmentTypeDetail> equipmentTypeDetailList = new List<EquipmentTypeDetail>();

            // Act
            equipmentTypeDetailList = _equipmentTypeManager.RetrieveEquipmentTypeDetailList();

            // Assert
            Assert.AreEqual(2, equipmentTypeDetailList.Count);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _equipmentTypeManager = null;
        }

        
    }
}
