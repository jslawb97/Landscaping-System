using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/02
    /// 
    /// Interface for the EmployeeAccessor
    /// <remarks>
    /// Brady Feller
    /// Revised 2018/03/01
    /// 
    /// Added 'RetrieveEmployeeByID'
    /// </remarks>
    /// </summary>
    public interface IEmployeeAccessor
    {
        List<Employee> RetrieveEmployeeListByActive(bool active = true);
        List<Employee> RetrieveEmployeeList();
        int CreateEmployee(Employee employee);
        int DeactivateEmployeeByID(int employeeID);
        Employee RetrieveEmployeeByID(int employeeID);
        bool EditEmployee(Employee newEmployee, Employee oldEmployee);
        List<Employee> RetrieveEmployeeListByCertificationAndAvailability(Certification certification, DateTime? startDate, DateTime? endDate);
        List<Employee> RetreiveEmployeeListByJobAvailability(int jobID);
		int RetreiveEmployeeIdByEmail(string email);
        int CreateEmployeeReturnScopeID(Employee employee);
    }
}
