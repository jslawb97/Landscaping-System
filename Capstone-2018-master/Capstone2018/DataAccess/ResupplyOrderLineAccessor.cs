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
    public class ResupplyOrderLineAccessor : IResupplyOrderLineAccessor
    {
        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Creates a resupply order line in database
        /// </summary>
        /// <param name="resupplyOrderLine"></param>
        /// <returns></returns>
        public int CreateResupplyOrderLine(ResupplyOrderLine resupplyOrderLine)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_resupply_order_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ResupplyOrderID", resupplyOrderLine.ResupplyOrderID);
            cmd.Parameters.AddWithValue("@SupplyItemID", resupplyOrderLine.SupplyItemID);
            cmd.Parameters.AddWithValue("@Quantity", resupplyOrderLine.Quantity);
            cmd.Parameters.AddWithValue("@Price", resupplyOrderLine.Price);
            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                result = (int)id;
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem adding the resupply order lines");
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Deletes a resupply order line in database
        /// </summary>
        /// <param name="resupplyOrderLineID"></param>
        /// <returns></returns>
        public int DeleteResupplyOrderLineByID(int resupplyOrderLineID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_resupply_order_line_by_resupply_order_line_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderLineID", resupplyOrderLineID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem deleting the resupply order line.");
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Deletes all resupply lines assosciated with a resupply order
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public int DeleteResupplyOrderLineByResupplyOrderID(int resupplyOrderID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_resupply_order_line_by_resupply_order_id";
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

            return rows;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Edits a resupply order line in database
        /// </summary>
        /// <param name="oldResupplyOrderLineDetail"></param>
        /// <param name="newResupplyOrderLineDetail"></param>
        /// <returns></returns>
        public int EditResupplyOrderLine(ResupplyOrderLineDetail oldResupplyOrderLineDetail, ResupplyOrderLineDetail newResupplyOrderLineDetail)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_resupply_order_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderLineID", oldResupplyOrderLineDetail.ResupplyOrderLineID);
            cmd.Parameters.AddWithValue("@OldQuantity", oldResupplyOrderLineDetail.Quantity);
            cmd.Parameters.AddWithValue("@NewQuantity", newResupplyOrderLineDetail.Quantity);
            cmd.Parameters.AddWithValue("@NewPrice", newResupplyOrderLineDetail.Price);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error." + ex);
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
        /// Edits the order lines received field
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldQtyReceived"></param>
        /// <param name="newQtyReceived"></param>
        /// <returns></returns>
        public int EditResupplyOrderLineQtyReceivedByID(int id, int oldQtyReceived, int newQtyReceived)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_resupplyorderline_qtyreceived_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderLineID", id);
            cmd.Parameters.AddWithValue("@OldQtyReceived", oldQtyReceived);
            cmd.Parameters.AddWithValue("@NewQtyReceived", newQtyReceived);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error." + ex);
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
        /// Updates all orderline records' QtyReceived to the same value as Quantity for a certain ResupplyOrderID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(int id)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_resupplyorderlines_qtyreceived_to_quantityordered_by_resupplyorderid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderID", id);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error." + ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Retrieves a list of resupply order line details from database
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderID(int resupplyOrderID)
        {
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_resupply_order_line_detail_list_by_resupply_order_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderID", resupplyOrderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var resupplyOrderLineDetail = new ResupplyOrderLineDetail()
                        {
                            ResupplyOrderLineID = reader.GetInt32(0),
                            ResupplyOrderID = resupplyOrderID,
                            SupplyItemID = reader.GetInt32(1),
                            Quantity = reader.GetInt32(2),
                            Price = reader.GetDecimal(3),
                            VendorName = reader.GetString(4),
                            VendorID = reader.GetInt32(5),
                            NameOfItem = reader.GetString(6),
                        };
                        resupplyOrderLineDetailList.Add(resupplyOrderLineDetail);
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
            return resupplyOrderLineDetailList;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// 
        /// Gets a list including the qty received
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(int resupplyOrderID)
        {
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_resupply_order_line_detail_list_by_resupply_order_id_with_received";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ResupplyOrderID", resupplyOrderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var resupplyOrderLineDetail = new ResupplyOrderLineDetail()
                        {
                            ResupplyOrderLineID = reader.GetInt32(0),
                            ResupplyOrderID = resupplyOrderID,
                            SupplyItemID = reader.GetInt32(1),
                            Quantity = reader.GetInt32(2),
                            NameOfItem = reader.GetString(3),
                            QtyReceived = reader.GetInt32(4)
                        };
                        resupplyOrderLineDetailList.Add(resupplyOrderLineDetail);
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
            return resupplyOrderLineDetailList;
        }

    }
}
