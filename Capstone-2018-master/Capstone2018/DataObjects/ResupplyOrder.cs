using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ResupplyOrder
    {
        public int ResupplyOrderID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? Date { get; set; }
        public string SupplyStatusID { get; set; }
        public int VendorID { get; set; }
    }
}
