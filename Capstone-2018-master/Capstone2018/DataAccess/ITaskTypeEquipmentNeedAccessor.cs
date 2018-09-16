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
    /// Created 2018/03/27
    /// 
    /// Interface for TaskTypeEquipmentNeed Accessor
    /// </summary>
    public interface ITaskTypeEquipmentNeedAccessor
    {
        List<TaskTypeEquipmentNeed> RetrieveTaskTypeEquipmentNeedList();

        int CreateTaskTypeEquipmentNeed(TaskTypeEquipmentNeed taskTypeEquipmentNeed);

        int EditTaskTypeEquipmentNeed(TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed, TaskTypeEquipmentNeed newTaskTypeEquipmentNeed);

        TaskTypeEquipmentNeed RetrieveTaskTypeEquipmentNeedByID(int taskTypeEquipmentNeedID);

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// For deleting a TaskTypeEquipmentNeed
        /// </summary>
        /// <param name="taskTypeEquipmentNeedID"></param>
        /// <returns></returns>
        int DeleteTaskTypeEquipmentNeedByID(int taskTypeEquipmentNeedID);
    }
}
