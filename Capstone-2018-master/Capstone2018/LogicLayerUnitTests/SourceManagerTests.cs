using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Logic;
using DataAccessMocks;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class SourceManagerTests
    {
        private SourceManager _sourceManager;

        [TestInitialize]
        public void TestSetup()
        {
            _sourceManager = new SourceManager(new SourceAccessorMock());
        }



        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/17
        /// 
        /// Testing editing a Source record
        /// </summary>
        [TestMethod]
        public void TestEditSource()
        {
            //arrange
            int rowAffected = 0;
            var oldSource = new Source()
            {
                SourceID = 1000001,
                SupplyItemID = 1,
                SpecialOrderItemID = 1,
                VendorID = 1,
                MinimumOrderQTY = 1,
                PriceEach = 1.99M,
                LeadTime = 1,
                Active = true

            };
            var newSource = new Source()
            {
                SourceID = 1000001,
                SupplyItemID = 1,
                SpecialOrderItemID = 1,
                VendorID = 1,
                MinimumOrderQTY = 2,
                PriceEach = 3.11M,
                LeadTime = 1,
                Active = true
            };

            //act
            try
            {
                rowAffected = _sourceManager.EditSource(oldSource, newSource);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.AreEqual(1, rowAffected);

        }


        /// <summary>
        /// Brady Feller
        /// Created 2018/02/19
        /// 
        /// Tests the deactivate function for the SourceManager class
        /// </summary>
        [TestMethod]
        public void TestDeactivateSourceByID()
        {
            // arrange
            bool result;

            // act
            result = _sourceManager.DeactivateSource(1000000);

            // assert
            Assert.AreEqual(true, result);
        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/04/19
        /// 
        /// Method tests that RetrieveSource returns the right number of sources
        /// </summary>
        [TestMethod]
        public void TestRetrieveSource()
        {
            // arrange
            List<Source> sourceList;

            // act
            sourceList = _sourceManager.RetrieveSource();

            // assert
            Assert.AreEqual(2, sourceList.Count);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _sourceManager = null;
        }



    }
}
