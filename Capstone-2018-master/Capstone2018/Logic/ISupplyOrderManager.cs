using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ISupplyOrderManager
    {
        List<SupplyOrder> RetrieveSupplyOrderList();

        SupplyOrder RetrieveSupplyOrderByID(int id);

        int EditSupplyOrder(SupplyOrder oldOrder, SupplyOrder newOrder);

        int EditSupplyOrderNoJob(SupplyOrder oldOrder, SupplyOrder newOrder);

        int CreateSupplyOrderNoJob(SupplyOrder order);

        int DeleteSupplyOrder(int supplyOrderID, string supplyStatusID);

    }
}
