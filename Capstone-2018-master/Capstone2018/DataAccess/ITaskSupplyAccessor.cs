using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// Mike Mason
    /// Created 2018/04/05
    /// 
    /// Interface for the TaskSupplyAccessor
    /// </summary>
    public interface ITaskSupplyAccessor
    {
        /// <summary>
        /// Mike Mason
        /// Created on 2018/04/05
        /// 
        /// Gets a list of TaskSuppy detail records
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        List<TaskSupplyDetail> RetrieveTaskSupplyDetailList(int JobID);
        int EditTaskSupplyQuantity(TaskSupplyDetail oldTaskSupply, TaskSupplyDetail newTaskSupply);
    }
}
