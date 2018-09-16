using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public Employee Employee { get; private set; }

        public List<Role> Roles { get; private set; }

        public bool PasswordMustBeChanged { get; private set; }

        public User(Employee employee, List<Role> roles, bool passwordMustBeChanged = false)
        {
            this.Employee = employee;
            this.Roles = roles;
            this.PasswordMustBeChanged = passwordMustBeChanged;
        }
    }
}
