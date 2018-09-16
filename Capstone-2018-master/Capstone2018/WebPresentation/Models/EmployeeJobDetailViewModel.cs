using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPresentation.Models
{
	public class EmployeeJobDetailViewModel : EmployeeJobDetail
	{
		
		[DisplayName("Job Address")]
		public string DisplayJobLocationAddress { get { return this.JobLocationStreet + ", " + this.JobLocationCity + ", " + this.JobLocationState + " " + this.JobLocationZipCode; } }

		[DisplayName("Job Date/Time")]
		public DateTime DisplayJobScheduled { get { return this.JobScheduled; } set { this.JobScheduled = value; } }

		public bool LoadMap { get; set; }

		public string AddressToLoad { get; set; }
	}
}