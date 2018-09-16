using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/01
    /// </summary>
    public class MaintenanceRecord
    {
        public int MaintenanceRecordID { get; set; }
        public int EquipmentID { get; set; }
        public int EmployeeID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
