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
    /// Created 2018/03/27
    /// 
    /// Interface for the TaskTypeEquipmentNeed Manager class
    /// </summary>
    public interface ITaskTypeEquipmentNeedManager
    {
        int CreateTaskTypeEquipmentNeed(TaskTypeEquipmentNeed taskTypeEquipmentNeed);

        int EditTaskTypeEquipmentNeed(TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed, TaskTypeEquipmentNeed newTaskTypeEquipmentNeed);

        TaskTypeEquipmentNeedDetail RetrieveTaskTypeEquipmentNeedDetail(TaskTypeEquipmentNeed taskTypeEquipmentNeed);

        List<TaskTypeEquipmentNeed> RetrieveTaskTypeEquipmentNeedList();

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// For deleting a TaskTypeEquipmentNeed
        /// </summary>
        /// <param name="taskTypeEquipmentNeedID"></param>
        /// <returns></returns>
        int DeleteTaskTypeEquipmentNeedItem(int taskTypeEquipmentNeedID);
    }
}
