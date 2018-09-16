using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Manager interface for task employee
    /// </summary>
    public interface ITaskEmployeeManager
    {
        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Gets a list of TaskEmployeeDetail objects by the associated JobID
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
        /// <returns></returns>
        bool CreateEmployeeTaskAssignment(int employeeID, int jobID, int taskTypeEmployeeNeedID);

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Deletes an assignment for a employee to a task.
        /// </summary>
        /// <returns></returns>
        bool DeleteEmployeeTaskAssignment(int employeeID, int jobID);


        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/12
        /// 
        /// Gets a list of employee records by the given taskTypeEmployeeNeedID
        /// </summary>
        /// <param name="taskTypeEmployeeNeedID"></param>
        /// <returns></returns>
        List<Employee> RetrieveEmployeeListByTaskTypeEmployeeNeedID(int taskTypeEmployeeNeedID, int jobID);

        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/26
        /// </summary>
        /// <param name="taskEmployeeID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool UpdateEmployeeID(int taskEmployeeID, int employeeID);
        bool UpdateEmployeeIDToNull(int taskEmployeeID);
    }
}
