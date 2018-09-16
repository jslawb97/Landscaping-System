using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class TaskSupplyManager : ITaskSupplyManager
    {
        private ITaskSupplyAccessor _taskSupplyAccessor;

        /// <summary>
        /// Manager Constructor for handling accessor dependency
        /// </summary>
        /// <remarks>
        /// Mike Mason
        /// Created 2018/04/05
        /// </remarks>
        public TaskSupplyManager()
        {
            _taskSupplyAccessor = new TaskSupplyAccessor();
        }
		
        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <remarks>
        /// Mike Mason
        /// Created 2018/04/05
        /// </remarks>
        /// <param name="taskSupplyAccessor"></param>
        public TaskSupplyManager(ITaskSupplyAccessor taskSupplyAccessor)
        {
            _taskSupplyAccessor = taskSupplyAccessor;
        }
		
        /// <summary>
        /// James McPherson
        /// Created 2018/04/06
        /// 
        /// Method to edit the quantity of a TaskSupply
        /// </summary>
        /// <param name="oldTaskSupply"></param>
        /// <param name="newTaskSupply"></param>
        /// <returns></returns>
        public bool EditTaskSupplyQuantity(TaskSupplyDetail oldTaskSupply, TaskSupplyDetail newTaskSupply)
        {
            bool result = false;

            if(!oldTaskSupply.TaskSupplyTaskSupplyID.IsValidID()
                || !oldTaskSupply.TaskSupplyQuantity.IsValidQuantity()
                || !newTaskSupply.TaskSupplyTaskSupplyID.IsValidID()
                || !newTaskSupply.TaskSupplyQuantity.IsValidQuantity()
                || oldTaskSupply.TaskSupplyTaskSupplyID != newTaskSupply.TaskSupplyTaskSupplyID
                || oldTaskSupply.TaskSupplyQuantity == newTaskSupply.TaskSupplyQuantity)
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }

            try
            {
                result = (0 != _taskSupplyAccessor.EditTaskSupplyQuantity(oldTaskSupply, newTaskSupply));
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Mike Mason
        /// Created: 2018/04/05
        /// 
        /// Retrieves a list of Task Supply Details
        /// </summary>
        public List<TaskSupplyDetail> RetrieveTaskSupplyDetailList(int jobID)
        {

            List<TaskSupplyDetail> taskSupplyList = null;

            try
            {
                taskSupplyList = _taskSupplyAccessor.RetrieveTaskSupplyDetailList(jobID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to process. Try again later.", ex);
            }
            return taskSupplyList;
        }
    }
}