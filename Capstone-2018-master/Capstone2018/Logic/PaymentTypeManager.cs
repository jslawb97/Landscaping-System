using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class PaymentTypeManager : IPaymentTypeManager
    {
        private IPaymentTypeAccessor _paymentTypeAccessor;

        // Real run
        public PaymentTypeManager()
        {
            _paymentTypeAccessor = new PaymentTypeAccessor();
        }

        // Test run
        public PaymentTypeManager(IPaymentTypeAccessor paymentTypeAccessor)
        {
            _paymentTypeAccessor = paymentTypeAccessor;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Retrieves a list of PaymentTypes by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns>A list of PaymentTypes</returns>
        public List<PaymentType> RetrievePaymentTypeListByActive(bool active = true)
        {
            List<PaymentType> paymentTypeList = null;

            try
            {
                paymentTypeList = _paymentTypeAccessor.RetrievePaymentTypeListByActive(active);
            }
            catch
            {
                throw;
            }

            return paymentTypeList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Deactivates a PaymentType by ID
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <returns>PaymentTypes deactivated</returns>
        public int DeactivatePaymentTypeByID(string paymentTypeID)
        {
            int result = 0;

            try
            {
                result = _paymentTypeAccessor.DeactivatePaymentTypeByID(paymentTypeID);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Edits a PaymentType
        /// </summary>
        /// <param name="oldPaymentType"></param>
        /// <param name="newPaymentType"></param>
        /// <returns></returns>
        public int EditPaymentTypeByID(PaymentType oldPaymentType, PaymentType newPaymentType)
        {
            int result = 0;

            try
            {
                result = _paymentTypeAccessor.EditPaymentTypeByID(oldPaymentType, newPaymentType);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/20/2018
        /// 
        /// Method to add a new Payment Type to the database with the data access method
        /// </summary>
        /// <param name="PaymentTypeID"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public int CreatePaymentType(string paymentTypeID, string description)
        {
            int rowCount;

            try
            {

                ValidatePaymentTypeID(paymentTypeID);
                ValidateDescription(description);

                rowCount = _paymentTypeAccessor.CreatePaymentType(paymentTypeID, description);
            }
            catch (Exception ex)
            {

                throw ex;//new ApplicationException("Error: " + ex.Message);
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/22/2018
        /// 
        /// Validates a payment type ID
        /// </summary>
        /// <param name="paymentTypeID"></param>
        private void ValidatePaymentTypeID(string paymentTypeID)
        {

            int maxPaymentTypeIDLength = 100;

            if (paymentTypeID == null)
            {
                throw new ArgumentOutOfRangeException("Must provide a Payment Type ID");
            }
            if (paymentTypeID.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Payment Type ID cannot be blank.");
            }
            if (paymentTypeID.Length > maxPaymentTypeIDLength)
            {
                throw new ArgumentOutOfRangeException("Payment Type ID must be less than 100 characters.");
            }
            //if (exists)
            //{
            //    throw new ArgumentOutOfRangeException("Payment Type ID already exists.");
            //}
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/22/2018
        /// 
        /// Validates a payment type description
        /// </summary>
        /// <param name="description"></param>
        private void ValidateDescription(string description)
        {
            int maxDescriptionLength = 1000;

            if (description == null)
            {
                throw new ArgumentOutOfRangeException("Must provide a description");
            }
            if (description.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Description cannot be blank.");
            }
            if (description.Length > maxDescriptionLength)
            {
                throw new ArgumentOutOfRangeException("Description must be less than 1000 characters.");
            }
        }

        public List<PaymentType> RetrievePaymentTypeList()
        {

            List<PaymentType> paymentTypeList = null;

            try
            {
                paymentTypeList = _paymentTypeAccessor.RetrievePaymentTypeListByActive();
            }
            catch (Exception)
            {

                throw;
            }

            return paymentTypeList;
        }
    }
}
