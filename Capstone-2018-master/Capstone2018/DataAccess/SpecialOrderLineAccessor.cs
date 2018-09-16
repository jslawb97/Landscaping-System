using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SpecialOrderLineAccessor : ISpecialOrderLineAccessor
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Creates a new Special Order Line
        /// </summary>
        /// <param name="specialOrderLine"></param>
        /// <returns></returns>
        public int CreateSpecialOrderLine(SpecialOrderLine specialOrderLine)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_specialorderline_correct";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", specialOrderLine.SpecialOrderID);
            cmd.Parameters.AddWithValue("@SpecialOrderItemID", specialOrderLine.SpecialOrderItemID);
            cmd.Parameters.AddWithValue("@Quantity", specialOrderLine.Quantity);

            try
            {
                conn.Open();

                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem adding your data:", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Deletes a Special Order Line by ID
        /// </summary>
        /// <param name="specialOrderLineID"></param>
        /// <returns></returns>
        public int DeleteSpecialOrderLine(int specialOrderLineID)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_delete_specialorderline";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderLineID", specialOrderLineID);

            try
            {
                conn.Open();

                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting your data", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Updates a Special Order Line
        /// </summary>
        /// <param name="oldLine"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        public int EditSpecialOrderLine(SpecialOrderLine oldLine, SpecialOrderLine newLine)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_specialorderline_correct";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderLineID", newLine.SpecialOrderLineID);
            cmd.Parameters.AddWithValue("@OldQuantity", oldLine.Quantity);
            cmd.Parameters.AddWithValue("@NewQuantity", newLine.Quantity);

            try
            {
                conn.Open();

                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem editing your data:", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldRecieved"></param>
        /// <param name="newRecieved"></param>
        /// <returns></returns>
        public int EditSpecialOrderLineQtyReceivedByID(int id, int oldRecieved, int newRecieved)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_specialorderline_qtyreceived_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SpecialOrderLineID", id);
            cmd.Parameters.AddWithValue("@OldQtyReceived", oldRecieved);
            cmd.Parameters.AddWithValue("@NewQtyReceived", newRecieved);

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
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EditSpecialOrderLineQtyReceivedToQtyOrderedByOrderID(int id)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_specialorderlines_qtyreceived_to_quantityordered_by_specialorderid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SpecialOrderID", id);

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
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Retrieves a Special Order Line by ID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        public SpecialOrderLine RetrieveSpecialOrderLineByID(int specialOrderLineID)
        {
            SpecialOrderLine specialOrderLine = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_specialorderline_by_id_correct";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderLineID", specialOrderLineID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    specialOrderLine = new SpecialOrderLine()
                    {
                        SpecialOrderLineID = reader.GetInt32(0),
                        SpecialOrderID = reader.GetInt32(1),
                        SpecialOrderItemID = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3)
                    };

                }
                else
                {
                    throw new ApplicationException("No Special Order Line with that ID found");
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

            return specialOrderLine;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Retrieves all Special Order Lines for a specific Special Order
        /// by ID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        public List<SpecialOrderLine> RetrieveSpecialOrderLineBySpecialOrderID(int specialOrderID)
        {
            var specialOrderLineList = new List<SpecialOrderLine>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_specialorderline_by_specialorderid_correct"; // This sp is commented out for some reason in the scripts

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", specialOrderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var specialOrderLine = new SpecialOrderLine()
                        {
                            SpecialOrderLineID = reader.GetInt32(0),
                            SpecialOrderID = reader.GetInt32(1),
                            SpecialOrderItemID = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3)
                        };
                        specialOrderLineList.Add(specialOrderLine);
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

            return specialOrderLineList;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SpecialOrderLineDetail> RetrieveSpecialOrderLineDetailListByOrderID(int id)
        {
            List<SpecialOrderLineDetail> specialOrderLineDetailList = new List<SpecialOrderLineDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_special_order_line_detail_list_by_special_order_id_with_received";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SpecialOrderID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var specialOrderLineDetail = new SpecialOrderLineDetail()
                        {
                            Line = new SpecialOrderLine()
                            {
                                SpecialOrderLineID = reader.GetInt32(0),
                                SpecialOrderItemID = reader.GetInt32(1),
                                Quantity = reader.GetInt32(2),
                                QtyReceived = reader.GetInt32(5),
                                SpecialOrderID = id
                            },
                            PriceEach = reader.GetDecimal(3),
                            ItemName = reader.GetString(4)

                        };
                        specialOrderLineDetailList.Add(specialOrderLineDetail);
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
            return specialOrderLineDetailList;
        }
    }
}
