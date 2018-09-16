using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary>
	/// Zachary Hall
	/// Created 2018/05/04
	/// </summary>
	public class EmployeeJobPost
	{
		public int EmployeeJobPostID { get; set; }
		public int PostingEmployeeID { get; set; }
		public string PostingEmployeeFirstName { get; set; }
		public string PostingEmployeeLastName { get; set; }
		public string JobLocationStreet { get; set; }
		public string JobLocationCity { get; set; }
		public string JobLocationState { get; set; }
		public string JobLocationZipCode { get; set; }
		public DateTime JobScheduled { get; set; }
		
	}
}
