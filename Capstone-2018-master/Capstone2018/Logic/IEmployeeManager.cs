using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    ///     James McPherson
    ///     Created 2018/02/02
    ///     
    ///     Interface for the EmployeeManager
    /// </summary>
    public interface IEmployeeManager
    {
        List<Employee> RetrieveEmployeeListByActive(bool active = true);
        List<Employee> RetrieveEmployeeList();
        int DeactivateEmployeeByID(int employeeID);
        Employee RetrieveEmployeeByID(int employeeID);
        bool CreateEmployee(Employee employee);
        int CreateEmployeeReturnScopeID(Employee employee);
        bool EditEmployee(Employee employee, Employee oldEmployee);
        List<Employee> RetrieveEmployeeListByCertificationAndAvailability(Certification certification, DateTime? startDate, DateTime? endDate);
        List<Employee> RetreiveEmployeeListByJobAvailability(int jobID);
		int RetreiveEmployeeIdByEmail(string email);


	}
}
