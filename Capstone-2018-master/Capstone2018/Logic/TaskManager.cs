using System;
using System.Collections.Generic;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class TaskManager : ITaskManager
    {
        private ITaskAccessor _taskAccessor;

        /// <summary>
        /// Manager Constructor for handling accessor dependency
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public TaskManager()
        {
            _taskAccessor = new TaskAccessor();
        }

        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/05
        /// </remarks>
        /// <param name="taskAccessor"></param>
        public TaskManager(ITaskAccessor taskAccessor)
        {
            _taskAccessor = taskAccessor;
        }

        /// <summary>
        /// Retrieves a list of Task objects from TaskAccessor class
        /// </summary>
        /// <returns>A list of Task objects</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public List<Task> RetrieveTaskList()
        {
            var tasks = new List<DataObjects.Task>();

            try
            {
                tasks = _taskAccessor.RetrieveTaskList();
            }
            catch (Exception)
            {
                throw;
            }
            return tasks;
        }

        /// <summary>
        /// Retrieves a Task by its ID from the TaskAccessor class 
        /// </summary>
        /// <returns>A Task object with the given id</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public DataObjects.Task RetrieveTaskByID(int id)
        {
            var task = new DataObjects.Task();

            try
            {
                task = _taskAccessor.RetrieveTaskByID(id);
            }
            catch (Exception)
            {
                throw;
            }
            return task;
        }
        
        /// <summary>
        /// Calls the accessor to add a Task to the database
        /// </summary>
        /// <param name="task">The task to be added</param>
        /// <returns>True if successful, false otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public bool CreateTask(Task task)
        {
            var result = false;
            try
            {
                result = _taskAccessor.CreateTask(task);

            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Edits an existing Vendor with new Vendor data
        /// </summary>
        /// <param name="oldVendor">The vendor being edited</param>
        /// <param name="newVendor">The vendor with the new data</param>
        /// <returns>True if the edit is successful, and False if it is not</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public bool EditTask(Task oldTask, Task newTask)
        {
            var result = false;

            try
            {
                result = _taskAccessor.EditTask(oldTask, newTask);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Deactivates a Task in the database
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>True if successful, False if unsuccessful</returns>
        ///<remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        public bool DeactivateTaskByID(int taskID)
        {
            var result = false;

            try
            {
                result = _taskAccessor.DeactivateTaskByID(taskID);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

    }
}
