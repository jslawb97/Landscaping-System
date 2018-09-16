using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class ResupplyOrderManager : IResupplyOrderManager
    {
        IResupplyOrderAccessor _resupplyOrderAccessor;
        IEmployeeAccessor _employeeAccessor;
        // Constructor for real run
        public ResupplyOrderManager()
        {
            _resupplyOrderAccessor = new ResupplyOrderAccessor();
            _employeeAccessor = new EmployeeAccessor();
        }

        // Constructor for unit tests
        public ResupplyOrderManager(IResupplyOrderAccessor resupplyOrderAccessor)
        {
            _resupplyOrderAccessor = resupplyOrderAccessor;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to edit a resupply order
        /// </summary>
        /// <param name="oldResupplyOrder"></param>
        /// <param name="newResupplyOrder"></param>
        /// <returns></returns>
        public bool EditResupplyOrder(ResupplyOrder oldResupplyOrder, ResupplyOrder newResupplyOrder)
        {
            if (newResupplyOrder.ResupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad Resupply Order ID value");
            }
            if (newResupplyOrder.EmployeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad Employee ID value");
            }
            if (newResupplyOrder.SupplyStatusID == null)
            {
                throw new ApplicationException("You must select a supply status");
            }
            if (newResupplyOrder.SupplyStatusID.Length < 1)
            {
                throw new ApplicationException("You must enter a supply status");
            }
            if (newResupplyOrder.SupplyStatusID.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The supply status must be less than 100 characters");
            }

            var result = false;

            try
            {
                result = (0 != _resupplyOrderAccessor.EditResupplyOrder(oldResupplyOrder, newResupplyOrder));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to retrieve a supply order by id
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public ResupplyOrder RetrieveResupplyOrderByID(int resupplyOrderID)
        {
            if (resupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID value");
            }

            try
            {
                return _resupplyOrderAccessor.RetrieveResupplyOrderByID(resupplyOrderID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to retrieve a resupply order list
        /// </summary>
        /// <returns></returns>
        public List<ResupplyOrder> RetrieveResupplyOrderList()
        {
            List<ResupplyOrder> resupplyOrderList = null;
            try
            {
                resupplyOrderList = _resupplyOrderAccessor.RetrieveResupplyOrderList();
            }
            catch (Exception)
            {
                throw;
            }
            return resupplyOrderList;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// 
        /// method to create a resupply order
        /// </summary>
        /// <param name="resupplyOrder"></param>
        /// <returns></returns>
        public int CreateResupplyOrder(ResupplyOrder resupplyOrder)
        {
            if (resupplyOrder.EmployeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad employee ID value");
            }
            if (resupplyOrder.SupplyStatusID.Length <= 0)
            {
                throw new ApplicationException("You must enter a Supply Status");
            }
            if (resupplyOrder.SupplyStatusID.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("Supply status must be less than 100 characters");
            }
            int result;
            try
            {
                result = _resupplyOrderAccessor.CreateResupplyOrder(resupplyOrder);
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    throw new ApplicationException("New record not created");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to delete a resupply order by its id
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public bool DeleteResupplyOrderByID(int resupplyOrderID)
        {
            if(resupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (0 !=_resupplyOrderAccessor.DeleteResupplyOrderByID(resupplyOrderID))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// 
        /// updates the resupplyorder supply status
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public bool EditResupplyOrderStatus(int resupplyOrderID, string oldStatus, string newStatus)
        {
            if (resupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (1 == _resupplyOrderAccessor.EditResupplyOrderStatus(resupplyOrderID, oldStatus, newStatus))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
