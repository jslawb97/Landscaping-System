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
    /// Brady Feller
    /// Created 2018/04/05
    /// 
    /// Accessor class for TaskEmployee records with a SqlServer database
    /// </summary>
    public class TaskEquipmentAccessor : ITaskEquipmentAccessor
    {
        /// <summary>
        /// Brady Feller
        /// Created on 2018/04/05
        /// 
        /// Gets a list of TaskEquipment detail records from the database
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskEquipmentDetail> RetrieveTaskEquipmentDetailByJobID(int jobID)
        {
            var detail = new List<TaskEquipmentDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_get_task_equipment_by_job_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobID", jobID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var taskEquipment = new TaskEquipmentDetail()
                        {
                            TaskName = reader.GetString(0),
                            EquipmentID = reader.IsDBNull(1) ? null : (int?)(reader.GetInt32(1)),
                            EquipmentName = reader.IsDBNull(2) ? "Not Assigned" : reader.GetString(2),
                            EquipmentType = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            TaskEquipmentID = reader.GetInt32(4)
                        };
                        detail.Add(taskEquipment);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data.", ex);

            }
            finally
            {
                conn.Close();
            }

            return detail;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/05
        /// 
        /// Gets a list of equipment assigned to a particular task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveAssignedEquipmentByTaskID(int taskID)
        {
            var equipmentList = new List<Equipment>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_assigned_equipment_by_task_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskID", taskID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var eq = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            EquipmentTypeID = reader.GetString(1),
                            Name = reader.GetString(2),
                            MakeModelID = reader.GetInt32(3),
                            DatePurchased = reader.GetDateTime(4),
                            DateLastRepaired = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                            PriceAtPurchase = reader.GetDecimal(6),
                            CurrentValue = reader.GetDecimal(7),
                            WarrantyUntil = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            EquipmentStatusID = reader.GetString(9),
                            EquipmentDetails = reader.GetString(10),
                        };
                        equipmentList.Add(eq);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found.");
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
            return equipmentList;
        }

        /// <summary>
        /// Sam Dramstad
        /// Created 2018/04/11
        /// 
        /// Adds a specified Equipment to a TaskEquipment list.
        /// </summary>
        /// 

        public bool AddEquipmentToTaskEquipment(int equipmentID, int jobID, int taskTypeEquipmentNeedID)
        {
            bool result;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_taskequipment_assignment";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EquipmentID", equipmentID);
            cmd.Parameters.AddWithValue("@JobID", jobID);
            cmd.Parameters.AddWithValue("@TaskTypeEquipmentNeedID", taskTypeEquipmentNeedID);
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    throw new ApplicationException("There was an error adding this equipment to the task.");
                }
                else
                {
                    result = true;
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
            return result;


        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/19
        /// 
        /// Gets a list of equipment assigned to a particular task and job
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID)
        {
            var equipmentList = new List<Equipment>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_assigned_equipment_by_task_id_and_job_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskID", taskID);
            cmd.Parameters.AddWithValue("@JobID", jobID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var eq = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            EquipmentTypeID = reader.GetString(1),
                            Name = reader.GetString(2),
                            MakeModelID = reader.GetInt32(3),
                            DatePurchased = reader.GetDateTime(4),
                            DateLastRepaired = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                            PriceAtPurchase = reader.GetDecimal(6),
                            CurrentValue = reader.GetDecimal(7),
                            WarrantyUntil = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            EquipmentStatusID = reader.GetString(9),
                            EquipmentDetails = reader.GetString(10),
                        };
                        equipmentList.Add(eq);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found.");
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
            return equipmentList;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/13
        /// Deletes all equipment assigned to a particular task based on taskID
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int DeleteAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID)
        {
            int rowsAffected = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_all_assigned_equipment_by_task_id_and_job_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskID", taskID);
            cmd.Parameters.AddWithValue("@JobID", jobID);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/13
        /// 
        /// Removes a specific Equipment from a TaskEquipment list.
        /// </summary>
        /// <param name="taskEquipmentID"></param>
        /// <returns></returns>
        public int DeleteEquipmentFromTaskEquipment(int jobID, int equipmentID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_delete_taskequipment_assignment";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@JobID", SqlDbType.Int);
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);

            cmd.Parameters["@JobID"].Value = jobID;
            cmd.Parameters["@EquipmentID"].Value = equipmentID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result != 0)
                {
                    throw new ApplicationException("Task Equipment removal failed.");
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

            return result;
        }

        public int UpdateEquipmentID(int taskEquipmentID, int equipmentID)
        {
            int rowsAffected = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_taskequipment_equipment_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskEquipmentID", taskEquipmentID);
            cmd.Parameters.AddWithValue("@EquipmentID", equipmentID);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        public int UpdateEquipmentIDToNull(int taskEquipmentID)
        {
            int rowsAffected = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_taskequipment_equipment_id_null";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskEquipmentID", taskEquipmentID);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }
    }

}
