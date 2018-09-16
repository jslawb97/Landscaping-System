using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;
using System.Windows;

namespace Logic
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/19
    /// 
    /// JobLocationAttributeType Manager class
    /// </summary>
    public class JobLocationAttributeTypeManager : IJobLocationAttributeTypeManager
    {
        private IJobLocationAttributeTypeAccessor _jobLocationAttributeTypeAccessor;

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Constructor
        /// </summary>
        public JobLocationAttributeTypeManager()
        {
            _jobLocationAttributeTypeAccessor = new JobLocationAttributeTypeAccessor();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Specialized constructor
        /// </summary>
        /// <param name="jobLocationAttributeTypeAccessor"></param>
        public JobLocationAttributeTypeManager(IJobLocationAttributeTypeAccessor jobLocationAttributeTypeAccessor)
        {
            _jobLocationAttributeTypeAccessor = jobLocationAttributeTypeAccessor;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Create
        /// </summary>
        /// <param name="jobLocationAttributeType"></param>
        /// <returns></returns>
        public int CreateJobLocationAttributeType(JobLocationAttributeType jobLocationAttributeType)
        {
            var result = 0;

            if (jobLocationAttributeType.JobLocationAttributeTypeID == "")
            {
                throw new ApplicationException("You must fill out the JobLocationAttributeType ID field.");
            }
            try
            {
                result = _jobLocationAttributeTypeAccessor.CreateJobLocationAttributeType(jobLocationAttributeType);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Edit
        /// </summary>
        /// <param name="oldJobLocationAttributeType"></param>
        /// <param name="newJobLocationAttributeType"></param>
        /// <returns></returns>
        public int EditJobLocationAttributeType(JobLocationAttributeType oldJobLocationAttributeType, JobLocationAttributeType newJobLocationAttributeType)
        {
            var result = 1;

            if (newJobLocationAttributeType.JobLocationAttributeTypeID == "")
            {
                throw new ApplicationException("You must fill out the JobLocationAttributeType ID field.");
            }
            try
            {
                result = _jobLocationAttributeTypeAccessor.EditJobLocationAttributeType(oldJobLocationAttributeType, newJobLocationAttributeType);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Retrieve by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobLocationAttributeType RetrieveJobLocationAttributeTypeByID(string id)
        {
            try
            {
                return _jobLocationAttributeTypeAccessor.RetrieveJobLocationAttributeTypeByID(id);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Retrieving Inspection Checklist By ID.");
                return null;
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Retrieve list
        /// </summary>
        /// <returns></returns>
        public List<JobLocationAttributeType> RetrieveJobLocationAttributeTypeList()
        {
            List<JobLocationAttributeType> jobLocationAttributeTypeList = null;

            try
            {
                jobLocationAttributeTypeList = _jobLocationAttributeTypeAccessor.RetrieveJobLocationAttributeTypeList();
            }
            catch (Exception)
            {
                throw;
            }

            return jobLocationAttributeTypeList;
        }
    }
}
