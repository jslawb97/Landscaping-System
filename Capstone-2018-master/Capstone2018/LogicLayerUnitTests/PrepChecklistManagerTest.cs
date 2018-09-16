using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using Logic;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class PrepChecklistManagerTests
    {
        private PrepChecklistManager _prepChecklistManager;

        [TestInitialize]
        public void TestSetup()
        {
            _prepChecklistManager = new PrepChecklistManager(new PrepChecklistAccessorMock());
        }



        [TestMethod]
        public void TestDeactivatePrepChecklistByID()
        {

             Assert.AreEqual(1, this._prepChecklistManager.DeactivatePrepChecklistByID(Constants.IDSTARTVALUE));

        }

        [TestMethod]
        public void TestRetrievePrepChecklistList()
        {
            Assert.AreEqual(3, this._prepChecklistManager.RetrievePrepChecklist().Count);
        }

        [TestMethod]
        public void TestRetrievePrepChecklistByID()
        {

            var c =  this._prepChecklistManager.RetrievePrepChecklistByID(Constants.IDSTARTVALUE);
            Assert.AreEqual(Constants.IDSTARTVALUE, c.PrepChecklistID);
        }


        [TestMethod]
        public void TestEditPrepChecklist()
        {
                       
            Assert.AreEqual(1, this._prepChecklistManager.EditPrepChecklist(
                this._prepChecklistManager.RetrievePrepChecklistByID(Constants.IDSTARTVALUE),
                new PrepChecklist
                {
                    Description = "Updated test Description"
                }));
        }

        [TestMethod]
        public void TestCreatePrepChecklist()
        {
            Assert.AreEqual(1, this._prepChecklistManager.AddPrepChecklist(new PrepChecklist
            {
                PrepChecklistID = 1,
                Name = "Testname",
                 Description = "test description"

            }));           
        }
        


        [TestCleanup]
        public void TestTearDown()
        {
            _prepChecklistManager = null;
        }
    }
}
