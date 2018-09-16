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
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/02/01
    /// 
    /// Accessor class for SupplyItems and an SqlServer database
    /// </summary>
    public class SupplyItemAccessor : ISupplyItemAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Adds the SupplyItem to the SqlServer database
        /// </summary>
        /// <param name="supplyItem">The SupplyItem to add</param>
        /// <returns>The ID of the newly created record</returns>
        public int CreateSupplyItem(SupplyItem supplyItem)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_supplyitem";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", supplyItem.Name);
            cmd.Parameters.AddWithValue("@Description", supplyItem.Description);
            cmd.Parameters.AddWithValue("@Location", supplyItem.Location);
            cmd.Parameters.AddWithValue("@QuantityInStock", supplyItem.QuantityInStock);
            cmd.Parameters.AddWithValue("@ReorderLevel", supplyItem.ReorderLevel);
            cmd.Parameters.AddWithValue("@ReorderQuantity", supplyItem.ReorderQuantity);
            cmd.Parameters.AddWithValue("@Active", supplyItem.Active);


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
        /// Created: 2018/02/01
        /// 
        /// Deactivates the record with the given ID in the SqlServer database
        /// </summary>
        /// <param name="supplyItemID">The ID of the SupplyItem to be deactivated</param>
        /// <returns>The number of rows affected</returns>
        public int DeactivateSupplyItemByID(int supplyItemID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_supplyitem_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplyItemID", supplyItemID);
            
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
        /// Created: 2018/02/01
        /// 
        /// Updates a SupplyItem record with new data
        /// </summary>
        /// <param name="oldSupplyItem">The SupplyItem being updated</param>
        /// <param name="newSupplyItem">The SupplyItem with the new data</param>
        /// <returns>The number of rows affected</returns>
        public int EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_supplyitem_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplyItemID", oldSupplyItem.SupplyItemID);
            cmd.Parameters.AddWithValue("@NewDescription", newSupplyItem.Description);
            cmd.Parameters.AddWithValue("@OldDescription", oldSupplyItem.Description);
            cmd.Parameters.AddWithValue("@NewLocation", newSupplyItem.Location);
            cmd.Parameters.AddWithValue("@OldLocation", oldSupplyItem.Location);
            cmd.Parameters.AddWithValue("@NewName", newSupplyItem.Name);
            cmd.Parameters.AddWithValue("@OldName", oldSupplyItem.Name);
            cmd.Parameters.AddWithValue("@NewQuantityInStock", newSupplyItem.QuantityInStock);
            cmd.Parameters.AddWithValue("@OldQuantityInStock", oldSupplyItem.QuantityInStock);
            cmd.Parameters.AddWithValue("@NewReorderLevel", newSupplyItem.ReorderLevel);
            cmd.Parameters.AddWithValue("@OldReorderLevel", oldSupplyItem.ReorderLevel);
            cmd.Parameters.AddWithValue("@NewReorderQuantity", newSupplyItem.ReorderQuantity);
            cmd.Parameters.AddWithValue("@OldReorderQuantity", oldSupplyItem.ReorderQuantity);
            cmd.Parameters.AddWithValue("@OldActive", oldSupplyItem.Active);
            cmd.Parameters.AddWithValue("@NewActive", newSupplyItem.Active);


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
        /// Created: 2018/02/01
        /// 
        /// Gets a list of SupplyItems from the SqlServer database
        /// </summary>
        /// <returns>List of all SupplyItems in the database</returns>
        public List<SupplyItem> RetrieveSupplyItemList()
        {
            List<SupplyItem> items = new List<SupplyItem>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_supplyitem_list";
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
                        var item = new SupplyItem()
                        {
                            SupplyItemID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Location = reader.GetString(3),
                            QuantityInStock = reader.GetInt32(4),
                            ReorderLevel = reader.GetInt32(5),
                            ReorderQuantity = reader.GetInt32(6),
                            Active = reader.GetBoolean(7)
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
        /// Weston Olund
        /// 2018/04/05
        /// Retrieves a list of supply item details from database where item is on order and quanity in stock
		/// is and items on order is less than reorder level
        /// </summary>
        /// <returns></returns>
        public List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailList()
        {
            List<SupplyItemDetail> supplyItemDetailList = new List<SupplyItemDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_supply_item_detail_where_quanity_in_stock_and_on_order_less_than_reorder_level";
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
                        var supplyItem = new SupplyItem();
                        var supplyItemDetail = new SupplyItemDetail();

                        supplyItem.SupplyItemID = reader.GetInt32(0);
                        supplyItem.Name = reader.GetString(1);
                        supplyItem.Description = reader.GetString(2);
                        supplyItem.Location = reader.GetString(3);
                        supplyItem.QuantityInStock = reader.GetInt32(4);
                        supplyItem.ReorderLevel = reader.GetInt32(5);
                        supplyItem.ReorderQuantity = reader.GetInt32(6);
                        supplyItem.Active = reader.GetBoolean(7);
                        supplyItemDetail.PriceEach = reader.GetDecimal(8);
                        supplyItemDetail.VendorName = reader.GetString(9);
                        supplyItemDetail.SourceID = reader.GetInt32(10);
                        supplyItemDetail.VendorID = reader.GetInt32(11);

                        supplyItemDetail.SupplyItem = supplyItem;

                        supplyItemDetailList.Add(supplyItemDetail);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem retrieving your data");
            }
            finally
            {
                conn.Close();
            }
            return supplyItemDetailList;
        }


		/// <summary>
		/// Weston Olund
		/// 2018/04/05
		/// Retrieves a list of supply item details from database where item is on order and quanity in stock
		/// is and items on order is less than reorder level
		/// </summary>
		/// <returns></returns>
		public List<SupplyItemDetail> RetrieveItemsNeedingReorderNotOnOrderSupplyItemDetailList()
		{
			List<SupplyItemDetail> supplyItemDetailList = new List<SupplyItemDetail>();

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_retrieve_supply_item_detail_where_quanity_in_stock_and_item_not_on_order_less_than_reorder_level";
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
						var supplyItem = new SupplyItem();
						var supplyItemDetail = new SupplyItemDetail();

						supplyItem.SupplyItemID = reader.GetInt32(0);
						supplyItem.Name = reader.GetString(1);
						supplyItem.Description = reader.GetString(2);
						supplyItem.Location = reader.GetString(3);
						supplyItem.QuantityInStock = reader.GetInt32(4);
						supplyItem.ReorderLevel = reader.GetInt32(5);
						supplyItem.ReorderQuantity = reader.GetInt32(6);
						supplyItem.Active = reader.GetBoolean(7);
						supplyItemDetail.PriceEach = reader.GetDecimal(8);
						supplyItemDetail.VendorName = reader.GetString(9);
						supplyItemDetail.SourceID = reader.GetInt32(10);
						supplyItemDetail.VendorID = reader.GetInt32(11);

						supplyItemDetail.SupplyItem = supplyItem;

						supplyItemDetailList.Add(supplyItemDetail);
					}
				}
			}
			catch (Exception)
			{
				throw new ApplicationException("There was a problem retrieving your data");
			}
			finally
			{
				conn.Close();
			}
			return supplyItemDetailList;
		}


		/// <summary>
		/// Weston Olund
		/// 2018/04/27
		/// Method to get supply items where quanity is below reorder level from database
		/// </summary>
		/// <returns></returns>
		public List<SupplyItemDetail> RetrieveSupplyItemDetailList()
		{
			List<SupplyItemDetail> supplyItemDetailList = new List<SupplyItemDetail>();

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_retrieve_supply_item_detail";
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
						var supplyItem = new SupplyItem();
						var supplyItemDetail = new SupplyItemDetail();

						supplyItem.SupplyItemID = reader.GetInt32(0);
						supplyItem.Name = reader.GetString(1);
						supplyItem.Description = reader.GetString(2);
						supplyItem.Location = reader.GetString(3);
						supplyItem.QuantityInStock = reader.GetInt32(4);
						supplyItem.ReorderLevel = reader.GetInt32(5);
						supplyItem.ReorderQuantity = reader.GetInt32(6);
						supplyItem.Active = reader.GetBoolean(7);
						supplyItemDetail.PriceEach = reader.GetDecimal(8);
						supplyItemDetail.VendorName = reader.GetString(9);
						supplyItemDetail.SourceID = reader.GetInt32(10);
						supplyItemDetail.VendorID = reader.GetInt32(11);

						supplyItemDetail.SupplyItem = supplyItem;

						supplyItemDetailList.Add(supplyItemDetail);
					}
				}
				else
				{
					throw new ApplicationException("No data found");
				}
			}
			catch (Exception)
			{
				throw new ApplicationException("There was a problem retrieving your data");
			}
			finally
			{
				conn.Close();
			}
			return supplyItemDetailList;
		}


	}
}
