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
    public class RoleAccessor : IRoleAccessor
    {
        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/26
        /// 
        /// Adds Role to the database
        /// </summary>
        int IRoleAccessor.CreateRole(Role role)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleID", role.RoleID);
            cmd.Parameters.AddWithValue("@Description", role.Description);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Role adition failed.");
                }
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
        /// Marshall Sejkora
        /// Created: 2018/01/26
        /// 
        /// Edit Roles in the database
        /// </summary>
        int IRoleAccessor.EditRole(Role oldRole, Role newRole)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleID", oldRole.RoleID);
            cmd.Parameters.AddWithValue("@OldDescription", oldRole.Description);
            cmd.Parameters.AddWithValue("@NewDescription", newRole.Description);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Role update failed.");
                }
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
        /// Marshall Sejkora
        /// Created: 2018/02/31
        /// 
        /// Delete Roles in the database
        /// </summary>
        int IRoleAccessor.DeleteRole(Role role)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_role_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleID", role.RoleID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Role Delete failed.");
                }
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
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Retrieve list of all Roles in database
        /// </summary>
        List<Role> IRoleAccessor.RetrieveRolesList()
        {
            List<Role> roleList = new List<Role>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_role_list";
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
                        var role = new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1)
                        };
                        roleList.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database accessor error", ex);
            }
            finally
            {
                conn.Close();
            }

            return roleList;
        } 
    }
}
