using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplyOrder
    {
        public int SupplyOrderID { get; set; }
        public int EmployeeID { get; set; }
        public int? JobID { get; set; }
        public string SupplyStatusID { get; set; }
        public DateTime Date { get; set; }
    }
}
