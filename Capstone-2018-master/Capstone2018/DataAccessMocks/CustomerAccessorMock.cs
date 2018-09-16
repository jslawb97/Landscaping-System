using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class CustomerAccessorMock : ICustomerAccessor
    {
        private List<Customer> _customerList = new List<Customer>();

        /// <summary>
        /// Sam Dramstad
        /// Created 02/20/2018
        /// 
        /// Mock data for the customer accessor.
        /// </summary>
        public CustomerAccessorMock()
        {
            _customerList.Add(new Customer()
            {
                CustomerID = 1000000,
                CustomerTypeID = "Residential",
                Email = "email@whatever.com",
                FirstName = "Name 1",
                LastName = "Name 2",
                PhoneNumber = "3994340502",
                PasswordHash = "23482f9fhj8298fj323j238f29j89f23",
                Active = true
            });

            _customerList.Add(new Customer()
            {
                CustomerID = 1000001,
                CustomerTypeID = "Residential",
                Email = "email@whatever.com",
                FirstName = "Name 3",
                LastName = "Name 4",
                PhoneNumber = "232322323",
                PasswordHash = "23482f9fhj8298fj323j238f29j89f23",
                Active = true
            });

        }
        /// <summary>
        /// Sam Dramstad
        /// Created 02/20/2018
        /// 
        /// Deactivates a customer from the mock data.
        /// </summary>
        public int DeactivateCustomer(int customerID)
        {

            if (customerID >= Constants.IDSTARTVALUE)
            {
                int result = 0;

                foreach (Customer customer in _customerList)
                {
                    if (customer.CustomerID == customerID)
                    {
                        customer.Active = false;
                        result++;
                        break;
                    }
                }

                if (result == 0)
                {
                    throw new ApplicationException("No customer with that ID.");
                }
                else
                {
                    return result;
                }
            }
            else
            {
                throw new ApplicationException("Bad Customer ID entered.");
            }


        }

        /// <summary>
        /// Jayden Tollefson
        /// Created 2018/03/01
        /// 
        /// Edit a mock customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="oldCustomer"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool EditCustomerById(Customer customer, Customer oldCustomer)
        {
            var result = 0;

            this._customerList.ForEach(customerList =>
            {
                if (customerList == oldCustomer)
                {
                    customerList.FirstName = customer.FirstName;
                    customerList.LastName = customer.LastName;
                    customerList.Email = customer.Email;
                    customerList.CustomerTypeID = customer.CustomerTypeID;
                    customerList.PhoneNumber = customer.PhoneNumber;
                    customerList.Active = customer.Active;
                    result = 1;
                }
            });
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Customer> RetrieveCustomerList()
        {
            return _customerList;
        }

        /// <summary>
        /// Sam Dramstad
        /// Created 02/20/2018
        /// 
        /// Retrieves the customers from the sample data that are active.
        /// </summary>
        public List<Customer> RetrieveCustomerListByActive(bool active = true)
        {
            List<Customer> customerList = new List<Customer>();

            foreach (Customer customer in _customerList)
            {
                if (customer.Active == active)
                {

                    customerList.Add(customer);
                }
            }

            return customerList;
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/02/21
        /// 
        /// Mock method to add customer
        /// </summary>
        /// <returns></returns>
        public int CreateCustomer(Customer customer)
        {
            try
            {
                this._customerList.Add(customer);
                return Constants.IDSTARTVALUE + _customerList.Count + 1; ;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Customer RetrieveCustomerByID(int id)
        {
            return this._customerList.Find(customer => customer.CustomerID.Equals(id));
        }

        public Customer RetrieveCustomerByEmail(string email)
        {
            return this._customerList.Find(customer => customer.Email.Equals(email));
        }
    }
}
