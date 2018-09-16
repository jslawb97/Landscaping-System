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
    /// Brittany Ward
    /// Created 2018/05/08
    /// </summary>
    public interface IJobTaskAccessor
    {
        List<JobTask> RetrieveJobTaskList();
        int EditIsDone(JobTask newJobTask, JobTask oldJobTask);
    }
}
