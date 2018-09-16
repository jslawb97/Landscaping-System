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
    /// Zachary Hall
    /// Created 2018/03/29
    /// 
    /// Class for managing TaskTypeEmployeeNeed records
    /// </summary>
    public class TaskTypeEmployeeNeedManager : ITaskTypeEmployeeNeedManager
    {
        private ITaskTypeEmployeeNeedAccessor _taskTypeEmployeeNeedAccessor;

        public TaskTypeEmployeeNeedManager()
        {
            _taskTypeEmployeeNeedAccessor = new TaskTypeEmployeeNeedAccessor();
        }

        public TaskTypeEmployeeNeedManager(ITaskTypeEmployeeNeedAccessor taskTypeEmployeeNeedAccessor)
        {
            _taskTypeEmployeeNeedAccessor = taskTypeEmployeeNeedAccessor;
        }

        private void validateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed need)
        {
            if(need == null)
            {
                throw new ApplicationException("The TaskTypeEmployeeNeed object was null");
            }

            if(need.TaskTypeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("The TaskTypeEmployeeNeed object's TaskTypeID field is invalid");
            }

            if(need.HoursOfWork <= 0)
            {
                throw new ApplicationException("The hours must be greater than zero");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Adds a TaskTypeEmployeeNeedRecord
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        public int CreateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed need)
        {
            int rowsAffected = 0;
            try
            {
                validateTaskTypeEmployeeNeed(need);
                rowsAffected = _taskTypeEmployeeNeedAccessor.CreateTaskTypeEmployeeNeed(need);
                if(rowsAffected == 0)
                {
                    throw new ApplicationException("DB ERROR: No new records were added");
                }
                if(rowsAffected > 1)
                {
                    throw new ApplicationException("DB ERROR: More than one new records were added.\nShould only be one");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Deactivate a TaskTypeEmployeeNeedRecord
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeactivateTaskTypeEmployeeNeedByID(int id)
        {
            int rowsAffected = 0;
            try
            {
                rowsAffected = _taskTypeEmployeeNeedAccessor.DeactivateTaskTypeEmployeeNeedByID(id);
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("DB ERROR: No records were deactivated");
                }
                if (rowsAffected > 1)
                {
                    throw new ApplicationException("DB ERROR: More than one record was deactivated.\nShould only be one");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Gets a list of TaskTypeEmployeeNeed records
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeEmployeeNeedDetail> RetrieveTaskTypeEmployeeDetailList()
        {
            List<TaskTypeEmployeeNeedDetail> list = null;
            try
            {
                list = _taskTypeEmployeeNeedAccessor.RetrieveTaskTypeEmployeeDetailList();

                if(list == null)
                {
                    throw new ApplicationException("The list is null");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Edits a TaskTypeEmployeeNeedRecord with data from newNeed
        /// </summary>
        /// <param name="oldNeed"></param>
        /// <param name="newNeed"></param>
        /// <returns></returns>
        public int UpdateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed oldNeed, TaskTypeEmployeeNeed newNeed)
        {
            int rowsAffected = 0;
            try
            {
                validateTaskTypeEmployeeNeed(newNeed);
                rowsAffected = _taskTypeEmployeeNeedAccessor.UpdateTaskTypeEmployeeNeed(oldNeed, newNeed);
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("DB ERROR: No records were edited");
                }
                if (rowsAffected > 1)
                {
                    throw new ApplicationException("DB ERROR: More than one records were edited.\nShould only be one");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }
    }
}
