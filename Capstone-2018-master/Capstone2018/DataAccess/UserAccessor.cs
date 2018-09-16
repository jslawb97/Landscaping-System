using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserAccessor : IUserAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created: 2018/1/27
        /// 
        /// Takes the user's email and password and attempts to 
        /// find the user's information within the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns>result: if verified will return 1, if not, 0</returns>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        int IUserAccessor.VerifyUserNameAndPassword(string username, string passwordHash)
        {
            var result = 0;
            
            var conn = DBConnection.GetDBConnection();
            
            var cmdText = @"sp_authenticate_user";
            
            var cmd = new SqlCommand(cmdText, conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);
            
            cmd.Parameters["@Email"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            
            try
            {
                conn.Open();
                result = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/1/27
        /// 
        /// Takes the user's email and finds the user's information 
        /// within the database and builds an employee object from it.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Employee: the current user</returns>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        Employee IUserAccessor.RetrieveEmployeeByUsername(string username)
        {
            Employee employee = null;
            
            var conn = DBConnection.GetDBConnection();
            
            var cmdText = @"sp_retrieve_employee_by_email";
            
            var cmd = new SqlCommand(cmdText, conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            
            cmd.Parameters["@Email"].Value = username;
            
            try
            {
                conn.Open();
                
                var reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    employee = new Employee()
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Address = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5),
                        Active = reader.GetBoolean(6)
                    };
                    if (employee.Active != true)
                    {
                        throw new ApplicationException("Not an active employee.");
                    }
                }
                else
                {
                    throw new ApplicationException("Employee record not found!");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server", ex);
            }
            finally
            {
                conn.Close();
            }

            return employee;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/1/27
        /// 
        /// Takes the user's employee id and attempts to find the
        /// roles that are given to the current user.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>Roles: the amount of roles associated with the current user</returns>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        List<Role> IUserAccessor.RetrieveRolesByEmployeeID(int employeeID)
        {
            List<Role> roles = new List<Role>();
            
            var conn = DBConnection.GetDBConnection();
            
            var cmdText = @"sp_retrieve_employee_roles";
            
            var cmd = new SqlCommand(cmdText, conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            
            cmd.Parameters["@EmployeeID"].Value = employeeID;
            
            try
            {
                conn.Open();
                
                var reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var role = new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1)
                        };
                        roles.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server", ex);
            }
            finally
            {
                conn.Close();
            }


            return roles;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/1/27
        /// 
        /// Takes the current user's employee id, current password, and new 
        /// password and attempts to change the currently associated password
        /// to the newly given password.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="oldPasswordHash"></param>
        /// <param name="newPasswordHash"></param>
        /// <returns>result: if verified will return 1, if not, 0</returns>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        int IUserAccessor.UpdatePasswordHash(int employeeID, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;
            
            var conn = DBConnection.GetDBConnection();
            
            var cmdText = @"sp_update_passwordHash";
            
            var cmd = new SqlCommand(cmdText, conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@oldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newPasswordHash", SqlDbType.NVarChar, 100);
            
            cmd.Parameters["@EmployeeID"].Value = employeeID;
            cmd.Parameters["@oldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@newPasswordHash"].Value = newPasswordHash;
            
            try
            {
                conn.Open();
                
                result = cmd.ExecuteNonQuery();
                
                if (result == 0)
                {
                    throw new ApplicationException("Password update failed.");
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/05/04
        /// 
        /// Method to retrieve a user's active roles using a stored
        /// procedure
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="active"></param>
        /// <returns>User's active roles</returns>
        public List<Role> RetrieveRolesByEmployeeIDAndActive(int employeeID, bool active = true)
        {
            List<Role> roles = new List<Role>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_employee_roles_by_active";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);

            cmd.Parameters["@EmployeeID"].Value = employeeID;
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var role = new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1)
                        };
                        roles.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server", ex);
            }
            finally
            {
                conn.Close();
            }


            return roles;
        }

    }
}
