using DataAccessMocks;
using DataObjects;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests {
    [TestClass]
    public class InspectionChecklistManagerTests
    {

        private IInspectionChecklistManager _inspectionChecklistManager;

        public InspectionChecklistManagerTests() {
            this._inspectionChecklistManager = new InspectionChecklistManager(new InspectionChecklistAccessorMock());
        }

        /// <summary>
        /// Zach Murphy
        /// Created on 2018/02/22
        /// 
        /// Verifies the retrieval of all sample data
        /// </summary>
        [TestMethod]
        public void TestRetrieveInspectionChecklistList() {
            Assert.AreEqual(4, this._inspectionChecklistManager.RetrieveInspectionChecklistItems().Count);
        }

        /// <summary>
        /// Zach Murphy
        /// Created on 2018/02/22
        /// 
        /// Verifies the retrieval of sample data by id
        /// </summary>
        [TestMethod]
        public void TestRetrieveInspectionChecklistByID() {
            var c = this._inspectionChecklistManager.RetrieveInspectionChecklistByID(Constants.IDSTARTVALUE);
            Assert.AreEqual(Constants.IDSTARTVALUE, c.InspectionChecklistID);
        }

        /// <summary>
        /// Zach Murphy
        /// Created on 2018/02/22
        /// 
        /// Verifies that sample data can be edited
        /// </summary>
        /// <remarks>QA Jayden T 4/6/18 Check all code paths</remarks>
        [TestMethod]
        public void TestEditInspectionChecklistItem() {
            Assert.AreEqual(1, this._inspectionChecklistManager.EditInspectionChecklist(
                this._inspectionChecklistManager.RetrieveInspectionChecklistByID(Constants.IDSTARTVALUE),
                new InspectionChecklist {
                    Name = "New Name",
                    Description = "Updated test description."
                }));
        }

        /// <summary>
        /// Zach Murphy
        /// Created on 2018/02/22
        /// 
        /// Verifies the creation of given sample data
        /// </summary>
        /// <remarks>QA Jayden T 4/6/18 Check all code paths</remarks>
        [TestMethod]
        public void TestCreateInspectionChecklist() {
            Assert.AreEqual(1, this._inspectionChecklistManager.AddInspectionChecklist(new InspectionChecklist {
                Name = "New Name",
                Description = "New test description."
            }));
        }

        /// <summary>
        /// Zach Murphy
        /// Created on 2018/02/22
        /// 
        /// Verifies the deactivation of given sample data
        /// </summary>
        [TestMethod]
        public void TestDeactivateInspectionChecklistByID() {
            Assert.AreEqual(1, this._inspectionChecklistManager.DeactivateInspectionChecklist(Constants.IDSTARTVALUE));
            Assert.IsFalse(_inspectionChecklistManager.RetrieveInspectionChecklistByID(Constants.IDSTARTVALUE).Active);
        }

        [TestCleanup]
        public void TestTearDown() {
            this._inspectionChecklistManager = null;
        }
    }
}