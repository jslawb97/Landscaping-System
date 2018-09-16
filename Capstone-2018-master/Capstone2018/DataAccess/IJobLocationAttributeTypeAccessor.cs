using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/19
    /// 
    /// Interface for the JobLocationAttributeType Accessor
    /// </summary>
    public interface IJobLocationAttributeTypeAccessor
    {
        List<JobLocationAttributeType> RetrieveJobLocationAttributeTypeList();

        JobLocationAttributeType RetrieveJobLocationAttributeTypeByID(string jobLocationAttributeTypeID);

        int CreateJobLocationAttributeType(JobLocationAttributeType jobLocationAttributeType);

        int EditJobLocationAttributeType(JobLocationAttributeType oldJobLocationAttributeType, JobLocationAttributeType newJobLocationAttributeType);

    }
}
