using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class PrepRecordAccessor : IPrepRecordAccessor
    {
        /// <summary>
        /// Creates a new PrepRecordlist item.
        /// </summary>
        /// <param name="newItem">The new PrepRecordlist item</param>
        /// <returns>The ID of the newly added PrepRecordlist item</returns>
        /// <remarks>
        /// Badis Saidani
        /// Updated 2018/02/24
        /// </remarks>
        public int CreatePrepRecord(PrepRecord newItem)
        {
            int newID;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_preprecord";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EquipmentID", newItem.EquipmentID);
            cmd.Parameters.AddWithValue("@EmployeeID", newItem.EmployeeID);
            cmd.Parameters.AddWithValue("@Description", newItem.Description);
            cmd.Parameters.AddWithValue("@Date", newItem.Date);

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                conn.Close();
            }

            return newID;
        }

        /// <summary>
        /// Deletes an PrepRecordlist by it's ID
        /// </summary>
        /// <param name="id">The ID of the Inspection Checklist to delete.</param>
        /// <returns>The row count from the database</returns>
        /// <remarks>
        /// Badis Saidani
        /// Updated 2018/02/24
        /// </remarks>
        public int DeletePrepRecordByID(int id)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_preprecord_by_id";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@PrepRecordID", SqlDbType.Int);
            cmd.Parameters["@PrepRecordID"].Value = id;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the Prep Record", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Edits a specified PrepRecordlist item with data from a new PrepRecordlist item
        /// </summary>
        /// <param name="oldPrepRecordlist">The item being edited</param>
        /// <param name="newPrepRecordlist">The item containing the new data</param>
        /// <returns>1 if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Badis Saidani
        /// Updated 2018/02/24
        /// </remarks>
        public int EditPrepRecordItem(PrepRecord oldItem, PrepRecord newItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_preprecord_by_id";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PrepRecordID", oldItem.PrepRecordID);
            cmd.Parameters.AddWithValue("@OldEquipmentID", oldItem.EquipmentID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldItem.EmployeeID);
            cmd.Parameters.AddWithValue("@OldDescription", oldItem.Description);
            cmd.Parameters.AddWithValue("@OldDate", oldItem.Date);
            cmd.Parameters.AddWithValue("@NewEquipmentID", newItem.EquipmentID);
            cmd.Parameters.AddWithValue("@NewEmployeeID", newItem.EmployeeID);
            cmd.Parameters.AddWithValue("@NewDescription", newItem.Description);
            cmd.Parameters.AddWithValue("@NewDate", newItem.Date);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Retrieves an PrepRecordlist by it's ID
        /// </summary>
        /// <param name="id">The ID of the Inspection Checklist to retrieve.</param>
        /// <returns>An PrepRecordlist item from the database</returns>
        /// <remarks>
        /// Badis Saidani
        /// Updated 2018/02/24
        /// </remarks>
        public PrepRecord RetrievePrepRecordByID(int id)
        {
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_preprecord_by_id";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new PrepRecord
                        {
                            PrepRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                    }
                }

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Retrieves a list of PrepRecordlists
        /// </summary>
        /// <returns>A list of PrepRecordlist items from the database</returns>
        /// <remarks>
        /// Badis Saidani
        /// Updated 2018/02/24
        /// </remarks>
        public List<PrepRecord> RetrievePrepRecordList()
        {
            List<PrepRecord> items = new List<PrepRecord>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_preprecord_list";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new PrepRecord
                        {
                            PrepRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };

                        items.Add(item);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return items;
        }

        /// <summary>
        /// Retrieves a list of PrepRecordDeatillist
        /// </summary>
        /// <returns>A list of PrepRecordDetaillist items from the database</returns>
        /// <remarks>
        /// Badis Saidani
        /// Created 2018/05/03
        /// </remarks>
        public List<PrepRecordDetail> RetrievePrepRecordDetailList()
        {
            List<PrepRecordDetail> items = new List<PrepRecordDetail>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_Prep_record_detail_list";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new PrepRecordDetail
                        {
                            PrepRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4),
                            EquipmentName = reader.GetString(5),
                            EmployeeName = reader.GetString(6) + " " + reader.GetString(7)
                        };

                        items.Add(item);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return items;
        }
    }
}
