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
    public class PaymentTypeManagerTests
    {
        private IPaymentTypeManager _paymentTypeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _paymentTypeManager = new PaymentTypeManager(new PaymentTypeAccessorMock());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Method to verify that RetrievePaymentTypeListByActive returns a list
        /// with the correct number of items
        /// </summary>
        /// <remarks>QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        public void TestRetrievePaymentTypeByActive()
        {
            // arrange
            List<PaymentType> paymentTypes;

            // act
            paymentTypes = _paymentTypeManager.RetrievePaymentTypeListByActive(active: true);

            // assert
            Assert.AreEqual(2, paymentTypes.Count());
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Method to verify that DeactivatePaymentTypeByID deactivates a payment
        /// type
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        public void TestDeactivatePaymentTypeByID()
        {
            // arrange
            int result = 0;

            // act
            result = _paymentTypeManager.DeactivatePaymentTypeByID("PaymentType1");

            // assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Method to verify that EditPaymentTypeByID edits a payment type
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        public void TestEditPaymentTypeByID()
        {
            // arrange
            int result = 0;
            PaymentType oldPaymentType = new PaymentType
            {
                PaymentTypeID = "PaymentType1",
                Description = "PaymentType1Description",
                Active = true
            };
            PaymentType newPaymentType = new PaymentType
            {
                PaymentTypeID = "PaymentType1",
                Description = "PaymentType1DescriptionEdited",
                Active = true
            };

            // act
            result = _paymentTypeManager.EditPaymentTypeByID(oldPaymentType, newPaymentType);

            // assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method works with valid values
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        public void TestAddPaymentType()
        {
            // arrange
            int expectedRowCount = 1;
            string paymentTypeID = "Card";
            string description = "A Credit Card";

            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // assert
            Assert.AreEqual(expectedRowCount, rowCount);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not accept a null 
        /// value for PaymentTypeID
        /// QA Shilin Xiong 4/27/2018  test past
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPaymentTypeIDIsNotNull()
        {
            // arrange
            string paymentTypeID = null;
            string description = "A Credit Card";

            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // arrange - testing for an ApplicationException
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not accept a blank
        /// value for PaymentTypeID
        /// QA Shilin Xiong 4/27/2018  test past
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPaymentTypeIDIsNotBlank()
        {
            // arrange
            string paymentTypeID = "";
            string description = "A Credit Card";

            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // arrange - testing for an ApplicationException
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not accept a
        /// PaymentTypeID that is too long
        /// QA Shilin Xiong 4/27/2018  test past
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPaymentTypeIDIsOutOfRange()
        {
            // arrange
            int maxPaymentIDLength = 100;
            string paymentTypeID = "X";
            string description = "A Credit Card";

            for (int i = 0; i < maxPaymentIDLength + 1; i++)
            {
                paymentTypeID += "X";
            }


            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // arrange - testing for an ApplicationException
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not accept
        /// a PaymentTypeID that already exists (ApplicationException)
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddPaymentTypeIDAlreadyExists()
        {
            // arrange
            string paymentTypeID = "PaymentType1";
            string description = "TestDescription";

            //act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // arrange - testing for an ApplicationException

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not except a null
        /// value for Description
        /// QA Shilin Xiong 4/27/2018  test past
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPaymentTypeDescrptionIsNotNull()
        {
            // arrange
            string paymentTypeID = "Credit Card";
            string description = null;

            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // arrange - testing for an ApplicationException
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not except a blank
        /// value for Description
        /// </summary>QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPaymentTypeDescriptionIsNotBlank()
        {
            // arrange
            string paymentTypeID = "Credit Card";
            string description = "";

            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, description);

            // arrange - testing for an ApplicationException
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/21/2018
        /// 
        /// Tests if the AddPaymentType method does not except a
        /// value for Description that is too large
        /// </summary>QA Shilin Xiong 4/27/2018  test past
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPaymentTypeDescriptionIsNotOutOfRange()
        {
            // arrange
            int maxPaymentDescriptionLength = 1000;
            string paymentTypeID = "Credit Card";
            string paymentTypeDescription = "X";

            for (int i = 0; i < maxPaymentDescriptionLength + 1; i++)
            {
                paymentTypeDescription += "X";
            }


            // act
            int rowCount = _paymentTypeManager.CreatePaymentType(paymentTypeID, paymentTypeDescription);

            // arrange - testing for an ApplicationException
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _paymentTypeManager = null;
        }
    }
}
