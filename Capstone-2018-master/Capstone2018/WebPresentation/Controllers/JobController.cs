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
    public class JobController : Controller
    {
        private IJobManager _jobManager = new JobManager();
        private ICustomerManager _customerManager = new CustomerManager();
        private IJobLocationManager _jobLocationManager = new JobLocationManager();
        
        /// <summary>
        /// Brady Feller
        /// Jacob Conley
        /// Created 2018/05/02
        /// 
        /// Creates a job for the Customer currently logged in
        /// </summary>
        /// <returns></returns>
        // GET: Job/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            var jobLocations = new List<JobLocation>();
            try
            {
                // retrieves the customerID by their email
                var customerEmail = User.Identity.GetUserName();
                var customerID = _customerManager.RetrieveCustomerByEmail(customerEmail);
                ViewBag.customerID = customerID.CustomerID;
                jobLocations = _jobLocationManager.RetrieveJobLocationListByCustomerID(customerID.CustomerID);
            }
            catch (Exception ex)
            {
                ViewBag.Ex = ex;
                return View("Error");
            }

            ViewBag.JobLocations = jobLocations;

            return View();
        }

        /// <summary>
        /// Brady Feller
        /// Jacob Conley
        /// Created 2018/05/02
        /// 
        /// Creates a job for the Customer currently logged in
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        // POST: Job/Create
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult Create(Job job)
        {
            //need to use the customer's email to find their customerID
            try
            {                
                var customerEmail = User.Identity.GetUserName();
                var customerID = _customerManager.RetrieveCustomerByEmail(customerEmail);
                job.CustomerID = customerID.CustomerID;

                if (ModelState.IsValid)
                {
                    try
                    {
                        //assign the customerID to the job
                        ViewBag.customerID = customerID.CustomerID;

                        // create a job
                        int jobID = _jobManager.CreateJobWeb(job);

                        var servicePackage = (List<ServicePackage>)System.Web.HttpContext.Current.Session["ServicePackages"];
                        // after creating the job, we need to grab the newly created JobID
                        // and also the ServicePackageID the user selected, then we need
                        // to create the JobService table using those values
                        _jobManager.CreateUpdateJobServicePackage(jobID, servicePackage);

                        // Remove any ordered service packages and reset the session
                        servicePackage.Clear();

                        System.Web.HttpContext.Current.Session["ServicePackages"] = servicePackage;

                        // Redirects to homepage
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        var jobLocations = new List<JobLocation>();
                        try
                        {
                            ViewBag.customerID = customerID.CustomerID;
                            jobLocations = _jobLocationManager.RetrieveJobLocationListByCustomerID(customerID.CustomerID);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Ex = ex;
                            return View("Error");
                        }

                        ViewBag.JobLocations = jobLocations;

                        return View();
                    }
                }
                else
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();
                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }
    }
}
