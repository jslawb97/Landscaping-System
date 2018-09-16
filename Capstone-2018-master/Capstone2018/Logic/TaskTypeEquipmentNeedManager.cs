using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/27
    /// 
    /// TaskTypeEquipmentNeed Manager class
    /// </summary>
    public class TaskTypeEquipmentNeedManager : ITaskTypeEquipmentNeedManager
    {
        ITaskTypeEquipmentNeedAccessor _taskTypeEquipmentNeedAccessor;
        ITaskTypeAccessor _taskTypeAccessor;
        IEquipmentTypeAccessor _equipmentTypeAccessor;

        public TaskTypeEquipmentNeedManager()
        {
            this._taskTypeEquipmentNeedAccessor = new TaskTypeEquipmentNeedAccessor();
            this._taskTypeAccessor = new TaskTypeAccessor();
            this._equipmentTypeAccessor = new EquipmentTypeAccessor();
        }

        public TaskTypeEquipmentNeedManager(ITaskTypeEquipmentNeedAccessor taskTypeEquipmentNeedAccessor, ITaskTypeAccessor taskTypeAccessor, IEquipmentTypeAccessor equipmentTypeAccessor)
        {
            this._taskTypeEquipmentNeedAccessor = taskTypeEquipmentNeedAccessor;
            this._taskTypeAccessor = taskTypeAccessor;
            this._equipmentTypeAccessor = equipmentTypeAccessor;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/04
        /// 
        /// Creates a TaskTypeEquipmentNeed record
        /// </summary>
        /// <param name="taskTypeEquipmentNeed"></param>
        /// <returns></returns>
        public int CreateTaskTypeEquipmentNeed(TaskTypeEquipmentNeed taskTypeEquipmentNeed)
        {
            var result = 1;
            validateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);

            try
            {
                result = _taskTypeEquipmentNeedAccessor.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/04
        /// 
        /// Edits a TaskTypeEquipmentNeed record
        /// </summary>
        /// <param name="oldTaskTypeEquipmentNeed"></param>
        /// <param name="newTaskTypeEquipmentNeed"></param>
        /// <returns></returns>
        public int EditTaskTypeEquipmentNeed(TaskTypeEquipmentNeed oldTaskTypeEquipmentNeed, TaskTypeEquipmentNeed newTaskTypeEquipmentNeed)
        {
            var result = 1;
            validateTaskTypeEquipmentNeed(newTaskTypeEquipmentNeed);

            try
            {
                result = _taskTypeEquipmentNeedAccessor.EditTaskTypeEquipmentNeed(oldTaskTypeEquipmentNeed, newTaskTypeEquipmentNeed);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/04
        /// 
        /// Retrieves a TaskTypeEquipmentNeedDetail record
        /// </summary>
        /// <param name="taskTypeEquipmentNeed"></param>
        /// <returns></returns>
        public TaskTypeEquipmentNeedDetail RetrieveTaskTypeEquipmentNeedDetail(TaskTypeEquipmentNeed taskTypeEquipmentNeed)
        {
            TaskTypeEquipmentNeedDetail taskTypeEquipmentNeedDetail = null;
            TaskTypeEquipmentNeed taskTypeEquipmentNeeded = null;
            TaskType taskType = null;
            EquipmentType equipmentType = null;

            try
            {
                taskTypeEquipmentNeeded = _taskTypeEquipmentNeedAccessor.RetrieveTaskTypeEquipmentNeedByID(taskTypeEquipmentNeed.TaskTypeEquipmentNeedID);
                taskType = _taskTypeAccessor.RetrieveTaskTypeByID(taskTypeEquipmentNeed.TaskTypeID);
                equipmentType = _equipmentTypeAccessor.RetrieveEquipmentTypeByID(taskTypeEquipmentNeed.EquipmentTypeID);

                taskTypeEquipmentNeedDetail = new TaskTypeEquipmentNeedDetail()
                {
                    TaskTypeEquipmentNeed = taskTypeEquipmentNeed,
                    TaskType = taskType,
                    EquipmentType = equipmentType
                };
            }
            catch (Exception)
            {
                throw;
            }

            return taskTypeEquipmentNeedDetail;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/04
        /// 
        /// Retrieves a List of TaskTypeEquipmentNeed records
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeEquipmentNeed> RetrieveTaskTypeEquipmentNeedList()
        {
            List<TaskTypeEquipmentNeed> taskTypeEquipmentNeedList = null;

            try
            {
                taskTypeEquipmentNeedList = _taskTypeEquipmentNeedAccessor.RetrieveTaskTypeEquipmentNeedList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to process. Try again later.", ex);
            }
            return taskTypeEquipmentNeedList;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Deletes a TasktypeEquipmentNeed by id
        /// </summary>
        /// <param name="taskTypeEquipmentNeedID"></param>
        /// <returns></returns>
        public int DeleteTaskTypeEquipmentNeedItem(int taskTypeEquipmentNeedID)
        {
            int rowsAffected = 0;
            try
            {
                rowsAffected = _taskTypeEquipmentNeedAccessor.DeleteTaskTypeEquipmentNeedByID(taskTypeEquipmentNeedID);
                if (rowsAffected < 1)
                {
                    throw new ApplicationException("Couldn't delete the TaskTypeEquipmentNeed");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/04
        /// 
        /// Validates all of the fields in TaskTypeEquipmentNeed
        /// </summary>
        /// <param name="need"></param>
        private void validateTaskTypeEquipmentNeed(TaskTypeEquipmentNeed need)
        {
            if (need == null)
            {
                throw new ApplicationException("The TaskTypeEquipmentNeed is null");
            }
            if (need.TaskTypeEquipmentNeedID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("The TaskTypeEquipmentNeedID is invalid");
            }
            if (need.TaskTypeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("The TaskTypeID is invalid");
            }
            if (need.HoursOfWork <= 0)
            {
                throw new ApplicationException("The hours need to be greater than 1");
            }
            if (need.EquipmentTypeID == null || need.EquipmentTypeID == "")
            {
                throw new ApplicationException("The TaskTypeEquipmentNeedID is invalid");
            }
            if (need.TaskTypeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("The TaskTypeID is invalid");
            }
        }
    }
}
