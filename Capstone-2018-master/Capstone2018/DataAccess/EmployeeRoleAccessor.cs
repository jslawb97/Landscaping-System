using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DataAccess
{
    public class EmployeeRoleAccessor : IEmployeeRoleAccessor
    {

        /// <summary>
        /// Deactivates the EmployeeRole using an EmployeeRole Detail Object
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/04/01
        /// </remarks>
        /// <param name="employeeRoleDetail"></param>
        /// <returns>True if deactivation is successful, False if unsuccessful.</returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        /// QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18 </remarks>
        public bool DeactivateEmployeeRole(EmployeeRoleDetail employeeRoleDetail)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_employeerole";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeRoleDetail.Employee.EmployeeID);
            cmd.Parameters.AddWithValue("@RoleID", employeeRoleDetail.EmployeeRole.RoleID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the EmployeeRole.", ex);
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
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public int DeactivateEmployeeRoleById(int employeeId, string roleId)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employee_role_by_employee_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar);

            cmd.Parameters["@EmployeeID"].Value = employeeId;
            cmd.Parameters["@RoldID"].Value = roleId;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the EmployeeCertification", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;

        }

        /// <summary>
        /// Retrieves a list of EmployeeRoleDetail objects
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of EmployeeRoleDetail objects from the sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/04/01
        /// </remarks>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public List<EmployeeRoleDetail> RetrieveEmployeeRoleDetailList()
        {
            List<EmployeeRoleDetail> list = new List<EmployeeRoleDetail>();
            EmployeeRoleDetail employeeRoleDetail = new EmployeeRoleDetail();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_corrected_employeerole_detail_list";
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
                            LastName = reader.GetString(2)
                        };

                        var employeeRole = new EmployeeRole()
                        {
                            RoleID = reader.GetString(3),
                            Active = reader.GetBoolean(4)
                        };
                        employeeRoleDetail = new EmployeeRoleDetail()
                        {
                            Employee = employee,
                            EmployeeRole = employeeRole
                        };
                        list.Add(employeeRoleDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                  throw new ApplicationException("There was a problem retrieving the EmployeeRole list", ex);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        /// <summary>
        /// Sends data to create a new EmployeeRole in the database
        /// </summary>
        /// <param name="employeeRole">The EmployeeRole being added to the database</param>
        /// <returns>True if successful, False if unsuccessful</returns>
        /// <remarks>
        /// John Miller
        /// 2018/04/01
        /// </remarks>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public int AddEmployeeRole(Employee employee, Role role)
        {
            var result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_new_employeerole";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
            cmd.Parameters.AddWithValue("@RoleID", role.RoleID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem adding the EmployeeRole.", ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldEmployeeRoleDetail"></param>
        /// <param name="newEmployeeRoleDetail"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public int EditEmployeeRoleDetail(EmployeeRoleDetail oldEmployeeRoleDetail, EmployeeRoleDetail newEmployeeRoleDetail)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_employeerole_by_employeeid_V2";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", oldEmployeeRoleDetail.Employee.EmployeeID);
            cmd.Parameters.AddWithValue("@NewRoleID", newEmployeeRoleDetail.EmployeeRole.RoleID);
            cmd.Parameters.AddWithValue("@OldRoleID", oldEmployeeRoleDetail.EmployeeRole.RoleID);
            cmd.Parameters.AddWithValue("@OldActive", oldEmployeeRoleDetail.EmployeeRole.Active);
            cmd.Parameters.AddWithValue("@NewActive", newEmployeeRoleDetail.EmployeeRole.Active);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing the EmployeeRole.\nEmployees cannot be assigned duplicate roles.", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public List<EmployeeRole> RetrieveEmployeeRoleList()
        {
            // EmployeeRole employeeRole = null;
            List<EmployeeRole> EmployeeRoleList = new List<EmployeeRole>();
            // connection
            var conn = DBConnection.GetDBConnection();
            // cmdText
            var cmdText = @"sp_retrieve_employeerole_list";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            //CommandType
            // cmd.Parameters.Add("@EMPLOYEE_ID", SqlDbType.Int);
            cmd.CommandType = CommandType.StoredProcedure;
            // parameteres and valuses
            // cmd.Parameters["@EMPLOYEE_ID"].Value = employeeId;
            //  cmd.Parameters.AddWithValue("@EmployeeID",employeeId);

            // Try-catch
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                var reader = cmd.ExecuteReader();
                // check for rows

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var employeeRole = new EmployeeRole()
                        {
                            // SELECT 
                            EmployeeId = reader.GetInt32(0),
                            RoleID = reader.GetString(1)

                        };
                        EmployeeRoleList.Add(employeeRole);

                    }
                }
                else
                {
                    throw new ApplicationException("No data found");
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving the employeeRole list.", ex);
            }
            finally
            {
                conn.Close();
            }
            return EmployeeRoleList;

        }

      
    }

}