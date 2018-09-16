using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Marshall Sejkora
    /// Created: 2018/01/26
    /// 
    /// Logic Class to handle Roles
    /// </summary>
    public class RoleManager : IRoleManager
    {
        IRoleAccessor _roleAccessor;

        public RoleManager()
        {
            _roleAccessor = new RoleAccessor();
        }
        public RoleManager(IRoleAccessor roleAccessor)
        {
            _roleAccessor = roleAccessor;
        }


        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        ///  Sends a Role to createRole in RoleAccessor
        ///  then returns result
        /// </summary>
        public int CreateRole(Role role)
        {
            var result = 0;

            if (role.RoleID == "")
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (role.Description == "")
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }

            try
            {
                result = _roleAccessor.CreateRole(role);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        ///  Sends old Role data and the updated data to editRole in RoleAccessor
        ///  then returns result
        /// </summary>
        public int EditRole(Role oldRole, Role newRole)
        {
            var result = 0;

            if (newRole.RoleID == "")
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (newRole.Description == "")
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }

            try
            {
                result = _roleAccessor.EditRole(oldRole, newRole);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        ///  Sends a Role to DeactivateRole in RoleAccessor
        ///  then returns result
        /// </summary>
        public int DeleteRole(Role role)
        {
            var result = 0;

            try
            {
                result = _roleAccessor.DeleteRole(role);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/13/02
        /// 
        ///  Asks for a list of roles in database from access layer
        /// </summary>
        public List<Role> RetrieveRolesList()
        {
            List<Role> roleList = null;

            try
            {
                roleList = _roleAccessor.RetrieveRolesList();
            }
            catch (Exception)
            {
                
                throw;
            }

            return roleList;
        }
    }
}
