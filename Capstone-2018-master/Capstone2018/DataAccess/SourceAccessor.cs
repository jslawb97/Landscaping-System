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
    /// <summary>
    /// Brady Feller
    /// Created 2018/02/19
    /// 
    /// Accessor class for Source
    /// </summary>
    public class SourceAccessor : ISourceAccessor
    {

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Creates a new source item
        /// </summary>
        /// <param name="source">The new source item</param>
        /// <returns>The ID of the new source item</returns>
        public int CreateSource(Source source)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_source";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplyItemID", source.SupplyItemID);
            cmd.Parameters.AddWithValue("@SpecialOrderItemID", source.SpecialOrderItemID);
            cmd.Parameters.AddWithValue("@VendorID", source.VendorID);
            cmd.Parameters.AddWithValue("@MinimumOrderQTY", source.MinimumOrderQTY);
            cmd.Parameters.AddWithValue("@PriceEach", source.PriceEach);
            cmd.Parameters.AddWithValue("@LeadTime", source.LeadTime);
            cmd.Parameters.AddWithValue("@Active", source.Active);

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                newId = (int)id;
            }
            catch (Exception)
            {
                throw;
            }

            return newId;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/19
        /// 
        /// Deactivates the a record from the Source table
        /// </summary>
        /// <param name="sourceID"></param>
        /// <returns></returns>
        public int DeactivateSourceByID(int sourceID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_source_by_sourceid";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SourceID", SqlDbType.Int);
            cmd.Parameters["@SourceID"].Value = sourceID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the employee", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Edits an existing source for changes in the source
        /// </summary>
        /// <param name="source">The item containing the new data</param>
        /// <param name="oldSource">The item being edited</param>
        /// <returns>The number of records affected</returns>
        public int EditSource(Source oldSource, Source source)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_source";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SourceID", source.SourceID);
            cmd.Parameters.AddWithValue("@NewSupplyItemID", source.SupplyItemID);
            cmd.Parameters.AddWithValue("@NewSpecialOrderItemID", source.SpecialOrderItemID);
            cmd.Parameters.AddWithValue("@NewVendorID", source.VendorID);
            cmd.Parameters.AddWithValue("@NewMinimumOrderQTY", source.MinimumOrderQTY);
            cmd.Parameters.AddWithValue("@NewPriceEach", source.PriceEach);
            cmd.Parameters.AddWithValue("@NewLeadTime", source.LeadTime);
            cmd.Parameters.AddWithValue("@NewActive", source.Active);

            cmd.Parameters.AddWithValue("@OldSupplyItemID", oldSource.SupplyItemID);
            cmd.Parameters.AddWithValue("@OldSpecialOrderItemID", oldSource.SpecialOrderItemID);
            cmd.Parameters.AddWithValue("@OldVendorID", oldSource.VendorID);
            cmd.Parameters.AddWithValue("@OldMinimumOrderQTY", oldSource.MinimumOrderQTY);
            cmd.Parameters.AddWithValue("@OldPriceEach", oldSource.PriceEach);
            cmd.Parameters.AddWithValue("@OldLeadTime", oldSource.LeadTime);
            cmd.Parameters.AddWithValue("@OldActive", oldSource.Active);

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
        /// Jayden Tollefson
        /// Created: 2018/02/02
        /// 
        /// Retrieves a list of Sources from the crlandscaping database
        /// </summary>
        /// <param name="active"></param>
        /// <returns>A list of Sources</returns>
        public List<Source> RetrieveSourceByActive(bool active = true)
        {
            var sourceList = new List<Source>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_source_by_active";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        var so = new Source()
                        {
                            SourceID = reader.GetInt32(0),
                            SupplyItemID = reader.GetInt32(1),
                            SpecialOrderItemID = reader.GetInt32(2),
                            VendorID = reader.GetInt32(3),
                            MinimumOrderQTY = reader.GetInt32(4),
                            PriceEach = reader.GetDecimal(5),
                            LeadTime = reader.GetInt32(6),
                            Active = reader.GetBoolean(7)
                        };
                        sourceList.Add(so);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }

            return sourceList;
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created 2018/03/02
        /// 
        /// Retrieve Vendor name and supply name for the source
        /// </summary>
        /// <returns></returns>
        public List<SourceDetail> RetrieveSourceDetailList()
        {
            var sourceList = new List<SourceDetail>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_source_detail_list";
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

                        var so = new SourceDetail()
                        {
                            SourceID = reader.GetInt32(0),
                            SupplyItemID = reader.GetInt32(1),
                            SpecialOrderItemID = reader.GetInt32(2),
                            VendorID = reader.GetInt32(3),
                            MinimumOrderQTY = reader.GetInt32(4),
                            PriceEach = reader.GetDecimal(5),
                            LeadTime = reader.GetInt32(6),
                            Active = reader.GetBoolean(7),
                            SpecialItemName = reader.GetString(8),
                            VendorName = reader.GetString(9),
                            SupplyItemName = reader.GetString(10)
                        };
                        sourceList.Add(so);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }

            return sourceList;
        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/04/19
        /// 
        /// Gets a Customer record from the Sql Server database that matches the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Source RetrieveSourceByID(int id)
        {
            Source source = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_source_list_by_sourceid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SourceID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    source = new Source()
                    {
                        SourceID = reader.GetInt32(0),
                        SupplyItemID = reader.GetInt32(1),
                        SpecialOrderItemID = reader.GetInt32(2),
                        VendorID = reader.GetInt32(3),
                        MinimumOrderQTY = reader.GetInt32(4),
                        PriceEach = reader.GetDecimal(5),
                        LeadTime = reader.GetInt32(6),
                        Active = reader.GetBoolean(7)
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


            return source;
        }
    }
}
