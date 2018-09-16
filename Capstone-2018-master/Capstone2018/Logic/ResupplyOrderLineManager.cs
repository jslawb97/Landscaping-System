using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class ResupplyOrderLineManager : IResupplyOrderLineManager
    {
        IResupplyOrderLineAccessor _resupplyOrderLineAccessor;

        // constructor for unit tests
        public ResupplyOrderLineManager(IResupplyOrderLineAccessor resupplyOrderLineAccessor)
        {
            _resupplyOrderLineAccessor = resupplyOrderLineAccessor;
        }

        // constructor for actual run
        public ResupplyOrderLineManager()
        {
            _resupplyOrderLineAccessor = new ResupplyOrderLineAccessor();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Manages creating a resupply order line
        /// </summary>
        /// <param name="resupplyOrderLine"></param>
        /// <returns></returns>
        public bool CreateResupplyOrderLine(ResupplyOrderLine resupplyOrderLine)
        {
            if (resupplyOrderLine.ResupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID value.");
            }
            if (resupplyOrderLine.SupplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID value.");
            }
            if (resupplyOrderLine.Quantity < 1)
            {
                throw new ApplicationException("Quantity ordered must be at least 1");
            }
            try
            {
                return (0 != _resupplyOrderLineAccessor.CreateResupplyOrderLine(resupplyOrderLine));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Manages retrieving a list of resupply order line details
        /// </summary>
        /// <param name="resupplyID"></param>
        /// <returns></returns>
        public List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderID(int resupplyID)
        {
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = null;
            try
            {
                resupplyOrderLineDetailList = _resupplyOrderLineAccessor.RetrieveResupplyOrderLineDetailListByResupplyOrderID(resupplyID);
            }
            catch (Exception)
            {
                throw;
            }
            return resupplyOrderLineDetailList;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Manages editing a resupply order line
        /// </summary>
        /// <param name="oldResupplyOrderLineDetail"></param>
        /// <param name="newResupplyOrderLineDetail"></param>
        /// <returns></returns>
        public bool EditResupplyOrderLine(ResupplyOrderLineDetail oldResupplyOrderLineDetail, ResupplyOrderLineDetail newResupplyOrderLineDetail)
        {
            if (newResupplyOrderLineDetail.ResupplyOrderLineID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad Resupply Order Line ID value.");
            }
            if (newResupplyOrderLineDetail.ResupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad Resupply Order ID value.");
            }
            if (newResupplyOrderLineDetail.SupplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad Supply Item ID value.");
            }
            if (newResupplyOrderLineDetail.Quantity < 1)
            {
                throw new ApplicationException("Quantity must be greater than 1.");
            }
            var result = false;
            try
            {
                result = (0 != _resupplyOrderLineAccessor.EditResupplyOrderLine(oldResupplyOrderLineDetail, newResupplyOrderLineDetail));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Manages deleting a resupply order line
        /// </summary>
        /// <param name="resupplyOrderLineDetail"></param>
        /// <returns></returns>
        public bool DeleteResupplyOrderLineByResupplyOrderLineID(int resupplyOrderLineID)
        {
            var result = false;
            try
            {
                result = (0 != _resupplyOrderLineAccessor.DeleteResupplyOrderLineByID(resupplyOrderLineID));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to delete all resupply order lines assosciated with a resupply order
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public bool DeleteResupplyOrderLineByResupplyOrderID(int resupplyOrderID)
        {
            var result = false;
            try
            {
                if (0 != _resupplyOrderLineAccessor.DeleteResupplyOrderLineByResupplyOrderID(resupplyOrderID))
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
        /// Gets a list but includes qty received with query
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(int resupplyOrderID)
        {
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = null;
            try
            {
                resupplyOrderLineDetailList = _resupplyOrderLineAccessor.RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(resupplyOrderID);
            }
            catch (Exception)
            {
                throw;
            }
            return resupplyOrderLineDetailList;
        }

        /// <summary>
        /// Zachary Hall
        /// 2019/04/19
        /// Edits the QtyReceived record of a orderline record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldQtyReceived"></param>
        /// <param name="newQtyReceived"></param>
        /// <returns></returns>
        public bool EditResupplyOrderLineQtyReceivedByID(int id, int oldQtyReceived, int newQtyReceived)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (1 == _resupplyOrderLineAccessor.EditResupplyOrderLineQtyReceivedByID(id, oldQtyReceived, newQtyReceived))
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
        /// Updates all orderline records' QtyReceived to the same value as Quantity for a certain ResupplyOrderID 
        /// </summary>
        public bool EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (0 < _resupplyOrderLineAccessor.EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(id))
                {
                    result = true;
                }
                else
                {
                    throw new ApplicationException("No records were updated.");
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
