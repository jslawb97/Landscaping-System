using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	public class EmployeeJobDetail
	{
		public int JobID { get; set; }
		public string JobLocationStreet { get; set; }
		public string JobLocationCity { get; set; }
		public string JobLocationState { get; set; }
		public string JobLocationZipCode { get; set; }
		public DateTime JobScheduled { get; set; }
		public string EmployeeAddress { get; set; }

	}
}
