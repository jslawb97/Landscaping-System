using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ITaskTypeSupplyNeedAccessor
    {
        int CreateTaskTypeSupplyNeed(TaskTypeSupplyNeed taskSupply);

        List<TaskTypeSupplyNeed> RetrieveTaskTypeSupplyNeedList();

        int EditTaskTypeSupplyNeed(TaskTypeSupplyNeed oldTaskSupply, TaskTypeSupplyNeed newTaskSupply);

        int DeactivateTaskTypeSupplyNeedByID(int taskTypeSupplyNeedID);

    }
}
