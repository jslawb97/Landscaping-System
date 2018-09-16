using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPresentation.Models;
using Logic;
using Microsoft.AspNet.Identity;

namespace WebPresentation.Controllers
{
	[Authorize(Roles = "Foreman,Temp,Worker")]
	public class EmployeeJobController : Controller
    {

		private IJobManager _jobManager;
		private IEmployeeManager _employeeManager;
		private int userID;

		public EmployeeJobController()
		{
			_jobManager = new JobManager();
			
		}
        // GET: EmployeeJob
        public ActionResult Index(string address = null)
        {
            try
            {
                _employeeManager = new EmployeeManager();
                string email = User.Identity.GetUserName();
                int userID = _employeeManager.RetreiveEmployeeIdByEmail(email);
                var employeeJob = _jobManager.RetreiveEmployeeJobDetailByEmployeeID(userID);
                List<EmployeeJobDetailViewModel> employeeJobDetailVM = new List<EmployeeJobDetailViewModel>();
                foreach (var item in employeeJob)
                {
                    var toAdd = new EmployeeJobDetailViewModel()
                    {
                        JobID = item.JobID,
                        JobLocationStreet = item.JobLocationStreet,
                        JobLocationCity = item.JobLocationCity,
                        JobLocationState = item.JobLocationState,
                        JobLocationZipCode = item.JobLocationZipCode,
                        JobScheduled = item.JobScheduled,
                        EmployeeAddress = item.EmployeeAddress
                    };
                    if (toAdd.DisplayJobLocationAddress.Equals(address))
                    {
                        toAdd.LoadMap = true;
                        toAdd.AddressToLoad = address;
                    }
                    employeeJobDetailVM.Add(toAdd);
                }

                return View(employeeJobDetailVM);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Home");
            }
			
        }

		public ActionResult Map(string address)
		{
			return RedirectToAction("Index", "EmployeeJob", new { address = address });
		}

        
    }
}
