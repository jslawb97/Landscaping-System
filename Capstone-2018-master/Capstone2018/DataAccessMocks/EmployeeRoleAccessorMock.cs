using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMock
{
    public class EmployeeRoleAccessorMock : IEmployeeRoleAccessor
    {

        /// <summary>
        /// Shilin Xiong
        /// Created 2018/02/04
        /// 
        /// Updated 2018/04/01 - John Miller
        /// Create the mock constructor to add,edit,delete data to the employeeRoleList
        /// </summary> QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18

        private List<EmployeeRole> _employeeRoleList = new List<EmployeeRole>();
        private List<EmployeeRoleDetail> _employeeRoleDetailList = new List<EmployeeRoleDetail>();

        public EmployeeRoleAccessorMock()
        {
            _employeeRoleList.Add(new EmployeeRole()
            {
                EmployeeId = 12012,
                RoleID = "Manager",
                Active = true
            });

            _employeeRoleList.Add(new EmployeeRole()
            {
                EmployeeId = 12011,
                RoleID = "Job Scheduler",
                Active = true
            });

            Employee employee = new Employee()
            {
                EmployeeID = Constants.IDSTARTVALUE
            };
            EmployeeRole employeeRole = new EmployeeRole()
            {
                EmployeeId = Constants.IDSTARTVALUE,
                RoleID = "Manager",
                Active = true
            };

            EmployeeRoleDetail employeeRoleDetail = new EmployeeRoleDetail()
            {
                Employee = employee,
                EmployeeRole = employeeRole
            };

            _employeeRoleDetailList.Add(employeeRoleDetail);
        }

        /// <summary>
        /// John Miller
        /// 2018/04/01
        /// 
        /// Accessor Mock to test DeactivateEmployeRoleDetail
        /// </summary>
        /// <param name="employeeRoleDetail"></param>
        /// <returns></returns>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public bool DeactivateEmployeeRole(EmployeeRoleDetail employeeRoleDetail)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var item in _employeeRoleDetailList)
                {
                    if (item.Employee.EmployeeID == Constants.IDSTARTVALUE && item.EmployeeRole.RoleID == "Manager")
                    {
                        item.EmployeeRole.Active = false;
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            if (rowsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// John Miller
        /// 2018/04/01
        /// 
        /// Accessor Mock to test DeactivateEmployeRoleDetail
        /// </summary>
        /// <param name="employeeRoleDetail"></param>
        /// <returns></returns>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public List<DataObjects.EmployeeRole> RetrieveEmployeeRoleList()
        {

            return _employeeRoleList;
        }

        /// <summary>
        /// John Miller
        /// 2018/04/01
        /// 
        /// Accessor Mock to test DeactivateEmployeRoleDetail
        /// </summary>
        /// <param name="employeeRoleDetail"></param>
        /// <returns></returns>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public List<EmployeeRoleDetail> RetrieveEmployeeRoleDetailList()
        {

            return _employeeRoleDetailList;
        }

        public int DeactivateEmployeeRoleById(int employeeId, string roleId)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var item in _employeeRoleDetailList)
                {
                    if (item.Employee.EmployeeID == Constants.IDSTARTVALUE && item.EmployeeRole.RoleID == "Manager")
                    {
                        item.EmployeeRole.Active = true;
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/04/01
        /// 
        /// Adds a new EmployeeRole 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeRole"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public int AddEmployeeRole(Employee employee, Role role)
        {
            try
            {
                //this._employeeRoleList.Add(employee, role.RoleID);
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/04/20
        /// Edits a mock EmployeeRoleDetail object.
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="newItem"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
        public int EditEmployeeRoleDetail(EmployeeRoleDetail oldEmployeeRoleDetail, EmployeeRoleDetail newEmployeeRoleDetail)
        {
            int result = 0;
            foreach (var item in _employeeRoleDetailList)
            {
                if (item.EmployeeRole.EmployeeId == oldEmployeeRoleDetail.EmployeeRole.EmployeeId)
                {
                    item.EmployeeRole.RoleID = newEmployeeRoleDetail.EmployeeRole.RoleID;
                    item.EmployeeRole.Active = newEmployeeRoleDetail.EmployeeRole.Active;
                    result = 1;
                }
            }
            return result;
        }
    }
}
