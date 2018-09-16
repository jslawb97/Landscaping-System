using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessMocks
{
    public class UserAccessorMocks : IUserAccessor
    {
        private List<User> _userList = new List<User>();

        public UserAccessorMocks()
        {
            _userList.Add(new User(new Employee()
            {
                EmployeeID = 1000000,
                FirstName = "Gary",
                LastName = "Maldun",
                Address = "1235 E 1st St",
                PhoneNumber = "1322912323",
                Email = "BatManStan@gmail.com",
                Active = true
            }, new List<Role>(), false));

            _userList.Add(new User(new Employee()
            {
                EmployeeID = 1000001,
                FirstName = "Selena",
                LastName = "Stratosphere",
                Address = "54021 Luna ln",
                PhoneNumber = "4561211231",
                Email = "earth-chan@gmail.com",
                Active = true
            }, new List<Role>(), false));

            _userList[0].Roles.Add(new Role { RoleID = "Supply Clerk", Description = "Clerks supplies" });
            _userList[1].Roles.Add(new Role { RoleID = "Manager", Description = "Manages" });
        }

        public Employee RetrieveEmployeeByUsername(string username)
        {
            User user = _userList.Find(e => e.Employee.Email == username);
            return user.Employee;
        }

        public List<Role> RetrieveRolesByEmployeeID(int employeeID)
        {
            User user = _userList.Find(e => e.Employee.EmployeeID == employeeID);
            return user.Roles;
        }

        public int UpdatePasswordHash(int employeeID, string oldPasswordHash, string newPasswordHash)
        {
            int employeeId = 1000001;
            int result;

            if (employeeID == employeeId)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        public int VerifyUserNameAndPassword(string username, string passwordHash)
        {
            int result = 0;
            if (username.Equals("earth-chan@gmail.com"))
            {
                if (passwordHash.Equals("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e"))
                {
                    result = 1;
                }
            }

            return result;
        }


        public List<Role> RetrieveRolesByEmployeeIDAndActive(int employeeID, bool active = true)
        {
            User user = _userList.Find(e => e.Employee.EmployeeID == employeeID);
            return user.Roles;
        }
    }
}
