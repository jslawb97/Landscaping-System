using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Reuben Cassell
    /// Created: 2018/2/2
    /// 
    /// Interface for UserManager
    /// </summary>
    interface IUserManager
    {
        /// <summary>
        /// Reuben Cassell
        /// Created: 2018/2/2
        /// 
        /// Checks if user in the the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User AuthenticateUser(string username, string password);

        /// <summary>
        /// Reuben Cassell
        /// Created: 2018/2/2
        /// 
        /// Updates a users password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        User UpdatePassword(User user, string oldPassword, string newPassword);
    }
}
