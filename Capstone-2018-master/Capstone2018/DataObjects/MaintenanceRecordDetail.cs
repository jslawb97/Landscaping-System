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
    public class MaintenanceRecordDetail
    {
        public MaintenanceRecord MaintenanceRecord { get; set; }
        public Equipment Equipment { get; set; }
        public Employee Employee { get; set; }
    }
}
