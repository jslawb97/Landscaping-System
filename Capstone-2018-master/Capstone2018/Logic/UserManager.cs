using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DataAccess;

namespace Logic
{
    public class UserManager : IUserManager
    {
        IUserAccessor _userAccessor;

        public UserManager() 
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor userAccessor) 
        {
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created: 1/29/2018
        /// 
        /// Takes a username and password to check if the user is in the database,
        /// and returns a user token if valid.
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/03
        /// 
        /// Added tests to make sure the username and password received are valid.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User AuthenticateUser(string username, string password)
        {
            if (username == null || username.Length < Constants.MINUSERNAMELENGTH || username.Length > Constants.MAXUSERNAMELENGTH)
            {
                throw new ApplicationException("Invalid username");
            }
            if (password == null || password.Length < Constants.MINPASSWORDLENGTH)
            {
                throw new ApplicationException("Invalid password");
            }
            User user = null;

            var passwordHash = HashSha256(password);

            try
            {
                var validationResult = _userAccessor.VerifyUserNameAndPassword(username, passwordHash);

                if (validationResult == 1) // user validated
                {
                    var employee = _userAccessor.RetrieveEmployeeByUsername(username);

                    var roles = _userAccessor.RetrieveRolesByEmployeeIDAndActive(employee.EmployeeID, true);

                    /// Reuben Cassell 1/29/2018
                    /// The following is the code for changing the default password
                    /// if the user is new, if we want to include this feature.

                    //bool passwordMustBeChanged = false;

                    //if (password == "newuser")
                    //{
                    //    passwordMustBeChanged = true;
                    //    roles.Clear();
                    //    roles.Add(new Role() { RoleID = "New User"});
                    //}

                    user = new User(employee, roles /*, passwordMustBeChanged*/);
                }
                else // user not validated
                {
                    throw new ApplicationException("Login Failed. Invalid username or password.");
                }

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to connect to server.", ex);
            }


            return user;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created: 1/29/2018
        /// 
        /// Function to apply a SHA256 (SecureHashAlgorithm)hash algorithm
        /// to a password to store or compare with the 
        /// user's password hash in the database.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string HashSha256(string source)
        {
            string result = "";

            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {

                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {

                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();

            return result;
        }


        /// <summary>
        /// Reuben Cassell
        /// Created: 1/29/2018
        /// 
        /// Used to create a new user password.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public User UpdatePassword(User user, string oldPassword, string newPassword)
        {
            User newUser = null;
            int rowsAffected = 0;

            string oldPasswordHash = HashSha256(oldPassword);
            string newPasswordHash = HashSha256(newPassword);

            try
            {
                rowsAffected = _userAccessor.UpdatePasswordHash(user.Employee.EmployeeID,
                    oldPasswordHash, newPasswordHash);
                if (rowsAffected == 1)
                {
                    if (user.Roles[0].RoleID == "New User")
                    {

                        var roles = _userAccessor.RetrieveRolesByEmployeeID(user.Employee.EmployeeID);
                        newUser = new User(user.Employee, roles);
                    }
                    else
                    {
                        newUser = user;
                    }
                }
                else
                {
                    throw new ApplicationException("Update returned 0 rows affected.");
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Password Change Failed.", ex);
            }

            return newUser;
        }


    }

}

