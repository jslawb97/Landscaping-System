using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Manager class for TaskEmployee records
    /// </summary>
    public class TaskEmployeeManager : ITaskEmployeeManager
    {
        private ITaskEmployeeAccessor _taskEmployeeAccessor;

        public TaskEmployeeManager()
        {
            _taskEmployeeAccessor = new TaskEmployeeAccessor();
        }

        public TaskEmployeeManager(ITaskEmployeeAccessor taskEmployeeAccessor)
        {
            _taskEmployeeAccessor = taskEmployeeAccessor;
        }

        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Gets a detail list representing TaskEmployee records for a given Job id
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskEmployeeDetail> RetrieveTaskEmployeeDetailByJobID(int jobID)
        {
            List<TaskEmployeeDetail> detail = null;

            try
            {
                detail = _taskEmployeeAccessor.RetrieveTaskEmployeeDetailByJobID(jobID);
                if(detail == null)
                {
                    throw new ApplicationException("The detail list returned null!");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return detail;
        }

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/04/05
        /// 
        /// Removes a TaskEmployee records from the database
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>

        public int RemoveTaskEmployeeByTaskTypeEmployeeNeedId(int taskID)
        {
            int result = 0;

            try
            {
                result = _taskEmployeeAccessor.RemoveTaskEmployeeByTaskTypeEmployeeNeedId(taskID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Creates an assignment for a employee to a task.
        /// </summary>
        /// <returns></returns>
        public bool CreateEmployeeTaskAssignment(int employeeID, int jobID, int taskTypeEmployeeNeedID)
        {
            var result = false;

            try
            {
                result = (_taskEmployeeAccessor.CreateEmployeeTaskAssignment(employeeID, jobID, taskTypeEmployeeNeedID));
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Deletes an assignment for a employee to a task.
        /// </summary>
        /// <returns></returns>
        public bool DeleteEmployeeTaskAssignment(int employeeID, int jobID)
        {
            var result = false;

            try
            {
                result = (_taskEmployeeAccessor.DeleteEmployeeTaskAssignment(employeeID, jobID));
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public List<Employee> RetrieveEmployeeListByTaskTypeEmployeeNeedID(int taskTypeEmployeeNeedID, int jobID)
        {
            List<Employee> employeeList = null;

            try
            {
                employeeList = _taskEmployeeAccessor.RetrieveEmployeeListByTaskTypeEmployeeNeedID(taskTypeEmployeeNeedID, jobID);
                if(employeeList == null)
                {
                    throw new ApplicationException("Assigned Employee list returned null");
                }
            }
            catch (Exception)
            {

                throw;
            }



            return employeeList;
        }

        public bool UpdateEmployeeID(int taskEmployeeID, int employeeID)
        {
            var result = true;

            try
            {
                int updateResult = _taskEmployeeAccessor.UpdateEmployeeID(taskEmployeeID, employeeID);

                if(updateResult != 1)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool UpdateEmployeeIDToNull(int taskEmployeeID)
        {
            var result = true;

            try
            {
                int updateResult = _taskEmployeeAccessor.UpdateEmployeeIDToNull(taskEmployeeID);

                if (updateResult != 1)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
