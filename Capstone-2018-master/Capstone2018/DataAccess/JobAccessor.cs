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
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Data access for Job related objects to a Sql Server database
    /// </summary>
    public class JobAccessor : IJobAccessor
    {

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Adds a job to the Sql Server database Job table
        /// Returns the id of the newly created record
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int CreateJob(Job job)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_job_with_target_window";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerID", job.CustomerID);
            cmd.Parameters.AddWithValue("@DateTimeTarget", job.DateTarget);
            cmd.Parameters.AddWithValue("@EmployeeID", job.EmployeeID);
            cmd.Parameters.AddWithValue("@JobLocationID", job.JobLocationID);
            cmd.Parameters.AddWithValue("@Active", job.Active);
            cmd.Parameters.AddWithValue("@DateCompleted", job.DateCompleted == null ? System.Data.SqlTypes.SqlDateTime.Null : (DateTime)(job.DateCompleted));
            cmd.Parameters.AddWithValue("@Comments", job.Comments);

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


            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/05/04
        /// 
        /// Adds a job to the Sql Server database Job table
        /// Returns the id of the newly created record from web
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int CreateJobWeb(Job job)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_job_web";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerID", job.CustomerID);
            cmd.Parameters.AddWithValue("@DateTimeScheduled", job.DateScheduled == null ? System.Data.SqlTypes.SqlDateTime.Null : (DateTime)(job.DateTarget));
            cmd.Parameters.AddWithValue("@DateTimeTarget", job.DateTarget);
            cmd.Parameters.AddWithValue("@JobLocationID", job.JobLocationID);
            cmd.Parameters.AddWithValue("@DateCompleted", job.DateCompleted == null ? System.Data.SqlTypes.SqlDateTime.Null : (DateTime)(job.DateCompleted));
            cmd.Parameters.AddWithValue("@Comments", job.Comments);

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


            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates/Updates records in the JobServicePackage table. Will merge the given list with the current records in the Sql Server databse.
        /// Returns the number of rows affected.
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="servicePackages"></param>
        /// <returns></returns>
        public int CreateUpdateJobServicePackage(int jobID, IEnumerable<ServicePackage> servicePackages)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_edit_jobservicepackage_updated";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var list = new List<SqlDataRecord>();

            foreach (var item in servicePackages)
            {
                var record = new SqlDataRecord(new SqlMetaData[] { new SqlMetaData("JobID", SqlDbType.Int),
                                                                   new SqlMetaData("ServicePackageID", SqlDbType.Int)
                                                                   });
                record.SetInt32(0, jobID);
                record.SetInt32(1, item.ServicePackageID);
                list.Add(record);
            }

            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@tvpServicePackages", list);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.JobServicePackageType";
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        /// Created 2018/03/10
        /// 
        /// Deactivates the job record with the given id
        /// Returns the number of rows affected
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeactivateJob(int id)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_job_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobID", id);

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
        /// Created 2018/03/10
        /// 
        /// Updates the job record represented by oldJob with data from the newJob object
        /// </summary>
        /// <param name="oldJob"></param>
        /// <param name="newJob"></param>
        /// <returns></returns>
        public int UpdateJob(Job oldJob, Job newJob)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_job_with_target_window";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobID", oldJob.JobID);
            cmd.Parameters.AddWithValue("@OldCustomerID", oldJob.CustomerID);
            cmd.Parameters.AddWithValue("@NewCustomerID", newJob.CustomerID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldJob.EmployeeID);
            cmd.Parameters.AddWithValue("@NewEmployeeID", newJob.EmployeeID);
            cmd.Parameters.AddWithValue("@OldJobLocationID", oldJob.JobLocationID);
            cmd.Parameters.AddWithValue("@NewJobLocationID", newJob.JobLocationID);
            cmd.Parameters.AddWithValue("@OldActive", oldJob.Active);
            cmd.Parameters.AddWithValue("@NewActive", newJob.Active);
            if (oldJob.DateCompleted.HasValue)
            {
                cmd.Parameters.AddWithValue("@OldDateCompleted", oldJob.DateCompleted);
            }

            if (newJob.DateCompleted.HasValue)
            {
                cmd.Parameters.AddWithValue("@NewDateCompleted", newJob.DateCompleted);
            }

            if (oldJob.DateScheduled != null)
            {
                cmd.Parameters.AddWithValue("@OldDateScheduled", oldJob.DateScheduled);
            }

            if (newJob.DateScheduled != null)
            {
                cmd.Parameters.AddWithValue("@NewDateScheduled", newJob.DateScheduled);
            }

            cmd.Parameters.AddWithValue("@OldTargetWindow", oldJob.DateTarget);
            cmd.Parameters.AddWithValue("@NewTargetWindow", newJob.DateTarget);
            cmd.Parameters.AddWithValue("@OldComments", oldJob.Comments);
            cmd.Parameters.AddWithValue("@NewComments", newJob.Comments);



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
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of JobDetail objects. Pulls from a list of all job objects.
        /// </summary>
        /// <returns></returns>
        public List<JobDetail> RetrieveJobDetailList()
        {
            var list = new List<JobDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_job_detail_list_with_target_window";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    Job job = null;
                    Customer customer = null;
                    JobLocation location = null;
                    List<JobLocationAttribute> attributes = new List<JobLocationAttribute>();
                    List<JobServicePackage> servicePackages = new List<JobServicePackage>();


                    while (reader.Read())
                    {
                        job = new Job
                        {
                            JobID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            DateScheduled = reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2),
                            EmployeeID = reader.GetInt32(3),
                            JobLocationID = reader.GetInt32(4),
                            Active = reader.GetBoolean(5),
                            DateCompleted = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                            Comments = reader.GetString(7),
                            DateTarget = reader.IsDBNull(21) ? null : (DateTime?)reader.GetDateTime(21)
						};

                        customer = new Customer
                        {
                            CustomerID = reader.GetInt32(1),
                            CustomerTypeID = reader.GetString(8),
                            Email = reader.GetString(9),
                            FirstName = reader.GetString(10),
                            LastName = reader.GetString(11),
                            PhoneNumber = reader.GetString(12),
                            Active = reader.GetBoolean(13)
                        };

                        location = new JobLocation
                        {
                            JobLocationID = reader.GetInt32(14),
                            Street = reader.GetString(15),
                            City = reader.GetString(16),
                            State = reader.GetString(17),
                            ZipCode = reader.GetString(18),
                            Comments = reader.GetString(19),
                            Active = reader.GetBoolean(20)
                        };

                        list.Add(new JobDetail {
                            Job = job,
                            JobLocationDetail = new JobLocationDetail
                            {
                                JobLocation = location
                            },
                            Customer = customer
                        });
                    }

                    //now job location attributes
                    reader.NextResult();

                    while (reader.Read())
                    {
                        attributes.Add(new JobLocationAttribute
                        {
                            JobLocationID = reader.GetInt32(0),
                            JobLocationAttributeTypeID = reader.GetString(1),
                            Value = reader.GetInt32(2)

                        });
                    }

                    //now JobServicePackages
                    reader.NextResult();

                    while (reader.Read())
                    {
                        servicePackages.Add(new JobServicePackage
                        {
                            JobID = reader.GetInt32(0),
                            ServicePackage = new ServicePackage
                            {
                                ServicePackageID = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Description = reader.GetString(3),
                                Active = reader.GetBoolean(4)
                            }

                        });
                    }


                    foreach (var detail in list)
                    {
                        detail.JobLocationDetail.JobLocationAttributes = attributes.Where(a => a.JobLocationID == detail.JobLocationDetail.JobLocation.JobLocationID).ToList();
                        var jobServicePackages = servicePackages.Where(j => j.JobID == detail.Job.JobID).ToList();
                        detail.ServicePackages = new List<ServicePackage>();
                        foreach (var package in jobServicePackages)
                        {
                            detail.ServicePackages.Add(package.ServicePackage);
                        }
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
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of all Job records in the Sql Server database
        /// </summary>
        /// <returns></returns>
        public List<Job> RetrieveJobList()
        {
            var list = new List<Job>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_job_list";
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
                        var item = new Job()
                        {
                            JobID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            DateScheduled = reader.GetDateTime(2),
                            EmployeeID = reader.GetInt32(3),
                            JobLocationID = reader.GetInt32(4),
                            Active = reader.GetBoolean(5),
                            DateCompleted = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                            Comments = reader.GetString(7)
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

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of ServicePackage object from the JobServicePackage table where id is the JobID of the records
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ServicePackage> RetrieveServicePackageListByJobID(int id)
        {
            var list = new List<ServicePackage>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_servicepackage_list_by_jobid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new ServicePackage()
                        {
                            ServicePackageID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3)
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

        /// <summary>
        /// Zachary Hall
        /// Created 2018/04/27
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="scheduledDate"></param>
        /// <returns></returns>
        public int UpdateJobScheduledDate(int jobID, DateTime scheduledDate)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_job_scheduled_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@JobID", jobID);
            cmd.Parameters.AddWithValue("@NewScheduledDate", scheduledDate);



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
        /// Created 2018/04/27
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public JobDetail RetreiveJobDetailByID(int jobID)
        {
            JobDetail detail = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_job_detail_by_jobid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobID", jobID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    Job job = null;
                    Customer customer = null;
                    JobLocation location = null;
                    List<JobLocationAttribute> attributes = new List<JobLocationAttribute>();
                    List<JobServicePackage> servicePackages = new List<JobServicePackage>();

                    var JobID = reader.GetInt32(0);
                    var CustomerID = reader.GetInt32(1);
                    var DateScheduled = reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2);
                    var EmployeeID = reader.GetInt32(3);
                    var JobLocationID = reader.GetInt32(4);
                    var Active = reader.GetBoolean(5);
                    var DateCompleted = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6);
                    var Comments = reader.GetString(7);
                    var DateTarget = reader.GetDateTime(21);


                    job = new Job
                    {
                        JobID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        DateScheduled = reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2),
                        EmployeeID = reader.GetInt32(3),
                        JobLocationID = reader.GetInt32(4),
                        Active = reader.GetBoolean(5),
                        DateCompleted = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                        Comments = reader.GetString(7),
                        DateTarget = reader.GetDateTime(21)
                    };

                    customer = new Customer
                    {
                        CustomerID = reader.GetInt32(1),
                        CustomerTypeID = reader.GetString(8),
                        Email = reader.GetString(9),
                        FirstName = reader.GetString(10),
                        LastName = reader.GetString(11),
                        PhoneNumber = reader.GetString(12),
                        Active = reader.GetBoolean(13)
                    };

                    location = new JobLocation
                    {
                        JobLocationID = reader.GetInt32(14),
                        Street = reader.GetString(15),
                        City = reader.GetString(16),
                        State = reader.GetString(17),
                        ZipCode = reader.GetString(18),
                        Comments = reader.GetString(19),
                        Active = reader.GetBoolean(20)
                    };

                   detail = new JobDetail
                    {
                        Job = job,
                        JobLocationDetail = new JobLocationDetail
                        {
                            JobLocation = location
                        },
                        Customer = customer
                    };
                    

                    //now job location attributes
                    reader.NextResult();

                    while (reader.Read())
                    {
                        attributes.Add(new JobLocationAttribute
                        {
                            JobLocationID = reader.GetInt32(0),
                            JobLocationAttributeTypeID = reader.GetString(1),
                            Value = reader.GetInt32(2)

                        });
                    }

                    //now JobServicePackages
                    reader.NextResult();

                    while (reader.Read())
                    {
                        servicePackages.Add(new JobServicePackage
                        {
                            JobID = reader.GetInt32(0),
                            ServicePackage = new ServicePackage
                            {
                                ServicePackageID = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Description = reader.GetString(3),
                                Active = reader.GetBoolean(4)
                            }

                        });
                    }
                    detail.JobLocationDetail.JobLocationAttributes = attributes.Where(a => a.JobLocationID == detail.JobLocationDetail.JobLocation.JobLocationID).ToList();
                    var jobServicePackages = servicePackages.Where(j => j.JobID == detail.Job.JobID).ToList();
                    detail.ServicePackages = new List<ServicePackage>();
                    foreach (var package in jobServicePackages)
                    {
                        detail.ServicePackages.Add(package.ServicePackage);
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


            return detail;
        }

		/// <summary>
		/// Zachary Hall
		/// Created 2018/05/04
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		public List<EmployeeJobDetail> RetreiveEmployeeJobDetailByEmployeeID(int employeeId)
		{
			var list = new List<EmployeeJobDetail>();

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_retreive_job_list_by_employee";
			var cmd = new SqlCommand(cmdText, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var item = new EmployeeJobDetail()
						{
							JobID = reader.GetInt32(0),
							JobLocationStreet = reader.GetString(1),
							JobLocationCity = reader.GetString(2),
							JobLocationState = reader.GetString(3),
							JobLocationZipCode = reader.GetString(4),
							JobScheduled = reader.GetDateTime(5),
							EmployeeAddress = reader.GetString(6)
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
