using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class TaskTypeSupplyNeedAccessorMock : ITaskTypeSupplyNeedAccessor
    {
        private List<TaskTypeSupplyNeed> _taskSupplyList = new List<TaskTypeSupplyNeed>();

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Mock constructor to add data to the task type supply need item list
        /// </summary>
        public TaskTypeSupplyNeedAccessorMock()
        {
            _taskSupplyList.Add(new TaskTypeSupplyNeed()
            {
                TaskTypeSupplyNeedID = Constants.IDSTARTVALUE,
                TaskTypeID = Constants.IDSTARTVALUE,
                SupplyItemID = Constants.IDSTARTVALUE,
                Quantity = 20,
                Active = true
            });
            _taskSupplyList.Add(new TaskTypeSupplyNeed()
            {
                TaskTypeSupplyNeedID = Constants.IDSTARTVALUE + 1,
                TaskTypeID = Constants.IDSTARTVALUE + 1,
                SupplyItemID = Constants.IDSTARTVALUE + 1,
                Quantity = 12,
                Active = true
            });
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Method to add mock task type supply need items 
        /// </summary>
        /// <param name="taskSupply"></param>
        /// <returns></returns>
        public int CreateTaskTypeSupplyNeed(TaskTypeSupplyNeed taskSupply)
        {
            int result = 0;

            _taskSupplyList.Add(taskSupply);

            if (_taskSupplyList.Contains(taskSupply))
            {
                result = 1;
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Method to deactivate a mock task type supply need item 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="supplyItemID"></param>
        /// <returns></returns>
        public int DeactivateTaskTypeSupplyNeedByID(int taskTypeSupplyNeedID)
        {
            int result = 0;

            bool existed = _taskSupplyList.Exists(o => o.TaskTypeSupplyNeedID == taskTypeSupplyNeedID);

            if (existed == true)
            {
                int index = _taskSupplyList.IndexOf(_taskSupplyList.Find(o => o.TaskTypeSupplyNeedID == taskTypeSupplyNeedID));
                _taskSupplyList[index].Active = false; ;
                if (_taskSupplyList[index].Active == false)
                {
                    result = 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Method to edit a mock task type supply need item
        /// </summary>
        /// <param name="oldTaskSupply"></param>
        /// <param name="newTaskSupply"></param>
        /// <returns></returns>
        public int EditTaskTypeSupplyNeed(TaskTypeSupplyNeed oldTaskSupply, TaskTypeSupplyNeed newTaskSupply)
        {
            int result = 0;

            bool existed = _taskSupplyList.Exists(o => o.SupplyItemID == oldTaskSupply.SupplyItemID && o.TaskTypeID == oldTaskSupply.TaskTypeID);

            if (existed == true)
            {
                int index = _taskSupplyList.IndexOf(_taskSupplyList.Find(o => o.SupplyItemID == oldTaskSupply.SupplyItemID && o.TaskTypeID == oldTaskSupply.TaskTypeID));
                _taskSupplyList[index] = newTaskSupply;
                if (_taskSupplyList[index].Equals(newTaskSupply))
                {
                    result = 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Method to retrieve a list of mock task type supply need items
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeSupplyNeed> RetrieveTaskTypeSupplyNeedList()
        {
            return _taskSupplyList;
        }
    }
}
