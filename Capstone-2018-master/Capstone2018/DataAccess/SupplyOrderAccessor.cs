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
    public class SupplyOrderAccessor : ISupplyOrderAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Adds a new supply order to the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int CreateSupplyOrderNoJob(SupplyOrder order)
        {
            int supplyOrderID = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_supplyorder_nojob";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@SupplyStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Date", SqlDbType.Date);

            cmd.Parameters["@EmployeeID"].Value = order.EmployeeID;
            cmd.Parameters["@SupplyStatusID"].Value = order.SupplyStatusID;
            cmd.Parameters["@Date"].Value = order.Date;

            try
            {
                conn.Open();
                supplyOrderID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return supplyOrderID;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Deactivates an existing order in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public int DeactivateSupplyOrderByID(int id, string statusID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_deactivate_supplyorder_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyOrderID", SqlDbType.Int);
            cmd.Parameters.Add("@OldSupplyStatusID", SqlDbType.NVarChar, 100);

            cmd.Parameters["@SupplyOrderID"].Value = id;
            cmd.Parameters["@OldSupplyStatusID"].Value = statusID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Supply Order cancellation failed.");
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

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Updates an existing supply order in the database with a job id
        /// </summary>
        /// <param name="oldOrder"></param>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public int EditSupplyOrder(SupplyOrder oldOrder, SupplyOrder newOrder)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_supplyorder_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyOrderID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldJobID", SqlDbType.Int);
            cmd.Parameters.Add("@OldSupplyStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldDate", SqlDbType.Date);
            cmd.Parameters.Add("@NewEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@NewJobID", SqlDbType.Int);
            cmd.Parameters.Add("@NewSupplyStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewDate", SqlDbType.Date);

            cmd.Parameters["@SupplyOrderID"].Value = oldOrder.SupplyOrderID;
            cmd.Parameters["@OldEmployeeID"].Value = oldOrder.EmployeeID;
            cmd.Parameters["@OldJobID"].Value = oldOrder.JobID;
            cmd.Parameters["@OldSupplyStatusID"].Value = oldOrder.SupplyStatusID;
            cmd.Parameters["@OldDate"].Value = oldOrder.Date;
            cmd.Parameters["@NewEmployeeID"].Value = newOrder.EmployeeID;
            cmd.Parameters["@NewJobID"].Value = newOrder.JobID;
            cmd.Parameters["@NewSupplyStatusID"].Value = newOrder.SupplyStatusID;
            cmd.Parameters["@NewDate"].Value = newOrder.Date;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Supply Order update failed.");
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

            return result;
        }


        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Updates an existing supply order in the database without a job id
        /// </summary>
        /// <param name="oldOrder"></param>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public int EditSupplyOrderNoJob(SupplyOrder oldOrder, SupplyOrder newOrder)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_supplyorder_by_id_no_job";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyOrderID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldSupplyStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldDate", SqlDbType.Date);
            cmd.Parameters.Add("@NewEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@NewSupplyStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewDate", SqlDbType.Date);

            cmd.Parameters["@SupplyOrderID"].Value = oldOrder.SupplyOrderID;
            cmd.Parameters["@OldEmployeeID"].Value = oldOrder.EmployeeID;
            cmd.Parameters["@OldSupplyStatusID"].Value = oldOrder.SupplyStatusID;
            cmd.Parameters["@OldDate"].Value = oldOrder.Date;
            cmd.Parameters["@NewEmployeeID"].Value = newOrder.EmployeeID;
            cmd.Parameters["@NewSupplyStatusID"].Value = newOrder.SupplyStatusID;
            cmd.Parameters["@NewDate"].Value = newOrder.Date;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Supply Order update failed.");
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

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Retrieves a specific supply order from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplyOrder RetrieveSupplyOrderByID(int id)
        {
            SupplyOrder supplyOrder = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_supplyorder_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyOrderID", SqlDbType.Int);

            cmd.Parameters["@SupplyOrderID"].Value = id;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    supplyOrder = new SupplyOrder()
                    {
                        SupplyOrderID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        JobID = (int?)reader.GetInt32(2),
                        SupplyStatusID = reader.GetString(3),
                        Date = reader.GetDateTime(4)
                    };

                }
                else
                {
                    throw new ApplicationException("Supply Order not found");
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


            return supplyOrder;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Retrieves all active supply orders in the database (not cancelled)
        /// </summary>
        /// <returns></returns>
        public List<SupplyOrder> RetrieveSupplyOrderList()
        {
            List<SupplyOrder> supplyOrders = new List<SupplyOrder>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_supplyorder_list_by_active";

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
                        var supplyOrder = new SupplyOrder()
                        {
                            SupplyOrderID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            JobID = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2),
                            SupplyStatusID = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                        supplyOrders.Add(supplyOrder);
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


            return supplyOrders;
        }
    }
}
