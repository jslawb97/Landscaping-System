using System.Collections.Generic;
using DataObjects;

namespace Logic
{
    public interface ITaskTypeManager
    {
        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return a list of TaskTypes
        /// </summary>
        /// <returns>A list of TaskTypes</returns>
        List<TaskType> RetrieveTaskTypeList();

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return TaskType by Name
        /// </summary>
        /// <returns>A TaskType</returns>
        TaskType RetrieveTaskTypeByName(string name);

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return TaskType by ID
        /// </summary>
        /// <returns>A TaskType</returns>
        TaskType RetrieveTaskTypeByID(int id);

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to return a list of TaskTypes by Active
        /// </summary>
        /// <returns>A list of Active TaskTypes</returns>
        List<TaskType> RetrieveTaskTypeListByActive();

        /// <summary>
        /// Retrieves a list of TaskTypeDetail objects
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of TaskTypeDetail objects</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/25
        /// </remarks>
        List<TaskTypeDetail> RetrieveTaskTypeDetailList();

        /// <summary>
        /// A list of JobLocationAttributeTypeIDs from the sql database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of JobLocationAttributeTypeIDs </returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/25
        /// </remarks>
        List<string> RetrieveJobLocationAttributeTypeList();

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to edit TaskType 
        /// </summary>
        /// <returns>True if edit is successful; false otherwise.</returns>
        bool EditTaskTypeByID(TaskType OldTaskType, TaskType NewTaskType);

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to Create a TaskType 
        /// </summary>
        /// <returns>True if Creation is successful; false otherwise.</returns>
        bool CreateTaskType(TaskType taskType);

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to Create a TaskType Using a TaskTypeDetail object
        /// </summary>
        /// <returns>True if Creation is successful; false otherwise.</returns>
        bool CreateTaskTypeDetail(TaskTypeDetail taskTypeDetail);

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to deactivate a TaskType
        /// </summary>
        /// <returns>True if deactivation is successful; false otherwise.</returns>
        bool DeactivateTaskType(TaskType taskType);

        /// <summary>
        /// John Miller
        /// Created on 2018/03/25
        /// 
        /// Method to delete a TaskType by ID
        /// </summary>
        /// <returns>True if delete is successful; false otherwise.</returns>
        bool DeleteTaskTypeByID(int taskTypeID);
    }
}