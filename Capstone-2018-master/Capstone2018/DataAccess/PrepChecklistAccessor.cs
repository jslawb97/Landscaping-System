using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess

/// <summary>
/// Amanda Tampir
/// Created: 2018/2/01
/// 
/// Access Prep Checklist from database and implements Prep CheckList interface
/// </summary>
{
    public class PrepChecklistAccessor : IPrepChecklistAccessor
    {

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Creates new Prep CheckList
        /// </summary>
        /// <param name="prepChecklist">The new Prep Checklist item</param>
        /// <returns>The ID of the added Prep Checklist item</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 need add the checkbox and No test past
        public int CreatePrepChecklist(PrepChecklist prepChecklist)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_prepchecklist";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", prepChecklist.Name);
            cmd.Parameters.AddWithValue("@Description", prepChecklist.Description);

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
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Retreives a list of the Prep CheckLists
        /// </summary>
        /// <returns>Prep Checklist</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 
        public List<PrepChecklist> RetrievePrepChecklistList()
        {

            List<PrepChecklist> prepChecklist = new List<PrepChecklist>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_prepchecklist_list";
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
                        var prepList = new PrepChecklist()
                        {
                           PrepChecklistID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };

                        prepChecklist.Add(prepList);
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


            return prepChecklist;


        }


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Edits a specified Prep Checklist with new data 
        /// </summary>
        /// <param name="oldPrepChecklist">The old Prep Checklist being edited</param>
        /// <param name="newPrepChecklist">The new PrepChecklist containing new data</param>
        /// <returns>The number of records affected</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 
        public int EditPrepChecklist(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist)
        {

            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_prepchecklist";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrepChecklistID", oldPrepChecklist.PrepChecklistID);
            cmd.Parameters.AddWithValue("@OldName", oldPrepChecklist.Name);
            cmd.Parameters.AddWithValue("@NewName", newPrepChecklist.Name);
            cmd.Parameters.AddWithValue("@OldDescription", oldPrepChecklist.Description);
            cmd.Parameters.AddWithValue("@NewDescription", newPrepChecklist.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldPrepChecklist.Active);
            cmd.Parameters.AddWithValue("@NewActive", newPrepChecklist.Active);


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
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Deactivates specified Prep Checklist by PrepChecklistID
        /// </summary>
        /// <param name="PrepChecklistID">The Prep Checklist ID</param>
        /// <returns>The number of records affected</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 
        public int DeactivatePrepChecklistByID(int PrepChecklistID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_prepchecklist_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrepChecklistID", PrepChecklistID);

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






        //may not need code below at this time.

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// ------------------------this doesn't make sense
        /// 
        /// </summary>
        //public int EditPrepChecklistActive(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist)
        //{


        //int result = 0;

        //var conn = DBConnection.GetDBConnection();
        //var cmdText = @"sp_edit_prep_checklist";

        //var cmd = new SqlCommand(cmdText, conn);
        //cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@PrepChecklistID", oldPrepChecklist.PrepChecklistID);
        //    cmd.Parameters.AddWithValue("@OldName", oldPrepChecklist.Name);
        //    cmd.Parameters.AddWithValue("@NewName", newPrepChecklist.Name);
        //    cmd.Parameters.AddWithValue("@OldDescription", oldSpecialItem.Description);
        //    cmd.Parameters.AddWithValue("@NewDescription", newPrepChecklist.Description);


        //    try
        //    {
        //        conn.Open();
        //        result = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }


        //    return result;

        //} throw new NotImplementedException();
        //}

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Retrieves all active Prep CheckList
        /// </summary>
        /// <param name="Active">Boolean Active for Prep Checklist </param>
        /// <returns>All active Prep Checklists</returns>
        //public List<PrepChecklist> RetrievePrepChecklistByActive(bool Active)
        //{
        //    var prepList = new List<PrepChecklist>();

        //    var conn = DBConnection.GetDBConnection();
        //    var cmdText = @"sp_retrieve_prepchecklist_by_active";

        //    var cmd = new SqlCommand(cmdText, conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@Active", Active);

        //    try
        //    {
        //        conn.Open();
        //        var reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                var pl = new PrepChecklist()
        //                {
        //                    /* [PrepChecklistID],[Description], [Active] */

        //                    PrepChecklistID = reader.GetInt32(0),
        //                    Name = reader.GetString(1)
        //                    Description = reader.GetString(2),
        //                    Active = reader.GetBoolean(3)
        //                };
        //                prepList.Add(pl);
        //            }
        //        }
        //        else
        //        {
        //            throw new ApplicationException("Data not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Database access error.", ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return prepList;

        //}


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Retrieves Prep CheckList by specified ID number
        /// </summary>
        /// <param name="prepChecklistID">The Prep Checklist ID</param>
        /// <returns>Prep Checklist item with the specified ID</returns>
        /// <remarks>QA Shilin Xiong Update 4/20/2018  
        public PrepChecklist RetrievePrepChecklistByID(int prepChecklistID)
        {

            PrepChecklist prepList = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_prepchecklist_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrepChecklistID", prepChecklistID);

            // try-catch
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    prepList = new PrepChecklist()
                    {
                        PrepChecklistID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Active = reader.GetBoolean(3)
                    };
                }
                else
                {
                    throw new ApplicationException("Record not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data access error.", ex);
            }
            finally
            {
                conn.Close();
            }

            return prepList;

            //}

        }
    }
}
