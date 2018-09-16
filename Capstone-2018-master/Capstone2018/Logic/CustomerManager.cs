using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{

    public class CustomerManager : ICustomerManager
    {
        private ICustomerAccessor _customerAccessor;

        //Real use
        public CustomerManager()
        {
            _customerAccessor = new CustomerAccessor();
        }

        //Unit testing
        public CustomerManager(ICustomerAccessor customerAccessor)
        {
            _customerAccessor = customerAccessor;
        }


        /// <summary>
        /// Sam Dramstad
        /// Created 02/20/2018
        /// 
        /// Method to retrieve a list of customers from the data access layer.
        /// </summary>

        public List<Customer> RetrieveCustomerListByActive(bool active = true)
        {
            List<Customer> customerList = new List<Customer>();

            try
            {
                customerList = _customerAccessor.RetrieveCustomerListByActive(active);
            }
            catch (Exception)
            {
                throw;
            }

            return customerList;
        }

        /// <summary>
        /// Sam Dramstad
        /// Created 02/20/2018
        /// 
        /// Method to deactivate a customer.
        /// </summary>
        public bool DeactivateCustomer(int customerID)
        {
            bool result = false;

            try
            {
                result = (1 == _customerAccessor.DeactivateCustomer(customerID));
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }

        /// <summary>
         /// Zachary Hall
         /// Created 2018/03/10
         /// 
         /// Gets a list of customer records
         /// </summary>
         /// <returns></returns>
        public List<Customer> RetrieveCustomerList()
        {
            var list = new List<Customer>();

            try
            {
                list = _customerAccessor.RetrieveCustomerList();
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        public bool EditCustomer(Customer oldCustomer, Customer customer)
        {
            if (customer.CustomerTypeID == null)
            {
                throw new ApplicationException("You must select a customer type.");
            }
            if (customer.CustomerTypeID.Length <= 0)
            {
                throw new ApplicationException("You must select a customer type.");
            }
            if (customer.CustomerTypeID.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The customer type must be shorter than 100 characters.");
            }

            if (customer.Email == null)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (customer.Email.Length <= 0)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (customer.Email.Length > Constants.MAXEMAILLENGTH)
            {
                throw new ApplicationException("The email must be shorter than 100 characters.");
            }

            if (customer.FirstName == null)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (customer.FirstName.Length <= 0)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (customer.FirstName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The first name must be shorter than 100 characters.");
            }

            if (customer.LastName == null)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (customer.LastName.Length <= 0)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (customer.LastName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The last name must be shorter than 100 characters.");
            }

            if (customer.PhoneNumber == null)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (customer.PhoneNumber.Length <= 0)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (customer.PhoneNumber.Length > Constants.MAXPHONENUMBERLENGTH)
            {
                throw new ApplicationException("The phone number must be shorter than 15 characters.");
            }
            try
            {
                return _customerAccessor.EditCustomerById(customer, oldCustomer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Customer> RetrieveCustomerTypeList(bool active = true)
        {


            List<Customer> customerTypeList = null;

            try
            {
                customerTypeList = _customerAccessor.RetrieveCustomerListByActive();
            }
            catch (Exception)
            {
                throw;
            }
            return customerTypeList;
        }

        /// <summary>
        ///	Mike Mason
        ///	Created on 2018/02/21
        ///	
        /// Method to create customers from data access layer 
        /// 
        /// </summary>
        /// <returns></returns>
        public int CreateCustomer(Customer customer)
        {
            //var result = 0;

            if (customer.CustomerTypeID == null)
            {
                throw new ApplicationException("You must select a customer type.");
            }
            if (customer.CustomerTypeID.Length <= 0)
            {
                throw new ApplicationException("You must select a customer type.");
            }
            if (customer.CustomerTypeID.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The customer type must be shorter than 100 characters.");
            }

            if (customer.Email == null)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (customer.Email.Length <= 0)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (customer.Email.Length > Constants.MAXEMAILLENGTH)
            {
                throw new ApplicationException("The email must be shorter than 100 characters.");
            }

            if (customer.FirstName == null)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (customer.FirstName.Length <= 0)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (customer.FirstName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The first name must be shorter than 100 characters.");
            }

            if (customer.LastName == null)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (customer.LastName.Length <= 0)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (customer.LastName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The last name must be shorter than 100 characters.");
            }

            if (customer.PhoneNumber == null)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (customer.PhoneNumber.Length <= 0)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (customer.PhoneNumber.Length > Constants.MAXPHONENUMBERLENGTH)
            {
                throw new ApplicationException("The phone number must be shorter than 15 characters.");
            }
            try
            {
                return  _customerAccessor.CreateCustomer(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }
		
        public Customer RetrieveCustomerById(int customerId)
        {
            Customer customerList = null;

            try
            {
                customerList = _customerAccessor.RetrieveCustomerByID(customerId);
            }
            catch (Exception)
            {
                throw;
            }
            return customerList;
        }

        public Customer RetrieveCustomerByEmail(string email)
        {
            Customer customerList = null;

            try
            {
                customerList = _customerAccessor.RetrieveCustomerByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
            return customerList;
        }
    }
}
