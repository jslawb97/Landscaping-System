using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Marshall Sejkora
    /// Created: 2018/02/02
    /// 
    /// Interface for RoleAccessor
    /// </summary>
    public interface IRoleAccessor
    {
        int CreateRole(Role role);
        int EditRole(Role oldRole, Role newRole);
        int DeleteRole(Role role);
        List<Role> RetrieveRolesList(); 
    }
}
