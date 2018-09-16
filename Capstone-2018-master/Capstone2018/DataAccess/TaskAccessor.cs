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
    /// Facilitates Task data movement between the application and the SqlServer database 
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/03/05
    /// </remarks>
    public class TaskAccessor : ITaskAccessor
    {
        /// <summary>
        /// Retrieves a Task by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A task from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public DataObjects.Task RetrieveTaskByID(int id)
        {
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_task_by_id";
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
                        return new DataObjects.Task()
                        {
                            TaskID = reader.GetInt32(0),
                            TaskTypeID = reader.GetInt32(1),
                            ServiceItemID = reader.GetInt32(2),
                            Name = reader.GetString(3),
                            Description = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Retrieves a list of Task objects from the SqlServer crlandscaping database
        /// </summary>
        /// <returns>A list of Tasks from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public List<DataObjects.Task> RetrieveTaskList()
        {
            List<DataObjects.Task> tasks = new List<DataObjects.Task>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_task_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var task = new DataObjects.Task()
                        {
                            TaskID = reader.GetInt32(0),
                            TaskTypeID = reader.GetInt32(1),
                            ServiceItemID = reader.GetInt32(2),
                            Name = reader.GetString(3),
                            Description = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        tasks.Add(task);
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
            return tasks;
        }

        /// <summary>
        /// Sends data to edit an existing task in the database by TaskID
        /// </summary>
        /// <param name="OldTask">The Task being edited</param>
        /// <param name="NewTask">The Task with the new data</param>
        /// <returns>true if the update succeeded, and false if the update failed.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public bool EditTask(DataObjects.Task OldTask, DataObjects.Task NewTask)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_task_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskID", OldTask.TaskID);
            cmd.Parameters.AddWithValue("@OldTaskTypeID", OldTask.TaskTypeID);
            cmd.Parameters.AddWithValue("@NewTaskTypeID", NewTask.TaskTypeID);
            cmd.Parameters.AddWithValue("@OldServiceItemID", OldTask.ServiceItemID);
            cmd.Parameters.AddWithValue("@NewServiceItemID", NewTask.ServiceItemID);
            cmd.Parameters.AddWithValue("@OldName", OldTask.Name);
            cmd.Parameters.AddWithValue("@NewName", NewTask.Name);
            cmd.Parameters.AddWithValue("@OldDescription", OldTask.Description);
            cmd.Parameters.AddWithValue("@NewDescription", NewTask.Description);
            cmd.Parameters.AddWithValue("@OldActive", OldTask.Active);
            cmd.Parameters.AddWithValue("@NewActive", NewTask.Active);

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

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sends data to create a new Task in the database
        /// </summary>
        /// <param name="task">The Task being added to the database</param>
        /// <returns>True if successful, False if unsuccessful</returns>
        /// <remarks>
        /// John Miller
        /// Updated2018/03/05
        /// </remarks>
        public bool CreateTask(DataObjects.Task task)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_task";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", task.TaskTypeID);
            cmd.Parameters.AddWithValue("@ServiceItemID", task.ServiceItemID);
            cmd.Parameters.AddWithValue("@Name", task.Name);
            cmd.Parameters.AddWithValue("@Description", task.Description);
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
            if (newID != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// John Miller
        /// Created: 2018/03/05
        /// 
        /// Deactivates the task record with the given ID in the SqlServer database
        /// </summary>
        /// <param name="taskID">The ID of the Task to be deactivated</param>
        /// <returns>True if successful, false if unsuccessful</returns>
        public bool DeactivateTaskByID(int taskID)
        {
            int result = 0;
            bool isDeactivated = false;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_task_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskID", taskID);

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
            if (result == 1)
            {
                isDeactivated = true;
            }
            else
            {
                isDeactivated = false;
            }
            return isDeactivated;
        }

    }
}
