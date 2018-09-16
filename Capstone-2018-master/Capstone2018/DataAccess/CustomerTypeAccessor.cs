using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;

namespace DataAccess
{
    /// <summary>
    /// Facilitates CustomerType data movement between the application and the SqlServer database 
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/02/18
    /// </remarks>
    public class CustomerTypeAccessor : ICustomerTypeAccessor
    {
        public List<CustomerType> RetrieveCustomerTypeList()
        {
            List<CustomerType> customerTypes = new List<CustomerType>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_customertype_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var customerType = new CustomerType()
                        {
                            CustomerTypeID = reader.GetString(0)
                        };
                        customerTypes.Add(customerType);
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
        public int DeleteCustomerTypeByID(string customerTypeID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_customertype_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerTypeID", SqlDbType.NVarChar);
            cmd.Parameters["@CustomerTypeID"].Value = customerTypeID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the customer type", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
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
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_customertype_by_id ";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewCustomerTypeID", newCustomerType.CustomerTypeID);

            cmd.Parameters.AddWithValue("@OldCustomerTypeID", oldCustomerType.CustomerTypeID);


            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return rows;
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/04/12
        /// 
        /// Method to create new customer types in the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        public int CreateCustomerType(CustomerType customerType)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_customertype";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@CustomerTypeID", customerType.CustomerTypeID);

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
    }
}
