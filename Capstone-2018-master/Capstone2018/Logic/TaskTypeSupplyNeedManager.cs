using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class TaskTypeSupplyNeedManager : ITaskTypeSupplyNeedManager
    {
        ITaskTypeSupplyNeedAccessor _taskSupplyAccessor;

        // Constructor for real run
        public TaskTypeSupplyNeedManager()
        {
            _taskSupplyAccessor = new TaskTypeSupplyNeedAccessor();
        }

        // Constructor for test run
        public TaskTypeSupplyNeedManager(ITaskTypeSupplyNeedAccessor supplyOrderItemAccessor)
        {
            _taskSupplyAccessor = supplyOrderItemAccessor;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method that adds a new Task Type Supply Need item to the list
        /// </summary>
        /// <param name="taskSupply">the TaskTypeSupplyNeed item to add</param>
        /// <returns></returns>
        public int AddTaskTypeSupplyNeedItem(TaskTypeSupplyNeed taskSupply)
        {
            if (taskSupply.SupplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Item ID Value");
            }
            if (taskSupply.TaskTypeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Task ID Value");
            }
            return _taskSupplyAccessor.CreateTaskTypeSupplyNeed(taskSupply);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method that deactivates a current task type supply need item in the list
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="supplyItemID"></param>
        /// <returns></returns>
        public int DeactivateTaskTypeSupplyNeedItem(int taskTypeSupplyNeedID)
        {
            if (taskTypeSupplyNeedID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Task Type Supply Need ID Value");
            }
            return _taskSupplyAccessor.DeactivateTaskTypeSupplyNeedByID(taskTypeSupplyNeedID);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method that edits an existing task type supply need item in the list
        /// </summary>
        /// <param name="oldTaskSupply"></param>
        /// <param name="newTaskSupply"></param>
        /// <returns></returns>
        public int EditTaskTypeSupplyNeedItem(TaskTypeSupplyNeed oldTaskSupply, TaskTypeSupplyNeed newTaskSupply)
        {
            int result = 0;
            if (oldTaskSupply.SupplyItemID < Constants.IDSTARTVALUE || newTaskSupply.SupplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Item ID Value");
            }
            if (oldTaskSupply.TaskTypeID < Constants.IDSTARTVALUE || newTaskSupply.TaskTypeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Task ID Value");
            }
            if (oldTaskSupply.TaskTypeID != newTaskSupply.TaskTypeID)
            {
                throw new ArgumentOutOfRangeException("Task ID Mismatch");
            }
            if (oldTaskSupply.SupplyItemID != newTaskSupply.SupplyItemID)
            {
                throw new ArgumentOutOfRangeException("Supply Item ID Mismatch");
            }
            result = _taskSupplyAccessor.EditTaskTypeSupplyNeed(oldTaskSupply, newTaskSupply);
            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/29
        /// 
        /// Method to retrieve a list of Task Type Supply Need Items
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeSupplyNeed> RetrieveTaskTypeSupplyNeedList()
        {
            List<TaskTypeSupplyNeed> taskSupplyList = null;

            try
            {
                taskSupplyList = _taskSupplyAccessor.RetrieveTaskTypeSupplyNeedList();
            }
            catch (Exception)
            {
                throw;
            }

            return taskSupplyList;
        }
    }
}
