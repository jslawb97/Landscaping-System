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
    public class CustomerTypeManagerTests
    {
        private CustomerTypeManager _customerTypeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _customerTypeManager = new CustomerTypeManager(new CustomerTypeAccessorMock());
        }

        [TestMethod]
        public void TestRetrieveCustomerTypeList()
        {
            // arrange
            List<CustomerType> customerTypeList;

            // act
            customerTypeList = _customerTypeManager.RetrieveCustomerTypeList();

            // assert
            Assert.AreEqual(2, customerTypeList.Count);
        }

        /// <summary>
        /// Noah Davison
        /// Created on 2018/02/22
        /// 
        /// verifies that delete customer type by ID deletes a customer
        /// </summary>
        [TestMethod]
        public void TestDeleteCustomerTypeByID()
        {
            // arrange
            bool result;

            // act
            result = _customerTypeManager.DeleteCustomerType("CommercialTest");

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/04/12
        /// 
        /// Method that verifies the adding of a new customer type
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreateCustomerType()
        {
            Assert.IsTrue(Constants.IDSTARTVALUE <= this._customerTypeManager.CreateCustomerType(new CustomerType
            {
                CustomerTypeID = "Created Customer Type",
                
            }));
        }




        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/09
        /// 
        /// Testing editing a CustomerType record
        /// </summary>
        [TestMethod]
        public void TestEditCustomerType()
        {
            //arrange
            int rowAffected = 0;
            var oldCustomerType = new CustomerType()
            {
                CustomerTypeID = "ResidentialTest"

            };
            var newCustomerType = new CustomerType()
            {
                CustomerTypeID = "LoyalCustomer"
            };

            //act
            try
            {
                rowAffected = _customerTypeManager.EditCustomerType(oldCustomerType, newCustomerType);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

            //assert
            Assert.AreEqual(1, rowAffected);

        }

        [TestCleanup]
        public void TestTearDown()
        {
            _customerTypeManager = null;
        }

    }
}
