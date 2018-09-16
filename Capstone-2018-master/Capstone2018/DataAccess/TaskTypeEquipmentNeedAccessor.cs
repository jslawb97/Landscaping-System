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
    /// Brady Feller
    /// Created 2018/03/27
    /// 
    /// TaskTypeEquipmentNeed Accessor class
    /// </summary>
    public class TaskTypeEquipmentNeedAccessor : ITaskTypeEquipmentNeedAccessor
    {
        /// <summary>
        /// Brady Feller
        /// Created 2018/03/27
        /// 
        /// Calls a stored procedure to create a TaskTypeEquipmentNeed record
        /// </summary>
        /// <param name="taskTypeEquipmentNeed"></param>
        /// <returns></returns>
        public int CreateTaskTypeEquipmentNeed(TaskTypeEquipmentNeed taskTypeEquipmentNeed)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_task_type_equipment_need";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", taskTypeEquipmentNeed.TaskTypeID);
            cmd.Parameters.AddWithValue("@EquipmentTypeID", taskTypeEquipmentNeed.EquipmentTypeID);
            cmd.Parameters.AddWithValue("@HoursOfWork", taskTypeEquipmentNeed.HoursOfWork);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return newId;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Deactivates a TaskTypeEquipmentNeedByID in the database
        /// </summary>
        /// <param name="taskTypeEquipmentNeedID"></param>
        /// <returns></returns>
        public int DeleteTaskTypeEquipmentNeedByID(int taskTypeEquipmentNeedID)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_tasktypeequipmentneed_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeEquipmentNeedID", taskTypeEquipmentNeedID);

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("TaskTypeEquipmentNeed deletion failed.");
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
        /// Brady Feller
        /// Created 2018/03/27
        /// 
        /// Calls a stored procedure to edit a TaskTypeEquipmentNeed record
        /// </summary>
        /// <param name="oldTaskTypeEquipmentNeed"></param>
        /// <param name="newTaskTypeEquipmentNeed"></param>
        /// <returns></returns>
        public int EditTaskTypeEquipmentNeed(TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed, TaskTypeEquipmentNeed newTaskTypeEquipmentNeed)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_task_type_equipment_need";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewTaskTypeID", newTaskTypeEquipmentNeed.TaskTypeID);
            cmd.Parameters.AddWithValue("@NewEquipmentTypeID", newTaskTypeEquipmentNeed.EquipmentTypeID);
            cmd.Parameters.AddWithValue("@NewHoursOfWork", newTaskTypeEquipmentNeed.HoursOfWork);

            cmd.Parameters.AddWithValue("@OldTaskTypeID", oldTaskTypeEquipmentNeed.TaskTypeID);
            cmd.Parameters.AddWithValue("@OldEquipmentTypeID", oldTaskTypeEquipmentNeed.EquipmentTypeID);
            cmd.Parameters.AddWithValue("@OldHoursOfWork", oldTaskTypeEquipmentNeed.HoursOfWork);

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
        /// Brady Feller
        /// Created 2018/03/27
        /// 
        /// Calls a stored procedure to retrieve a TaskTypeEquipmentNeed record by its IDs
        /// </summary>
        /// <param name="taskTypeID"></param>
        /// <param name="equipmentTypeID"></param>
        /// <returns></returns>
        public TaskTypeEquipmentNeed RetrieveTaskTypeEquipmentNeedByID(int taskTypeEquipmentNeed)
        {
            TaskTypeEquipmentNeed tten = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_task_type_equipment_need_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeEquipmentNeedID", taskTypeEquipmentNeed);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    tten = new TaskTypeEquipmentNeed()
                    {
                        TaskTypeEquipmentNeedID = reader.GetInt32(0),
                        TaskTypeID = reader.GetInt32(1),
                        EquipmentTypeID = reader.GetString(2),
                        HoursOfWork = reader.GetInt32(3)
                    };
                }
                else
                {
                    throw new ApplicationException("No data found.");
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
            return tten;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/27
        /// 
        /// Calls a stored procedure to retrieve a list of TaskTypeEquipmentNeed records
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeEquipmentNeed> RetrieveTaskTypeEquipmentNeedList()
        {
            List<TaskTypeEquipmentNeed> taskEquipment = new List<TaskTypeEquipmentNeed>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktypeequipmentneed_detail_list";
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
                        var task = new TaskTypeEquipmentNeed()
                        {
                            TaskTypeEquipmentNeedID = reader.GetInt32(0),
                            TaskTypeID = reader.GetInt32(1),
                            EquipmentTypeID = reader.GetString(2),
                            HoursOfWork = reader.GetInt32(3)
                        };
                        taskEquipment.Add(task);
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


            return taskEquipment;
        }
    }
}
