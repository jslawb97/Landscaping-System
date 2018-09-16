using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPresentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Logic;


namespace WebPresentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager _userManager;

        public AdminController()
        {
            _userManager = new UserManager();
        }

        //// GET: Admin
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var users = userManager.Users;

            return View(users);
        }

        //// GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (id == null)
            {
                return View("Error");
            }
            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return View("Error");
            }

            try
            {
                var roles = userManager.GetRoles(id).ToList();
                ViewBag.Roles = roles;
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(applicationUser);
        }

        //// GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ApplicationUsers.Add(applicationUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(applicationUser);
        //}

        //// GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser applicationUser = userManager.FindById(id);

            if (applicationUser == null)
            {
                return View("Error");
            }

            try
            {
                var roles = userManager.GetRoles(id).ToList();
                ViewBag.Roles = roles;

                List<string> allRoles = new List<string>();

                //this should include a list of all roles within the program
                allRoles.Add("Admin");
                allRoles.Add("Employee");
                allRoles.Add("Applicant");
                allRoles.Add("Customer");
                allRoles.Add("Delivery");
                allRoles.Add("Equipment Scheduler");
                allRoles.Add("Foreman");
                allRoles.Add("Inspector");
                allRoles.Add("Job Scheduler");
                allRoles.Add("Labor Scheduler");
                allRoles.Add("Maintenance");
                allRoles.Add("Manager");
                allRoles.Add("Mechanic");
                allRoles.Add("Prep");
                allRoles.Add("Supply Clerk");
                allRoles.Add("Temp");
                allRoles.Add("Worker");


                var unusedRoles = allRoles.Except(roles);
                ViewBag.UnusedRoles = unusedRoles;
            }
            catch (Exception)
            {
                return View("Error");
            }            

            return View(applicationUser);
        }

        //// POST: Admin/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicationUser).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(applicationUser);
        //}

        //// GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (id == null)
            {
                return View("Error");
            }

            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return View("Error");
            }
            return View(applicationUser);
        }

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser applicationUser = userManager.FindById(id);
            try
            {
                userManager.Delete(applicationUser);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Sam Dramstad
        /// April 27th, 2018
        /// 
        /// Removes a role from a selected user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Remove(string id)
        {
            if (id == null)
            {
                return View("Error");
            }

            char[] splitter = { '|' };
            string[] parts = id.Split(splitter);

            string roleID = parts[0];
            string useID = parts[1];

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser applicationUser = userManager.FindById(useID);

            if (applicationUser == null)
            {
                return View("Error");
            }

            try
            {
                userManager.RemoveFromRole(useID, roleID);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Edit", "Admin", new { id = useID });
        }

        /// <summary>
        /// Sam Dramstad
        /// April 27th, 2018
        /// 
        /// Adds a role to a selected user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            char[] splitter = { '|' };
            string[] parts = id.Split(splitter);

            string roleID = parts[0];
            string useID = parts[1];

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser applicationUser = userManager.FindById(useID);

            if (applicationUser == null)
            {
                return View("Error");
            }

            try
            {
                userManager.AddToRole(useID, roleID);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Edit", "Admin", new { id = useID });
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
