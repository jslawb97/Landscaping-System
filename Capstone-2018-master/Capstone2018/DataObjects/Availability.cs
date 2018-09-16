using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
	///Badis Saidani
	/// Created 2018/02/22
	/// 
	/// Class for availabilities
	/// </summary>
    public class Availability
    {
        public int AvailabilityID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
