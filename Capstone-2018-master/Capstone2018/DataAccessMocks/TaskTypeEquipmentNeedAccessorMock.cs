using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMocks
{
    public class TaskTypeEquipmentNeedAccessorMock : ITaskTypeEquipmentNeedAccessor
    {
        private List<TaskTypeEquipmentNeed> _taskTypeEquipmentNeedList = new List<TaskTypeEquipmentNeed>();

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Mock constructor to add data to the TaskTypeEquipmentNeed
        /// </summary>
        public TaskTypeEquipmentNeedAccessorMock()
        {
            _taskTypeEquipmentNeedList.Add(new TaskTypeEquipmentNeed
            {
                TaskTypeEquipmentNeedID = 1000009,
                TaskTypeID = 1000001,
                EquipmentTypeID = "equipmentType1",
                HoursOfWork = 1
            });
            _taskTypeEquipmentNeedList.Add(new TaskTypeEquipmentNeed
            {
                TaskTypeEquipmentNeedID = 1000008,
                TaskTypeID = 1000002,
                EquipmentTypeID = "equipmentType2",
                HoursOfWork = 2
            });
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Mock method to create a TaskTypeEquipmentNeed
        /// </summary>
        public int CreateTaskTypeEquipmentNeed(TaskTypeEquipmentNeed taskTypeEquipmentNeed)
        {
            if (taskTypeEquipmentNeed.TaskTypeEquipmentNeedID >= 1000000 &&
                taskTypeEquipmentNeed.TaskTypeID >= 1000000 &&
                taskTypeEquipmentNeed.EquipmentTypeID != null ||
                taskTypeEquipmentNeed.EquipmentTypeID != "" &&
                taskTypeEquipmentNeed.HoursOfWork >= 0)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Mock method to edit a TaskTypeEquipmentNeed
        /// </summary>
        public int EditTaskTypeEquipmentNeed(TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed, TaskTypeEquipmentNeed newTaskTypeEquipmentNeed)
        {
            if (oldTaskTypeEquipmentNeed.TaskTypeEquipmentNeedID >= 1000000 &&
                oldTaskTypeEquipmentNeed.TaskTypeID >= 1000000 &&
                oldTaskTypeEquipmentNeed.EquipmentTypeID != null ||
                oldTaskTypeEquipmentNeed.EquipmentTypeID != "" &&
                oldTaskTypeEquipmentNeed.HoursOfWork >= 0 &&
                newTaskTypeEquipmentNeed.TaskTypeEquipmentNeedID >= 1000000 &&
                newTaskTypeEquipmentNeed.TaskTypeID >= 1000000 &&
                newTaskTypeEquipmentNeed.EquipmentTypeID != null ||
                newTaskTypeEquipmentNeed.EquipmentTypeID != "" &&
                newTaskTypeEquipmentNeed.HoursOfWork >= 0)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Mock method to retrieve a TaskTypeEquipmentNeed
        /// </summary>
        public TaskTypeEquipmentNeed RetrieveTaskTypeEquipmentNeedByID(int taskTypeEquipmentNeedID)
        {
            return this._taskTypeEquipmentNeedList.Find(t => t.TaskTypeEquipmentNeedID.Equals(taskTypeEquipmentNeedID));
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Mock method to retrieve a TaskTypeEquipmentNeed
        /// </summary>
        public List<TaskTypeEquipmentNeed> RetrieveTaskTypeEquipmentNeedList()
        {
            return _taskTypeEquipmentNeedList;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Mock method to deactivate a TaskTypeEquipmentNeed
        /// </summary>
        /// <param name="taskTypeEquipmentNeedID"></param>
        /// <returns></returns>
        public int DeleteTaskTypeEquipmentNeedByID(int taskTypeEquipmentNeedID)
        {
            int result = 0;

            TaskTypeEquipmentNeed found = null;

            foreach (TaskTypeEquipmentNeed record in _taskTypeEquipmentNeedList)
            {
                if (taskTypeEquipmentNeedID == record.TaskTypeEquipmentNeedID)
                {
                    found = record;
                    result++;
                }
            }
            if (result < 1)
            {
                throw new ApplicationException("There was a problem deleting");
            }
            _taskTypeEquipmentNeedList.Remove(found);

            return result;
        }
    }
}
