using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Accessor Mock for Jobs
    /// </summary>
    public class JobAccessorMock : IJobAccessor
    {
        public List<Job> Jobs { get; set; }
        public List<JobDetail> JobDetails { get; set; }

        public JobAccessorMock()
        {
            Jobs = new List<Job>();
            JobDetails = new List<JobDetail>();
            var JobID = Constants.IDSTARTVALUE;
            Jobs.Add(new Job() {
                JobID = Constants.IDSTARTVALUE,
                DateScheduled = new DateTime(2018, 3, 17, 10, 0, 0),
                DateCompleted = new DateTime(2018, 3, 17, 15, 0, 0),
                EmployeeID = 100000,
                JobLocationID = 1000000,
                Comments = "Test comments",
                CustomerID = 1000000,
                Active = true

            });
            Jobs.Add(new Job()
            {
                JobID = Constants.IDSTARTVALUE + 1,
                DateScheduled = new DateTime(2018, 3, 17, 14, 0, 0),
                DateCompleted = new DateTime(2018, 3, 17, 17, 0, 0),
                EmployeeID = 100000,
                JobLocationID = 1000000,
                Comments = "Test comments",
                CustomerID = 1000000,
                Active = false

            });

            JobDetails.Add(new JobDetail {
                Job = Jobs.ElementAt(0),
                JobLocationDetail = new JobLocationDetail
                {
                    JobLocation = new JobLocation
                    {
                        JobLocationID = 1000000,
                        CustomerID = 1000000,
                        Street = "123 Main St",
                        City = "Cedar Rapids",
                        State = "IA",
                        ZipCode = "52404",
                        Comments = "Test Comments",
                        Active = true
                    },
                    JobLocationAttributes = new List<JobLocationAttribute>()
                    {
                        new JobLocationAttribute
                        {
                            JobLocationID = 1000000,
                            JobLocationAttributeTypeID = "Acres to Mow",
                            Value = 5
                        },
                        new JobLocationAttribute
                        {
                            JobLocationID = 1000000,
                            JobLocationAttributeTypeID = "Acres to Sod",
                            Value = 2
                        }
                    }
                },
                Customer = new Customer
                {
                    CustomerID = 1000000,
                    CustomerTypeID = "Residential",
                    Email = "Test1@test.com",
                    FirstName = "Sammy",
                    LastName = "Tester",
                    PhoneNumber = "3193334444",
                    Active = true
                },
                ServicePackages = new List<ServicePackage>()
                {
                    new ServicePackage
                    {
                        ServicePackageID = 1000000,
                        Name = "Gold Package",
                        Description = "The Golden Package",
                        Active = true
                    },
                    new ServicePackage
                    {
                        ServicePackageID = 1000001,
                        Name = "Silver Package",
                        Description = "The Silver Package",
                        Active = true
                    }
                }
            });
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Adds a job to the data store
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int CreateJob(Job job)
        {
            int rowsAffected = 0;
            try
            {
                if(Jobs == null)
                {
                    throw new ApplicationException("The list is null");
                }
                job.JobID = Constants.IDSTARTVALUE + Jobs.Count + 1;
                Jobs.Add(job);
            }
            catch (ApplicationException)
            {
                throw;
            }
            
            return job.JobID;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Merges the given service package list into JobServicePackage data store
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="servicePackages"></param>
        /// <returns></returns>
        public int CreateUpdateJobServicePackage(int jobID, IEnumerable<ServicePackage> servicePackages)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var jd in JobDetails)
                {
                    if (jd.Job.JobID == jobID)
                    {
                        foreach (var sp in servicePackages)
                        {
                            bool insert = false;
                            foreach (var jsp in jd.ServicePackages)
                            {
                                if(jsp.ServicePackageID == sp.ServicePackageID)
                                {
                                    jd.ServicePackages.Remove(jsp);
                                    jd.ServicePackages.Add(sp);
                                    rowsAffected++;
                                    break;
                                }
                                insert = true;
                            }
                            if(insert == true)
                            {
                                jd.ServicePackages.Add(sp);
                                rowsAffected++;
                            }
                        }
                        
                    }
                    
                }
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("The job does not exist.");
                }

            }catch(ApplicationException ae)
            {
                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates the job record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeactivateJob(int id)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var j in Jobs)
                {
                    if(j.JobID == id)
                    {
                        j.Active = false;
                        rowsAffected++;
                    }
                }
                if(rowsAffected == 0)
                {
                    throw new ApplicationException("The Job was not deactivated");

                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all JobDetail records
        /// </summary>
        /// <returns></returns>
        public List<JobDetail> RetrieveJobDetailList()
        {
            if(JobDetails == null)
            {
                throw new ApplicationException("The JobDetail list is empty");
            }
            else
            {
                return JobDetails;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all jobs
        /// </summary>
        /// <returns></returns>
        public List<Job> RetrieveJobList()
        {
            if (Jobs == null)
            {
                throw new ApplicationException("The Job list is empty");
            }
            else
            {
                return Jobs;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all service packages associated with the given Job's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ServicePackage> RetrieveServicePackageListByJobID(int id)
        {
            try
            {
                foreach (var j in JobDetails)
                {
                    if (j.Job.JobID == id)
                    {
                        return j.ServicePackages;
                    }
                }
                throw new ApplicationException("Job was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the data of the oldJob record with data from newJob
        /// </summary>
        /// <param name="oldJob"></param>
        /// <param name="newJob"></param>
        /// <returns></returns>
        public int UpdateJob(Job oldJob, Job newJob)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var j in Jobs)
                {
                    if (j.JobID == oldJob.JobID)
                    {
                        j.JobLocationID = newJob.JobLocationID;
                        j.DateCompleted = newJob.DateCompleted;
                        j.DateScheduled = newJob.DateScheduled;
                        j.EmployeeID = newJob.EmployeeID;
                        j.Comments = newJob.Comments;
                        j.CustomerID = newJob.CustomerID;
                        j.Active = newJob.Active;
                        rowsAffected++;
                    }
                }
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("The Job was not updated");

                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/05/04
        /// 
        /// Creates a job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int CreateJobWeb(Job job)
        {
            int rowsAffected = 0;
            try
            {
                if (Jobs == null)
                {
                    throw new ApplicationException("The list is null");
                }
                job.JobID = Constants.IDSTARTVALUE + Jobs.Count + 1;
                Jobs.Add(job);
            }
            catch (ApplicationException)
            {
                throw;
            }

            return job.JobID;
        }

        public int UpdateJobScheduledDate(int jobID, DateTime scheduledDate)
        {
            throw new NotImplementedException();
        }

        public JobDetail RetreiveJobDetailByID(int jobID)
        {
            throw new NotImplementedException();
        }

		public List<EmployeeJobDetail> RetreiveEmployeeJobDetailByEmployeeID(int employeeId)
		{
			throw new NotImplementedException();
		}
	}
}
