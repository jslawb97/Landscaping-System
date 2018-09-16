using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    /// <summary>
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Accessor class for TaskEmployee records with a SqlServer database
    /// </summary>
    public class TaskEmployeeAccessor : ITaskEmployeeAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Gets a list of TaskEmployee detail records from the database
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskEmployeeDetail> RetrieveTaskEmployeeDetailByJobID(int jobID)
        {
            var detail = new List<TaskEmployeeDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_get_task_employees_by_job_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobID", jobID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {//reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2)
                        var taskEmployee = new TaskEmployeeDetail()
                        {
                            TaskName = reader.GetString(0),
                            EmployeeID = reader.IsDBNull(1) ? null : (int?)(reader.GetInt32(1)),
                            EmployeeFirstName = reader.IsDBNull(2) ? "Not Assigned" : reader.GetString(2),
                            EmployeeLastName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            TaskEmployeeID = reader.GetInt32(4)
                        };
                        detail.Add(taskEmployee);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data.", ex);

            }
            finally
            {
                conn.Close();
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
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_remove_taskemployee_by_tasktypeemployeeneed_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TaskTypeEmployeeNeedID", SqlDbType.Int);
            cmd.Parameters["@TaskTypeEmployeeNeedID"].Value = taskID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem removing the taskEmployee", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Creates an assignment for a employee to a task.
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>

        public bool CreateEmployeeTaskAssignment(int employeeID, int jobID, int taskTypeEmployeeNeedID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_taskemployee_assignment";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@JobID", jobID);
            cmd.Parameters.AddWithValue("@TaskTypeEmployeeNeedID", taskTypeEmployeeNeedID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Deletes an assignment for a employee to a task.
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool DeleteEmployeeTaskAssignment(int employeeID, int jobID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_taskemployee_assignment";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@JobID", jobID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/04/12
        /// 
        /// Gets a list of employees using a given jobid and taskTypeEmployeeNeedID
        /// </summary>
        /// <param name="taskTypeEmployeeNeedID"></param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeListByTaskTypeEmployeeNeedID(int taskTypeEmployeeNeedID, int jobID)
        {
            var employeeList = new List<Employee>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employee_list_tasktypeemployeeneedid_and_jobid";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskTypeEmployeeNeedID", taskTypeEmployeeNeedID);
            cmd.Parameters.AddWithValue("@JobID", jobID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new Employee()
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Email = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };
                        employeeList.Add(employee);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data.", ex);

            }
            finally
            {
                conn.Close();
            }

            return employeeList;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/04/26
        /// </summary>
        /// <param name="taskEmployeeID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int UpdateEmployeeID(int taskEmployeeID, int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_taskemployee_employee_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@TaskEmployeeID", taskEmployeeID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/04/26
        /// </summary>
        /// <param name="taskEmployeeID"></param>
        /// <returns></returns>
        public int UpdateEmployeeIDToNull(int taskEmployeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_taskemployee_employee_id_null";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@TaskEmployeeID", taskEmployeeID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
