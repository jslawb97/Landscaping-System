using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class TaskTypeManager : ITaskTypeManager
    {
        ITaskTypeAccessor _taskTypeAccessor;
        IJobLocationAccessor _jobLocationAccessor;

        public TaskTypeManager()
        {
            _taskTypeAccessor = new TaskTypeAccessor();
            _jobLocationAccessor = new JobLocationAccessor();
        }

        public TaskTypeManager(ITaskTypeAccessor taskTypeAccessor)
        {
            _taskTypeAccessor = taskTypeAccessor;
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to Create a TaskType using a TaskTypeDetail 
        /// </summary>
        /// <returns>True if Creation is successful; false otherwise.</returns>
        public bool CreateTaskTypeDetail(TaskTypeDetail taskTypeDetail)
        {
            try
            {
                return (_taskTypeAccessor.CreateTaskTypeDetail(taskTypeDetail));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to Create a TaskType 
        /// </summary>
        /// <returns>True if Creation is successful; false otherwise.</returns>
        public bool CreateTaskType(TaskType taskType)
        {
            try
            {
                return (_taskTypeAccessor.CreateTaskType(taskType));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to deactivate a TaskType
        /// </summary>
        /// <returns>True if deactivation is successful; false otherwise.</returns>
        public bool DeactivateTaskType(TaskType taskType)
        {
            try
            {
                return _taskTypeAccessor.DeactivateTaskType(taskType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to delete a TaskType by ID
        /// </summary>
        /// <returns>True if delete is successful; false otherwise.</returns>
        public bool DeleteTaskTypeByID(int taskTypeID)
        {
            bool result = false;
            try
            {
                result = _taskTypeAccessor.DeleteTaskTypeByID(taskTypeID);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to edit TaskType 
        /// </summary>
        /// <returns>True if edit is successful; false otherwise.</returns>
        public bool EditTaskTypeByID(TaskType oldTaskType, TaskType newTaskType)
        {
            try
            {
                return _taskTypeAccessor.EditTaskTypeByID(oldTaskType, newTaskType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return TaskType by ID
        /// </summary>
        /// <returns>A TaskType</returns>
        public TaskType RetrieveTaskTypeByID(int id)
        {
            try
            {
                return _taskTypeAccessor.RetrieveTaskTypeByID(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return TaskType by Name
        /// </summary>
        /// <returns>A TaskType</returns>
        public TaskType RetrieveTaskTypeByName(string name)
        {
            try
            {
                return _taskTypeAccessor.RetrieveTaskTypeByName(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return a list of TaskTypes
        /// </summary>
        /// <returns>A list of TaskTypes</returns>
        public List<TaskType> RetrieveTaskTypeList()
        {
            try
            {
                return _taskTypeAccessor.RetrieveTaskTypeList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return a list of TaskTypes by Active
        /// </summary>
        /// <returns>A list of Active TaskTypes</returns>
        public List<TaskType> RetrieveTaskTypeListByActive()
        {
            try
            {
                return _taskTypeAccessor.RetrieveTaskTypeListByActive();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return A List of JobLocationAttributeIDs 
        /// </summary>
        /// <returns>A List of JobLocationAttributeIDs</returns>
        public List<string> RetrieveJobLocationAttributeTypeList()
        {
            try
            {
                return _taskTypeAccessor.RetrieveJobLocationAttributeTypeList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TaskTypeDetail> RetrieveTaskTypeDetailList()
        {
            try
            {
                return _taskTypeAccessor.RetrieveTaskTypeDetailList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
