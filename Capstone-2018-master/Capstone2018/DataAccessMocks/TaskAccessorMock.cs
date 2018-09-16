using System;
using System.Collections.Generic;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class TaskAccessorMock : ITaskAccessor
    {
        private List<DataObjects.Task> _taskList = new List<DataObjects.Task>();

        /// <summary>
        /// John Miller
        /// Created 2018/03/07
        /// 
        /// Mock constructor to add data to the task list
        /// </summary>
        public TaskAccessorMock()
        {
            _taskList.Add(new DataObjects.Task()
            {
                TaskID = 1000000,
                ServiceItemID = 1000000,
                TaskTypeID = 1000000,
                Name = "Mock Task 1",
                Description = "This is a mock task data object for task 1.",
                Active = true
            });
            _taskList.Add(new DataObjects.Task()
            {
                TaskID = 1000001,
                ServiceItemID = 1000001,
                TaskTypeID = 1000001,
                Name = "Mock Task 2",
                Description = "This is a mock task data object for task 2.",
                Active = true
            });
            _taskList.Add(new DataObjects.Task()
            {
                TaskID = 1000002,
                ServiceItemID = 1000002,
                TaskTypeID = 1000002,
                Name = "Mock Task 3",
                Description = "This is a mock task data object for task 3.",
                Active = true
            });
        }

        /// <summary>
        /// Created by John Miller
        /// Last updated 2018/03/07
        /// 
        /// Gets all Tasks
        /// </summary>
        public List<DataObjects.Task> RetrieveTaskList()
        {
            return this._taskList;
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/03/07
        /// Last Updated 2018/03/07
        /// 
        /// Edits a mock Task object.
        /// </summary>
        /// <param name="oldTask"></param>
        /// <param name="newTask"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool EditTask(DataObjects.Task oldTask, DataObjects.Task newTask)
        {
            var found = 0;

            this._taskList.ForEach(taskList =>
            {
                if (taskList == oldTask)
                {
                    taskList.Name = newTask.Name;
                    taskList.ServiceItemID = newTask.ServiceItemID;
                    taskList.TaskTypeID = newTask.TaskTypeID;
                    taskList.Description = newTask.Description;
                    found = 1;
                }
            });
            if (found == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/03/07
        /// </summary>
        /// <returns>true if successful, false if unsuccessful</returns>
        public List<DataObjects.Task> RetrieveTaskListByActive()
        {
            List<DataObjects.Task> activeTasks = new List<DataObjects.Task>();
            foreach (var task in _taskList)
            {
                if (task.Active == true)
                {
                    activeTasks.Add(task);
                }
            }
            return activeTasks;
        }

        /// <summary>
        /// /// Created by John Miller
        /// 2018/02/23
        /// Last Updated 2018/03/02
        /// 
        /// Adds a new Vendor item to the list 
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool CreateTask(DataObjects.Task newTask)
        {
            try
            {
                this._taskList.Add(newTask);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataObjects.Task RetrieveTaskByID(int id)
        {
            return this._taskList.Find(taskList => taskList.TaskID.Equals(id));
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/03/07
        /// 
        /// Deactivates a Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool DeactivateTaskByID(int id)
        {
            try
            {
                RetrieveTaskByID(id).Equals(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataObjects.Task RetrieveTaskByName(string name)
        {
            DataObjects.Task task = new DataObjects.Task();
            foreach (var taskByName in _taskList)
            {
                if (task.Name == name)
                {
                    task.Equals(taskByName);
                }
            }
            return task;
        }

        public List<DataObjects.Task> RetrieveTaskByServiceItemID(int serviceItemId)
        {
            foreach (var task in _taskList)
            {
                if (task.Active == true)
                {
                    _taskList.Add(task);
                }
            }
            return _taskList;
        }



        public List<DataObjects.Task> RetrieveTaskByTaskTypeID(int taskTypeId)
        {
            throw new NotImplementedException();
        }


    }
}
