using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Public interface for handling access to TaskType data
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/03/23
    /// </remarks>
    public interface ITaskTypeAccessor
    {
        /// <summary>
        /// Retrieves a TaskType by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A taskType from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        TaskType RetrieveTaskTypeByName(string name);

        /// <summary>
        /// Retrieves a TaskType from the SqlServer crlandscaping database by its ID
        /// </summary>
        /// <returns>A TaskType from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        TaskType RetrieveTaskTypeByID(int id);

        /// <summary>
        /// Retrieves a list of TaskTypes 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of all taskTypes from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        List<TaskType> RetrieveTaskTypeList();

        /// <summary>
        /// Retrieves a List of all active TaskTypes 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of TaskTypes from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        List<TaskType> RetrieveTaskTypeListByActive();

        /// <summary>
        /// Retrieves a List of TaskTypes with the same JobLocationAttributeTypeID
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of TaskTypes from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        List<string> RetrieveJobLocationAttributeTypeList();

        /// <summary>
        /// Sends data to edit an existing taskType in the database by ID
        /// </summary>
        /// <param name="OldTaskType">The TaskType being edited</param>
        /// <param name="NewTaskType">The TaskType with the new data</param>
        /// <returns>True if the update succeeded, False if it failed.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        bool EditTaskTypeByID(TaskType OldTaskType, TaskType NewTaskType);

        /// <summary>
        /// Sends data to create a new TaskType in the database
        /// </summary>
        /// <param name="taskType">The TaskType being added to the database</param>
        /// <returns>True if TaskType creation is successful, False if unsuccessful. </returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/23
        /// </remarks>
        bool CreateTaskType(TaskType taskType);

        /// <summary>
        /// Sends data to create a new TaskType in the database
        /// </summary>
        /// <param name="taskTypeDetail">The TaskTypeDetail being added to the database</param>
        /// <returns>True if TaskType creation is successful, False if unsuccessful. </returns>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/25
        /// </remarks>
        bool CreateTaskTypeDetail(TaskTypeDetail taskTypeDetail);

        /// <summary>
        /// Deactivates the TaskType
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/23
        /// </remarks>
        /// <param name="taskType"></param>
        /// <returns>True if deactivation is successful, False if unsuccessful.</returns>
        bool DeactivateTaskType(TaskType taskType);

        /// <summary>
        /// Deletes a TaskType by ID
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/23
        /// </remarks>
        /// <param name="taskTypeID"></param>
        /// <returns>True if delete is successful, False if unsuccessful.</returns>
        bool DeleteTaskTypeByID(int taskTypeID);

        /// <summary>
        /// Retrieves a list of TaskTypeDetail objects
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/03/25
        /// </remarks>        
        /// <returns>a list of TaskTypeDetail objects</returns>
        List<TaskTypeDetail> RetrieveTaskTypeDetailList();
    }
}
