using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ISupplyOrderItemManager
    {
        List<SupplyOrderItem> RetrieveSupplyOrderItemsByID(int id);

        int EditSupplyOrderItem(SupplyOrderItem oldOrderItem, SupplyOrderItem newOrderItem);

        int CreateSupplyOrderItem(SupplyOrderItem orderItem);

        int DeleteSupplyOrderItem(int supplyOrderLineID);
        bool EditSupplyOrderLineQuantityReceived(SupplyOrder supplyOrder
            , List<SupplyOrderItem> newSupplyOrderItems
            , List<SupplyOrderItem> oldSupplyOrderItems);
    }
}
