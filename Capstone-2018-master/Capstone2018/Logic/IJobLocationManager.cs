using DataObjects;
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
    /// JobLocation Manager class
    /// </summary>
    public interface IJobLocationManager
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates the Job location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeactivateJobLocationByID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates a JobLocation object. Returns the newly created id.
        /// </summary>
        /// <param name="jobLocation"></param>
        /// <returns></returns>
        int CreateJobLocation(JobLocation jobLocation);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the JobLocation of oldJobLocation with data from newJobLocation. 
        /// Returns true if the update worked properly, false otherwise
        /// </summary>
        /// <param name="oldJobLocation"></param>
        /// <param name="newJobLocation"></param>
        /// <returns></returns>
        bool UpdateJobLocation(JobLocation oldJobLocation, JobLocation newJobLocation);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a JobLocation object from a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JobLocation RetrieveJobLocationByID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobLocationAttributes associated with a given JobLocation's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<JobLocationAttribute> RetrieveJobLocationAttributeListByJobLocationID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of JobLocationAttributes accociated with a given ServicePackage's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<JobLocationAttribute> RetrieveJobLocationAttributeListByServicePackageID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves a list of JobLocations associated with a given Customer's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<JobLocation> RetrieveJobLocationListByCustomerID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates/Updates JobLocaiton attributes (merging with database records)
        /// </summary>
        /// <param name="jobLocationAttributes"></param>
        /// <returns></returns>
        int CreateUpdateJobLocationAttributes(IEnumerable<JobLocationAttribute> jobLocationAttributes);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of jobLocationDetails
        /// </summary>
        /// <returns></returns>
        List<JobLocationDetail> RetrieveJobLocationDetailList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a default list of JobLocationattributes
        /// </summary>
        /// <returns></returns>
        List<JobLocationAttribute> RetrieveJobLocationAttributeList();
    }
}
