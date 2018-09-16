using DataObjects;
using Logic;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class JobLocationController : Controller
    {
        private IJobLocationManager _jobLocationManager = new JobLocationManager();
        private ICustomerManager _customerManager = new CustomerManager();

        /// <summary>
        /// Brady Feller
        /// Created 2018/05/03
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        // GET: JobLocation
        public ActionResult Index(int id)
        {
            return View(_jobLocationManager.RetrieveJobLocationListByCustomerID(id));
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/05/03
        /// 
        /// GET Create JobLocation
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        // GET: JobLocation/Create
        public ActionResult Create()
        {
            try
            {
                // retrieves the customerID by their email
                var customerEmail = User.Identity.GetUserName();
                var customerID = _customerManager.RetrieveCustomerByEmail(customerEmail);
                ViewBag.customerID = customerID.CustomerID;

            }
            catch (Exception ex)
            {
                ViewBag.Ex = ex;
                return View("Error");
            }

            return View();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/05/03
        /// 
        /// POST Create JobLocation
        /// </summary>
        /// <param name="jobLocation"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        // POST: JobLocation/Create
        [HttpPost]
        public ActionResult Create(JobLocation jobLocation)
        {
            var customerEmail = User.Identity.GetUserName();
            var customerID = _customerManager.RetrieveCustomerByEmail(customerEmail);

            jobLocation.CustomerID = customerID.CustomerID;
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.customerID = customerID.CustomerID;

                    _jobLocationManager.CreateJobLocation(jobLocation);

                    return RedirectToAction("Create", "Job");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
