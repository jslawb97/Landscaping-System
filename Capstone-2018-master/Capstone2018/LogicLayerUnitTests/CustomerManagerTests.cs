using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerUnitTests;
using DataAccessMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class CustomerManagerTests
    {
        private CustomerManager _customerManager;

        [TestInitialize]
        public void TestSetup()
        {
            _customerManager = new CustomerManager(new CustomerAccessorMock());

        }

        /// <summary>
        /// Sam Dramstad
        /// Created on 2018/02/20
        /// 
        /// Method that verifies retrieve customer returns correct number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveCustomerList()
        {
            // arrange
            List<Customer> customerList;

            // act
            customerList = _customerManager.RetrieveCustomerListByActive(true);

            // assert
            Assert.AreEqual(2, customerList.Count);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeactivateCustomersLowID()
        {
            //arrange
            List<Customer> customerList = new List<Customer>();

            //act
            _customerManager.DeactivateCustomer(5);


        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeactivateCustomerNoCustomer()
        {
            //arrange
            List<Customer> customerList = new List<Customer>();

            //act
            _customerManager.DeactivateCustomer(4200025);


        }

        [TestMethod]
        public void TestDeactivateCustomerByID()
        {
            // arrange
            List<Customer> customerList = new List<Customer>();

            // act                        
            _customerManager.DeactivateCustomer(1000001);
            _customerManager.DeactivateCustomer(1000000);
            customerList = _customerManager.RetrieveCustomerListByActive(true);

            // assert
            Assert.AreEqual(0, customerList.Count);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/02/21
        /// 
        /// Method that verifies the adding of a new customer
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreateCustomer()
        {
            //arrange
            int returnedNewCustomerID;
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.PasswordHash = "New Password Hash";
            cst.Active = true;
            //act 
            returnedNewCustomerID = _customerManager.CreateCustomer(cst);

            //assert 
            Assert.AreEqual(1000004, returnedNewCustomerID);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if customer type was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Customer Type value.")]
        public void TestCreateCustomerNullCustomerType()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = null;
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if email was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Email value.")]
        public void TestCreateCustomerNullEmail()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = null;
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if first name was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null First Name value.")]
        public void TestCreateCustomerNullFirstName()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "New Email";
            cst.FirstName = null;
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if last name was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Last Name value.")]
        public void TestCreateCustomerNullLastName()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = null;
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if phone number was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Phone Number value.")]
        public void TestCreateCustomerNullPhoneNumber()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "Customer Type";
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = null;
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }


        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Customer Type too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Customer Type value too short.")]
        public void TestCreateCustomerShortCustomerType()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "";
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Email too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Email value too short.")]
        public void TestCreateCustomerShortEmail()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "";
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if First Name too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "First Name value too short.")]
        public void TestCreateCustomerShortFirstName()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "New Email";
            cst.FirstName = "";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Last Name too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Last Name value too short.")]
        public void TestCreateCustomerShortLastName()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = "";
            cst.PhoneNumber = "3154465564";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Phone Number too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Phone Number value too short.")]
        public void TestCreateCustomerShortPhoneNumber()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerTypeID = "New Customer Type";
            cst.Email = "New Email";
            cst.FirstName = "New First Name";
            cst.LastName = "New Last Name";
            cst.PhoneNumber = "";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);

        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Customer Type too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Customer type value too long.")]
        public void TestCreateCustomerCustomerTypeTooLong()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerID = Constants.IDSTARTVALUE;

            string customerType = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                customerType += "a";
            }
            cst.CustomerTypeID = customerType;
            cst.Email = "email";
            cst.FirstName = "first name";
            cst.LastName = "last name";
            cst.PhoneNumber = "phone number";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);
        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Email too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Email value too long.")]
        public void TestCreateCustomerEmailTooLong()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerID = Constants.IDSTARTVALUE;
                        
            cst.CustomerTypeID = "customer type";
            string email = "";
            for (int i = 0; i < Constants.MAXEMAILLENGTH + 1; i++)
            {
                email += "a";
            }
            cst.Email = email;
            cst.FirstName = "first name";
            cst.LastName = "last name";
            cst.PhoneNumber = "phone number";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);
        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if first name too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "First name value too long.")]
        public void TestCreateCustomerFirstNameTooLong()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerID = Constants.IDSTARTVALUE;

            cst.CustomerTypeID = "customer type";
            cst.Email = "email";
            string firstName = "";
            for (int i = 0; i < Constants.MAXEMAILLENGTH + 1; i++)
            {
                firstName += "a";
            }
            cst.FirstName = firstName;
            cst.LastName = "last name";
            cst.PhoneNumber = "phone number";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);
        }

        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Last Name too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Last Name value too long.")]
        public void TestCreateCustomerLastNameTooLong()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerID = Constants.IDSTARTVALUE;

            cst.CustomerTypeID = "customer type";
            cst.Email = "email";
            cst.FirstName = "first name";
            string lastName = "";
            for (int i = 0; i < Constants.MAXEMAILLENGTH + 1; i++)
            {
                lastName += "a";
            }
            cst.LastName = lastName;
            cst.PhoneNumber = "phone number";
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);
        }


        /// Mike Mason
        /// Created on 2018/05/03
        /// 
        /// Method to ensure error returned if Phone Number too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Phone number value too long.")]
        public void TestCreateCustomerPhoneNumberLong()
        {
            //arrange
            Customer cst = new Customer();
            cst.CustomerID = Constants.IDSTARTVALUE;

            cst.CustomerTypeID = "customer type";
            cst.Email = "email";
            cst.FirstName = "first name";
            cst.LastName = "last name";
            string phoneNumber = "";
            for (int i = 0; i < Constants.MAXPHONENUMBERLENGTH + 1; i++)
            {
                phoneNumber += "a";
            }
            cst.PhoneNumber = phoneNumber;
            cst.Active = true;

            //act
            _customerManager.CreateCustomer(cst);
        }



        /// <summary>
        /// Jayden Tollefson
        /// Created 2018/03/02
        /// 
        /// Prove that sample data can be edited
        /// <remarks>Added customerTypeID - Mike Mason</remarks>
        /// </summary>
        [TestMethod]
        public void TestEditCustomer()
        {
            Assert.AreEqual(true, this._customerManager.EditCustomer(
                this._customerManager.RetrieveCustomerById(Constants.IDSTARTVALUE),
                new Customer
                {
                    CustomerTypeID = "Updated Customer Type",
                    FirstName = "Updated Test Name",
                    LastName = "Updated Test Rep",
                    Email = "Updated Test Address",
                    PhoneNumber = "3194542434",
                    Active = !_customerManager.RetrieveCustomerById(Constants.IDSTARTVALUE).Active
                }));
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new customer type is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Customer type is null")]
        public void TestEditCustomerNullCustomerTypeValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = null;
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
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
        public void TestEditCustomerNullEmailValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerTypeID");
            newCst.Email = null;
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new first name is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "First name is null")]
        public void TestEditCustomerNullFirstNameValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerTypeID");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = null;
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new last name is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Last Name is null")]
        public void TestEditCustomerNullLastNameValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerTypeID");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = null;
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
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
        public void TestEditCustomerNullPhoneNumberValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerTypeID");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = null;
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new customer type is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Customer Type is too short")]
        public void TestEditCustomerShortCustomerTypeValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = "";
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new email is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Email is too short")]
        public void TestEditCustomerShortEmailValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = "";
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new first name is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "First Name is too short")]
        public void TestEditCustomerShortFirstNameValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = "";
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new last name is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Last Name is too short")]
        public void TestEditCustomerShortLastNameValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = "";
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new phone number is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Phone Number is short")]
        public void TestEditCustomerShortPhoneNumberValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = "";
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new customer type is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Customer type is too long")]
        public void TestEditCustomerLongCustomerTypeValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            string customerType = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                customerType += "a";
            }
            newCst.CustomerTypeID = customerType;
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new email is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Email is too long")]
        public void TestEditCustomerLongEmailValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            string email = "";
            for (int i = 0; i < Constants.MAXEMAILLENGTH + 1; i++)
            {
                email += "a";
            }
            newCst.Email = email;
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
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
        public void TestEditCustomerLongFirstNameValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = ("ValidEmail");
            string firstName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                firstName += "a";
            }
            newCst.FirstName = firstName;
            newCst.LastName = ("ValidLastName");
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new last name is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Last Name is too long")]
        public void TestEditCustomerLongLastNameValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            string lastName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                lastName += "a";
            }
            newCst.LastName = lastName;
            newCst.PhoneNumber = ("ValidPhoneNumber");
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/05/04
        /// 
        /// Method to verify exception thrown if new phone number is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Phone Number is long")]
        public void TestEditCustomerLongPhoneNumberValue()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = ("ValidCustomerType");
            newCst.Email = ("ValidEmail");
            newCst.FirstName = ("ValidFirstName");
            newCst.LastName = ("ValidLastName");
            string phoneNumber = "";
            for (int i = 0; i < Constants.MAXPHONENUMBERLENGTH + 1; i++)
            {
                phoneNumber += "a";
            }
            newCst.PhoneNumber = phoneNumber;
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
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
        public void TestEditCustomerNoRowsAffected()
        {
            // arrange
            Customer newCst = new Customer();
            Customer oldCst = new Customer();
            oldCst.CustomerID = Constants.IDSTARTVALUE;
            oldCst.CustomerTypeID = "Valid Customer Type";
            oldCst.Email = "Valid Email";
            oldCst.FirstName = "Valid First Name";
            oldCst.LastName = "Valid Last Name";
            oldCst.PhoneNumber = "Valid Phone Number";
            oldCst.Active = true;
            newCst.CustomerID = Constants.IDSTARTVALUE;
            newCst.CustomerTypeID = "Valid Customer Type";
            newCst.Email = "Valid Email";
            newCst.FirstName = "Valid First Name";
            newCst.LastName = "Valid Last Name";
            newCst.PhoneNumber = "Valid Phone Number";
            newCst.Active = (true);


            // act
            _customerManager.EditCustomer(oldCst, newCst);
        }

      
      

        [TestCleanup]
        public void TestTearDown()
        {
            _customerManager = null;
        }
    }
}
