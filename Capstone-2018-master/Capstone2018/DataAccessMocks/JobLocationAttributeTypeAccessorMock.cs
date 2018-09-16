using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/19
    /// 
    /// Mock class for JobLocationAttributeType
    /// </summary>
    public class JobLocationAttributeTypeAccessorMock : IJobLocationAttributeTypeAccessor
    {
        private List<JobLocationAttributeType> _jobLocationAttributeTypes = new List<JobLocationAttributeType>();

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Constructor
        /// </summary>
        public JobLocationAttributeTypeAccessorMock()
        {
            _jobLocationAttributeTypes.Add(new JobLocationAttributeType
            {
                JobLocationAttributeTypeID = "thing 1"
            });
            _jobLocationAttributeTypes.Add(new JobLocationAttributeType
            {
                JobLocationAttributeTypeID = "thing 2"
            });
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Create mock
        /// </summary>
        /// <param name="jobLocationAttributeType"></param>
        /// <returns></returns>
        public int CreateJobLocationAttributeType(JobLocationAttributeType jobLocationAttributeType)
        {
            if (jobLocationAttributeType.JobLocationAttributeTypeID != "" &&
                jobLocationAttributeType.JobLocationAttributeTypeID.Length <= 100)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Edit mock
        /// </summary>
        /// <param name="oldJobLocationAttributeType"></param>
        /// <param name="newJobLocationAttributeType"></param>
        /// <returns></returns>
        public int EditJobLocationAttributeType(JobLocationAttributeType oldJobLocationAttributeType, JobLocationAttributeType newJobLocationAttributeType)
        {
            if (oldJobLocationAttributeType.JobLocationAttributeTypeID != "" &&
                oldJobLocationAttributeType.JobLocationAttributeTypeID.Length <= 100 &&
                newJobLocationAttributeType.JobLocationAttributeTypeID != "" &&
                newJobLocationAttributeType.JobLocationAttributeTypeID.Length <= 100)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Retrieve by ID Mock
        /// </summary>
        /// <param name="jobLocationAttributeTypeID"></param>
        /// <returns></returns>
        public JobLocationAttributeType RetrieveJobLocationAttributeTypeByID(string jobLocationAttributeTypeID)
        {
            return this._jobLocationAttributeTypes.Find(jobLocationAttributeType => jobLocationAttributeType.JobLocationAttributeTypeID.Equals(jobLocationAttributeTypeID));
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Retrieve list mock
        /// </summary>
        /// <returns></returns>
        public List<JobLocationAttributeType> RetrieveJobLocationAttributeTypeList()
        {
            return _jobLocationAttributeTypes;
        }
    }
}
