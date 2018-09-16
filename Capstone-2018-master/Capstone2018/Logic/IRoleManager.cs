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
    /// Created: 2018/02/02
    /// 
    /// Interface for RoleManager
    /// </summary>
    public interface IRoleManager
    {
        int CreateRole(Role role);
        int EditRole(Role oldRole, Role newRole);
        int DeleteRole(Role role);
        List<Role> RetrieveRolesList();
    }
}
