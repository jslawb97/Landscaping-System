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
    [TestClass]
    public class EquipmentStatusManagerTests
    {
        private IEquipmentStatusManager _equipmentStatusManager;
        private EquipmentStatus _statusTest;

        [TestInitialize]
        public void TestSetup()
        {
            _equipmentStatusManager = new EquipmentStatusManager(new EquipmentStatusAccessorMock());
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        /// Method tests that RetrieveEquipmentStatusList returns the right number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveEquipmentStatus()
        {
            // arrange
            List<EquipmentStatus> eqstList;

            // act
            eqstList = _equipmentStatusManager.RetrieveEquipmentStatusList();

            // assert
            Assert.AreEqual(3, eqstList.Count);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/02
        /// 
        /// Method tests that CreateEquipmentStatus creates a new status
        /// </summary>
        [TestMethod]
        public void TestCreateEquipmentStatus()
        {
            // arrange
            bool equipmentStatusCreate = false;
            EquipmentStatus equipmentStatus = new EquipmentStatus()
            {
                EquipmentStatusID = "Needs Washed"
            };

            // act
            equipmentStatusCreate = _equipmentStatusManager.AddEquipmentStatus(equipmentStatus);

            // assert
            Assert.AreEqual(true, equipmentStatusCreate);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/25
        /// 
        /// Test for creating an equipment status with an empty name
        /// </summary>
        [TestMethod]
        public void TestCreateEquipmentStatusEmptyName()
        {
            // arrange
            _statusTest = new EquipmentStatus
            {
                EquipmentStatusID = ""
            };

            // act
            try
            {
                var result = _equipmentStatusManager.AddEquipmentStatus(_statusTest);

                Assert.Fail("Should throw an error for empty name");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/25
        /// 
        /// Test for creating an equipment status with a name that is too long
        /// </summary>
        [TestMethod]
        public void TestCreateEquipmentStatusNameTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string name = new string(chars);
            _statusTest = new EquipmentStatus
            {
                EquipmentStatusID = name
            };

            // act
            try
            {
                var result = _equipmentStatusManager.AddEquipmentStatus(_statusTest);

                Assert.Fail("Should throw an error for long name");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/25
        /// 
        /// Test for creating an equipment status as null
        /// </summary>
        [TestMethod]
        public void TestCreateEquipmentStatusNull()
        {
            // arrange
            _statusTest = null;

            // act
            try
            {
                var result = _equipmentStatusManager.AddEquipmentStatus(_statusTest);

                Assert.Fail("Null item should error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/08
        /// 
        /// Verifies that DeleteEquipmentStatus deletes a status
        /// </summary>
        [TestMethod]
        public void TestDeleteEquipmentStatusID()
        {
            // arrange
            int result = 0;

            // act
            result = _equipmentStatusManager.DeleteEquipmentStatus("Needs Preparation");

            // assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/08
        /// 
        /// Method verifies that EditEquipmentStatus changes an EquipmentStatus
        /// </summary>
        [TestMethod]
        public void TestEditEquipmentStatus()
        {
            // arrange
            int result = 0;
            EquipmentStatus oldEquipmentStatus = new EquipmentStatus
            {
                EquipmentStatusID = "Needs Parts"
            };
            EquipmentStatus newEquipmentStatus = new EquipmentStatus
            {
                EquipmentStatusID = "Needs Fixed"
            };

            // act
            result = _equipmentStatusManager.EditEquipmentStatus(oldEquipmentStatus, newEquipmentStatus);

            // assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/25
        /// 
        /// Test for edit an equipment status with an empty name
        /// </summary>
        [TestMethod]
        public void TestEditEquipmentStatusEmptyName()
        {
            // arrange
            List<EquipmentStatus> items = _equipmentStatusManager.RetrieveEquipmentStatusList();

            var newItem = new EquipmentStatus
            {
                EquipmentStatusID = ""
            };

            // act
            try
            {
                var result = _equipmentStatusManager.EditEquipmentStatus(items.ElementAt(0), newItem);

                Assert.Fail("Should throw an error for empty name");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/25
        /// 
        /// Test for editing an equipment status with a name that is too long
        /// </summary>
        [TestMethod]
        public void TestEditEquipmentStatusNameTooLong()
        {
            // arrange
            var chars = new char[Constants.MAXNAMELENGTH + 1];
            string name = new string(chars);
            List<EquipmentStatus> items = _equipmentStatusManager.RetrieveEquipmentStatusList();
            var newItem = new EquipmentStatus
            {
                EquipmentStatusID = name
            };

            // act
            try
            {
                var result = _equipmentStatusManager.EditEquipmentStatus(items.ElementAt(0), newItem);

                Assert.Fail("Should throw an error for long name");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/25
        /// 
        /// Test for editing an equipment status as null
        /// </summary>
        [TestMethod]
        public void TestEditEquipmentStatusNull()
        {
            // arrange
            List<EquipmentStatus> items = _equipmentStatusManager.RetrieveEquipmentStatusList();
            EquipmentStatus newItem = null;

            // act
            try
            {
                var result = _equipmentStatusManager.EditEquipmentStatus(items.ElementAt(0), newItem);

                Assert.Fail("Null item should error");
            }
            catch (Exception)
            {
                // assert
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/08
        /// 
        /// Method verifies that RetrieveEquipmentStatusByID returns the correct ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveEquipmentStatusByID()
        {
            // arrange
            EquipmentStatus equipmentStatus = null;

            // act
            equipmentStatus = _equipmentStatusManager.RetrieveEquipmentStatusByID("Needs Maintenance");

            // assert
            Assert.AreEqual("Needs Maintenance", equipmentStatus.EquipmentStatusID);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _equipmentStatusManager = null;
        }
    }
}
