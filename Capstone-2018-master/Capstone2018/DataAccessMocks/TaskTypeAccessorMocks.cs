using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    /// <summary>
    /// John Miller
    /// Created 2018/03/25
    /// 
    /// Accessor Mock for TaskType
    /// </summary>
    public class TaskTypeAccessorMock : ITaskTypeAccessor
    {
        private List<TaskType> _taskTypes;
        private List<TaskTypeDetail> _taskTypeDetails;

        public TaskTypeAccessorMock()
        {
            _taskTypes = new List<TaskType>();
            _taskTypeDetails = new List<TaskTypeDetail>();

            _taskTypeDetails.Add(new TaskTypeDetail
            {
                TaskType = new TaskType
                {
                    TaskTypeID = 1000000,
                    Name = "Mow",
                    Quantity = 1,
                    JobLocationAttributeTypeID = "Acres to Mow",
                    Active = true
                }
            });

            _taskTypeDetails.Add(new TaskTypeDetail
            {
                TaskType = new TaskType
                {


                    TaskTypeID = 1000002,
                    Name = "Trim Trees",
                    Quantity = 3,
                    JobLocationAttributeTypeID = "Trees to Trim",
                    Active = true
                }
            });

            _taskTypes.Add(_taskTypeDetails.ElementAt(0).TaskType);
            _taskTypes.Add(new TaskType
            {
                TaskTypeID = 1000001,
                Name = "Trim Trees",
                Quantity = 5,
                JobLocationAttributeTypeID = "Trees to Trim",
                Active = true
            });
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Creates a new TaskType 
        /// </summary>
        /// <param name="OldTaskType"></param>
        /// <param name="newTaskType"></param>
        /// <returns>true if TaskType creation is successfull; false otherwise.</returns>
        public bool CreateTaskTypeDetail(TaskTypeDetail taskTypeDetail)
        {
            int taskTypeId = 0;
            try
            {
                if (_taskTypes == null)
                {
                    throw new ApplicationException("Data store inaccessible.");
                }
                taskTypeId = Constants.IDSTARTVALUE + _taskTypes.Count + 1;
                taskTypeDetail.TaskType.TaskTypeID = taskTypeId;
                _taskTypes.Add(taskTypeDetail.TaskType);
            }
            catch (Exception)
            {
                throw;
            }
            if (taskTypeId == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Deactivates the taskType by ID
        /// </summary>
        /// <param name="OldTaskType"></param>
        /// <param name="newTaskType"></param>
        /// <returns>True if TaskType is deactivated; false otherwise</returns>
        public bool DeactivateTaskType(TaskType taskType)
        {
            int rowsAffected = 0;

            try
            {
                foreach (var item in _taskTypes)
                {
                    if (item.TaskTypeID == taskType.TaskTypeID)
                    {
                        item.Active = false;
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            if (rowsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Deletes a TaskType by ID
        /// </summary>
        /// <param name="OldTaskType"></param>
        /// <param name="newTaskType"></param>
        /// <returns>True if Task type is deleted; false otherwise</returns>
        public bool DeleteTaskTypeByID(int taskTypeID)
        {
            {
                int rowsAffected = 0;

                try
                {
                    foreach (var taskType in _taskTypes)
                    {
                        if (taskType.TaskTypeID == taskTypeID)
                        {
                            _taskTypes.Remove(taskType);
                            rowsAffected++;
                        }
                    }
                }
                catch (ApplicationException)
                {
                    throw;
                }
                if (rowsAffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Updates the given TaskType with data from the newTaskType
        /// </summary>
        /// <param name="OldTaskType"></param>
        /// <param name="newTaskType"></param>
        /// <returns></returns>
        public bool EditTaskTypeByID(TaskType OldTaskType, TaskType NewTaskType)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var taskType in _taskTypes)
                {
                    if (taskType.TaskTypeID == OldTaskType.TaskTypeID)
                    {
                        taskType.Name = NewTaskType.Name;
                        taskType.Quantity = NewTaskType.Quantity;
                        taskType.Active = NewTaskType.Active;
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            if (rowsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Gets the TaskType record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskType RetrieveTaskTypeByID(int id)
        {
            try
            {
                foreach (var taskType in _taskTypes)
                {
                    if (taskType.TaskTypeID == id)
                    {
                        return taskType;
                    }
                }
                throw new ApplicationException("TaskType does not exist.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TaskTypeDetail RetrieveTaskTypeDetailByID(int id)
        {
            try
            {
                foreach (var taskTypeDetail in _taskTypeDetails)
                {
                    if (taskTypeDetail.TaskType.TaskTypeID == id)
                    {
                        return taskTypeDetail;
                    }
                }
                throw new ApplicationException("TaskTypeDetail does not exist.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TaskType RetrieveTaskTypeByName(string name)
        {
            try
            {
                foreach (var taskType in _taskTypes)
                {
                    if (taskType.Name == name)
                    {
                        return taskType;
                    }
                }
                throw new ApplicationException("TaskType does not exist.");
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
        /// Gets a list of TaskTypes
        /// </summary>
        /// <returns></returns>
        public List<TaskType> RetrieveTaskTypeList()
        {
            return _taskTypes;
        }

        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Gets a list of TaskTypeDetails
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeDetail> RetrieveTaskTypeDetailList()
        {
            return _taskTypeDetails;
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Gets a list of active TaskTypes
        /// </summary>
        /// <returns></returns>
        public List<TaskType> RetrieveTaskTypeListByActive()
        {
            List<TaskType> taskTypeList = new List<TaskType>();
            try
            {
                foreach (var taskType in _taskTypes)
                {
                    if (taskType.Active == true)
                    {
                        taskTypeList.Add(taskType);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("TaskType does not exist.");
            }
            return taskTypeList;
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Gets a list of TaskTypes with the given JobLocationAttributeTypeID
        /// </summary>
        ///// <returns></returns>
        public List<string> RetrieveJobLocationAttributeTypeList()
        {
            List<string> jobLocationAttributeTypeList = new List<string>();
            try
            {
                foreach (var jobLocationAttributeType in jobLocationAttributeTypeList)
                {
                    jobLocationAttributeTypeList.Add(jobLocationAttributeType);
                }

            }
            catch (Exception)
            {
                throw new ApplicationException("Error Retrieving JobLocationAttributeType List");
            }
            return jobLocationAttributeTypeList;
        }

        public bool CreateTaskType(TaskType taskType)
        {
            int taskTypeId = 0;
            try
            {
                if (_taskTypes == null)
                {
                    throw new ApplicationException("Data store inaccessible.");
                }
                taskTypeId = Constants.IDSTARTVALUE;
                taskType.TaskTypeID = taskTypeId;
                _taskTypes.Add(taskType);
            }
            catch (Exception)
            {
                throw;
            }
            if (taskTypeId == Constants.IDSTARTVALUE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
