using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// Sam Dramstad
    /// Created on 2018/02/20
    /// 
    /// Interface for Customer Manager class.
    /// <remarks>
    /// Brady Feller
    /// Revised 2018/04/20
    /// 
    /// added RetrievebyCustomerEmail
    /// </remarks>
    /// </summary>
    /// <returns></returns>
    /// 
    public interface ICustomerManager
    {
        List<Customer> RetrieveCustomerList();
        bool DeactivateCustomer(int customerID);
        List<Customer> RetrieveCustomerListByActive(bool active = true);
        bool EditCustomer(Customer customer, Customer oldCustomer);
        List<Customer> RetrieveCustomerTypeList(bool active = true);
        int CreateCustomer(Customer customer);
        Customer RetrieveCustomerById(int customerID);
        Customer RetrieveCustomerByEmail(string email);

    }
}
