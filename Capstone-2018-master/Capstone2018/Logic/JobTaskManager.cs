using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class JobTaskManager : IJobTaskManager
    {
        private IJobTaskAccessor _jobTaskAccessor;

        public JobTaskManager()
        {
            _jobTaskAccessor = new JobTaskAccessor();
        }

        public JobTaskManager(IJobTaskAccessor jobTaskAccessor)
        {
            _jobTaskAccessor = jobTaskAccessor;
        }

        /// <summary>
        /// Brittany Ward
        /// 2018/05/08
        /// 
        /// Retrieves the JobTask Table
        /// </summary>
        /// <returns></returns>
        public List<JobTask> RetrieveJobTaskList()
        {
            List<JobTask> jobTasks = null;

            try
            {
                jobTasks = _jobTaskAccessor.RetrieveJobTaskList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to connect to server.", ex);
            }

            return jobTasks;

        }

        /// <summary>
        /// Brittany Ward
        /// 2018/05/08
        /// 
        /// Edits the IsDone field in JobTask table
        /// </summary>
        /// <param name="newJobTask"></param>
        /// <param name="oldJobTask"></param>
        /// <returns></returns>
        public int EditIsDone(JobTask newJobTask, JobTask oldJobTask)
        {
            int result = 0;

            try
            {
                result = _jobTaskAccessor.EditIsDone(newJobTask, oldJobTask);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
