using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;
using System.Data.SqlClient;

namespace Logic
{
    public class CustomerTypeManager : ICustomerTypeManager
    {
        private ICustomerTypeAccessor _customerTypeAccessor;

        /// <summary>
        /// Manager Constructor for handling accessor dependency
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/18
        /// </remarks>
        public CustomerTypeManager()
        {
            _customerTypeAccessor = new CustomerTypeAccessor();
        }

        // Constructor for unit tests
        public CustomerTypeManager(ICustomerTypeAccessor customerTypeAccessor)
        {
            this._customerTypeAccessor = customerTypeAccessor;
        }

        /// <summary>
        /// Retrieves a list of CustomerType objects from CustomerType class
        /// </summary>
        /// <returns>A list of CustomerType objects</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        public List<CustomerType> RetrieveCustomerTypeList()
        {
            var customerTypes = new List<CustomerType>();

            try
            {
                customerTypes = _customerTypeAccessor.RetrieveCustomerTypeList();
            }
            catch (Exception)
            {
                throw;
            }
            return customerTypes;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/22
        /// 
        /// Deletes a customer type from the database.
        /// <param name="customerTypeID">The ID of the customer type to be deleted</param>
        /// <exception cref="Exception">Delete fails</exception>
        /// </summary>
        public bool DeleteCustomerType(string customerTypeID)
        {
            try
            {
                if (1 == _customerTypeAccessor.DeleteCustomerTypeByID(customerTypeID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Badis Saidani
        /// Created: 2018/04/08
        /// 
        /// Updates a Customer Type Record in the Database
        /// </summary>
        /// <param name="oldCustomerType"></param>
        /// <param name="newCustomerType"></param>
        /// <returns></returns>
        public int EditCustomerType(CustomerType oldCustomerType, CustomerType newCustomerType)
        {
            var result = 0;

            if (newCustomerType.CustomerTypeID == "")
            {
                throw new ApplicationException("You must fill the 'type' field.");
            }
            try
            {
                result = _customerTypeAccessor.EditCustomerType(oldCustomerType, newCustomerType);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        ///	Mike Mason
        ///	Created on 2018/04/12
        ///	
        /// Method to create customerType from data access layer 
        /// 
        /// </summary>
        /// <returns></returns>
        public int CreateCustomerType(CustomerType customerType)
        {
            var result = 0;

            try
            {
                result = _customerTypeAccessor.CreateCustomerType(customerType);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


    }
}
