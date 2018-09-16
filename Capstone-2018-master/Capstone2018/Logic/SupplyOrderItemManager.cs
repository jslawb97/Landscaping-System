using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class SupplyOrderItemManager : ISupplyOrderItemManager
    {
        ISupplyOrderItemAccessor _supplyOrderItemAccessor;

        // Constructor for real run
        public SupplyOrderItemManager()
        {
            _supplyOrderItemAccessor = new SupplyOrderItemAccessor();
        }

        // Constructor for test run
        public SupplyOrderItemManager(ISupplyOrderItemAccessor supplyOrderItemAccessor)
        {
            _supplyOrderItemAccessor = supplyOrderItemAccessor;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/14
        /// 
        /// Method that adds a new supply order item to the list
        /// </summary>
        /// <param name="orderItem">The supply order item to add</param>
        /// <returns></returns>
        public int CreateSupplyOrderItem(SupplyOrderItem orderItem)
        {
            if (orderItem.SupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Order Item ID Value");
            }
            if (orderItem.SupplyOrderLineID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Order ID Value");
            }
            if (orderItem.SupplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Item ID Value");
            }
            return _supplyOrderItemAccessor.CreateSupplyOrderItem(orderItem);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/14
        /// 
        /// Method that removes a current supply order item from the list
        /// </summary>
        /// <param name="supplyOrderItemID"> The id of the order item to delete</param>
        /// <returns></returns>
        public int DeleteSupplyOrderItem(int supplyOrderItemID)
        {
            if (supplyOrderItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }
            return _supplyOrderItemAccessor.DeleteSupplyOrderItemByID(supplyOrderItemID);
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/14
        /// 
        /// Method that edits an existing item in the list
        /// </summary>
        /// <param name="oldOrderItem"></param>
        /// <param name="newOrderItem"></param>
        /// <returns></returns>
        public int EditSupplyOrderItem(SupplyOrderItem oldOrderItem, SupplyOrderItem newOrderItem)
        {
            int result = 0;
            if (oldOrderItem.SupplyOrderID < Constants.IDSTARTVALUE || newOrderItem.SupplyOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Order ID Value");
            }
            if (oldOrderItem.SupplyItemID < Constants.IDSTARTVALUE || newOrderItem.SupplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Item ID Value");
            }
            if (oldOrderItem.SupplyOrderLineID < Constants.IDSTARTVALUE || newOrderItem.SupplyOrderLineID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Supply Item ID Value");
            }
            if (oldOrderItem.SupplyOrderLineID != newOrderItem.SupplyOrderLineID)
            {
                throw new ArgumentOutOfRangeException("Supply Order Line ID Mismatch");
            }
            result = _supplyOrderItemAccessor.EditSupplyOrderItem(oldOrderItem, newOrderItem);
            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/14
        /// 
        /// Method to retrieve a list of Supply Order Items
        /// </summary>
        /// <param name="id">The ID of the Supply Order the items belong to</param>
        /// <returns></returns>
        public List<SupplyOrderItem> RetrieveSupplyOrderItemsByID(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }

            List<SupplyOrderItem> supplyOrderItemList = null;

            try
            {
                supplyOrderItemList = _supplyOrderItemAccessor.RetrieveSupplyOrderItemByID(id);
            }
            catch (Exception)
            {
                throw;
            }

            return supplyOrderItemList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Method to update the quantity received of all
        /// SupplyOrderLines associated with a SupplyOrder
        /// </summary>
        /// <param name="supplyOrder"></param>
        /// <param name="newSupplyOrderItems"></param>
        /// <param name="oldSupplyOrderItems"></param>
        /// <returns></returns>
        public bool EditSupplyOrderLineQuantityReceived(SupplyOrder supplyOrder, List<SupplyOrderItem> newSupplyOrderItems, List<SupplyOrderItem> oldSupplyOrderItems)
        {
            var result = false;
            var changesMade = false;
            var validOrderLineIDs = true;
            foreach (var newItem in newSupplyOrderItems)
            {
                if (newItem.SupplyOrderLineID.IsValidID())
                {
                    validOrderLineIDs = false;
                }
                foreach (var oldItem in oldSupplyOrderItems)
                {
                    if (newItem.SupplyOrderLineID
                        == oldItem.SupplyOrderLineID
                        && newItem.QuantityReceived
                        != oldItem.QuantityReceived)
                    {
                        changesMade = true;
                        break;
                    }
                }
            }
            if(!changesMade || !supplyOrder.SupplyOrderID.IsValidID()) {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }
            result = (0 !=_supplyOrderItemAccessor.EditSupplyOrderLineQuantityReceived(supplyOrder
                , newSupplyOrderItems, oldSupplyOrderItems));

            return result;
        }
    }
}
