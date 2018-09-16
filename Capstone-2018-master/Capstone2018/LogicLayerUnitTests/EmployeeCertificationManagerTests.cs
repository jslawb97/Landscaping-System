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
    public class EmployeeCertificationManagerTests
    {
        private IEmployeeCertificationManager _employeeCertManager;

        [TestInitialize]
        public void TestSetup()
        {
            _employeeCertManager = new EmployeeCertificationManager(new EmployeeCertificationAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        /// 
        /// Method to verify that DeactivateEmployeeCertification deactivates an
        /// EmployeeCertification
        /// </summary>
        [TestMethod]
        public void TestDeactivateEmployeeCertification()
        {
            // Arrange
            int result = 0;

            // Act
            result = _employeeCertManager.DeactivateEmployeeCertificationByID(1000000, 1000001);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/02/20
        /// 
        /// Method that verifies retrieve employee certification returns correct number of items
        /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        [TestMethod]
        public void TestRetrieveEmployeeCertificationList()
        {
            // arrange
            List<EmployeeCertificationDetail> employeeCertList;


            // act
            employeeCertList = _employeeCertManager.RetrieveEmployeeCertificationList();


            // assert
            Assert.AreEqual(2, employeeCertList.Count);

        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Method to verify that the record is created
        /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        [TestMethod]
        public void TestCreateEmployeeCertification()
        {
            EmployeeCertification employeeCertification = new EmployeeCertification()
            {
                CertificationID = 1000003,
                EmployeeID = 1000002,
                EndDate = DateTime.Now,
                Active = true
            };

            // act
            int rowsAffected = _employeeCertManager.CreateEmployeeCertification(employeeCertification);

            // assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Method to verify that the record is edited
        /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        [TestMethod]
        public void TestEditEmployeeCertification()
        {
            // arange
            EmployeeCertification oldEmployeeCertification = new EmployeeCertification()
            {
                CertificationID = 1000000,
                EmployeeID = 1000001,
                EndDate = DateTime.Now,
                Active = true
            };
            EmployeeCertification newEmployeeCertification = new EmployeeCertification()
            {
                CertificationID = 1000000,
                EmployeeID = 1000000,
                EndDate = DateTime.Now,
                Active = true
            };

            // act
            bool rowsAffected = _employeeCertManager.EditEmployeeCertification(oldEmployeeCertification, newEmployeeCertification);

            // assert
            Assert.AreEqual(true, rowsAffected);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _employeeCertManager = null;
        }
    }
}
