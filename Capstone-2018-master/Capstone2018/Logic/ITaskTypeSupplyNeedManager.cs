using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ITaskTypeSupplyNeedManager
    {
        int AddTaskTypeSupplyNeedItem(TaskTypeSupplyNeed taskSupply);

        List<TaskTypeSupplyNeed> RetrieveTaskTypeSupplyNeedList();

        int EditTaskTypeSupplyNeedItem(TaskTypeSupplyNeed oldTaskSupply, TaskTypeSupplyNeed newTaskSupply);

        int DeactivateTaskTypeSupplyNeedItem(int taskTypeSupplyNeedID);
    }
}
