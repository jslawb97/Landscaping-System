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
    /// Created 2018/03/10
    /// 
    /// Accessor for JobLocation related data for a data store
    /// </summary>
    public interface IJobLocationAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a JobLocationDetail object based on a the id of a JobLocation record in the data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JobLocationDetail RetrieveJobLocationDetailByID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates a JobLocation record in a data store
        /// Returns the newly created record's id
        /// </summary>
        /// <param name="jobLocation"></param>
        /// <returns></returns>
        int CreateJobLocation(JobLocation jobLocation);
        
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the JobLocation record with data from a newJobLocation record
        /// </summary>
        /// <param name="oldJobLocation"></param>
        /// <param name="newJobLocation"></param>
        /// <returns></returns>
        int UpdateJobLocation(JobLocation oldJobLocation, JobLocation newJobLocation);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a JobLocation record from the data store with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JobLocation RetrieveJobLocationByID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of JobLocationAttributes from the data store based on the given id of a JobLocation record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<JobLocationAttribute> RetrieveJobLocationAttributeListByJobLocationID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all the JobLocationAttributes associated with a ServicePackage record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<JobLocationAttribute> RetrieveJobLocationAttributeListByServicePackageID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of JobLocation objects based on the given id of a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<JobLocation> RetrieveJobLocationListByCustomerID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Merges data from the given list into the data store.
        /// </summary>
        /// <param name="jobLocationAttributes"></param>
        /// <returns></returns>
        int CreateUpdateJobLocationAttributes(IEnumerable<JobLocationAttribute> jobLocationAttributes);


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all the job location with details list
        /// </summary>
        /// <returns></returns>
        List<JobLocationDetail> RetrieveJobLocationDetailList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates the record of the job location by the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeactivateJobLocationByID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets all the JobLocationAttributes from the database
        /// </summary>
        /// <returns></returns>
        List<JobLocationAttribute> RetrieveJobLocationAttributeList();
    }
}
