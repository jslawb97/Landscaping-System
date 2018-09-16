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
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/01/31
    /// 
    /// Facilatates data movement between the application and the SqlServer database for Special Order Items
    /// </summary>
    public class SpecialOrderItemAccessor : ISpecialOrderItemAccessor
    {

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Retrieves a List of SpecialItem objects from the SqlServer crlandscaping database
        /// </summary>
        public List<SpecialItem> RetrieveSpecialOrderItemList()
        {
            List<SpecialItem> items = new List<SpecialItem>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_specialorderitem_list";
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
                        var item = new SpecialItem()
                        {
                            SpecialOrderItemID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Active = reader.GetBoolean(2)
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
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Sends data to the database for a special order item to be edited. 
        /// </summary>
        /// <param name="oldSpecialItem">The item being edited</param>
        /// <param name="newSpecialItem">The item with the new data</param>
        /// <returns>The number of rows affected</returns>
        public int EditSpecialOrderItem(SpecialItem oldSpecialItem, SpecialItem newSpecialItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_specialorderitem";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderItemID", oldSpecialItem.SpecialOrderItemID);
            cmd.Parameters.AddWithValue("@OldName", oldSpecialItem.Name);
            cmd.Parameters.AddWithValue("@NewName", newSpecialItem.Name);
            cmd.Parameters.AddWithValue("@OldActive", oldSpecialItem.Active);
            cmd.Parameters.AddWithValue("@NewActive", newSpecialItem.Active);


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
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Sends data to create a new Special Order Item to the database
        /// </summary>
        /// <param name="newItem">The new item being added</param>
        /// <returns>The id of the newly added item</returns>
        public int CreateSpecialOrderItem(SpecialItem newItem)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_specialorderitem";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", newItem.Name);
            cmd.Parameters.AddWithValue("@Active", newItem.Active);


            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return newID;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Deactivate a SpecialItem record in the SQL Server database by its ID field
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The number of records affected by the deactivate.</returns>
        public int DeactivateSpecialOrderByID(int id)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_specialorderitem_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderItemID", id);

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
        /// Created 4/20/2018
        /// 
        /// Retrieves a list of SpecialOrderITemDetails
        /// </summary>
        /// <returns></returns>
        public List<SpecialOrderItemDetail> RetrieveSpecialOrderItemDetails()
        {
            List<SpecialOrderItemDetail> detailList = new List<SpecialOrderItemDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_specialorderitem_detail";
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
                        var item = new SpecialItem()
                        {
                            SpecialOrderItemID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Active = reader.GetBoolean(2)
                        };

                        var detail = new SpecialOrderItemDetail()
                        {
                            PriceEach = reader.GetDecimal(3),
                            VendorName = reader.GetString(4),
                            SourceID = reader.GetInt32(5),
                            VendorID = reader.GetInt32(6)
                        };

                        detail.SpecialItem = item;

                        detailList.Add(detail);
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

            return detailList;
        }

        
    }
}
