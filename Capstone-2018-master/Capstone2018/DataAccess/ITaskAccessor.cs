using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Public interface for handling access to Task data
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/03/05
    /// </remarks>
    public interface ITaskAccessor
    {
        /// <summary>
        /// Retrieves a Task object from the SqlServer crlandscaping database by its TaskID
        /// </summary>
        /// <returns>A Task from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        DataObjects.Task RetrieveTaskByID(int id);

        /// <summary>
        /// Retrieves a list of Task objects from the SqlServer crlandscaping database
        /// </summary>
        /// <returns>A list of Tasks from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        List<DataObjects.Task> RetrieveTaskList();

        /// <summary>
        /// Sends data to edit an existing task in the database by TaskID
        /// </summary>
        /// <param name="OldTask">The Task being edited</param>
        /// <param name="NewTask">The Task with the new data</param>
        /// <returns>True if the update succeeded, False if it failed.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        bool EditTask(DataObjects.Task OldTask, DataObjects.Task NewTask);

        /// <summary>
        /// Sends data to create a new Task in the database
        /// </summary>
        /// <param name="task">The Task being added to the database</param>
        /// <returns>True if Task creation is successful, False if unsuccessful. </returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/05
        /// </remarks>
        bool CreateTask(DataObjects.Task task);

        /// <summary>
        /// Deactivates the Task with the given taskID
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/05
        /// </remarks>
        /// <param name="taskID"></param>
        /// <returns>True if deactivation is successful, False if unsuccessful.</returns>
        bool DeactivateTaskByID(int taskID);
    }
}
