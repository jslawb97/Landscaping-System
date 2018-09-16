using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class SpecialOrderAccessor : ISpecialOrderAccessor
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Adds a Special Order to the database
        /// </summary>
        /// <param name="specialOrder"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        public int CreateSpecialOrder(SpecialOrder specialOrder)
        {
            int newOrderID;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_specialorder_with_vendor";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", specialOrder.EmployeeID);
            cmd.Parameters.Add("@JobID", SqlDbType.Int);

            if (specialOrder.JobID.HasValue)
            {
                cmd.Parameters["@JobID"].Value = specialOrder.JobID;
            }
            else
            {
                cmd.Parameters["@JobID"].Value = DBNull.Value;
            }
            cmd.Parameters.AddWithValue("@Date", specialOrder.Date);
            cmd.Parameters.AddWithValue("@SupplyStatusID", specialOrder.SupplyStatusID);
            cmd.Parameters.AddWithValue("@VendorID", specialOrder.VendorID);

            try
            {
                conn.Open();

                newOrderID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem adding your data:", ex);
            }

            return newOrderID;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Deletes a Special Order from the database
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        public int DeleteSpecialOrderByID(int specialOrderID)
        {
            int rowCount;
            
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_delete_specialorder_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", specialOrderID);

            try
            {
                conn.Open();

                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Edits a Special Order in the database
        /// </summary>
        /// <param name="newSpecialOrder"></param>
        /// <param name="oldSpecialOrder"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        public int EditSpecialOrder(SpecialOrder newSpecialOrder, SpecialOrder oldSpecialOrder)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_specialorder_with_vendor";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", oldSpecialOrder.SpecialOrderID);

            cmd.Parameters.AddWithValue("@OldEmployeeID", oldSpecialOrder.EmployeeID);
            cmd.Parameters.Add("@OldJobID", SqlDbType.Int);

            if (oldSpecialOrder.JobID.HasValue)
            {
                cmd.Parameters["@OldobID"].Value = oldSpecialOrder.JobID;
            }
            else
            {
                cmd.Parameters["@OldJobID"].Value = DBNull.Value;
            }
            cmd.Parameters.AddWithValue("@OldDate", oldSpecialOrder.Date);
            cmd.Parameters.AddWithValue("@OldSupplyStatusID", oldSpecialOrder.SupplyStatusID);
            cmd.Parameters.AddWithValue("@OldVendorID", oldSpecialOrder.VendorID);

            cmd.Parameters.AddWithValue("@NewEmployeeID", newSpecialOrder.EmployeeID);
            cmd.Parameters.Add("@NewJobID", SqlDbType.Int);

            if (newSpecialOrder.JobID.HasValue)
            {
                cmd.Parameters["@NewJobID"].Value = newSpecialOrder.JobID;
            }
            else
            {
                cmd.Parameters["@NewJobID"].Value = DBNull.Value;
            }
            cmd.Parameters.AddWithValue("@NewDate", newSpecialOrder.Date);
            cmd.Parameters.AddWithValue("@NewSupplyStatusID", newSpecialOrder.SupplyStatusID);
            cmd.Parameters.AddWithValue("@NewVendorID", newSpecialOrder.VendorID);

            try
            {
                conn.Open();

                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem editing your data:", ex);
            }

            return rowCount;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        public int EditSpecialOrderSupplyStatusByID(int id, string oldStatus, string newStatus)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_specialorder_status_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", id);
            cmd.Parameters.AddWithValue("@OldStatus", oldStatus);
            cmd.Parameters.AddWithValue("@NewStatus", newStatus);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
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
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from 
        /// the database made by the same employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrderByEmployeeID(int employeeID)
        {
            var specialOrderList = new List<SpecialOrder>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_specialorder_by_employeeid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeId", employeeID);

            try
            {

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var specialOrder = new SpecialOrder()
                        {
                            SpecialOrderID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            JobID = reader.GetInt32(2),
                            Date = reader.GetDateTime(3),
                            SupplyStatusID = reader.GetString(4)
                        };
                        specialOrderList.Add(specialOrder);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found");
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data", ex); ;
            }
            finally
            {
                conn.Close();
            }

            return specialOrderList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a Special Order from the database
        /// using its unique Special Order ID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public SpecialOrder RetrieveSpecialOrderByID(int specialOrderID)
        {
            SpecialOrder specialOrder = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_specialorder_by_id_with_vendor";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderId", specialOrderID);

            try
            {

                conn.Open();
                var reader = cmd.ExecuteReader();

                // look at this and make sure this works and makes with
                // what we've done in the past.
                if (reader.HasRows)
                {
                    reader.Read();

                    specialOrder = new SpecialOrder()
                    {
                        SpecialOrderID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        JobID = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        SupplyStatusID = reader.GetString(4),
                        VendorID = reader.GetInt32(5)
                    };
                    
                }
                else
                {
                    throw new ApplicationException("No Special Order with that ID found");
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data", ex); ;
            }
            finally
            {
                conn.Close();
            }

            return specialOrder;

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from 
        /// the database for the same job
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrderByJobID(int jobID)
        {
            var specialOrderList = new List<SpecialOrder>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_specialorder_by_jobid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobID", jobID);

            try
            {

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var specialOrder = new SpecialOrder()
                        {
                            SpecialOrderID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            JobID = reader.GetInt32(2),
                            Date = reader.GetDateTime(3),
                            SupplyStatusID = reader.GetString(4)
                        };
                        specialOrderList.Add(specialOrder);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found");
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data", ex); ;
            }
            finally
            {
                conn.Close();
            }

            return specialOrderList;

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from
        /// the database with the same status
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrderByStatusID(string statusID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all Special Orders
        /// </summary>
        /// <returns></returns>
        public List<SpecialOrder> RetrieveSpecialOrders()
        {
            var specialOrderList = new List<SpecialOrder>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_specialorder_list_with_vendor";

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
                        var specialOrder = new SpecialOrder()
                        {
                            SpecialOrderID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            JobID = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2),
                            Date = reader.GetDateTime(3),
                            SupplyStatusID = reader.GetString(4),
                            VendorID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5)
                        };
                        specialOrderList.Add(specialOrder);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found");
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data", ex); ;
            }
            finally
            {
                conn.Close();
            }

            return specialOrderList;
        }
    }
}
