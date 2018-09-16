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
    public class AvailabilityManagerTests
    {
         private IAvailabilityManager _availabilityManager;
        
        [TestInitialize]
        public void TestSetup()
        {
            _availabilityManager = new AvailabilityManager(new AvailabilityAccessorMock());
        }

        [TestMethod]
        public void TestRetrieveAvailabilityListByEmployeeID()
        {
            //arrange
            int employeeID = Constants.IDSTARTVALUE;

            //act
            var availabilityList = _availabilityManager.RetrieveAvailabilityByEmployeeID(employeeID);

            //assert
            Assert.IsNotNull(availabilityList);
        }

        [TestMethod]
        public void TestEditAvailability()
        {
            // arrange
            int id = Constants.IDSTARTVALUE;
            var list = new List<Availability>() {
                new Availability(){AvailabilityID = 1000000,EmployeeID=1000000,StartTime=Convert.ToDateTime("2018-02-18 03:00:00"), EndTime=Convert.ToDateTime("2018-02-18 13:00:00")},
                new Availability(){AvailabilityID = 1000001,EmployeeID=1000000,StartTime=Convert.ToDateTime("2018-02-18 07:00:00"), EndTime=Convert.ToDateTime("2018-02-18 15:00:00")},
                new Availability(){AvailabilityID = 1000002,EmployeeID=1000000,StartTime=Convert.ToDateTime("2018-02-18 09:00:00"), EndTime=Convert.ToDateTime("2018-02-18 11:00:00")}

            };

            //act

            try
            {
                int result = _availabilityManager.EditAvailability(id, list);
                Assert.IsTrue(result >= 0, "Number of records affected: " + result);
            }
            catch (Exception ex)
            {

                Assert.Fail("Error editing availability records: " + ex);
            }

        }
        
    }
}
