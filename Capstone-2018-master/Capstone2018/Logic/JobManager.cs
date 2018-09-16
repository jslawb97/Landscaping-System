using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class JobManager : IJobManager
    {

        private IJobAccessor _jobAccessor;

        public JobManager()
        {
            _jobAccessor = new JobAccessor();
        }

        public JobManager(IJobAccessor jobAccessor)
        {
            _jobAccessor = jobAccessor;
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates the given Job record. 
        /// Returns the newly created record's id
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int CreateJob(Job job)
        {
            int result = 0;

            try
            {
                result = _jobAccessor.CreateJob(job);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Merges the list of ServicePackages into JobServicePackage records with the given id.
        /// If records are in the list that are not in the database, records are inserted.
        /// If records are in the database that are not in the list, those records are deleted.
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="servicePackages"></param>
        /// <returns></returns>
        public int CreateUpdateJobServicePackage(int jobID, IEnumerable<ServicePackage> servicePackages)
        {
            int result = 0;

            try
            {
                result = _jobAccessor.CreateUpdateJobServicePackage(jobID, servicePackages);
            }
            catch (Exception)
            {

                result = -1;
            }


            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates the job with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeactivateJob(int id)
        {
            if(id < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid ID: ID must be no less than " + Constants.IDSTARTVALUE);
            }

            try
            {
                return 1 == _jobAccessor.DeactivateJob(id);
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
        /// Updates the oldJob record with data from the newJob record
        /// </summary>
        /// <param name="oldJob"></param>
        /// <param name="newJob"></param>
        /// <returns></returns>
        public bool UpdateJob(Job oldJob, Job newJob)
        {
            var result = true;

            try
            {
                _jobAccessor.UpdateJob(oldJob, newJob);
            }
            catch (Exception)
            {

                throw;
            }


            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobDetail objects
        /// </summary>
        /// <returns></returns>
        public List<JobDetail> RetrieveJobDetailList()
        {
            var list = new List<JobDetail>();

            try
            {
                list = _jobAccessor.RetrieveJobDetailList();
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// gets a list of all job objects
        /// </summary>
        /// <returns></returns>
        public List<Job> RetrieveJobList()
        {
            List<Job> list = new List<Job>();

            try
            {
                list = _jobAccessor.RetrieveJobList();
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all servicepackages asscociated with the given Job's id from the JobServicePackage table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ServicePackage> RetrieveServicePackageListByJobID(int id)
        {
            var list = new List<ServicePackage>();

            try
            {
                list = _jobAccessor.RetrieveServicePackageListByJobID(id);
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        public bool UpdateJobScheduledDate(int jobID, DateTime scheduledDate)
        {
            var result = true;

            try
            {
                _jobAccessor.UpdateJobScheduledDate(jobID, scheduledDate);
            }
            catch (Exception)
            {

                throw;
            }


            return result;
        }

        public JobDetail RetreiveJobDetailByID(int jobID)
        {
            JobDetail detail = null;

            try
            {
                detail = _jobAccessor.RetreiveJobDetailByID(jobID);
            }
            catch (Exception)
            {

                throw;
            }

            return detail;
        }

		public List<EmployeeJobDetail> RetreiveEmployeeJobDetailByEmployeeID(int employeeId)
		{
			var list = new List<EmployeeJobDetail>();

			try
			{
				list = _jobAccessor.RetreiveEmployeeJobDetailByEmployeeID(employeeId);
			}
			catch (Exception)
			{

				throw;
			}

			return list;
		}

        /// <summary>
        /// Jacob Conley
        /// 2018/05/04
        /// 
        /// Creates a Job using the Web application
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int CreateJobWeb(Job job)
        {
            int result = 0;

            try
            {
                result = _jobAccessor.CreateJobWeb(job);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
