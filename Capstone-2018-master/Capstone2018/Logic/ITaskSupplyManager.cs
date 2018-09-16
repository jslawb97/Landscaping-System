using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    /// <summary>
    /// Mike Mason
    /// Created on 2018/04/05
    /// 
    /// Manager interface for task supply
    /// </summary>
    public interface ITaskSupplyManager
    {

        /// <summary>
        /// Mike Mason
        /// Created on 2018/04/05
        /// 
        /// Gets a list of TaskSupplyDetail objects by the associated JobID
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        List<TaskSupplyDetail> RetrieveTaskSupplyDetailList(int jobID);
        bool EditTaskSupplyQuantity(TaskSupplyDetail oldTaskSupply, TaskSupplyDetail newTaskSupply);
    }
}
