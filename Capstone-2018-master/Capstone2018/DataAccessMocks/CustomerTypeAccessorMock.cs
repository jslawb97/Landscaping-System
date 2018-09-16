using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class CustomerTypeAccessorMock : ICustomerTypeAccessor
    {
        private List<CustomerType> _customerTypeList = new List<CustomerType>();

        /// <summary>
        /// John Miller
        /// Created 2018/02/18
        /// 
        /// Mock constructor to add data to the customer type list
        /// </summary>
        public CustomerTypeAccessorMock()
        {
            _customerTypeList.Add(new CustomerType()
            {
                CustomerTypeID = "CommercialTest"
            });
            _customerTypeList.Add(new CustomerType()
            {
                CustomerTypeID = "ResidentialTest"
            });
        }

        public List<CustomerType> RetrieveCustomerTypeList()
        {
            return _customerTypeList;
        }

        /// /// <summary>
        /// Noah Davison
        /// Created on 2018/02/22
        /// 
        /// method to return mock data
        /// </summary>
        /// <returns></returns>
        public int DeleteCustomerTypeByID(string customerTypeID)
        {
            int result = 0;

            foreach (CustomerType customerType in _customerTypeList)
            {
                if (customerType.CustomerTypeID == customerTypeID)
                {
                    _customerTypeList.Remove(customerType);
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/9
        /// 
        /// Edits the data of the oldCustomerType record with data from newCustomerType
        /// </summary>
        /// <param name="oldCustomerType"></param>
        /// <param name="newCustomerType"></param>
        /// <returns></returns>
        public int EditCustomerType(CustomerType oldCustomerType, CustomerType newCustomerType)
        {
            int rowsAffected = 0;
            try
            {

                foreach (var c in _customerTypeList)
                {
                    if (c.CustomerTypeID == oldCustomerType.CustomerTypeID)
                    {
                        c.CustomerTypeID = newCustomerType.CustomerTypeID;

                        rowsAffected++;
                    }
                }


                if (rowsAffected == 0)
                {
                    throw new ApplicationException("The CustomerType was not updated");

                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/04/12
        /// 
        /// Mock method to add customerType
        /// </summary>
        /// <returns></returns>
        public int CreateCustomerType(CustomerType customerType)
        {
            try
            {
                this._customerTypeList.Add(customerType);
                return Constants.IDSTARTVALUE + _customerTypeList.Count + 1; ;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
