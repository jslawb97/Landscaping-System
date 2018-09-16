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
    /// Created 2018/03/29
    /// 
    /// Interface for TaskTypeEmployeeNeedRecords
    /// </summary>
    public interface ITaskTypeEmployeeNeedAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Gets a list of TaskTypeEmployeeNeed records
        /// </summary>
        /// <returns></returns>
        List<TaskTypeEmployeeNeedDetail> RetrieveTaskTypeEmployeeDetailList();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Adds a TaskTypeEmployeeNeedRecord
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        int CreateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed need);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Deactivate a TaskTypeEmployeeNeedRecord
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeactivateTaskTypeEmployeeNeedByID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Edits a TaskTypeEmployeeNeedRecord with data from newNeed
        /// </summary>
        /// <param name="oldNeed"></param>
        /// <param name="newNeed"></param>
        /// <returns></returns>
        int UpdateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed oldNeed, TaskTypeEmployeeNeed newNeed);
    }
}
