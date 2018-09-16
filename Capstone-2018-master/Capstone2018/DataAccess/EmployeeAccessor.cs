using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccess
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/02/02
        /// 
        /// Method to deactivate an employee by ID
        /// </summary>
        /// <param name="employeeID">The ID of the employee to be deactivated</param>
        /// <exception cref="SQLException">Deactivate fails</exception>
        /// <returns>Employees deactivated</returns>
        public int DeactivateEmployeeByID(int EmployeeID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_employee_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters["@EmployeeID"].Value = EmployeeID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();

                if(rowcount == 0)
                {
                    throw new ApplicationException("Employee deactivation failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the employee", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/01/26
        /// 
        /// Method to create new employees in the database
        /// </summary>
        /// <returns></returns>
        public int CreateEmployee(Employee employee)
        {
            int rowsAffected = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_employee_without_passwordhash";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Active", employee.Active);



            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Sam Dramstad
        /// Created on 2018/01/26
        /// 
        /// Method to create new employees in the database
        /// </summary>
        /// <returns></returns>
        public int CreateEmployeeReturnScopeID(Employee employee)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_employee_without_passwordhash_return_scope_identity";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Active", employee.Active);



            try
            {
                conn.Open(); 
                result = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// Dan Cable
        /// Created on 2018/03/29
        /// 
        /// Method to edit Employees in the database
        /// </summary>
        public bool EditEmployee(Employee oldEmployee, Employee newEmployee)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_employee_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewFirstName", newEmployee.FirstName);
            cmd.Parameters.AddWithValue("@OldFirstName", oldEmployee.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newEmployee.LastName);
            cmd.Parameters.AddWithValue("@OldLastName", oldEmployee.LastName);
            cmd.Parameters.AddWithValue("@NewAddress", newEmployee.Address);
            cmd.Parameters.AddWithValue("@OldAddress", oldEmployee.Address);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newEmployee.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldEmployee.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newEmployee.Email);
            cmd.Parameters.AddWithValue("@OldEmail", oldEmployee.Email);
            cmd.Parameters.AddWithValue("@EmployeeID", oldEmployee.EmployeeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error. ", ex);
            }
            finally
            {
                conn.Close();
            }
            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Employee RetrieveEmployeeByID(int employeeID)
        {
            var employee = new Employee();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employee_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    employee = new Employee()
                    {
                        EmployeeID = employeeID,
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        Address = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                    if (employee.Active != true)
                    {
                        throw new ApplicationException("Employee is not active.");
                    }
                }
                else
                {
                    throw new ApplicationException("Employee record was not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }
            return employee;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/13
        /// 
        /// Method to retrieve all Employees using a stored procedure
        /// </summary>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeList()
        {
            var employeeList = new List<Employee>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employee_list";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

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
                    throw new ApplicationException("No data found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }

            return employeeList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/02
        /// 
        /// Method to retrieve a list of employees by the active field using
        /// a stored procedure
        /// </summary>
        /// <param name="active"></param>
        /// <returns>A list of Employees</returns>
        public List<Employee> RetrieveEmployeeListByActive(bool active = true)
        {
            var employeeList = new List<Employee>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employee_list_by_active";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);

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
                    throw new ApplicationException("No data found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }

            return employeeList;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/17
        /// 
        /// Method to Retrieve a Employee List by Certification and availability
        /// </summary>
        /// <param name="employeeCertification"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeListByCertificationAndAvailability(Certification certification, DateTime? startDate, DateTime? endDate)
        {
            var employeeList = new List<Employee>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employee_list_by_certification_and_schedule";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CertificationID", SqlDbType.Int);
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
            cmd.Parameters["@CertificationID"].Value = certification.CertificationID;
            cmd.Parameters["@StartDate"].Value = startDate;
            cmd.Parameters["@EndDate"].Value = endDate;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var ep = new Employee()
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Email = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };
                        employeeList.Add(ep);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
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
        /// 
        /// Gets a list of employees who are available over a certain date (from job)
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<Employee> RetreiveEmployeeListByJobAvailability(int jobID)
        {
            var employeeList = new List<Employee>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_get_available_employee_for_job";
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
                    {
                        var ep = new Employee()
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        };
                        employeeList.Add(ep);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }
            return employeeList;
        }

		public int RetreiveEmployeeIdByEmail(string email)
		{//sp_retreive_employee_id_by_email
			int result = 0;

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_retreive_employee_id_by_email";
			var cmd = new SqlCommand(cmdText, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Email", email);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					reader.Read();

					result = reader.GetInt32(0);
				}
				
			}
			catch (Exception ex)
			{
				throw new ApplicationException("There was a problem retrieving your data", ex);
			}
			finally
			{
				conn.Close();
			}
			return result;
		}
	}
}
