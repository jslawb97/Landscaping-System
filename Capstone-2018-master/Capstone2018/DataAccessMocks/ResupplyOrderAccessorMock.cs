using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class ResupplyOrderAccessorMock : IResupplyOrderAccessor
    {
        private List<ResupplyOrder> _resupplyOrderList = new List<ResupplyOrder>();

        /// <summary>
        /// Weston Olund
        /// created on 2018/03/08
        /// 
        /// Constructor to add data to resupply order list
        /// </summary>
        public ResupplyOrderAccessorMock()
        {
            _resupplyOrderList.Add(new ResupplyOrder()
            {
                ResupplyOrderID = Constants.IDSTARTVALUE,
                EmployeeID = Constants.IDSTARTVALUE,
                Date = DateTime.Now,
                SupplyStatusID = "Mock supply status 1"
            });
            _resupplyOrderList.Add(new ResupplyOrder()
            {
                ResupplyOrderID = Constants.IDSTARTVALUE + 1,
                EmployeeID = Constants.IDSTARTVALUE + 1,
                Date = DateTime.Now,
                SupplyStatusID = "Mock supply status 2"
            });
        }

        /// <summary>
        /// Weston Olund 
        /// created on 2018/03/08
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyOrder"></param>
        /// <returns></returns>
        public int CreateResupplyOrder(ResupplyOrder resupplyOrder)
        {
            if (resupplyOrder.ResupplyOrderID == Constants.IDSTARTVALUE * 500)
            {
                throw new ApplicationException("Database error");
            }
            else return Constants.IDSTARTVALUE;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public int DeleteResupplyOrderByID(int resupplyOrderID)
        {
            int result = 0;
            foreach (var item in _resupplyOrderList)
            {
                if(item.ResupplyOrderID == resupplyOrderID)
                {
                    result++;
                }
            }
            if(result == 0)
            {
                throw new ApplicationException("Database error");
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="oldResupplyOrder"></param>
        /// <param name="newResupplyOrder"></param>
        /// <returns></returns>
        public int EditResupplyOrder(ResupplyOrder oldResupplyOrder, ResupplyOrder newResupplyOrder)
        {
            var rowsAffected = 0;
            foreach (var r in _resupplyOrderList)
            {
                if (oldResupplyOrder.ResupplyOrderID == r.ResupplyOrderID
                    && newResupplyOrder.ResupplyOrderID == r.ResupplyOrderID)
                {
                    rowsAffected++;
                }
            }
            if (rowsAffected == 0)
            {
                throw new ApplicationException("No rows affected by edit");
            }
            return rowsAffected;
        }

        public int EditResupplyOrderStatus(int resupplyOrderID, string oldStatus, string newStatus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public ResupplyOrder RetrieveResupplyOrderByID(int resupplyOrderID)
        {
            ResupplyOrder resupplyOrder = null;
            foreach (var r in _resupplyOrderList)
            {
                if (r.ResupplyOrderID == resupplyOrderID)
                {
                    resupplyOrder = r;
                }
            }
            if (resupplyOrder == null)
            {
                throw new ApplicationException("Resupply order record not found.");
            }
            return resupplyOrder;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to return mock data
        /// </summary>
        /// <returns></returns>
        public List<ResupplyOrder> RetrieveResupplyOrderList()
        {
            return _resupplyOrderList;
        }
    }
}
