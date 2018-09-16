using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public interface ICustomerTypeAccessor
    {
        /// <summary>
        /// Retrieves a list of CustomerType objects from the SqlServer crlandscaping database
        /// </summary>
        /// <returns>A list of CustomerTypes from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/18
        /// </remarks>
        List<CustomerType> RetrieveCustomerTypeList();

        int DeleteCustomerTypeByID(string customerID);

        /// <summary>
        /// Badis Saidani
        /// Created: 2018/04/08
        /// 
        /// Updates a Customer Type Record in the Database
        /// </summary>
        /// <param name="oldCustomerType"></param>
        /// <param name="newCustomerType"></param>
        /// <returns></returns>
        int EditCustomerType(CustomerType oldCustomerType, CustomerType newCustomerType);


        /// <summary>
        /// Mike Mason
        /// Created: 2018/04/12
        /// 
        /// Creates a Customer Type Record in the Database
        /// </summary>
        /// <param name="CustomerType"></param>
        /// <param name="customerType"></param>
        /// <returns></returns>
        int CreateCustomerType(CustomerType customerType);

    }
}
