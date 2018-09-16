using DataObjects;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    [Authorize(Roles = "Foreman")]
    public class JobTasksController : Controller
    {
        private IJobTaskManager _jobTaskManager = new JobTaskManager();

        /// <summary>
        /// Brittany Ward
        /// Created 2018/05/08
        /// 
        /// Retrieves the JobTask list
        /// </summary>
        /// <returns></returns>
        // GET: /JobTask/
        public ActionResult Index()

        {
            return View(_jobTaskManager.RetrieveJobTaskList());
        }

        /// <summary>
        /// Brittany Ward
        /// Created 2018/05/08
        /// 
        /// Retrieves a detail page of a JobTask
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: /JobTask/Details/5
        public ActionResult Details(int id)
        {
            var jobTaskList = _jobTaskManager.RetrieveJobTaskList();
            var jobTask = jobTaskList.Find(jt => jt.JobTaskID.Equals(id));
            
            return View(jobTask);
        }

        /// <summary>
        /// Brittany Ward
        /// Brady Feller
        /// Created 2018/05/08
        /// 
        /// Edits the IsDone field in the database which determines whether a 
        /// job item has been completed
        /// 
        /// GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET: /JobTask/Edit/5
        public ActionResult Edit(int id)
        {
            var jtList = _jobTaskManager.RetrieveJobTaskList();
            JobTask jobTask = jtList.Find(jt => jt.JobTaskID == id);

            return View(jobTask);
        }

        /// <summary>
        /// Brittany Ward
        /// Brady Feller
        /// Created 2018/05/08
        /// 
        /// Edits the IsDone field in the database which determines whether a 
        /// job item has been completed
        /// 
        /// POST
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobTask"></param>
        /// <returns></returns>
        // POST: /JobTask/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, JobTask jobTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jtList = _jobTaskManager.RetrieveJobTaskList();
                    var oldJobTask = jtList.Find(jt => jt.JobTaskID == id);

                    _jobTaskManager.EditIsDone(jobTask, oldJobTask);

                    return RedirectToAction("Index");
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
