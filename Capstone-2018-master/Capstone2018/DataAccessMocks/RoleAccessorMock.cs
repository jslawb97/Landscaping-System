using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    public class RoleAccessorMock : IRoleAccessor
    {
        private List<Role> _roleList = new List<Role>();

        public RoleAccessorMock()
        {
            _roleList.Add(new Role()
            {
                RoleID = "TestRole1",
                Description = "Test Description"
            });
            _roleList.Add(new Role()
            {
                RoleID = "TestRole2",
                Description = "Test Description"
            });
            _roleList.Add(new Role()
            {
                RoleID = "TestRole3",
                Description = "Test Description"
            });
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/05
        /// 
        /// Mock method to create a new role
        /// 
        /// Jacob Slaubaugh
        /// Update 2018/05/02
        /// 
        /// </summary>
        public int CreateRole(Role role)
        {
            int rowsAffected = 0;

            try
            {
                _roleList.Add(role);
                rowsAffected++;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;

        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/02
        /// 
        /// Mock method to edit an existing Role
        /// </summary>
        /// <param name="oldRole"></param>
        /// <param name="newRole"></param>
        /// <returns></returns>
        public int EditRole(Role oldRole, Role newRole)
        {
            var result = 0;

            foreach (Role role in _roleList)
            {
                if (oldRole.RoleID == role.RoleID && oldRole.Description == role.Description)
                {
                    role.RoleID = newRole.RoleID;
                    role.Description = newRole.Description;
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/02
        /// 
        /// Mock method to delete a Role by the ID
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int DeleteRole(Role role)
        {
            int result = 0;

            foreach (Role rle in _roleList)
            {
                if (rle.RoleID == role.RoleID)
                {
                    rle.RoleID = "";
                    rle.Description = "";
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Retrieve list of all Roles in database
        /// </summary>
        public List<Role> RetrieveRolesList()
        {
            return _roleList;
        }
    }
}
