using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Jacob Conley
    /// Created On 2018/02/02
    /// 
    /// Interface for the UserAccessor
    /// </summary>
    ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
    public interface IUserAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created On 2018/02/02
        /// 
        /// Checks the database for a given user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        int VerifyUserNameAndPassword(string username, string passwordHash);

        /// <summary>
        /// Jacob Conley
        /// Created On 2018/02/02
        /// 
        /// Retrieves an existing employee from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Employee RetrieveEmployeeByUsername(string username);

        /// <summary>
        /// Jacob Conley
        /// Created On 2018/02/02
        /// 
        /// Retrieves a list of roles by employeeID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        List<Role> RetrieveRolesByEmployeeID(int employeeID);

        /// <summary>
        /// Jacob Conley
        /// Created On 2018/02/02
        /// 
        /// Updates the password of an existing user
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="oldPasswordHash"></param>
        /// <param name="newPasswordHash"></param>
        /// <returns></returns>
        int UpdatePasswordHash(int employeeID, string oldPasswordHash, string newPasswordHash);

        List<Role> RetrieveRolesByEmployeeIDAndActive(int employeeID, bool active = true);
    }
}
