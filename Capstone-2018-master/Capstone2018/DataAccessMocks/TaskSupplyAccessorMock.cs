using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{

    /// <summary>
    /// Mike Mason
    /// Created on 2018/04/05
    /// 
    /// Mock Accessor for TaskSupplyAccessor
    /// </summary>
    public class TaskSupplyAccessorMock : ITaskSupplyAccessor
    {
        private List<TaskSupplyDetail> _detail;

        public TaskSupplyAccessorMock()
        {
            _detail = new List<TaskSupplyDetail>();

            _detail.Add(new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000000,
                TaskName = "Mow the Lawn",
                SupplyItemName = "Pipewrench",
                TaskTypeSupplyNeedQuantity = 2,
                SupplyItemQuantityInStock = 14,
                TaskSupplyQuantity = 2

            });
            _detail.Add(new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000001,
                TaskName = "Mow the Lawn",
                SupplyItemName = "Monkey Wrench",
                TaskTypeSupplyNeedQuantity = 5,
                SupplyItemQuantityInStock = 6,
                TaskSupplyQuantity = 7

            });
            _detail.Add(new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000002,
                TaskName = "Mow the Lawn",
                SupplyItemName = "Pipewrench",
                TaskTypeSupplyNeedQuantity = 22,
                SupplyItemQuantityInStock = 6,
                TaskSupplyQuantity = 8
            });
            _detail.Add(new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000003,
                TaskName = "Mow the Lawn",
                SupplyItemName = "Pipewrench",
                TaskTypeSupplyNeedQuantity = 22,
                SupplyItemQuantityInStock = 6,
                TaskSupplyQuantity = 0
            });
            _detail.Add(new TaskSupplyDetail
            {
                TaskSupplyTaskSupplyID = 1000004,
                TaskName = "Mow the Lawn",
                SupplyItemName = "Pipewrench",
                TaskTypeSupplyNeedQuantity = 22,
                SupplyItemQuantityInStock = 6,
                TaskSupplyQuantity = 5
            });
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/07
        /// 
        /// Mock method to change a TaskSupply's quantity
        /// </summary>
        /// <param name="oldTaskSupply"></param>
        /// <param name="newTaskSupply"></param>
        /// <returns></returns>
        public int EditTaskSupplyQuantity(TaskSupplyDetail oldTaskSupply
            , TaskSupplyDetail newTaskSupply)
        {
            int result = 0;

            foreach(var ts in _detail)
            {
                if(ts.TaskSupplyTaskSupplyID == oldTaskSupply.TaskSupplyTaskSupplyID)
                {
                    if(ts.TaskSupplyQuantity == oldTaskSupply.TaskSupplyQuantity)
                    {
                        ts.TaskSupplyQuantity = newTaskSupply.TaskSupplyQuantity;
                        result++;
                        break;
                    }
                    else
                    {
                        throw new ApplicationException("Old TaskSupply Quantity does not equal current TaskSupply quantity");
                    }
                }
            }

            return result;
        }
		
        /// Mike Mason
        /// Created on 2018/04/05
        /// 
        /// Mocks getting a list of details from a datastore
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskSupplyDetail> RetrieveTaskSupplyDetailList(int jobID)
        {
            List<TaskSupplyDetail> detail = null;
            try
            {
                detail = _detail;
                if(detail == null)
                {
                    throw new ApplicationException("The detail list was null");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return detail;
        }
    }
}