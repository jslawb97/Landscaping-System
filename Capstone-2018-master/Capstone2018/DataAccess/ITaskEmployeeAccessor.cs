using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Accessor interface for task employee
    /// </summary>
    public interface ITaskEmployeeAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Gets a list of TaskEmployee detail records
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        List<TaskEmployeeDetail> RetrieveTaskEmployeeDetailByJobID(int jobID);

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/04/05
        /// 
        /// Removes a TaskEmployee records from the database
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        int RemoveTaskEmployeeByTaskTypeEmployeeNeedId(int taskID);

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Creates an assignment for a employee to a task.
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        bool CreateEmployeeTaskAssignment(int employeeID, int jobID, int taskTypeEmployeeNeedID);
        
        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Deletes an assignment for a employee to a task.
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        bool DeleteEmployeeTaskAssignment(int employeeID, int jobID);


        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/12
        /// 
        /// Gets a list of employees assigned to a job for a given task type employee need id
        /// </summary>
        /// <param name="taskTypeEmployeeNeedID"></param>
        /// <returns></returns>
        List<Employee> RetrieveEmployeeListByTaskTypeEmployeeNeedID(int taskTypeEmployeeNeedID, int jobID);

        /// <summary>
        /// Updates the employee id field of the given task employee record
        /// </summary>
        /// <param name="taskEmployeeID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        int UpdateEmployeeID(int taskEmployeeID, int employeeID);
        int UpdateEmployeeIDToNull(int taskEmployeeID);
    }
}
