using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Jacob Conley
    /// Created 2018/03/08
    /// 
    /// Interface for the SupplyOrderAccessor
    /// </summary>
    public interface ISupplyOrderAccessor
    {

        List<SupplyOrder> RetrieveSupplyOrderList();

        SupplyOrder RetrieveSupplyOrderByID(int id);

        int EditSupplyOrder(SupplyOrder oldOrder, SupplyOrder newOrder);

        int EditSupplyOrderNoJob(SupplyOrder oldOrder, SupplyOrder newOrder);

        int CreateSupplyOrderNoJob(SupplyOrder order);

        int DeactivateSupplyOrderByID(int id, string supplyStatusID);
    }
}
