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
    /// Created 2018/03/19
    /// 
    /// JobLocationAttributeType Accessor class
    /// </summary>
    public class JobLocationAttributeTypeAccessor : IJobLocationAttributeTypeAccessor
    {
        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Calls a stored procedure to create a JobLocationAttributeType record
        /// </summary>
        /// <param name="jobLocationAttributeType"></param>
        /// <returns></returns>
        public int CreateJobLocationAttributeType(JobLocationAttributeType jobLocationAttributeType)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_job_location_attribute_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobLocationAttributeTypeID", jobLocationAttributeType.JobLocationAttributeTypeID);

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
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Calls a stored procedure to edit a JobLocationAttributeType record
        /// </summary>
        /// <param name="oldJobLocationAttributeType"></param>
        /// <param name="newJobLocationAttributeType"></param>
        /// <returns></returns>
        public int EditJobLocationAttributeType(JobLocationAttributeType oldJobLocationAttributeType, JobLocationAttributeType newJobLocationAttributeType)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_job_location_attribute_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewJobLocationAttributeTypeID", newJobLocationAttributeType.JobLocationAttributeTypeID);
            
            cmd.Parameters.AddWithValue("@OldJobLocationAttributeTypeID", oldJobLocationAttributeType.JobLocationAttributeTypeID);

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
        /// Created 2018/03/19
        /// 
        /// Calls a stored procedure to retrieve a JobLocationAttributeType record by their ID
        /// </summary>
        /// <param name="jobLocationAttributeTypeID"></param>
        /// <returns></returns>
        public JobLocationAttributeType RetrieveJobLocationAttributeTypeByID(string jobLocationAttributeTypeID)
        {
            JobLocationAttributeType jobLocationAttributeType = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_job_location_attribute_type_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobLocationAttributeTypeID", jobLocationAttributeTypeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    jobLocationAttributeType = new JobLocationAttributeType()
                    {
                        JobLocationAttributeTypeID = reader.GetString(0)
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
            return jobLocationAttributeType;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Calls a stored procedure to retrieve a list JobLocationAttributeType records
        /// </summary>
        /// <returns></returns>
        public List<JobLocationAttributeType> RetrieveJobLocationAttributeTypeList()
        {
            var jobLocationAttributeType = new List<JobLocationAttributeType>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_job_location_attribute_type_list";
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
                        var jlat = new JobLocationAttributeType()
                        {
                            JobLocationAttributeTypeID = reader.GetString(0)
                        };
                        jobLocationAttributeType.Add(jlat);
                    }
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
            return jobLocationAttributeType;
        }
    }
}
