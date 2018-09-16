using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Weston Olund
    /// Created on 2018/02/29
    /// 
    /// Object to hold both the employee and time off request objects
    /// </summary>
    public class TimeOffRequestDetail
    {
        public TimeOffRequest TimeOffRequest { get; set; }
        public Employee Employee { get; set; }
    }
}
