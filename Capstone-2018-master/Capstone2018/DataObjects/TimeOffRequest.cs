using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Weston Olund
    /// Created 2018/02/22
    /// 
    /// Class for Time off requests
    /// </summary>
    public class TimeOffRequest
    {
        public int TimeOffID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Approved { get; set; }
        public bool Active { get; set; }
    }
}
