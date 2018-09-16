using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// Interface for interacting with Task objects.
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Created 2018/03/05
    /// </remarks>
    /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
    public interface ITaskManager
    {
        // List<DataObjects.TaskDetail> RetrieveTaskDetailList(); 

        /// <summary>
        /// John Miller
        /// Created 2018/03/05
        /// Gets a list of Tasks.
        /// </summary>
        /// <returns>a collection of Tasks</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        List<DataObjects.Task> RetrieveTaskList();
        
        /// <summary>
        /// John Miller
        /// Created 2018/03/05
        /// Gets a Task by its TaskID.
        /// </summary>
        /// <returns>a Task</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        DataObjects.Task RetrieveTaskByID(int id);

        /// <summary>
        /// John Miller
        /// Updated 2018/03/05
        /// Adds a Task
        /// </summary>
        /// <param name="task">the Task to be added</param>
        /// <returns>True if Task is successfully added, False otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        bool CreateTask(DataObjects.Task task);

        /// <summary>
        /// John Miller
        /// Updated 2018/03/05
        /// Adds a Task
        /// </summary>
        /// <param name="oldTask">the Task being edited</param>
        /// <param name="newTask">the Task with the new data</param>
        /// <returns>True if Task is successfully edited, False otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        bool EditTask(DataObjects.Task oldTask, DataObjects.Task newTask);

        /// <summary>
        /// John Miller
        /// Updated 2018/03/05
        /// Deactivates a Task by its ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>True if successful, false if unsuccessful</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        bool DeactivateTaskByID(int taskID);
    }
}
