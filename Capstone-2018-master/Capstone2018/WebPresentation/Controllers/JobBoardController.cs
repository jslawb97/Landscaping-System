using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logic;
using DataObjects;
using WebPresentation.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace WebPresentation.Controllers
{
	[Authorize(Roles = "Foreman,Temp,Worker")]
	public class JobBoardController : Controller
    {

		private IEmployeeJobPostManager _employeeJobPostManager;
		private IEmployeeManager _employeeManager;
		private int userID;

		public JobBoardController()
		{
			_employeeJobPostManager = new EmployeeJobPostManager();
			

		}

        // GET: JobBoard
        public ActionResult Add(int id)
        {

			try
			{
				_employeeManager = new EmployeeManager();
				string email = User.Identity.GetUserName();
				int userID = _employeeManager.RetreiveEmployeeIdByEmail(email);
				bool reasult = _employeeJobPostManager.CreateEmployeeJobPost(userID, id);
			}
			catch (Exception)
			{
                return RedirectToAction("ErrorPage", "Home");
			}
			

            return RedirectToAction("Index", "EmployeeJob", new { });
        }

		public ActionResult Index()
		{
			List<EmployeeJobPostViewModel> vmList = new List<EmployeeJobPostViewModel>();
			try
			{
				_employeeManager = new EmployeeManager();
				string email = User.Identity.GetUserName();
				int userID = _employeeManager.RetreiveEmployeeIdByEmail(email);
				List<EmployeeJobPost> list = _employeeJobPostManager.RetreiveJobPostingByEmployeeCertification(userID);
				
				foreach (var item in list)
				{
					vmList.Add(new EmployeeJobPostViewModel()
					{
						EmployeeJobPostID = item.EmployeeJobPostID,
						PostingEmployeeID = item.PostingEmployeeID,
						PostingEmployeeFirstName = item.PostingEmployeeFirstName,
						PostingEmployeeLastName = item.PostingEmployeeLastName,
						JobLocationStreet = item.JobLocationStreet,
						JobLocationCity = item.JobLocationCity,
						JobLocationState = item.JobLocationState,
						JobLocationZipCode = item.JobLocationZipCode,
						JobScheduled = item.JobScheduled
					});
				}
			}
			catch (Exception)
			{
                return RedirectToAction("ErrorPage", "Home");
            }

			return View(vmList);
		}

		public ActionResult Accept(int id)
		{
			try
			{
				_employeeManager = new EmployeeManager();
				string email = User.Identity.GetUserName();
				int userID = _employeeManager.RetreiveEmployeeIdByEmail(email);
				_employeeJobPostManager.AcceptJobPosting(id, userID);
			}
			catch (Exception)
			{
                return RedirectToAction("ErrorPage", "Home");
            }

			return RedirectToAction("Index", "EmployeeJob", new { });
		}
    }
}