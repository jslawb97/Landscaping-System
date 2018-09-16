using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccess
{
    /// <summary>
    /// Shilin Xiong
    /// Created 2018/02/04
    /// 
    /// Updated 2018/04/01 - John Miller
    /// Interface for the EmployeeRoleAccessor
    /// </summary>
    ///  QA add,edit, delete EmployeeRole ShilinXiong T 5/4//18
    public interface IEmployeeRoleAccessor
    {
        bool DeactivateEmployeeRole(EmployeeRoleDetail employeeRoleDetail);
        int AddEmployeeRole(Employee employee, Role role);
        int EditEmployeeRoleDetail(EmployeeRoleDetail oldEmployeeRoleDetail, EmployeeRoleDetail newEmployeeRoleDetail);
        List<EmployeeRoleDetail> RetrieveEmployeeRoleDetailList();
        int DeactivateEmployeeRoleById(int employeeId,string roleId);
        List<EmployeeRole> RetrieveEmployeeRoleList();       
    }
}