using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
