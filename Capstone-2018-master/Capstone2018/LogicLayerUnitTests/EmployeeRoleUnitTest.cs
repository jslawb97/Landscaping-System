using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMock;
using Logic;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace CapstoneUnitTest
{
    [TestClass]
    public class EmployeeRoleUnitTest
    {
        private IEmployeeRoleManager _employeeRoleManager;

        [TestInitialize]
        public void TestSetUp()
        {
            _employeeRoleManager = new EmployeeRoleManager(new EmployeeRoleAccessorMock());
        }

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
        /// 
        /// Testing AddEmployeeRole method
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestAddEmployeeRole()
        {
            // arrange
            var employeeRoleDetail = new EmployeeRoleDetail()
            {
                Employee = new Employee()
                {
                    EmployeeID = 1000000
                },
                EmployeeRole = new EmployeeRole()
                {
                    RoleID = "Manager",
                    Active = true
                }
            };

            var role = new Role()
            {
                RoleID = "Manager"
            };


            int result = 0;

            // act 
            result = _employeeRoleManager.AddEmployeeRoleDetail(employeeRoleDetail.Employee, role);

            // assert
            Assert.AreEqual(result, 0);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/04/20
        /// 
        /// Testing EditEmployeeRole method
        /// </summary>
        [TestMethod]
        public void TestEditEmployeeRole()
        {
            var employee = new Employee()
            {
                EmployeeID = Constants.IDSTARTVALUE
            };
            var employeeRole = new EmployeeRole()
            {
                EmployeeId = Constants.IDSTARTVALUE,
                RoleID = "Job Scheduler",
                Active = true
            };
            var employeeRoleDetail = new EmployeeRoleDetail()
            {
                Employee = employee,
                EmployeeRole = employeeRole
            };

            employee = new Employee()
            {
                EmployeeID = Constants.IDSTARTVALUE
            };
            employeeRole = new EmployeeRole()
            {
                EmployeeId = Constants.IDSTARTVALUE,
                RoleID = "Manager",
                Active = true
            };

            EmployeeRoleDetail oldEmployeeRoleDetail = new EmployeeRoleDetail()
            {
                Employee = employee,
                EmployeeRole = employeeRole
            };
            int result = 0;

            // act
            result = _employeeRoleManager.EditEmployeeRoleDetail(oldEmployeeRoleDetail, employeeRoleDetail);
            // assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
        /// 
        /// Testing DeactivateEmployeeRole() method
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        /// <remarks>QA updates made John Miller 4/20/18</remarks>
        [TestMethod]
        public void TestDeactivateEmployeeRole()
        {
            // arrange
            bool result = false;
            Employee employee = new Employee()
            {
                EmployeeID = Constants.IDSTARTVALUE
            };
            EmployeeRole employeeRole = new EmployeeRole()
            {
                RoleID = "Manager",
                Active = true
            };
            EmployeeRoleDetail employeeRoleDetail = new EmployeeRoleDetail()
            {
                Employee = employee,
                EmployeeRole = employeeRole
            };

            // act

            result = _employeeRoleManager.DeactivateEmployeeRole(employeeRoleDetail);
            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        [TestMethod]
        public void TestRetrieveEmployeeRoleList()
        {
            // arrange
            List<EmployeeRole> employeeRoleList;

            // act
            employeeRoleList = _employeeRoleManager.RetrieveEmployeeRoleList();

            // assert
            Assert.AreEqual(2, employeeRoleList.Count);
        }
        [TestCleanup]
        public void TestTearDown()
        {
            _employeeRoleManager = null;
        }
    }
}
