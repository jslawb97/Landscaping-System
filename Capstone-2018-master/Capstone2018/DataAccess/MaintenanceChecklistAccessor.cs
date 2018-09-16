using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccess
{
    public class MaintenanceChecklistAccessor : IMaintenanceChecklistAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to retrieve a list of maintenance checklists using a stored
        /// procedure
        /// </summary>
        /// <returns>A list of MintenanceChecklists</returns>
        /// <remarks>QA Jayden T 4/27/18</remarks>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public List<MaintenanceChecklist> RetrieveMaintenanceChecklistList()
        {
            var maintenanceChecklistList = new List<MaintenanceChecklist>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_maintenancechecklist_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var maintenanceChecklist = new MaintenanceChecklist()
                        {
                            MaintenanceChecklistID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };
                        maintenanceChecklistList.Add(maintenanceChecklist);
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
            return maintenanceChecklistList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to retrieve a maintenance checklist by ID using a stored
        /// procedure
        /// </summary>
        /// <param name="maintenanceChecklistID"></param>
        /// <returns>A MaintenanceChecklist</returns>
        /// <remarks>QA Jayden T 4/27/18</remarks>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public MaintenanceChecklist RetrieveMaintenanceChecklistByID(int maintenanceChecklistID)
        {
            MaintenanceChecklist maintenanceChecklist = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_maintenancechecklist_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MaintenanceChecklistID", SqlDbType.Int);
            cmd.Parameters["@MaintenanceChecklistID"].Value = maintenanceChecklistID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
				
                if (reader.HasRows)
                {
                    reader.Read();
                    maintenanceChecklist = new MaintenanceChecklist()
                    {
                        MaintenanceChecklistID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Active = reader.GetBoolean(3)
                    };
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
            return maintenanceChecklist;
		}

        /// <summary>
        /// Creates a new MaintenanceChecklist item.
        /// </summary>
        /// <param name="newItem">The new MaintenanceChecklist item</param>
        /// <returns>The ID of the newly added MaintenanceChecklist item</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// </remarks>
        /// <remarks>QA added a catch block Jayden T 4/27/18</remarks>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public int CreateMaintenanceChecklist(MaintenanceChecklist newItem)
        {
            int newID;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_maintenancechecklist";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", newItem.Name);
            cmd.Parameters.AddWithValue("@Description", newItem.Description);

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }
            
            return newID;
        }

        /// <summary>
        /// Edits a specified MaintenanceChecklist item with data from a new MaintenanceChecklist item
        /// </summary>
        /// <param name="oldItem">The item being edited</param>
        /// <param name="newItem">The item containing the new data</param>
        /// <returns>1 if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// </remarks>
        /// <remarks>QA added a catch block Jayden T 4/27/18</remarks>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public int EditMaintenanceChecklistItem(MaintenanceChecklist oldItem, MaintenanceChecklist newItem)
        {
            int result;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_maintenancechecklist";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@MaintenanceChecklistID", oldItem.MaintenanceChecklistID);
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
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
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
        /// <remarks>QA Jayden T 4/27/18</remarks>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public int DeactivateMaintenanceChecklistByID(int id)
        {
            int rowcount;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_maintenancechecklist_by_id";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@MaintenanceChecklistID", SqlDbType.Int);
            cmd.Parameters["@MaintenanceChecklistID"].Value = id;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the inspection checklist", ex);
            }
            return rowcount;
        }
	}
}
