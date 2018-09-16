using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISupplyOrderItemAccessor
    {
        int CreateSupplyOrderItem(SupplyOrderItem orderitem);

        List<SupplyOrderItem> RetrieveSupplyOrderItemByID(int id);

        int EditSupplyOrderItem(SupplyOrderItem oldOrderItem, SupplyOrderItem newOrderItem);

        int DeleteSupplyOrderItemByID(int id);
        int EditSupplyOrderLineQuantityReceived(SupplyOrder supplyOrder
            , List<SupplyOrderItem> newSupplyOrderItems
            , List<SupplyOrderItem> oldSupplyOrderItems);
    }
}
