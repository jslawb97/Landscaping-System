using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// Sam Dramstad
    /// Created 02/20/2018
    /// 
    /// Interface for the Customer Accessor
    /// </summary>
    public interface ICustomerAccessor
    {
        List<Customer> RetrieveCustomerList();
        int DeactivateCustomer(int customerID);
        List<Customer> RetrieveCustomerListByActive(bool active = true);
        int CreateCustomer(Customer customer);
        bool EditCustomerById(Customer customer, Customer oldCustomer);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a Customer object by the id of a customer record in a data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer RetrieveCustomerByID(int id);

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Gets a Customer object by the email of a customer record in a data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer RetrieveCustomerByEmail(string email);
    }
}
