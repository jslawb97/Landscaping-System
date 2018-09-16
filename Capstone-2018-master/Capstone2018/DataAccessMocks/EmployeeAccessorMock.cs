using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class EmployeeAccessorMock : IEmployeeAccessor
    {
        private List<Employee> _employeeList = new List<Employee>();

        /// <summary>
        /// James McPherson
        /// Created 2018/02/03
        /// 
        /// Mock constructor to add data to the Employee list
        /// </summary>
        /// 
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public EmployeeAccessorMock()
        {
            _employeeList.Add(new Employee()
            {
                EmployeeID = 1000000,
                FirstName = "Frank",
                LastName = "Atman",
                Address = "123 A St.",
                PhoneNumber = "(123) 456-7890",
                Email = "aperson@somedomain.com",
                Active = true
            });
            _employeeList.Add(new Employee()
            {
                EmployeeID = 1000001,
                FirstName = "Sherlock",
                LastName = "Holmes",
                Address = "221B Baker St.",
                PhoneNumber = "(123) 456-7891",
                Email = "aperson@someotherdomain.com",
                Active = true
            });
            _employeeList.Add(new Employee()
            {
                EmployeeID = 1000002,
                FirstName = "John",
                LastName = "Doe",
                Address = "456 B St.",
                PhoneNumber = "(123) 456-7892",
                Email = "aperson@yetanotherdomain.com",
                Active = false
            });
        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/02/21
        /// 
        /// Mock method to add employee
        /// </summary>
        /// <returns></returns>
        /// /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public int CreateEmployee(Employee employee)
        {
            if (employee.EmployeeID == Constants.IDSTARTVALUE * 500)
            {
                throw new ApplicationException("Database access error");
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/03
        /// 
        /// Mock method to deactivate employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public int DeactivateEmployeeByID(int employeeID)
        {
            int result = 0;

            foreach(Employee employee in _employeeList)
            {
                if(employee.EmployeeID == employeeID)
                {
                    employee.Active = false;
                    result++;
                    break;
                }
            }

            return result;
        }

        public int DeleteEmployeeByID(int employeeID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dan Cable
        /// Created 2018/03/29
        /// 
        /// Mock method to edit employees
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public bool EditEmployee(Employee newEmployee, Employee oldEmployee)
        {
            var result = 0;

            this._employeeList.ForEach(employeeList =>
            {
                if (employeeList == oldEmployee)
                {
                    employeeList.FirstName = newEmployee.FirstName;
                    employeeList.LastName = newEmployee.LastName;
                    employeeList.Address = newEmployee.Address;
                    employeeList.PhoneNumber = newEmployee.PhoneNumber;
                    employeeList.Email = newEmployee.Email;
                    employeeList.Active = newEmployee.Active;
                    result = 1;
                }
            });
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CreateEmployeeReturnScopeID(Employee employee)
        {
            throw new NotImplementedException();
        }

		public int RetreiveEmployeeIdByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public List<Employee> RetreiveEmployeeListByJobAvailability(int jobID)
        {
            throw new NotImplementedException();
        }

        public Employee RetrieveEmployeeByEmail(string email)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public Employee RetrieveEmployeeByID(int employeeID)
        {
            Employee employee = null;
            foreach (var emp in _employeeList)
            {
                if (emp.EmployeeID == employeeID)
                {
                    employee = emp;
                }
            }
            if (employee == null)
            {
                throw new ApplicationException("Employee record not found.");
            }
            return employee;
        }

        public Employee RetrieveEmployeeByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/13
        /// 
        /// Mock method to retrieve all Employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeList()
        {
            return _employeeList;
        }

        /// <summary>
        /// Mock method to retrieve employees by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public List<Employee> RetrieveEmployeeListByActive(bool active = true)
        {
            List<Employee> employeeList = new List<Employee>();

            foreach(Employee employee in _employeeList)
            {
                if(employee.Active == active)
                {
                    employeeList.Add(employee);
                }
            }

            return employeeList;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/17
        /// 
        /// Method to Retrieve a Employee List by Certification and availability
        /// </summary>
        /// <param name="employeeCertification"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeListByCertificationAndAvailability(Certification certification, DateTime? startDate, DateTime? endDate)
        {
            return _employeeList;
        }
    }
}
