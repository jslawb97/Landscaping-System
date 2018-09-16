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
    public class EmployeeManagerTests
    {
        private IEmployeeManager _employeeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _employeeManager = new EmployeeManager(new EmployeeAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created on 2018/02/03
        /// 
        /// Method that verifies retrieve employee list by active returns correct
        /// number of items
        /// </summary>
        /// /// <remarks>QA ShilinXiong 2018/04/06  the test past </remarks>
        [TestMethod]
        public void TestRetrieveEmployeeListByActive()
        {
            // arrange
            List<Employee> employeeList;

            // act
            employeeList = _employeeManager.RetrieveEmployeeListByActive(true);

            // assert
            Assert.AreEqual(2, employeeList.Count);
        }

        /// <summary>
        /// James McPherson
        /// Created on 2018/04/13
        /// 
        /// Method that verifies retrieve employee list returns correct
        /// number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveEmployeeList()
        {
            // arrange
            List<Employee> employeeList;

            // act
            employeeList = _employeeManager.RetrieveEmployeeList();

            // assert
            Assert.AreEqual(3, employeeList.Count);
        }

        /// <summary>
        /// James McPherson
        /// Created on 2018/02/03
        /// 
        /// Method that verifies deactivate employee by id deactivates an employee
        /// This method modifies the amount of employees returned by 
        /// RetrieveEmployeeListByActive - be careful when adding methods below this
        /// </summary>
        /// /// <remarks>QA ShilinXiong 2018/04/06 approved</remarks>
        [TestMethod]
        public void TestDeactivateEmployeeByID()
        {
            // arrange
            int deactivatedEmployees = 0;

            // act
            deactivatedEmployees = _employeeManager.DeactivateEmployeeByID(1000000);

            // assert
            Assert.AreEqual(1, deactivatedEmployees);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to ensure bad id values cannot be passed
        /// </summary>
        ///  /// <remarks>QA ShilinXiong 2018/04/06 approved </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
            "Bad ID value.")]
        public void TestRetrieveEmployeeByIDEmployeeIDTooSmall()
        {
            // arrange
            Employee employee = new Employee();
            employee.EmployeeID = Constants.IDSTARTVALUE - 1;

            // act
            _employeeManager.RetrieveEmployeeByID(employee.EmployeeID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to ensure error returned if employee not found
        /// </summary>
        ///  /// <remarks>QA ShilinXiong 2018/04/06 approved</remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
            "Employee record not found.")]
        public void TestRetrieveEmployeeByIDNotFound()
        {
            // arrange
            Employee employee = new Employee();
            employee.EmployeeID = Constants.IDSTARTVALUE * 500;

            // act
            employee = _employeeManager.RetrieveEmployeeByID(employee.EmployeeID);
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/02/03
        /// 
        /// Method that verifies the adding of a new employee
        ///  /// <remarks>QA ShilinXiong 2018/04/06 approved</remarks>
        /// </summary>
        [TestMethod]
        public void TestCreateEmployee()
        {
            
                //arrange
                bool returnedNewEmployeeID;
                Employee emp = new Employee();
                emp.FirstName = "New First Name";
                emp.LastName = "New Last Name";
                emp.Address = "New Address";
                emp.PhoneNumber = "3154465564";
                emp.Email = "New Email";
                emp.PasswordHash = "New Password Hash";
                emp.Active = true;
                //act 
                returnedNewEmployeeID = _employeeManager.CreateEmployee(emp);

                //assert 
                Assert.AreEqual(true, returnedNewEmployeeID);
        }


        ///// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        //[TestMethod]
        //public void TestEditEmployee()
        //{
        //    Assert.AreEqual(true, this._employeeManager.EditEmployee(
        //        this._employeeManager.RetrieveEmployeeByID(1000000),
        //        new Employee
        //        {
        //            FirstName = "Updated Test Name",
        //            LastName = "Updated Test Rep",
        //            Email = "Updated Test Address",
        //            PhoneNumber = "Updated Test Phone Number",
        //            Active = !_employeeManager.RetrieveEmployeeByID(1000000).Active
        //        }));
        //}

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new first name is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "First name is null")]
        public void TestEditEmployeeNullFirstNameValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = null;
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new last name is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Last name is null")]
        public void TestEditEmployeeNullLastNameValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName"); ;
            newEmp.LastName = null;
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new address is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Address is null")]
        public void TestEditEmployeeNullAddressValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = null;
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new phone number is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Phone Number is null")]
        public void TestEditEmployeeNullPhoneNumberValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = null;
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new email is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Email is null")]
        public void TestEditEmployeeNullEmailValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("Address");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = null;
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new first name is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "First name is too short")]
        public void TestEditEmployeeShortFirstNameValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = "";
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new last name is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Last name is too short")]
        public void TestEditEmployeeShortLastNameValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName"); ;
            newEmp.LastName = "";
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new address is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Address is too short")]
        public void TestEditEmployeeShortAddressValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = "";
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new phone number is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Phone Number is too short")]
        public void TestEditEmployeeShortPhoneNumberValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = "";
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new email is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Email is short")]
        public void TestEditEmployeeShortEmailValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("Address");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = "";
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new first name is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "First name is too long")]
        public void TestEditEmployeeLongFirstNameValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            string firstName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                firstName += "a";
            }
            newEmp.FirstName = firstName;
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new last name is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Last name is too long")]
        public void TestEditEmployeeLongLastNameValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            string lastName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                lastName += "a";
            }
            newEmp.LastName = lastName;
            newEmp.Address = ("ValidAddress");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new address is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Address is too long")]
        public void TestEditEmployeeLongAddressValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            string address = "";
            for (int i = 0; i < Constants.MAXADDRESSLENGTH + 1; i++)
            {
                address += "a";
            }
            newEmp.Address = address;
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new phone number is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Phone Number is too long")]
        public void TestEditEmployeeLongPhoneNumberValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("ValidAddress");
            string phoneNumber = "";
            for (int i = 0; i < Constants.MAXPHONENUMBERLENGTH + 1; i++)
            {
                phoneNumber += "a";
            }
            newEmp.PhoneNumber = phoneNumber;
            newEmp.Email = ("ValidEmail");
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new email is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Email is long")]
        public void TestEditEmployeeLongEmailValue()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = ("ValidFirstName");
            newEmp.LastName = ("ValidLastName");
            newEmp.Address = ("Address");
            newEmp.PhoneNumber = ("ValidPhoneNumber");
            string email = "";
            for (int i = 0; i < Constants.MAXEMAILLENGTH + 1; i++)
            {
                email += "a";
            }
            newEmp.Email = email;
            newEmp.Active = (true);


            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }
      


        [TestMethod]
        public void RetrieveEmployeeListByCertificationAndAvailability()
        {
            //arrange
            Certification certification = new Certification { CertificationID = 1, Active = true};

            //act
            List<Employee> equipmentList = _employeeManager.RetrieveEmployeeListByCertificationAndAvailability(certification, new DateTime(1919, 1, 1), new DateTime(1919, 1, 1));

            //assert
            Assert.IsNotNull(equipmentList);
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if first name was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null First Name value.")]
        public void TestCreateEmployeeNullFirstName()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = null;
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "phone";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if last name was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Last Name value.")]
        public void TestCreateEmployeeNullLastName()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "First name";
            emp.LastName = null;
            emp.Address = "address";
            emp.PhoneNumber = "phone";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if address was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Address value.")]
        public void TestCreateEmployeeNullAddress()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = null;
            emp.PhoneNumber = "phone";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if phone number was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Phone Number value.")]
        public void TestCreateEmployeeNullPhoneNumber()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = "phone number";
            emp.PhoneNumber = null;
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if email was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Email value.")]
        public void TestCreateEmployeeNullEmail()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "First name";
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "phone";
            emp.Email = null;
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }


        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if First Name too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "First Name value too short.")]
        public void TestCreateEmployeeShortFirstName()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "";
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "phone";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Last Name too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Last Name value too short.")]
        public void TestCreateEmployeeShortLastName()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "";
            emp.Address = "address";
            emp.PhoneNumber = "phone";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Address too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Address value too short.")]
        public void TestCreateEmployeeShortAddress()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = "";
            emp.PhoneNumber = "phone";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Phone Number too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Phone Number value too short.")]
        public void TestCreateEmployeeShortPhoneNumber()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Email too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Email value too short.")]
        public void TestCreateEmployeeShortEmail()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "phone number";
            emp.Email = "";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if First Name too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "First Name value too long.")]
        public void TestCreateEmployeeFirstNameTooLong()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            
            string firstName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                firstName += "a";
            }
            emp.FirstName = firstName;
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "phone number";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);
        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Last Name too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Last Name value too long.")]
        public void TestCreateEmployeeLastNameTooLong()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            string lastName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                lastName += "a";
            }
            emp.LastName = lastName;
            emp.Address = "address";
            emp.PhoneNumber = "phone number";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);
        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Address too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Address value too long.")]
        public void TestCreateEmployeeAddressTooLong()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            string address = "";
            for (int i = 0; i < Constants.MAXADDRESSLENGTH + 1; i++)
            {
                address += "a";
            }
            emp.Address = address;
            emp.PhoneNumber = "phone number";
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);
        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Phone Number too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Phone Number value too long.")]
        public void TestCreateEmployeePhoneTooLong()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = "address";
            string phoneNumber = "";
            for (int i = 0; i < Constants.MAXPHONENUMBERLENGTH + 1; i++)
            {
                phoneNumber += "a";
            }
            emp.PhoneNumber = phoneNumber;
            emp.Email = "email";
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);
        }


        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Email too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Email value too long.")]
        public void TestCreateEmployeeEmailLong()
        {
            //arrange
            Employee emp = new Employee();
            emp.EmployeeID = Constants.IDSTARTVALUE;
            emp.FirstName = "first name";
            emp.LastName = "last name";
            emp.Address = "address";
            emp.PhoneNumber = "phone number";
            string email = "";
            for (int i = 0; i < Constants.MAXEMAILLENGTH + 1; i++)
            {
                email += "a";
            }
            emp.Email = email;
            emp.Active = true;

            //act
            _employeeManager.CreateEmployee(emp);
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to ensure exception thrown if no rows affected by edit
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "No rows affected by edit")]
        public void TestEditEmployeeNoRowsAffected()
        {
            // arrange
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE * 500;
            oldEmp.FirstName = "Valid First Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Last Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            newEmp.EmployeeID = Constants.IDSTARTVALUE * 500;
            newEmp.FirstName = "Valid First Name";
            newEmp.LastName = "Valid Last Name";
            newEmp.Address = "Valid Last Address";
            newEmp.PhoneNumber = "Valid Phone Number";
            newEmp.Email = "Valid Email";

            // act
            _employeeManager.EditEmployee(oldEmp, newEmp);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to ensure only 1 row affected by edit
        /// </summary>
        [TestMethod]
        public void TestEditEmployeeGood()
        {
            // arrange
            bool editSuccess;
            Employee newEmp = new Employee();
            Employee oldEmp = new Employee();
            oldEmp.EmployeeID = Constants.IDSTARTVALUE;
            oldEmp.FirstName = "Valid First Name";
            oldEmp.LastName = "Valid Last Name";
            oldEmp.Address = "Valid Address";
            oldEmp.PhoneNumber = "Valid Phone Number";
            oldEmp.Email = "Valid Email";
            oldEmp.Active = true;
            newEmp.EmployeeID = Constants.IDSTARTVALUE;
            newEmp.FirstName = "Valid First Name";
            newEmp.LastName = "Valid Last Name";
            newEmp.Address = "Valid Address";
            newEmp.PhoneNumber = "Valid Phone";
            newEmp.Email = "New Email";
            newEmp.Active = true;

            // act
            editSuccess = _employeeManager.EditEmployee(oldEmp, newEmp);

            // assert
            Assert.AreEqual(true, editSuccess);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _employeeManager = null;
        }
    }
}
