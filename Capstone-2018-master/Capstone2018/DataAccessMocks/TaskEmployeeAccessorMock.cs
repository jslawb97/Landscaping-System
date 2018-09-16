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
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Mock Accessor for TaskEmployeeAccessor
    /// </summary>
    public class TaskEmployeeAccessorMock : ITaskEmployeeAccessor
    {
        private List<TaskEmployeeDetail> _detail;
        private List<Employee> _employeeList;

         

        public TaskEmployeeAccessorMock()
        {
            _detail = new List<TaskEmployeeDetail>();
            _employeeList = new List<Employee>();
            _detail.Add(new TaskEmployeeDetail {
                TaskName = "Mow the Lawn",
                EmployeeFirstName = "TestFirstName",
                EmployeeLastName = "TestLastName",
                TaskEmployeeID = Constants.IDSTARTVALUE,
                TaskTypeEmployeeNeedID = Constants.IDSTARTVALUE,
                EmployeesAssignedCount = 2


            });
            _detail.Add(new TaskEmployeeDetail
            {
                TaskName = "Prepare Sod",
                EmployeeFirstName = "TestFirstName",
                EmployeeLastName = "TestLastName",
                TaskEmployeeID = Constants.IDSTARTVALUE + 1,
                TaskTypeEmployeeNeedID = Constants.IDSTARTVALUE + 1,
                EmployeesAssignedCount = 2

            });
            _detail.Add(new TaskEmployeeDetail
            {
                TaskName = "Trim Trees",
                EmployeeFirstName = "TestFirstName",
                EmployeeLastName = "TestLastName",
                TaskEmployeeID = Constants.IDSTARTVALUE + 2,
                TaskTypeEmployeeNeedID = Constants.IDSTARTVALUE + 2,
                EmployeesAssignedCount = 2

            });
            _employeeList.Add(new Employee()
            {
                EmployeeID = 1000000,
                FirstName = "Frank",
                LastName = "Atman",
                Address = "123 A St.",
                PhoneNumber = "(123) 456-7890",
                Email = "aperson@somedomain.com",
                Active = true
            });
            _employeeList.Add(new Employee()
            {
                EmployeeID = 1000001,
                FirstName = "Sherlock",
                LastName = "Holmes",
                Address = "221B Baker St.",
                PhoneNumber = "(123) 456-7891",
                Email = "aperson@someotherdomain.com",
                Active = true
            });
            _employeeList.Add(new Employee()
            {
                EmployeeID = 1000002,
                FirstName = "John",
                LastName = "Doe",
                Address = "456 B St.",
                PhoneNumber = "(123) 456-7892",
                Email = "aperson@yetanotherdomain.com",
                Active = false
            });
        }

        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Mocks getting a list of details from a datastore
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskEmployeeDetail> RetrieveTaskEmployeeDetailByJobID(int jobID)
        {
            List<TaskEmployeeDetail> detail = null;
            try
            {
                detail = _detail;
                if(detail == null)
                {
                    throw new ApplicationException("The detail list was null");
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

            TaskEmployeeDetail item = null;

            foreach (TaskEmployeeDetail taskEmployee in _detail)
            {


                if (taskEmployee.TaskTypeEmployeeNeedID == taskID)
                {
                    item = taskEmployee;
                    result++;
                    break;
                }

            }

            if (item != null)
            {
                _detail.Remove(item);
            }
            else
            {
                throw new ApplicationException("Could not find record");
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
            return true;
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
            return true;

        }

        public List<Employee> RetrieveEmployeeListByTaskTypeEmployeeNeedID(int taskTypeEmployeeNeedID, int jobID)
        {
            List<Employee> employeeList = null;

            try
            {
                employeeList = _employeeList;
                if(employeeList == null)
                {
                    throw new ApplicationException("The list returned null");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return employeeList;
        }

        public int UpdateEmployeeID(int taskEmployeeID, int employeeID)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployeeIDToNull(int taskEmployeeID)
        {
            throw new NotImplementedException();
        }
    }
}
