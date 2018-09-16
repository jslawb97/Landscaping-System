using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessMocks;

namespace LogicTests
{
    [TestClass]
    public class RoleManagerTest
    {
        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// </summary>
        RoleManager _roleManager = new RoleManager(new RoleAccessorMock());
        Role _role = new Role { RoleID = "Test", Description = "Description" };
        Role _roleBadData = new Role { RoleID = "", Description = "" };
        Role _newRole = new Role { RoleID = "TestRole1", Description = "Test Description" };


        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/15
        /// 
        /// Tests that the role list is retrieved
        /// </summary>
        [TestMethod]
        public void TestRetrieveRolesListReturnsList()
        {
            //arrange

            //act
            List<Role> roleList = _roleManager.RetrieveRolesList();

            //assert
            Assert.IsNotNull(roleList);
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Tests that a role can be created
        /// </summary>
        [TestMethod]
        public void TestCreateRoleReturnsOne()
        {
            //arrange

            //act
            int result = _roleManager.CreateRole(_role);

            //assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/01
        /// 
        /// Tests that a role isn't created with a null description
        /// </summary>
        [TestMethod]
        public void TestCreateRoleNullDescription()
        {
            // arrange
            Role testRole = new Role()
            {
                RoleID = "Test",
                Description = null
            };

            // act
            try
            {
                int result = _roleManager.CreateRole(testRole);

                // assert
                Assert.Fail("Delete should fail because of the bad ID");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/01
        /// 
        /// Tests that a role isn't edited with a null description
        /// </summary>
        [TestMethod]
        public void TestEditRoleNullDescription()
        {
            // arrange
            Role testRole = new Role()
            {
                RoleID = "Test",
                Description = null
            };

            // act
            try
            {
                int result = _roleManager.EditRole(_role, testRole);

                // assert
                Assert.Fail("Delete should fail because of the bad ID");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Tests that a role can be edited
        /// </summary>
        [TestMethod]
        public void TestEditRoleReturnsOne()
        {
            //arrange
            int result = 0;

            //act
            result = _roleManager.EditRole(_newRole, _role);

            //assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Tests that role wont create with bad data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestCreateRoleBadData()
        {
            //arrange

            //act
            int result = _roleManager.CreateRole(_roleBadData);
        }


        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Tests that a role won't be edited with a bad ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestEditRoleBadID()
        {
            //arrange

            //act
            int result = _roleManager.EditRole(_role, _roleBadData);
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/02
        /// 
        /// Tests that a role is deleted with a good ID
        /// </summary>
        [TestMethod]
        public void TestDeleteRoleReturnsOne()
        {
            //arrange
            int result = 0;

            //act
            result = _roleManager.DeleteRole(_newRole);

            //assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/01
        /// 
        /// Tests that a role isn't deleted with a bad ID
        /// </summary>
        [TestMethod]
        public void TestDeleteRoleBadID()
        {
            // arrange
            Role testRole = new Role()
            {
                RoleID = "",
                Description = "Test"
            };

            // act
            try
            {
                int result = _roleManager.DeleteRole(testRole);

                // assert
                Assert.Fail("Delete should fail because of the bad ID");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }

        }
    }
}
