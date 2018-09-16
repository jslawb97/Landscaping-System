using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class SupplyOrderManager : ISupplyOrderManager
    {
        ISupplyOrderAccessor _supplyOrderAccessor; 

        // Constructor for real run
        public SupplyOrderManager()
        {
            _supplyOrderAccessor = new SupplyOrderAccessor();
        }

        // Constructor for test run
        public SupplyOrderManager(ISupplyOrderAccessor supplyOrderAccessor)
        {
            _supplyOrderAccessor = supplyOrderAccessor;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method that adds a new supply order to the list
        /// </summary>
        /// <param name="order">The Supply Order to add</param>
        /// <returns></returns>
        public int CreateSupplyOrderNoJob(SupplyOrder order)
        {
            if (order.SupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Order ID Value");
            }
            if (order.EmployeeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Employee ID Value");
            }
            if (order.JobID != null && order.JobID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Job ID Value");
            }
            return _supplyOrderAccessor.CreateSupplyOrderNoJob(order);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method that removes a current supply order from the list
        /// </summary>
        /// <param name="supplyOrderID"> The id of the order to delete</param>
        /// <returns></returns>
        public int DeleteSupplyOrder(int supplyOrderID, string supplyStatusID)
        {
            if (supplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }
            return _supplyOrderAccessor.DeactivateSupplyOrderByID(supplyOrderID, supplyStatusID);
        }

        public int EditSupplyOrder(SupplyOrder oldOrder, SupplyOrder newOrder)
        {
            int result = 0;
            if (oldOrder.SupplyOrderID < Constants.IDSTARTVALUE || newOrder.SupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }
            if (oldOrder.EmployeeID < Constants.IDSTARTVALUE || newOrder.EmployeeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Employee ID Value");
            }
            if (oldOrder.JobID < Constants.IDSTARTVALUE || newOrder.JobID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Job ID Value");
            }
            if (oldOrder.SupplyOrderID != newOrder.SupplyOrderID)
            {
                throw new ArgumentOutOfRangeException("Supply Order ID Mismatch");
            }
            result = _supplyOrderAccessor.EditSupplyOrder(oldOrder, newOrder);

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method that edits an existing item in the list
        /// </summary>
        /// <param name="oldOrder">The original order</param>
        /// <param name="newOrder">The updated order</param>
        /// <returns></returns>
        public int EditSupplyOrderNoJob(SupplyOrder oldOrder, SupplyOrder newOrder)
        {
            int result = 0;
            if (oldOrder.SupplyOrderID < Constants.IDSTARTVALUE || newOrder.SupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }
            if (oldOrder.EmployeeID < Constants.IDSTARTVALUE || newOrder.EmployeeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Employee ID Value");
            }
            if (oldOrder.SupplyOrderID != newOrder.SupplyOrderID)
            {
                throw new ArgumentOutOfRangeException("Supply Order ID Mismatch");
            }
            result = _supplyOrderAccessor.EditSupplyOrderNoJob(oldOrder, newOrder);

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method that retrieves a supply order by the given id
        /// </summary>
        /// <param name="id"> The id of the supply order to get</param>
        /// <returns></returns>
        public SupplyOrder RetrieveSupplyOrderByID(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }
            return _supplyOrderAccessor.RetrieveSupplyOrderByID(id);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/08
        /// 
        /// Method to retrieve a list of Supply Orders
        /// </summary>
        /// <returns></returns>
        public List<SupplyOrder> RetrieveSupplyOrderList()
        {
            List<SupplyOrder> supplyOrderList = null;

            try
            {
                supplyOrderList = _supplyOrderAccessor.RetrieveSupplyOrderList();
            }
            catch (Exception)
            {
                throw;
            }

            return supplyOrderList;
        }
    }
}
