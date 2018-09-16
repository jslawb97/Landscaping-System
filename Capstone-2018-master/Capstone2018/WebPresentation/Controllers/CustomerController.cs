using DataObjects;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebPresentation.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerManager _customerManager = new CustomerManager();

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Customer Details page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var customer = _customerManager.RetrieveCustomerById(id);

            return View(customer);
        }

        [Authorize]
        public ActionResult Index()
        {
            var customerList = _customerManager.RetrieveCustomerList();

            return View(customerList);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Customer Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var cuList = _customerManager.RetrieveCustomerList();
            Customer customer = cuList.Find(cu => cu.CustomerID == id);

            return View(customer);
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Customer Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cuList = _customerManager.RetrieveCustomerList();
                    var oldCustomer = cuList.Find(cu => cu.CustomerID == id);

                    _customerManager.EditCustomer(customer, oldCustomer);

                    return RedirectToAction("Details");
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

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Customer Create page
        /// </summary>
        /// <returns></returns>
        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/04/20
        /// 
        /// Customer Create page
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _customerManager.CreateCustomer(customer);

                    return RedirectToAction("Register", "Account");
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


        /// <summary>
        /// Sam Dramstad
        /// 2018/5/3
        /// 
        /// Deactivates a customer by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            _customerManager.DeactivateCustomer(id);

            return RedirectToAction("Index");
        }
    }
}
