using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace DataAccess
{
    public class SupplyOrderItemAccessor : ISupplyOrderItemAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Creates a new supply order item connected to a single supply order
        /// </summary>
        /// <param name="orderitem"></param>
        /// <returns></returns>
        public int CreateSupplyOrderItem(SupplyOrderItem orderitem)
        {

            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_supplyorderline";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyItemID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);
            cmd.Parameters.Add("@SupplyOrderID", SqlDbType.Int);

            cmd.Parameters["@SupplyItemID"].Value = orderitem.SupplyItemID;
            cmd.Parameters["@Quantity"].Value = orderitem.Quantity;
            cmd.Parameters["@SupplyOrderID"].Value = orderitem.SupplyOrderID;

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
        /// Jacob Conley
        /// Created on 2018/03/15
        /// 
        /// Deletes an existing supply order item 
        /// </summary>
        /// <param name="supplyOrderLineID"></param>
        /// <returns></returns>
        public int DeleteSupplyOrderItemByID(int supplyOrderLineID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_delete_supplyorderline";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyOrderLineID", SqlDbType.Int);

            cmd.Parameters["@SupplyOrderLineID"].Value = supplyOrderLineID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Supply Order Item removal failed.");
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
        /// Edits an existing supply order item 
        /// </summary>
        /// <param name="oldOrderItem"></param>
        /// <param name="newOrderItem"></param>
        /// <returns></returns>
        public int EditSupplyOrderItem(SupplyOrderItem oldOrderItem, SupplyOrderItem newOrderItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_supplyorderline_by_supplyorderlineid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplyOrderLineID", SqlDbType.Int);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            cmd.Parameters["@SupplyOrderLineID"].Value = oldOrderItem.SupplyOrderLineID;
            cmd.Parameters["@OldQuantity"].Value = oldOrderItem.Quantity;
            cmd.Parameters["@NewQuantity"].Value = newOrderItem.Quantity;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Supply Order Item update failed.");
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
        /// Retrieves all supply order items connected to a single supply order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SupplyOrderItem> RetrieveSupplyOrderItemByID(int id)
        {
            List<SupplyOrderItem> supplyOrders = new List<SupplyOrderItem>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_supplyorderline_by_supplyorderid";

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
                    while (reader.Read())
                    {
                        var supplyOrder = new SupplyOrderItem()
                        {
                            SupplyOrderLineID = reader.GetInt32(0),
                            SupplyItemID = reader.GetInt32(1),
                            Quantity = reader.GetInt32(2),
                            QuantityReceived = reader.GetInt32(3),
                            SupplyOrderID = id
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

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Method to update the QuantityReceived field for
        /// all SupplyOrderLines associated with a SupplyOrder
        /// using a stored procedure
        /// </summary>
        /// <param name="supplyOrder"></param>
        /// <param name="newSupplyOrderItems"></param>
        /// <param name="oldSupplyOrderItems"></param>
        /// <returns></returns>
        public int EditSupplyOrderLineQuantityReceived(SupplyOrder supplyOrder, List<SupplyOrderItem> newSupplyOrderItems, List<SupplyOrderItem> oldSupplyOrderItems)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_supplyorderline_quantityreceived";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            var newItemsRecordList = new List<SqlDataRecord>();
            var oldItemsRecordList = new List<SqlDataRecord>();
            foreach (var item in newSupplyOrderItems)
            {
                var record = new SqlDataRecord(
                    new SqlMetaData[] {
                        new SqlMetaData("SupplyOrderLineID", SqlDbType.Int),
                        new SqlMetaData("QuantityReceived", SqlDbType.Int)
                    }
                );
                record.SetInt32(0, item.SupplyOrderLineID);
                record.SetInt32(1, item.QuantityReceived);
                newItemsRecordList.Add(record);
            }
            foreach (var item in oldSupplyOrderItems)
            {
                var record = new SqlDataRecord(
                    new SqlMetaData[] {
                        new SqlMetaData("SupplyOrderLineID", SqlDbType.Int),
                        new SqlMetaData("QuantityReceived", SqlDbType.Int)
                    }
                );
                record.SetInt32(0, item.SupplyOrderLineID);
                record.SetInt32(1, item.QuantityReceived);
                oldItemsRecordList.Add(record);
            }

            cmd.Parameters.AddWithValue("@SupplyOrderID", supplyOrder.SupplyOrderID);
            SqlParameter tvpNewItems = cmd.Parameters.AddWithValue("@tvpNewQuantitiesReceived", newItemsRecordList);
            SqlParameter tvpOldItems = cmd.Parameters.AddWithValue("@tvpOldQuantitiesReceived", oldItemsRecordList);
            tvpNewItems.SqlDbType = SqlDbType.Structured;
            tvpOldItems.SqlDbType = SqlDbType.Structured;
            tvpNewItems.TypeName = "dbo.SupplyOrderLineReceivedTableType";
            tvpOldItems.TypeName = "dbo.SupplyOrderLineReceivedTableType";

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();

                if (rowcount == 0)
                {
                    throw new ApplicationException("No rows affected!");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing your data", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }
    }
}
