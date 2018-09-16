using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/08
    /// 
    /// Job Accessor interface to a data store
    /// </summary>
    public interface IJobAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Adds a Job record to the data store
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        int CreateJob(Job job);

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/05/04
        /// 
        /// Adds a Job record to the data store for web only
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        int CreateJobWeb(Job job);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a JobDetail list using records from an Sql Server database
        /// </summary>
        /// <returns></returns>
        List<JobDetail> RetrieveJobDetailList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the oldJob record with data from the newJob in a data store
        /// </summary>
        /// <param name="oldJob"></param>
        /// <param name="newJob"></param>
        /// <returns>The number of records affected</returns>
        int UpdateJob(Job oldJob, Job newJob);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates a job record that has the given id in a data store.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeactivateJob(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of all job records from a data store
        /// </summary>
        /// <returns></returns>
        List<Job> RetrieveJobList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of ServicePackage records that are associated with a Job from the data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ServicePackage> RetrieveServicePackageListByJobID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates/Updates JobServicePackage records in the data store by merging the given collection into the data store
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="servicePackages"></param>
        /// <returns></returns>
        int CreateUpdateJobServicePackage(int jobID, IEnumerable<ServicePackage> servicePackages);
        int UpdateJobScheduledDate(int jobID, DateTime scheduledDate);
        JobDetail RetreiveJobDetailByID(int jobID);
		List<EmployeeJobDetail> RetreiveEmployeeJobDetailByEmployeeID(int employeeId);
    }
}
