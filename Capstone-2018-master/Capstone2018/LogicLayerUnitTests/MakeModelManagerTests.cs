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
    public class MakeModelManagerTests
    {
        private IMakeModelManager _makeModelManager;

        [TestInitialize]
        public void TestSetup()
        {
            _makeModelManager = new MakeModelManager(new MakeModelAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/26
        /// 
        /// Method to verify that RetrieveMakeModelDetailList returns the proper
        /// number of MakeModelDetails
        /// </summary>
        [TestMethod]
        public void TestRetrieveMakeModelDetailListGood()
        {
            // arrange
            List<MakeModelDetail> detailList = null;

            // act
            detailList = _makeModelManager.RetrieveMakeModelDetailList();

            // assert
            Assert.AreEqual(4, detailList.Count());
        }

        /// <summary>
        /// James McPherson
        /// 2018/02/04
        /// 
        /// Method to verify that RetrieveMakeModelByID returns a MakeModel
        /// </summary>
        [TestMethod]
        public void TestRetrieveMakeModelByID()
        {
            // arrange
            MakeModel makeModel = null;

            // act
            makeModel = _makeModelManager.RetrieveMakeModelByID(1000001);

            // assert
            Assert.AreEqual(1000001, makeModel.MakeModelID);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method to verify that RetrieveMakeModelList returns a list
        /// with the correct number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveMakeModelList()
        {
            // arrange
            List<MakeModel> makeModelList = null;

            // act
            makeModelList = _makeModelManager.RetrieveMakeModelList();

            // assert
            Assert.AreEqual(4, makeModelList.Count());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/16
        /// 
        /// Method to verify that RetrieveMakeModelList returns a list
        /// with the correct number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveMakeModelListByActive()
        {
            // arrange
            List<MakeModel> makeModelList = null;

            // act
            makeModelList = _makeModelManager.RetrieveMakeModelListByActive(active: true);

            // assert
            Assert.AreEqual(3, makeModelList.Count());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method to verify that CreateMakeModel creates a new MakeModel
        /// </summary>
        [TestMethod]
        public void TestCreateMakeModel()
        {
            // arrange
            bool makeModelCreated = false;
            MakeModel makeModel = new MakeModel()
            {
                MakeModelID = 1000003,
                Make = "TestMake",
                Model = "TestModel",
                MaintenanceChecklistID = 1000000
            };

            // act
            makeModelCreated = _makeModelManager.CreateMakeModel(makeModel);

            // assert
            Assert.AreEqual(true, makeModelCreated);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with an invalid MaintenanceChecklistID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateMakeModelInvalidMaintenanceChecklistID()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE - 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with a null Make value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateMakeModelNullMakeValue()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = null,
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with a null Model value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateMakeModelNullModelValue()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = null,
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with an empty Make value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateMakeModelEmptyMakeValue()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with an empty Model value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateMakeModelEmptyModelValue()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with null Make and Model value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateMakeModelNullMakeAndModelValues()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = null,
                Model = null,
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/26
        /// 
        /// Method to verify that CreateMakeModel does not add a MakeModel
        /// with empty Model and Make values.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateMakeModelEmptyMakeAndModelValues()
        {
            // arrange
            bool makeModelsCreated = false;

            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "",
                Model = "",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsCreated = _makeModelManager.CreateMakeModel(newMakeModel);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method to verify that EditMakeModel changes a MakeModel
        /// </summary>
        [TestMethod]
        public void TestEditMakeModel()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = 1000001,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = 1000001
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = 1000001,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = 1000003
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);

            // assert
            Assert.AreEqual(1, makeModelsEdited);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an invalid MakeModelID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelInvalidMakeModelID()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE - 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE - 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 3
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an invalid MakeModelID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelInvalidOldMakeModelID()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE - 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 3
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an invalid MakeModelID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelInvalidNewMakeModelID()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE - 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 3
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with different MakeModelID's.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelMakeModelIDMismatch()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 3
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an invalid MaintenanceChecklistID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelInvalidMaintenanceChecklistID()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE - 1
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE - 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an invalid MaintenanceChecklistID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelInvalidOldMaintenanceChecklistID()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE - 1
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an invalid MaintenanceChecklistID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEditMakeModelInvalidNewMaintenanceChecklistID()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE - 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with a null Make value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditMakeModelNullMakeValue()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = null,
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with a null Model value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditMakeModelNullModelValue()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = null,
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an empty Make value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditMakeModelEmptyMakeValue()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "",
                Model = "TestMake4",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with an empty Model value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditMakeModelEmptyModelValue()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake4",
                Model = "",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with null Make and Model value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditMakeModelNullMakeAndModelValues()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = null,
                Model = null,
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/24
        /// 
        /// Method to verify that EditMakeModel does not change a MakeModel
        /// with empty Model and Make values.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditMakeModelEmptyMakeAndModelValues()
        {
            // arrange
            int makeModelsEdited = 0;
            MakeModel oldMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = Constants.IDSTARTVALUE
            };
            MakeModel newMakeModel = new MakeModel()
            {
                MakeModelID = Constants.IDSTARTVALUE + 1,
                Make = "",
                Model = "",
                MaintenanceChecklistID = Constants.IDSTARTVALUE + 1
            };

            // act
            makeModelsEdited = _makeModelManager.EditMakeModel(oldMakeModel, newMakeModel);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method to verify that DeactivateMakeModel deactivates a MakeModel
        /// </summary>
        [TestMethod]
        public void TestDeactivateMakeModel()
        {
            // Arrange
            bool result = false;

            // Act
            result = _makeModelManager.DeactivateMakeModelByID(1000002);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Method to verify that RetrieveMakeModelListByMake() returns the right MakeModel
        /// </summary>
        [TestMethod]
        public void TestRetrieveMakeModelListByMake()
        {
            //arrange
            string make = "TestMake1";
            List<MakeModel> makeModelList = new List<MakeModel>();

            //act
            makeModelList = _makeModelManager.RetrieveMakeModelListByMake(make);

            //assert
            Assert.AreEqual(1, makeModelList.Count());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _makeModelManager = null;
        }
    }
}
