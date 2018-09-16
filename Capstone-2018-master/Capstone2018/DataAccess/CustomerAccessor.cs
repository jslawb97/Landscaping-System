using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccess
{
    public class CustomerAccessor : ICustomerAccessor
    {

        /// <summary>
		/// Sam Dramstad
		/// Created on 2018/02/20
		/// 
		/// Method to use stored procedure to get list of customers from database
		/// </summary>
		/// <returns></returns>
        /// 
        List<Customer> ICustomerAccessor.RetrieveCustomerListByActive(bool active = true)
        {
            var customers = new List<Customer>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_customer_list";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var customer = new Customer()
                        {
                            CustomerID = reader.GetInt32(0),
                            CustomerTypeID = reader.GetString(1),
                            Email = reader.GetString(2),
                            FirstName = reader.GetString(3),
                            LastName = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };
                        customers.Add(customer);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data.", ex);

            }
            finally
            {
                conn.Close();
            }

            return customers;
        }

        /// <summary>
		/// Sam Dramstad
		/// Created on 2018/02/20
		/// 
		/// Method to use stored procedure to deactivate a customer
		/// </summary>
		/// <returns></returns>
        /// 
        int ICustomerAccessor.DeactivateCustomer(int customerID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_deactivate_customer_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the list of all customer records from the Sql Server database
        /// </summary>
        /// <returns></returns>
        public List<Customer> RetrieveCustomerList()
        {
            List<Customer> items = new List<Customer>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_customer_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new Customer()
                        {
                            CustomerID = reader.GetInt32(0),
                            CustomerTypeID = reader.GetString(1),
                            Email = reader.GetString(2),
                            FirstName = reader.GetString(3),
                            LastName = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };

                        items.Add(item);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return items;
        }

        /// <summary>
		/// Sam Dramstad
		/// Created on 2018/02/20
		/// 
		/// Method to use stored procedure to edit a customer
        /// <remarks>Added CustomerTypeID and changed stored procedure to reflect it - Mike Mason</remarks>
		/// </summary>
        public bool EditCustomerById(Customer customer, Customer oldCustomer)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_customer_by_id_with_customerTypeID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerID", oldCustomer.CustomerID);
            cmd.Parameters.AddWithValue("@NewCustomerTypeID", customer.CustomerTypeID);
            cmd.Parameters.AddWithValue("@NewEmail", customer.Email);
            cmd.Parameters.AddWithValue("@NewFirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", customer.LastName);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldCustomerTypeID", oldCustomer.CustomerTypeID);
            cmd.Parameters.AddWithValue("@OldEmail", oldCustomer.Email);
            cmd.Parameters.AddWithValue("@OldFirstName", oldCustomer.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldCustomer.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldCustomer.PhoneNumber);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error. ", ex);
            }
            finally
            {
                conn.Close();
            }
            if(rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    

        /// <summary>
        /// Mike Mason
        /// Created on 2018/03/8
        /// 
        /// Method to use stored procedure to get list of customer types from database
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customer> RetrieveCustomerTypeList(bool active = true)
        {
            var customerTypeList = new List<Customer>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_customertype";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var customer = new Customer()
                        {
                            
                            CustomerTypeID = reader.GetString(0)
                           
                        };
                        customerTypeList.Add(customer);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data", ex);

            }
            finally
            {
                conn.Close();
            }
            return customerTypeList;
        }


        /// <summary>
        /// Mike Mason
        /// Created on 2018/02/21
        /// 
        /// Method to create new customers in the database
        /// </summary>
        /// <remarks>
        /// Zachary Hall
        /// Created 2018/03/23
        /// 
        /// We need a way to get the newly created id from the database, so we need it to not return rows affect, but the scopeidentity
        /// </remarks>
        /// <returns></returns>
        public int CreateCustomer(Customer customer)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@CustomerTypeID", customer.CustomerTypeID);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a Customer record from the Sql Server database that matches the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer RetrieveCustomerByID(int id)
        {
            Customer customer = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_customer_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        CustomerTypeID = reader.GetString(1),
                        Email = reader.GetString(2),
                        FirstName = reader.GetString(3),
                        LastName = reader.GetString(4),
                        PhoneNumber = reader.GetString(5),
                        Active = reader.GetBoolean(6)
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }


            return customer;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Gets a Customer record from the Sql Server database that matches the given email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer RetrieveCustomerByEmail(string email)
        {
            Customer customer = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_customer_by_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        CustomerTypeID = reader.GetString(1),
                        Email = reader.GetString(2),
                        FirstName = reader.GetString(3),
                        LastName = reader.GetString(4),
                        PhoneNumber = reader.GetString(5),
                        Active = reader.GetBoolean(6)
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }


            return customer;
        }

        
        
    }
}

