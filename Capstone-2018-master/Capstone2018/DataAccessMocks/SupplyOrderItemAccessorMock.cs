using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMocks
{
    public class SupplyOrderItemAccessorMock : ISupplyOrderItemAccessor
    {
        private List<SupplyOrderItem> _supplyOrderItemList = new List<SupplyOrderItem>();

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Mock constructor to add data to the supply order item list
        /// </summary>
        public SupplyOrderItemAccessorMock()
        {
            _supplyOrderItemList.Add(new SupplyOrderItem()
            {
                SupplyOrderID = 1000000,
                SupplyItemID = 10000000,
                SupplyOrderLineID = 1000000,
                Quantity = 20,
                QuantityReceived = 0
            });
            _supplyOrderItemList.Add(new SupplyOrderItem()
            {
                SupplyOrderID = 1000001,
                SupplyItemID = 10000001,
                SupplyOrderLineID = 1000001,
                Quantity = 12,
                QuantityReceived = 0
            });
        }

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to add mock supply order items 
        /// </summary>
        public int CreateSupplyOrderItem(SupplyOrderItem orderitem)
        {
            int result = 0;

            _supplyOrderItemList.Add(orderitem);

            if (_supplyOrderItemList.Contains(orderitem))
            {
                result = 1;
            }

            return result;
        }

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to delete a mock order item
        /// </summary>
        public int DeleteSupplyOrderItemByID(int id)
        {
            int result = 0;

            bool existed = _supplyOrderItemList.Remove(_supplyOrderItemList.Find(o => o.SupplyOrderID == id));

            if (_supplyOrderItemList.Contains(_supplyOrderItemList.Find(o => o.SupplyOrderID == id)) == false && existed == true)
            {
                result = 1;
            }

            return result;
        }

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to edit a mock order item
        /// </summary>
        public int EditSupplyOrderItem(SupplyOrderItem oldOrderItem, SupplyOrderItem newOrderItem)
        {
            int result = 0;
            
            bool existed = _supplyOrderItemList.Exists(o => o.SupplyOrderID == oldOrderItem.SupplyOrderID);

            if (existed == true)
            {
                int index = _supplyOrderItemList.IndexOf(_supplyOrderItemList.Find(o => o.SupplyOrderID == oldOrderItem.SupplyOrderID));
                _supplyOrderItemList[index] = newOrderItem;
                if (_supplyOrderItemList[index].Equals(newOrderItem))
                {
                    result = 1;
                }
            }

            return result;
        }

        /// /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to retrieve a list of mock order items
        /// </summary>
        public List<SupplyOrderItem> RetrieveSupplyOrderItemByID(int id)
        {
            List<SupplyOrderItem> list = null;

            list = _supplyOrderItemList.FindAll(o => o.SupplyOrderID == id);

            return list;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Mock method to edit the QuantityReceived fields of
        /// SupplyOrderLines
        /// </summary>
        /// <param name="supplyOrder"></param>
        /// <param name="newSupplyOrderItems"></param>
        /// <param name="oldSupplyOrderItems"></param>
        /// <returns></returns>
        public int EditSupplyOrderLineQuantityReceived(SupplyOrder supplyOrder, List<SupplyOrderItem> newSupplyOrderItems, List<SupplyOrderItem> oldSupplyOrderItems)
        {
            int rowcount = 0;
            // Join original, old, and new lines on the ID
            var linesToEdit = _supplyOrderItemList
                .Join(newSupplyOrderItems
                , originalLine => originalLine.SupplyOrderLineID
                , newLine => newLine.SupplyOrderLineID
                , (originalLine, newLine) => new { originalLine, newLine })
                .Join(oldSupplyOrderItems
                , joined => joined.newLine.SupplyOrderLineID
                , oldLine => oldLine.SupplyOrderLineID
                , (joined, oldLine) => new { joined.originalLine, joined.newLine, oldLine });
            foreach (var joined in linesToEdit)
            {
                if(joined.originalLine.QuantityReceived
                    == joined.oldLine.QuantityReceived)
                {
                    joined.originalLine.QuantityReceived
                        = joined.newLine.QuantityReceived;
                    rowcount++;
                }
            }
            return rowcount;
        }
    }
}
