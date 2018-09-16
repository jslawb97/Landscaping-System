using DataObjects;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Job Manager interface
    /// </summary>
    public interface IJobManager
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates a Job record
        /// Returns the id of the newly created record
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        int CreateJob(Job job);

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/05/04
        /// 
        /// Creates a Job record
        /// Returns the id of the newly created record
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        int CreateJobWeb(Job job);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobDetail objects 
        /// </summary>
        /// <returns></returns>
        List<JobDetail> RetrieveJobDetailList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the oldJob record with data from newJob
        /// </summary>
        /// <param name="oldJob"></param>
        /// <param name="newJob"></param>
        /// <returns></returns>
        bool UpdateJob(Job oldJob, Job newJob);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates the Job record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeactivateJob(int id);

        /// <summary>
        /// Retireves a list of all job records
        /// </summary>
        /// <returns></returns>
        List<Job> RetrieveJobList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of ServicePackages accociated with the given Job's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ServicePackage> RetrieveServicePackageListByJobID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Merges the given list of ServicePackages associated with the given Job's id (from JobServicePackage) into the data store
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="servicePackages"></param>
        /// <returns></returns>
        int CreateUpdateJobServicePackage(int jobID, IEnumerable<ServicePackage> servicePackages);

        bool UpdateJobScheduledDate(int jobID, DateTime scheduledDate);
        JobDetail RetreiveJobDetailByID(int jobID);
		List<EmployeeJobDetail> RetreiveEmployeeJobDetailByEmployeeID(int employeeId);
	}
}
