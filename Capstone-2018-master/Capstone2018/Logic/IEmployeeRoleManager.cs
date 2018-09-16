using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using DataAccess;

namespace Logic
{
    public interface IEmployeeRoleManager
    {


        /// <summary>
        /// Shilin Xiong
        /// Created 2018/02/04
        /// 
        /// Updated 2018/04/01 - John Miller
        /// Edit The EmployeeRole abstract class 
        /// </summary>
        //  int DeactivateEmployeeRoleById(int oldEmpemloyeeRole, EmployeeRole newEmployeeRole);

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
        /// Deactivates an EmployeeRole 
        /// </summary>
        /// <param name="employeeRoleDetail"></param>
        /// <returns>True if successful, false if unsuccessful</returns>
        bool DeactivateEmployeeRole(EmployeeRoleDetail employeeRoleDetail);
        
        /// <summary>
        /// John Miller
        /// Updated 2018/04/01
        /// Adds an EmployeeRole
        /// </summary>
        /// <param name="employeeRoleDetail">the EmployeeRole to be added</param>
        /// <returns>True if EmployeeRole is successfully added, False otherwise</returns>
        int AddEmployeeRoleDetail(Employee employee, Role role);


        int EditEmployeeRoleDetail(EmployeeRoleDetail oldEmployeeRoleDetail, EmployeeRoleDetail newEmployeeRoleDetail);

        // EmployeeRoleDetail RetrieveEmployeeRoleDetail(EmployeeRole employeeRole);

        List<EmployeeRoleDetail> RetrieveEmployeeRoleDetailList();
        List<EmployeeRole> RetrieveEmployeeRoleList();





    }

}