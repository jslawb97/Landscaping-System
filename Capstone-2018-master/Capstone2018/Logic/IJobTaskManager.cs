using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IJobTaskManager
    {
        List<JobTask> RetrieveJobTaskList();
        int EditIsDone(JobTask newJobTask, JobTask oldJobTask);
    }
}
