using System;
using System.Collections.Generic;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class EmployeeRoleManager : IEmployeeRoleManager
    {
        private IEmployeeRoleAccessor _employeeRoleAccessor;

        /// <summary>
        /// Shilin Xiong
        /// Created 2018/02/04
        /// 
        /// Updated 2018/04/01 - John Miller
        /// 
        /// implement the IemployeeRoleManager interface
        /// </summary>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        
        EmployeeAccessor _employeeAccessor = new EmployeeAccessor();
        RoleAccessor _roleAccessor = new RoleAccessor();

        // Manager Constructor for handling accessor dependency
        public EmployeeRoleManager()
        {
            _employeeRoleAccessor = new EmployeeRoleAccessor();
        }

        // Constructor for unit tests
        public EmployeeRoleManager(IEmployeeRoleAccessor employeeRoleAccessor)
        {
            _employeeRoleAccessor = employeeRoleAccessor;
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/04/01
        /// 
        /// Method to deactivate an EmployeeRole
        /// </summary>
        /// <returns>True if deactivation is successful; false otherwise.</returns>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public bool DeactivateEmployeeRole(EmployeeRoleDetail employeeRoleDetail)
        {
            try
            {
                return _employeeRoleAccessor.DeactivateEmployeeRole(employeeRoleDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calls the accessor to add an EmployeeRole to the database
        /// </summary>
        /// <param name="employeeRoleDetail">The employeeRoleDetail to be added</param>
        /// <returns>True if successful, false otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/04/01
        /// </remarks>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public int AddEmployeeRoleDetail(Employee employee, Role role)
        {
            var result = 0;
            try
            {
                result = _employeeRoleAccessor.AddEmployeeRole(employee, role);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public int EditEmployeeRoleDetail(EmployeeRoleDetail oldEmployeeRoleDetail, EmployeeRoleDetail newEmployeeRoleDetail)
        {
            var result = 0;
            try
            {
                result = _employeeRoleAccessor.EditEmployeeRoleDetail(oldEmployeeRoleDetail, newEmployeeRoleDetail);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Shilin Xiong
        /// Created: 2018/02/01
        /// 
        ///deactivate a employeerole by id
        /// QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        /// </summary>
        public int DeactivateEmployeeRoleById(int employeeId, string roleId)
        {
            int result = 0;

            try
            {
                result = _employeeRoleAccessor.DeactivateEmployeeRoleById(employeeId, roleId);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Shilin Xiong
        /// Created: 2018/02/01
        /// 
        /// Retrieves a list of Employee role details
        /// </summary>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public List<EmployeeRoleDetail> RetrieveEmployeeRoleDetailList()
        {
            var insertDetailList = new List<EmployeeRoleDetail>();
            try
            {
                insertDetailList = _employeeRoleAccessor.RetrieveEmployeeRoleDetailList();
            }
            catch (Exception)
            {
                throw;
            }
            return insertDetailList;
        }

        /// <summary>
        /// Shilin Xiong
        /// Created: 2018/02/01
        /// 
        /// Retrieves a list of EmployeeRoles
        /// </summary>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18

        public List<EmployeeRole> RetrieveEmployeeRoleList()
        {
            var employeeRoleList = new List<EmployeeRole>();

            try
            {
                employeeRoleList = _employeeRoleAccessor.RetrieveEmployeeRoleList();
            }
            catch
            {
                throw;
            }
            return employeeRoleList;
        }

        //public List<EmployeeRole> RetrieveEmployeeRoleListByRoleId(string roleID)
        //{
        //    throw new NotImplementedException();
        //}

        //public EmployeeRoleDetail RetrievEmployeeRoleDetail(EmployeeRole employeeRole)
        //{
        //    EmployeeRoleDetail employeeRoleDetail = null;

        //    try
        //    {
        //       // var empID = _employeeAccessor.RetrieveEmployeeByID(employeeRoleDetail.EmployeeId);

        //        var empID = _employeeAccessor.DeactivateEmployeeByID(employeeRoleDetail.EmployeeId);

        //        var roID = _roleAccessor.RetrieveRoleByID(employeeRoleDetail.RoleId);


        //        employeeRoleDetail = new EmployeeRoleDetail()
        //        {
        //            EmployeeId = empID,
        //            RoleId = roID
                   
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return employeeRoleDetail;
        //}


        public int AddEmployeeRoleDetail(EmployeeRoleDetail employeeRoleDetail)
        {
            throw new NotImplementedException();
        }

        public bool DeactivateEmployeeRole(Employee employee, EmployeeRole employeeRole)
        {
            throw new NotImplementedException();
        }
    }
}
