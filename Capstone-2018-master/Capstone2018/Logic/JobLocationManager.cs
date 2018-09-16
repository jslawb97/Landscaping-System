using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// JobLocation Manager
    /// </summary>
    public class JobLocationManager : IJobLocationManager
    {
        private IJobLocationAccessor _jobLocationAccessor;

        public JobLocationManager()
        {
            _jobLocationAccessor = new JobLocationAccessor();
        }

        public JobLocationManager(IJobLocationAccessor jobLocationAccessor)
        {
            _jobLocationAccessor = jobLocationAccessor;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates a Job Location record in the Sql Server database
        /// Returns the newly created id
        /// </summary>
        /// <param name="jobLocation"></param>
        /// <returns></returns>
        public int CreateJobLocation(JobLocation jobLocation)
        {
            int result = 0;


            try
            {
                result = _jobLocationAccessor.CreateJobLocation(jobLocation);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Manages the merging of a list of JobLocationAttributes into the Sql Server database
        /// </summary>
        /// <param name="jobLocationAttributes"></param>
        /// <returns></returns>
        public int CreateUpdateJobLocationAttributes(IEnumerable<JobLocationAttribute> jobLocationAttributes)
        {
            int result = 0;
            try
            {
                result = _jobLocationAccessor.CreateUpdateJobLocationAttributes(jobLocationAttributes);
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
        /// Edits the oldJobLocation record with data from the newJobLocation record
        /// </summary>
        /// <param name="oldJobLocation"></param>
        /// <param name="newJobLocation"></param>
        /// <returns></returns>
        public bool UpdateJobLocation(JobLocation oldJobLocation, JobLocation newJobLocation)
        {
            bool result = false;


            try
            {
                result = (1 == _jobLocationAccessor.UpdateJobLocation(oldJobLocation, newJobLocation));
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
        /// Gets a list of JobLocation attributes associated with a given JobLocation id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeListByJobLocationID(int id)
        {
            List<JobLocationAttribute> list = new List<JobLocationAttribute>();

            try
            {
                list = _jobLocationAccessor.RetrieveJobLocationAttributeListByJobLocationID(id);
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
        /// Gets a list of JobLocationAttributes needed for a given ServicePackage id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeListByServicePackageID(int id)
        {
            var list = new List<JobLocationAttribute>();

            try
            {
                list = _jobLocationAccessor.RetrieveJobLocationAttributeListByServicePackageID(id);
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
        /// Gets a JobLocation object from a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobLocation RetrieveJobLocationByID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Get a list of JobLocation records that have a given customer id of id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocation> RetrieveJobLocationListByCustomerID(int id)
        {
            var list = new List<JobLocation>();

            try
            {
                list = _jobLocationAccessor.RetrieveJobLocationListByCustomerID(id);
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
        /// Gets a list of JobLocationDetails
        /// </summary>
        /// <returns></returns>
        public List<JobLocationDetail> RetrieveJobLocationDetailList()
        {
            var list = new List<JobLocationDetail>();

            try
            {
                list = _jobLocationAccessor.RetrieveJobLocationDetailList();
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
        /// Deactivates the job location with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeactivateJobLocationByID(int id)
        {
            bool result = false;
            try
            {
                result = (1 == _jobLocationAccessor.DeactivateJobLocationByID(id));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// Gets generic list of all job location attributes
        /// </summary>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeList()
        {
            var list = new List<JobLocationAttribute>();

            try
            {
                list = _jobLocationAccessor.RetrieveJobLocationAttributeList();
            }
            catch (Exception)
            {

                throw;
            }


            return list;
        }
    }
}
