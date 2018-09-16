using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class ServiceItemController : Controller
    {
        private static readonly IServiceItemManager _serviceItemManager = new ServiceItemManager();

        // GET: ServiceItem
        public ActionResult Index()
        {
            return View(_serviceItemManager.RetrieveServiceItemList());
        }

        // GET: ServiceItem/Details/5
        public ActionResult Details(int id)
        {
            var siList = _serviceItemManager.RetrieveServiceItemList();
            var serviceItem = siList.Find(si => si.ServiceItemID.Equals(id));

            return View(serviceItem);
        }

        // GET: ServiceItem/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceItem/Create
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

        // GET: ServiceItem/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var siList = _serviceItemManager.RetrieveServiceItemList();
            var serviceItem = siList.Find(si => si.ServiceItemID.Equals(id));

            return View(serviceItem);
        }

        // POST: ServiceItem/Edit/5
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

        // GET: ServiceItem/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _serviceItemManager.DeactivateServiceItemByID(id);
            return View();
        }

        // POST: ServiceItem/Delete/5
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