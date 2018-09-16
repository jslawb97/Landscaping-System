using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplyOrderDetail
    {
        public SupplyOrder Order { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }
}
