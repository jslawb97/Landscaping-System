using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/19
    /// 
    /// JobLocationAttributeType interface for the manager class
    /// </summary>
    public interface IJobLocationAttributeTypeManager
    {
        int CreateJobLocationAttributeType(JobLocationAttributeType jobLocationAttributeType);

        List<JobLocationAttributeType> RetrieveJobLocationAttributeTypeList();

        int EditJobLocationAttributeType(JobLocationAttributeType oldJobLocationAttributeType, JobLocationAttributeType newJobLocationAttributeType);

        JobLocationAttributeType RetrieveJobLocationAttributeTypeByID(string id);
    }
}
