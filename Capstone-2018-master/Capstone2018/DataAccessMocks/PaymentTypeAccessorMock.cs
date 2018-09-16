using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class PaymentTypeAccessorMock : IPaymentTypeAccessor
    {
        private List<PaymentType> _paymentTypeList = new List<PaymentType>();

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Mock constructor to add data to the PaymentType list
        /// </summary>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is not working
        public PaymentTypeAccessorMock()
        {
            _paymentTypeList.Add(new PaymentType
            {
                PaymentTypeID = "PaymentType1",
                Description = "PaymentType1Description",
                Active = true
            });
            _paymentTypeList.Add(new PaymentType
            {
                PaymentTypeID = "PaymentType2",
                Description = "PaymentType2Description",
                Active = true
            });
            _paymentTypeList.Add(new PaymentType
            {
                PaymentTypeID = "PaymentType3",
                Description = "PaymentType3Description",
                Active = false
            });
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Mock method to retrieve a list of PaymentTypes by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns>A list of PaymentTypes</returns>
        public List<PaymentType> RetrievePaymentTypeListByActive(bool active = true)
        {
            List<PaymentType> paymentTypeList = new List<PaymentType>();

            foreach(var pt in _paymentTypeList)
            {
                if(pt.Active == true)
                {
                    paymentTypeList.Add(pt);
                }
            }

            return paymentTypeList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Mock method to deactivate a PaymentType
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <returns>PaymentTypes deactivated</returns>
        public int DeactivatePaymentTypeByID(string paymentTypeID)
        {
            int result = 0;

            foreach(var pt in _paymentTypeList)
            {
                if(pt.PaymentTypeID == paymentTypeID)
                {
                    pt.Active = false;
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Mock method to edit a PaymentType
        /// </summary>
        /// <param name="oldPaymentType"></param>
        /// <param name="newPaymentType"></param>
        /// <returns>PaymentTypes edited</returns>
        public int EditPaymentTypeByID(PaymentType oldPaymentType, PaymentType newPaymentType)
        {
            int result = 0;

            foreach(var pt in _paymentTypeList)
            {
                if (pt.PaymentTypeID == oldPaymentType.PaymentTypeID
                    && pt.Description == oldPaymentType.Description)
                {
                    pt.Description = newPaymentType.Description;
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/20/2018
        /// 
        /// Method to add a new Payment Type
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public int CreatePaymentType(string paymentTypeID, string description)
        {
            int rowCount;

            int typeCount = _paymentTypeList.Count;

            PaymentType paymentType = new PaymentType() { PaymentTypeID = paymentTypeID, Description = description };

            if (_paymentTypeList.Any(type => type.PaymentTypeID == paymentTypeID))
            {
                throw new ApplicationException();
            }

            _paymentTypeList.Add(paymentType);

            if (_paymentTypeList.Count > typeCount)
            {
                rowCount = 1;
            }
            else
            {
                rowCount = 0;
            }

            return rowCount;
        }
    }
}
