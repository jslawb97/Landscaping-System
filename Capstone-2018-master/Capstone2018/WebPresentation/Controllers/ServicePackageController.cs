using DataObjects;
using Logic;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    /// <summary>
    /// Zach Murphy
    /// Created on 2018/4/6
    /// </summary>
    public class ServicePackageController : Controller
    {

        private static readonly IServicePackageManager _servicePackageManager = new ServicePackageManager();

        // GET: ServicePackage
        /// <summary>
        /// Marshall Sejkora
        /// Modified: 5/10/18
        /// Fixed the code
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<ServicePackage> servicePackageList = null;
            try
            {
                servicePackageList = _servicePackageManager.RetrieveServicePackageList();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(servicePackageList);
        }

        // GET: ServicePackage/Details/5
        /// <summary>
        /// Marshall Sejkora
        /// Modified: 5/10/18
        /// Fixed the code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            List<ServicePackage> spList = null;
            try
            {
                spList = _servicePackageManager.RetrieveServicePackageList();
            }
            catch (Exception)
            {
                return View("Error");
            }

            var servicePackage = spList.Find(sp => sp.ServicePackageID.Equals(id));

            return View(servicePackage);
        }

        
        // GET: ServicePackage/Edit/5
        /// <summary>
        /// Marshall Sejkora
        /// Modified: 5/10/18
        /// Added try and catch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            List<ServicePackage> spList = null;
            try
            {
                spList = _servicePackageManager.RetrieveServicePackageList();
            }
            catch (Exception)
            {
                return View("Error");
            }
            var servicePackage = spList.Find(sp => sp.ServicePackageID.Equals(id));

            return View(servicePackage);
        }

        // GET: ServicePackage/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditSubmit(ServicePackage newServicePackage)
        {
            List<ServicePackage> spList = null;
            try
            {
                spList = _servicePackageManager.RetrieveServicePackageList();
            }
            catch (Exception)
            {
                return View("Error");
            }
            var oldServicePackage = spList.Find(sp => sp.ServicePackageID.Equals(newServicePackage.ServicePackageID));

            _servicePackageManager.EditServicePackage(oldServicePackage, newServicePackage);

            return RedirectToAction("Index");
        }

        // GET: ServicePackage/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Deactivate(int id)
        {
            _servicePackageManager.DeactivateServicePackage(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Jacob Conley
        /// 2018/05/09
        /// 
        /// Removes a service package from the session list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Remove(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servicePackage = (List<ServicePackage>)System.Web.HttpContext.Current.Session["ServicePackages"];

            servicePackage.Remove(servicePackage.Find(sp => sp.ServicePackageID == id));

            return RedirectToAction("Details", "ServicePackage", new { id = id });
        }

        /// <summary>
        /// Jacob Conley
        /// 2018/05/09
        /// 
        /// Adds a selected service package to the session list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var servicePackages = (List<ServicePackage>)System.Web.HttpContext.Current.Session["ServicePackages"];

            var spList = _servicePackageManager.RetrieveServicePackageList();
            var servicePackage = spList.Find(sp => sp.ServicePackageID.Equals(id));

            servicePackages.Add(servicePackage);
            System.Web.HttpContext.Current.Session["ServicePackages"] = servicePackages;
            return RedirectToAction("Details", "ServicePackage", new { id = id });
        }
    }
}
