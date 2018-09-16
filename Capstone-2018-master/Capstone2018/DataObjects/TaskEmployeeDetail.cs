using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created on 2018/04/05
    /// 
    /// Detail object for TaskEmployee records
    /// </summary>
    public class TaskEmployeeDetail
    {
        public string TaskName { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string FullName { get { return EmployeeFirstName + " " + EmployeeLastName; } }
        public int TaskEmployeeID { get; set; }
        public int TaskTypeEmployeeNeedID { get; set; }
        public int EmployeesAssignedCount { get; set; }
    }
}
