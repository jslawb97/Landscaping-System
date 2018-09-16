using DataObjects;
using Logic;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Here is a list of available services.";
            ServicePackageManager manager = new ServicePackageManager();
            var servicePackages = manager.RetrieveServicePackageList();
            ViewBag.ServicePackageOne = servicePackages[0].ServicePackageID;
            ViewBag.ServicePackageTwo = servicePackages[1].ServicePackageID;
            ViewBag.ServicePackageThree = servicePackages[2].ServicePackageID;
            return View();
        }

        public ActionResult JobRequests()
        {
            ViewBag.Message = "Make a job request here.";

            return View();
        }
        public ActionResult ErrorPage()
        {
            ViewBag.Message = "Error page";

            return View();
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
            ServicePackageManager _servicePackageManager = new ServicePackageManager();
            var spList = _servicePackageManager.RetrieveServicePackageList();
            var servicePackage = spList.Find(sp => sp.ServicePackageID.Equals(id));

            if (!servicePackages.Contains(servicePackage))
            {
                servicePackages.Add(servicePackage);
            }
            System.Web.HttpContext.Current.Session["ServicePackages"] = servicePackages;
            return RedirectToAction("Create", "Job");
        }

    }
}