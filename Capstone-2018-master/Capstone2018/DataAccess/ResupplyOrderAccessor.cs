using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public class ResupplyOrderAccessor : IResupplyOrderAccessor
    {
        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to use stored procedure to create a supply order
        /// </summary>
        /// <param name="resupplyOrder"></param>
        /// <returns></returns>
        public int CreateResupplyOrder(ResupplyOrder resupplyOrder)
        {
            int resupplyOrderID = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_resupplyorder";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", resupplyOrder.EmployeeID);
            cmd.Parameters.AddWithValue("@Date", resupplyOrder.Date);
            cmd.Parameters.AddWithValue("@SupplyStatusID", resupplyOrder.SupplyStatusID);
            cmd.Parameters.AddWithValue("@VendorID", resupplyOrder.VendorID);

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                resupplyOrderID = (int)id;
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem adding your resupply order.");
            }
            finally
            {
                conn.Close();
            }
            return resupplyOrderID;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to delete a resupply order from the database
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public int DeleteResupplyOrderByID(int resupplyOrderID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_resupply_order_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderID", resupplyOrderID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error" + ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Weston Olund 
        /// Created on 2018/03/08
        /// 
        /// Method to edit a resupply order in the database using a stored procedure
        /// </summary>
        /// <param name="oldResupplyOrder"></param>
        /// <param name="newResupplyOrder"></param>
        /// <returns></returns>
        public int EditResupplyOrder(ResupplyOrder oldResupplyOrder, ResupplyOrder newResupplyOrder)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_resupplyorder_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderID", oldResupplyOrder.ResupplyOrderID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldResupplyOrder.EmployeeID);
            cmd.Parameters.AddWithValue("@OldDate", oldResupplyOrder.Date);
            cmd.Parameters.AddWithValue("@OldSupplyStatusID", oldResupplyOrder.SupplyStatusID);
            cmd.Parameters.AddWithValue("@NewEmployeeID", newResupplyOrder.EmployeeID);
            cmd.Parameters.AddWithValue("@NewDate", newResupplyOrder.Date);
            cmd.Parameters.AddWithValue("@NewSupplyStatusID", newResupplyOrder.SupplyStatusID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ApplicationException("Database access error.");
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// 
        /// Updates supply status field for resupply order record
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public int EditResupplyOrderStatus(int resupplyOrderID, string oldStatus, string newStatus)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_resupplyorder_status_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ResupplyOrderID", resupplyOrderID);
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
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to retrieve a resupply order by id from database
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public ResupplyOrder RetrieveResupplyOrderByID(int resupplyOrderID)
        {
            var resupplyOrder = new ResupplyOrder();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_resupplyorder_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderID", resupplyOrderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resupplyOrder = new ResupplyOrder()
                    {
                        ResupplyOrderID = resupplyOrderID,
                        EmployeeID = reader.GetInt32(1),
                        Date = reader.GetDateTime(2),
                        SupplyStatusID = reader.GetString(3),
                        VendorID = reader.GetInt32(4)
                    };
                }
                else
                {
                    throw new ApplicationException("Resupply order record not found");
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
            return resupplyOrder;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// To return list of resupply orders from database
        /// </summary>
        /// <returns></returns>
        public List<ResupplyOrder> RetrieveResupplyOrderList()
        {
            List<ResupplyOrder> resupplyOrderList = new List<ResupplyOrder>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_resupplyorder_list";
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
                        var resupplyOrder = new ResupplyOrder();
                        resupplyOrder.ResupplyOrderID = reader.GetInt32(0);
                        resupplyOrder.EmployeeID = reader.GetInt32(1);
                        resupplyOrder.Date = reader.GetDateTime(2);
                        resupplyOrder.SupplyStatusID = reader.GetString(3);
                        resupplyOrder.VendorID = reader.GetInt32(4);
                        resupplyOrderList.Add(resupplyOrder);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
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
            return resupplyOrderList;
        }
    }
}
