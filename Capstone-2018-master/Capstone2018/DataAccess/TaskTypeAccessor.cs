using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace DataAccess
{
    /// <summary>
    /// Facilitates TaskType data movement between the application and the SqlServer database 
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/03/23
    /// </remarks>
    public class TaskTypeAccessor : ITaskTypeAccessor
    {
        /// <summary>
        /// Sends data to create a new TaskType in the database
        /// </summary>
        /// <param name="taskType">The TaskType being added to the database</param>
        /// <returns>The id of the new TaskType, or 0 if unsuccessful.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public bool CreateTaskType(TaskType taskType)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_tasktype";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", taskType.Name);
            cmd.Parameters.AddWithValue("@Quantity", taskType.Quantity);
            cmd.Parameters.AddWithValue("@JobLocationAttributeTypeID", taskType.JobLocationAttributeTypeID);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Sends data to create a new TaskType in the database using TaskTypeDetail
        /// </summary>
        /// <param name="taskTypeDetail">The TaskType being added to the database</param>
        /// <returns>The id of the new TaskType, or 0 if unsuccessful.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public bool CreateTaskTypeDetail(TaskTypeDetail taskTypeDetail)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_tasktype";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", taskTypeDetail.TaskType.Name);
            cmd.Parameters.AddWithValue("@Quantity", taskTypeDetail.TaskType.Quantity);
            cmd.Parameters.AddWithValue("@JobLocationAttributeTypeID", taskTypeDetail.JobLocationAttribute.JobLocationAttributeTypeID);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Deactivates the TaskType
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/23
        /// </remarks>
        /// <param name="taskType"></param>
        /// <returns>True if deactivation is successful, False if unsuccessful.</returns>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public bool DeactivateTaskType(TaskType taskType)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_tasktype_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", taskType.TaskTypeID);

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
        /// Deletes a TaskType by ID
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/23
        /// </remarks>
        /// <param name="taskTypeID"></param>
        /// <returns>True if delete is successful, False if unsuccessful.</returns>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public bool DeleteTaskTypeByID(int taskTypeID)
        {
            int result;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_tasktype_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeID", taskTypeID);

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
        /// Sends data to edit an existing taskType in the database by ID
        /// </summary>
        /// <param name="OldTaskType">The TaskType being edited</param>
        /// <param name="NewTaskType">The TaskType with the new data</param>
        /// <returns>True if the update succeeded, False if it failed.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public bool EditTaskTypeByID(TaskType OldTaskType, TaskType NewTaskType)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_tasktype_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", OldTaskType.TaskTypeID);
            cmd.Parameters.AddWithValue("@NewName", NewTaskType.Name);
            cmd.Parameters.AddWithValue("@NewQuantity", NewTaskType.Quantity);
            cmd.Parameters.AddWithValue("@NewJobLocationAttributeTypeID", NewTaskType.JobLocationAttributeTypeID);
            cmd.Parameters.AddWithValue("@NewActive", NewTaskType.Active);
            cmd.Parameters.AddWithValue("@OldName", OldTaskType.Name);
            cmd.Parameters.AddWithValue("@OldQuantity", OldTaskType.Quantity);
            cmd.Parameters.AddWithValue("@OldJobLocationAttributeTypeID", OldTaskType.JobLocationAttributeTypeID);
            cmd.Parameters.AddWithValue("@OldActive", OldTaskType.Active);

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
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retrieves a TaskType by its ID
        /// </summary>
        /// <returns>A TaskType from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public TaskType RetrieveTaskTypeByID(int id)
        {
            TaskType taskType = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktype_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    taskType = new TaskType()
                    {
                        TaskTypeID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        JobLocationAttributeTypeID = reader.GetString(3),
                        Active = reader.GetBoolean(4)
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
            return taskType;
        }

        /// <summary>
        /// Retrieves a TaskType by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A taskType from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public TaskType RetrieveTaskTypeByName(string name)
        {
            TaskType taskType = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktype_by_name";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeID", name);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    taskType = new TaskType()
                    {
                        TaskTypeID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        JobLocationAttributeTypeID = reader.GetString(3),
                        Active = reader.GetBoolean(4)
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
            return taskType;
        }

        /// <summary>
        /// Retrieves a list of JobLocationAttribute IDs
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of JobLocationAttribute IDs from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/25
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public List<string> RetrieveJobLocationAttributeTypeList()
        {
            List<string> list = new List<string>();
            string jobLocationAttributeTypeID;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_joblocationattributetype_list";
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
                        jobLocationAttributeTypeID = reader.GetString(0);

                        list.Add(jobLocationAttributeTypeID);
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
            return list;
        }

        /// <summary>
        /// Retrieves a list of TaskTypeDetail objects
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of TaskTypeDetail objects from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/25
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public List<TaskTypeDetail> RetrieveTaskTypeDetailList()
        {
            List<TaskTypeDetail> list = new List<TaskTypeDetail>();
            TaskTypeDetail taskTypeDetail = new TaskTypeDetail();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktype_detail_list";
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
                        var taskType = new TaskType()
                        {
                            TaskTypeID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                            Active = reader.GetBoolean(3)
                        };

                        var jobLocationAttribute = new JobLocationAttribute()
                        {
                            JobLocationAttributeTypeID = reader.GetString(4)
                        };

                        taskTypeDetail = new TaskTypeDetail()
                        {
                            TaskType = taskType,
                            JobLocationAttribute = jobLocationAttribute
                        };

                        list.Add(taskTypeDetail);
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
            return list;
        }

        /// <summary>
        /// Retrieves a list of TaskTypes 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of all taskTypes from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public List<TaskType> RetrieveTaskTypeList()
        {
            var list = new List<TaskType>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktype_list";
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
                        var taskType = new TaskType()
                        {
                            TaskTypeID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                            JobLocationAttributeTypeID = reader.GetString(3),
                            Active = reader.GetBoolean(4)
                        };
                        list.Add(taskType);
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
            return list;
        }

        /// <summary>
        /// Retrieves a List of all active TaskTypes 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of TaskTypes from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public List<TaskType> RetrieveTaskTypeListByActive()
        {
            var list = new List<TaskType>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktype_by_active";
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
                        var taskType = new TaskType()
                        {
                            TaskTypeID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                            JobLocationAttributeTypeID = reader.GetString(3),
                            Active = reader.GetBoolean(4)
                        };
                        list.Add(taskType);
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
            return list;
        }

        /// <summary>
        /// Retrieves a List of TaskTypes with the same JobLocationAttributeTypeID
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of TaskTypes from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        /// <remarks>QA Jayden Tollefson 4/27/2018</remarks>
        public List<TaskType> RetrieveTaskTypeListByJobLocationAttributeTypeID(string id)
        {
            var list = new List<TaskType>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasktype_by_joblocationattributetypeid";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new TaskType()
                        {
                            TaskTypeID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                            JobLocationAttributeTypeID = reader.GetString(3),
                            Active = reader.GetBoolean(4)
                        };
                        list.Add(item);
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
            return list;
        }
    }
}
