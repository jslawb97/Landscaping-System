using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPresentation.Models
{
	public class EmployeeJobPostViewModel : EmployeeJobPost
	{
		[DisplayName("Posting Employee")]
		public string Employee { get { return this.PostingEmployeeFirstName + " " + this.PostingEmployeeLastName; } set { this.Employee = value; } }

		[DisplayName("Job Address")]
		public string DisplayJobLocationAddress { get { return this.JobLocationStreet + ", " + this.JobLocationCity + ", " + this.JobLocationState + " " + this.JobLocationZipCode; } }

		
	}
}