using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class ServiceOfferingController : Controller
    {
        private static readonly IServiceOfferingManager _serviceOfferingManager = new ServiceOfferingManager();

        // GET: ServiceOffering
        public ActionResult Index()
        {
            return View(_serviceOfferingManager.RetrieveServiceOfferingList());
        }

        // GET: ServiceOffering/Details/5
        public ActionResult Details(int id)
        {
            var spList = _serviceOfferingManager.RetrieveServiceOfferingList();
            var serviceOffering = spList.Find(sp => sp.ServiceOfferingID.Equals(id));

            return View(serviceOffering);
        }

        // GET: ServiceOffering/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceOffering/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceOffering/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var soList = _serviceOfferingManager.RetrieveServiceOfferingList();
            var serviceOffering = soList.Find(so  => so.ServicePackageID.Equals(id));

            return View(serviceOffering);
        }

        // POST: ServiceOffering/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceOffering/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _serviceOfferingManager.DeleteServiceOfferingByID(id);
            return View();
        }

        // POST: ServiceOffering/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}