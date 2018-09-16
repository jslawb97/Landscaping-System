using DataObjects;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class PersonalEquipmentController : Controller
    {
        private IEmployeeManager _employeeManager = new EmployeeManager();
        private IPersonalEquipmentManager _personalEquipmentManager = new PersonalEquipmentManager();

        // GET: PersonalEquipment
        public ActionResult EmployeeList()
        {
            List<Employee> employees = new List<Employee>();
            employees = _employeeManager.RetrieveEmployeeListByActive();

            return View(employees);
        }

        public ActionResult EquipmentAssignment(int employeeID)
        {
            Employee employee = _employeeManager.RetrieveEmployeeByID(employeeID);
            List<PersonalEquipment> assignedEquipment = _personalEquipmentManager.RetrieveAssignedPersonalEquipmentByEmployeeID(employeeID);

            System.Web.HttpContext.Current.Session["CurrentEmployee"] = employee;
            System.Web.HttpContext.Current.Session["CurrentEmployeeAssignedPE"] = assignedEquipment;

            return View();
        }

        public PartialViewResult UnassignedPersonalEquipment()
        {
            List<PersonalEquipment> unassignedPersonalEquipment = _personalEquipmentManager.RetrievePersonalEquipmentByAssigned(false);

            return PartialView(unassignedPersonalEquipment);
        }

        public RedirectToRouteResult AssignPersonalEquipment(int personalEquipmentID, int employeeID)
        {
            _personalEquipmentManager.CreatePersonalEquipmentAssignment(employeeID, personalEquipmentID);

            return RedirectToAction("EquipmentAssignment", "PersonalEquipment", new { employeeID });
        }

        public RedirectToRouteResult UnassignPersonalEquipment(int personalEquipmentID, int employeeID)
        {
            _personalEquipmentManager.DeletePersonalEquipmentAssignment(employeeID, personalEquipmentID);

            return RedirectToAction("EquipmentAssignment", "PersonalEquipment", new { employeeID });
        }
    }
}