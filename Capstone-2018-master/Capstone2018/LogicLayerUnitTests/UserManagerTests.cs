using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using DataObjects;
using Logic;
using DataAccessMocks;


namespace LogicLayerUnitTests
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager _userManager;

        [TestInitialize]
        public void TestSetup()
        {
            _userManager = new UserManager(new UserAccessorMocks());

        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/06
        /// 
        /// Method that verifies authenticate user returns a user
        /// </summary>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        [TestMethod]
        public void TestAuthenticateUserSuccess()
        {
            // arrange
            var username = "earth-chan@gmail.com";
            var password = "newuser";
            User user = null;
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Manager", Description = "Manages" });


            // act
            user = _userManager.AuthenticateUser(username, password);

            // assert
            Assert.AreEqual(user.Employee.EmployeeID, 1000001);
            Assert.AreEqual(user.Employee.FirstName, "Selena");
            Assert.AreEqual(user.Employee.LastName, "Stratosphere");
            Assert.AreEqual(user.Employee.Address, "54021 Luna ln");
            Assert.AreEqual(user.Employee.PhoneNumber, "4561211231");
            Assert.AreEqual(user.Employee.Email, "earth-chan@gmail.com");
            Assert.AreEqual(user.Employee.Active, true);
            Assert.AreEqual(user.Roles.Count, roles.Count);
            for (int i = 0; i < user.Roles.Count; i++)
            {
                Assert.AreEqual(user.Roles[i].RoleID, roles[i].RoleID);
                Assert.AreEqual(user.Roles[i].Description, roles[i].Description);
            }
            Assert.AreEqual(user.PasswordMustBeChanged, false);
        }
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/06
        /// 
        /// Method that verifies authenticate user returns a user
        /// </summary>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailure()
        {
            // arrange
            var username = "thisfails@mail.com";
            var password = "newuser";
            User user = null;
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Manager", Description = "Manages" });


            // act
            user = _userManager.AuthenticateUser(username, password);

            // assert
            Assert.AreEqual(user.Employee.EmployeeID, 10000000);
            Assert.AreEqual(user.Employee.FirstName, "Selena");
            Assert.AreEqual(user.Employee.LastName, "Stratosphere");
            Assert.AreEqual(user.Employee.Address, "54021 Luna ln");
            Assert.AreEqual(user.Employee.PhoneNumber, "4561211231");
            Assert.AreEqual(user.Employee.Email, "earth-chan@gmail.com");
            Assert.AreEqual(user.Employee.Active, true);
            Assert.AreEqual(user.Roles.Count, roles.Count);
            for (int i = 0; i < user.Roles.Count; i++)
            {
                Assert.AreEqual(user.Roles[i].RoleID, roles[i].RoleID);
                Assert.AreEqual(user.Roles[i].Description, roles[i].Description);
            }
            Assert.AreEqual(user.PasswordMustBeChanged, false);
        }

        /// <summary>
        /// Jacob Conley
        /// 2018/05/03
        /// 
        /// Method that verifies authenticate user does not return a user
        /// with a null username
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserNullUsername()
        {
            // arrange
            string username = null;
            string password = "newuser";
            User user = null;
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Manager", Description = "Manages" });


            // act
            user = _userManager.AuthenticateUser(username, password);
        }

        /// <summary>
        /// Jacob Conley
        /// 2018/05/03
        /// 
        /// Method that verifies authenticate user does not return a user
        /// with an empty username
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserEmptyUsername()
        {
            // arrange
            string username = "";
            string password = "newuser";
            User user = null;
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Manager", Description = "Manages" });


            // act
            user = _userManager.AuthenticateUser(username, password);
        }

        /// <summary>
        /// Jacob Conley
        /// 2018/05/03
        /// 
        /// Method that verifies authenticate user does not return a user
        /// with a null password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserNullPassword()
        {
            // arrange
            string username = "thisfails@mail.com";
            string password = null;
            User user = null;
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Manager", Description = "Manages" });


            // act
            user = _userManager.AuthenticateUser(username, password);
        }

        /// <summary>
        /// Jacob Conley
        /// 2018/05/03
        /// 
        /// Method that verifies authenticate user does not return a user
        /// with an empty password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserEmptyPassword()
        {
            // arrange
            string username = "thisfails@mail.com";
            string password = "";
            User user = null;
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Manager", Description = "Manages" });


            // act
            user = _userManager.AuthenticateUser(username, password);
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/06
        /// 
        /// Method that verifies Update Password will change the current password
        /// </summary>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        [TestMethod]
        public void TestUpdatePassword()
        {
            // arrange
            var currentPassword = "newuser";
            var newPassword = "password";
            
            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Supply Clerk", Description = "Clerks supplies" });
            User user = new User(new Employee()
            {
                EmployeeID = 1000001,
                FirstName = "Selena",
                LastName = "Stratosphere",
                Address = "54021 Luna ln",
                PhoneNumber = "4561211231",
                Email = "earth-chan@gmail.com",
                Active = true
            }, roles, false); ;
            


            // act
            // assert
            Assert.AreEqual(_userManager.UpdatePassword(user, currentPassword, newPassword), user);

        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/06
        /// 
        /// Method that verifies Update Password will not change the current password
        /// </summary>
        /// <remarks>QA Jayden Tollefosn 4/27/2018</remarks>
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void TestUpdatePasswordFailed()
        {
            // arrange
            var currentPassword = "newuser";
            var newPassword = "password";

            var roles = new List<Role>();
            roles.Add(new Role { RoleID = "Supply Clerk", Description = "Clerks supplies" });
            User user = new User(new Employee()
            {
                EmployeeID = 1000000,
                FirstName = "Selena",
                LastName = "Stratosphere",
                Address = "54021 Luna ln",
                PhoneNumber = "4561211231",
                Email = "earth-chan@gmail.com",
                Active = true
            }, roles, false); ;


            // act
            // assert
            Assert.AreEqual(_userManager.UpdatePassword(user, currentPassword, newPassword), user);

        }


        [TestCleanup]
        public void TestTearDown()
        {
            _userManager = null;
        }


    }
}
