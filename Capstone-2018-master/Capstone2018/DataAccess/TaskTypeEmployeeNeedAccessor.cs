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
    /// Created 2018/03/29
    /// 
    /// Accessor for TaskTypeEmployeeNeed Records between a SqlServer database
    /// </summary>
    public class TaskTypeEmployeeNeedAccessor : ITaskTypeEmployeeNeedAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Adds a TaskTypeEmployeeNeedRecord
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        public int CreateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed need)
        {
            var rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_taskttypeemployeeneed";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", need.TaskTypeID);
            cmd.Parameters.AddWithValue("@HoursOfWork", need.HoursOfWork);
            cmd.Parameters.AddWithValue("@Active", need.Active);


            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Deactivate a TaskTypeEmployeeNeedRecord
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeactivateTaskTypeEmployeeNeedByID(int id)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_taskttypeemployeeneed_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", id);

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
        /// Created 2018/03/29
        /// 
        /// Gets a list of TaskTypeEmployeeNeed records
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeEmployeeNeedDetail> RetrieveTaskTypeEmployeeDetailList()
        {//sp_retrieve_taskttypeemployeeneed_detail_list
            /*[TaskTypeEmployeeNeed].[TaskTypeID], [TaskTypeEmployeeNeed].[HoursOfWork],
					[TaskType].[Name], [TaskType].[Quantity], [TaskType].[JobLocationAttributeTypeID]*/
            List<TaskTypeEmployeeNeedDetail> items = new List<TaskTypeEmployeeNeedDetail>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_taskttypeemployeeneed_detail_list";
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
                        var item = new TaskTypeEmployeeNeedDetail()
                        {
                            TaskTypeEmployeeNeed = new TaskTypeEmployeeNeed
                            {
                                TaskTypeID = reader.GetInt32(0),
                                HoursOfWork = reader.GetInt32(1),
                                Active = reader.GetBoolean(2)
                            },
                            TaskType = new TaskType
                            {
                                TaskTypeID = reader.GetInt32(0),
                                Name = reader.GetString(3),
                                Quantity = reader.GetInt32(4),
                                JobLocationAttributeTypeID = reader.GetString(5)
                            }
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
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Edits a TaskTypeEmployeeNeedRecord with data from newNeed
        /// </summary>
        /// <param name="oldNeed"></param>
        /// <param name="newNeed"></param>
        /// <returns></returns>
        public int UpdateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed oldNeed, TaskTypeEmployeeNeed newNeed)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_taskttypeemployeeneed_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeID", oldNeed.TaskTypeID);
            cmd.Parameters.AddWithValue("@NewHoursOfWork", newNeed.HoursOfWork);
            cmd.Parameters.AddWithValue("@OldHoursOfWork", oldNeed.HoursOfWork);
            cmd.Parameters.AddWithValue("@NewActive", newNeed.Active);
            cmd.Parameters.AddWithValue("@OldActive", oldNeed.Active);
            


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
    }
}
