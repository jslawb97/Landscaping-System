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
    public class JobTaskAccessor : IJobTaskAccessor
    {
        public List<JobTask> RetrieveJobTaskList()
        {
            var jobTasks = new List<JobTask>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_job_task";
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
                        var jobTask = new JobTask()
                        {
                            JobTaskID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            isDone = reader.GetBoolean(3)
                        };
                        jobTasks.Add(jobTask);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
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

            return jobTasks;
        }

        public int EditIsDone(JobTask newJobTask, JobTask oldJobTask)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_is_done";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobTaskID", oldJobTask.JobTaskID);

            cmd.Parameters.AddWithValue("@OldIsDone", oldJobTask.isDone);

            cmd.Parameters.AddWithValue("@NewIsDone", newJobTask.isDone);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error. ", ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
