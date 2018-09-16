using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _iEmployeeAccessor;

        // Constructor for real run
        public EmployeeManager()
        {
            this._iEmployeeAccessor = new EmployeeAccessor();
        }

        // Constructor for unit tests
        public EmployeeManager(IEmployeeAccessor iEmployeeAccessor)
        {
            this._iEmployeeAccessor = iEmployeeAccessor;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/02
        /// 
        /// Deactivates an Employee
        /// </summary>
        /// <param name="employeeID">The ID of the employee to be deactivated</param>
        /// <exception cref="SQLException">Deactivate fails</exception>
        /// <returns>Number of Employees deactivated</returns>
        public int DeactivateEmployeeByID(int employeeID)
        {
            int result = 0;

            try
            {
                result = _iEmployeeAccessor.DeactivateEmployeeByID(employeeID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/02
        /// 
        /// Method to retrieve a list of employees by the active field
        /// </summary>
        /// <param name="active"></param>
        /// <returns>A list of Employees</returns>
        public List<Employee> RetrieveEmployeeListByActive(bool active = false)
        {
            List<Employee> employeeList = null;

            try
            {
                employeeList = _iEmployeeAccessor.RetrieveEmployeeListByActive();
            }
            catch
            {
                throw;
            }

            return employeeList;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to retrieve an employee by their ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee RetrieveEmployeeByID(int employeeID)
        {
            if (employeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID value.");
            }
            Employee employee = new Employee();
            try
            {
                return employee = _iEmployeeAccessor.RetrieveEmployeeByID(employeeID);
            }
            catch (Exception)
            {

                throw;
            }
        }

            /// <summary>
        ///	Mike Mason
        ///	Created on 2018/02/15
        ///	
        /// Creates an employee
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateEmployee(Employee employee)
        {
            if (employee.FirstName == null)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (employee.FirstName.Length <= 0)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (employee.FirstName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The name must be shorter than 100 characters.");
            }

            if (employee.LastName == null)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (employee.LastName.Length <= 0)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (employee.LastName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The last name must be shorter than 100 characters.");
            }

            if (employee.Address == null)
            {
                throw new ApplicationException("You must enter an address.");
            }
            if (employee.Address.Length <= 0)
            {
                throw new ApplicationException("You must enter an address.");
            }
            if (employee.Address.Length > Constants.MAXADDRESSLENGTH)
            {
                throw new ApplicationException("The address must be shorter than 250 characters.");
            }

            if (employee.PhoneNumber == null)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (employee.PhoneNumber.Length <= 0)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (employee.PhoneNumber.Length > Constants.MAXPHONENUMBERLENGTH)
            {
                throw new ApplicationException("The phone number must be shorter than 15 characters.");
            }

            if (employee.Email == null)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (employee.Email.Length <= 0)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (employee.Email.Length > Constants.MAXEMAILLENGTH)
            {
                throw new ApplicationException("The email must be shorter than 100 characters.");
            }
            try
            {
                return (0 != this._iEmployeeAccessor.CreateEmployee(employee));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///	Mike Mason
        ///	Created on 2018/02/15
        ///	
        /// Creates an employee
        /// 
        /// </summary>
        /// <returns></returns>
        public int CreateEmployeeReturnScopeID(Employee employee)
        {
            if (employee.FirstName == null)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (employee.FirstName.Length <= 0)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (employee.FirstName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The name must be shorter than 100 characters.");
            }

            if (employee.LastName == null)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (employee.LastName.Length <= 0)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (employee.LastName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The last name must be shorter than 100 characters.");
            }

            if (employee.Address == null)
            {
                throw new ApplicationException("You must enter an address.");
            }
            if (employee.Address.Length <= 0)
            {
                throw new ApplicationException("You must enter an address.");
            }
            if (employee.Address.Length > Constants.MAXADDRESSLENGTH)
            {
                throw new ApplicationException("The address must be shorter than 250 characters.");
            }

            if (employee.PhoneNumber == null)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (employee.PhoneNumber.Length <= 0)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (employee.PhoneNumber.Length > Constants.MAXPHONENUMBERLENGTH)
            {
                throw new ApplicationException("The phone number must be shorter than 15 characters.");
            }

            if (employee.Email == null)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (employee.Email.Length <= 0)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (employee.Email.Length > Constants.MAXEMAILLENGTH)
            {
                throw new ApplicationException("The email must be shorter than 100 characters.");
            }
            try
            {
                return _iEmployeeAccessor.CreateEmployeeReturnScopeID(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        

        /// <summary>
        /// Dan Cable
        /// Created 2018/03/22
        /// 
        /// Edits an Employee
        /// 
        /// <remarks>
        /// Mike Mason
        /// 2018/05/04
        /// 
        /// Add validation to edit method
        /// </remarks>
        /// </summary>
        public bool EditEmployee(Employee oldEmployee, Employee newEmployee)
        {
            if (newEmployee.FirstName == null)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (newEmployee.FirstName.Length <= 0)
            {
                throw new ApplicationException("You must enter a first name.");
            }
            if (newEmployee.FirstName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The name must be shorter than 100 characters.");
            }

            if (newEmployee.LastName == null)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (newEmployee.LastName.Length <= 0)
            {
                throw new ApplicationException("You must enter a last name.");
            }
            if (newEmployee.LastName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The last name must be shorter than 100 characters.");
            }

            if (newEmployee.Address == null)
            {
                throw new ApplicationException("You must enter an address.");
            }
            if (newEmployee.Address.Length <= 0)
            {
                throw new ApplicationException("You must enter an address.");
            }
            if (newEmployee.Address.Length > Constants.MAXADDRESSLENGTH)
            {
                throw new ApplicationException("The address must be shorter than 250 characters.");
            }

            if (newEmployee.PhoneNumber == null)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (newEmployee.PhoneNumber.Length <= 0)
            {
                throw new ApplicationException("You must enter a phone number.");
            }
            if (newEmployee.PhoneNumber.Length > Constants.MAXPHONENUMBERLENGTH)
            {
                throw new ApplicationException("The phone number must be shorter than 15 characters.");
            }

            if (newEmployee.Email == null)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (newEmployee.Email.Length <= 0)
            {
                throw new ApplicationException("You must enter an email.");
            }
            if (newEmployee.Email.Length > Constants.MAXEMAILLENGTH)
            {
                throw new ApplicationException("The email must be shorter than 100 characters.");
            }
            var result = false;
            try
            {
                result = (true != this._iEmployeeAccessor.EditEmployee(oldEmployee, newEmployee));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
		
		
        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/18
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeListByCertificationAndAvailability(Certification certification, DateTime? startDate, DateTime? endDate)
        {
            List<Employee> employeeList = null;

            try
            {
                employeeList = _iEmployeeAccessor.RetrieveEmployeeListByCertificationAndAvailability(certification, startDate, endDate);
            }
            catch
            {
                throw;
            }
            return employeeList;
        }
        /// <summary>
        /// James McPherson
        /// Created 2018/04/13
        /// 
        /// Method to retrieve all Employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> RetrieveEmployeeList()
        {
            List<Employee> employeeList = null;
            try
            {
                employeeList = _iEmployeeAccessor.RetrieveEmployeeList();
            }
            catch
            {
                throw;
            }
            return employeeList;
        }

        public List<Employee> RetreiveEmployeeListByJobAvailability(int jobID)
        {
            List<Employee> employeeList = null;

            try
            {
                employeeList = _iEmployeeAccessor.RetreiveEmployeeListByJobAvailability(jobID);
            }
            catch
            {
                throw;
            }
            return employeeList;
        }

		public int RetreiveEmployeeIdByEmail(string email)
		{
			int result = 0;
			try
			{
				result = _iEmployeeAccessor.RetreiveEmployeeIdByEmail(email);
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
	}
}
