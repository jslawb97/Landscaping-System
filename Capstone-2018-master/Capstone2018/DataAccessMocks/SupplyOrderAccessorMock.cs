using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class SupplyOrderAccessorMock : ISupplyOrderAccessor
    {

        private List<SupplyOrder> _supplyOrderList = new List<SupplyOrder>();

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Mock constructor to add data to the supply order list
        /// </summary>
        public SupplyOrderAccessorMock()
        {
            _supplyOrderList.Add(new SupplyOrder()
            {
                SupplyOrderID = 1000000,
                EmployeeID = 1000000,
                JobID = null,
                SupplyStatusID = "Delivered",
                Date = new DateTime(2018, 3, 30)
            });
            _supplyOrderList.Add(new SupplyOrder()
            {
                SupplyOrderID = 1000001,
                EmployeeID = 1000001,
                JobID = 1000000,
                SupplyStatusID = "Pending Delivery",
                Date = new DateTime(2018, 3, 30)
            });
        }

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Method to add mock supply orders 
        /// </summary>
        public int CreateSupplyOrderNoJob(SupplyOrder order)
        {
            int result = 0;

            _supplyOrderList.Add(order);

            if (_supplyOrderList.Contains(order))
            {
                result = 1;
            }

            return result;
        }


        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Method to delete mock orders
        /// </summary>
        public int DeactivateSupplyOrderByID(int id, string supplyStatusID)
        {
            int result = 0;

            bool existed = _supplyOrderList.Remove(_supplyOrderList.Find(o => o.SupplyOrderID == id));

            if (_supplyOrderList.Contains(_supplyOrderList.Find(o => o.SupplyOrderID == id)) == false && existed == true)
            {
                result = 1;
            }

            return result;
        }


        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Method to edit mock orders
        /// </summary>
        public int EditSupplyOrder(SupplyOrder oldOrder, SupplyOrder newOrder)
        {
            int result = 0;

            bool existed = _supplyOrderList.Exists(o => o.SupplyOrderID == oldOrder.SupplyOrderID);

            if (existed == true)
            {
                int index = _supplyOrderList.IndexOf(_supplyOrderList.Find(o => o.SupplyOrderID == oldOrder.SupplyOrderID));
                _supplyOrderList[index] = newOrder;
                if (_supplyOrderList[index].Equals(newOrder))
                {
                    result = 1;
                }
            }

            return result;
        }

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Method to edit mock orders
        /// </summary>
        public int EditSupplyOrderNoJob(SupplyOrder oldOrder, SupplyOrder newOrder)
        {
            int result = 0;

            bool existed = _supplyOrderList.Exists(o => o.SupplyOrderID == oldOrder.SupplyOrderID);

            if (existed == true)
            {
                int index = _supplyOrderList.IndexOf(_supplyOrderList.Find(o => o.SupplyOrderID == oldOrder.SupplyOrderID));
                _supplyOrderList[index] = newOrder;
                if (_supplyOrderList[index].Equals(newOrder))
                {
                    result = 1;
                }
            }

            return result;
        }


        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Method to retrieve mock order by id
        /// </summary>
        public SupplyOrder RetrieveSupplyOrderByID(int id)
        {
            SupplyOrder order = null;

            order = _supplyOrderList.Find(o => o.SupplyOrderID == id);

            return order;
        }


        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/08
        /// 
        /// Method to retrieve a list of mock orders
        /// </summary>
        public List<SupplyOrder> RetrieveSupplyOrderList()
        {
            List<SupplyOrder> list = null;

            list = _supplyOrderList;

            return list;
        }

    }
}
