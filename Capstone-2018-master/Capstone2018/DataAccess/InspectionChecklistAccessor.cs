using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class InspectionChecklistAccessor : IInspectionChecklistAccessor
    {

        /// <summary>
        /// Creates a new InspectionChecklist item.
        /// </summary>
        /// <param name="newItem">The new InspectionChecklist item</param>
        /// <returns>The ID of the newly added InspectionChecklist item</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        /// <remarks>QA Jayden T 4/6/18 Added a catch block after the try block</remarks>
        public int CreateInspectionChecklist(InspectionChecklist newItem)
        {
            int newID;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_inspectionchecklist";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", newItem.Description);
            cmd.Parameters.AddWithValue("@Description", newItem.Description);

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch(Exception ex)
            {
                throw new ApplicationException("There was a problem creating the inspection checklist", ex);
            }
            finally
            {
                conn.Close();
            }
            
            return newID;
        }

        /// <summary>
        /// Edits a specified InspectionChecklist item with data from a new InspectionChecklist item
        /// </summary>
        /// <param name="oldItem">The item being edited</param>
        /// <param name="newItem">The item containing the new data</param>
        /// <returns>1 if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        /// <remarks>QA Jayden T 4/6/18 Added a catch block after the try block</remarks>
        public int EditInspectionChecklistItem(InspectionChecklist oldItem, InspectionChecklist newItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_inspectionchecklist";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InspectionChecklistID", oldItem.InspectionChecklistID);
            cmd.Parameters.AddWithValue("@OldName", oldItem.Name);
            cmd.Parameters.AddWithValue("@NewName", newItem.Name);
            cmd.Parameters.AddWithValue("@OldDescription", oldItem.Description);
            cmd.Parameters.AddWithValue("@NewDescription", newItem.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldItem.Active);
            cmd.Parameters.AddWithValue("@NewActive", newItem.Active);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing the inspection checklist", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Retrieves a list of InspectionChecklists
        /// </summary>
        /// <returns>A list of InspectionChecklist items from the database</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        /// <remarks>QA Jayden T 4/6/18 Added a catch block after the try block</remarks>
        public List<InspectionChecklist> RetrieveInspectionChecklistList()
        {
            List<InspectionChecklist> items = new List<InspectionChecklist>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_inspectionchecklist_list";
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
                        var item = new InspectionChecklist
                        {
                            InspectionChecklistID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };

                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving the inspection checklist", ex);
            }
            finally
            {
                conn.Close();
            }

            return items;
        }

        /// <summary>
        /// Retrieves an InspectionChecklist by it's ID
        /// </summary>
        /// <param name="id">The ID of the Inspection Checklist to retrieve.</param>
        /// <returns>An InspectionChecklist item from the database</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// </remarks>
        /// <remarks>QA Jayden T 4/6/18 Added a catch block after the try block</remarks>
        public InspectionChecklist RetrieveInspectionChecklistByID(int id)
        {
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_inspectionchecklist_by_id";
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
                        return new InspectionChecklist
                        {
                            InspectionChecklistID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving the inspection checklist", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Deactivates an InspectionChecklist by it's ID
        /// </summary>
        /// <param name="id">The ID of the Inspection Checklist to deactivate.</param>
        /// <returns>The row count from the database</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/16
        /// </remarks>
        public int DeactivateInspectionChecklistByID(int id)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_inspectionchecklist_by_id";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@InspectionChecklistID", SqlDbType.Int);
            cmd.Parameters["@InspectionChecklistID"].Value = id;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the inspection checklist", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Deletes an InspectionChecklist by it's ID
        /// </summary>
        /// <param name="id">The ID of the Inspection Checklist to delete.</param>
        /// <returns>The row count from the database</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/16
        /// </remarks>
        public int DeleteInspectionChecklistByID(int id)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_inspectionchecklist_by_id";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@InspectionChecklistID", SqlDbType.Int);
            cmd.Parameters["@InspectionChecklistID"].Value = id;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the inspection checklist", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

    }
}
