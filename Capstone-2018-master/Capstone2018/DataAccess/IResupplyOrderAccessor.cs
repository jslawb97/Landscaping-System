using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public interface IResupplyOrderAccessor
    {
        List<ResupplyOrder> RetrieveResupplyOrderList();
        ResupplyOrder RetrieveResupplyOrderByID(int resupplyOrderID);
        int CreateResupplyOrder(ResupplyOrder resupplyOrder);
        int EditResupplyOrder(ResupplyOrder oldResupplyOrder, ResupplyOrder newResupplyOrder);
        int DeleteResupplyOrderByID(int resupplyOrderID);
        int EditResupplyOrderStatus(int resupplyOrderID, string oldStatus, string newStatus);
    }
}
