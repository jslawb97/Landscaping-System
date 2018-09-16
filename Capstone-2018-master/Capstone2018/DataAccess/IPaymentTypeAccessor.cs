using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/19
    /// 
    /// Interface for the PaymentTypeAccessor
    /// </summary>
    /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is not working
    public interface IPaymentTypeAccessor
    {
        List<PaymentType> RetrievePaymentTypeListByActive(bool active = true);
        int DeactivatePaymentTypeByID(string paymentTypeID);
        int EditPaymentTypeByID(PaymentType oldPaymentType, PaymentType newPaymentType);
        /// <summary>
        /// Reuben Cassell
        /// Created 2/20/2018
        /// 
        /// Method to add a new Payment Type to the database with the data access method
        /// </summary>
        /// <param name="PaymentTypeID"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        int CreatePaymentType(string paymentTypeID, string description);
    }
}
