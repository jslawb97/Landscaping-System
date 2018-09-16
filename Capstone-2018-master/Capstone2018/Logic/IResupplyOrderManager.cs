using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    public interface IResupplyOrderManager
    {
        List<ResupplyOrder> RetrieveResupplyOrderList();
        ResupplyOrder RetrieveResupplyOrderByID(int resupplyOrderID);
        bool EditResupplyOrder(ResupplyOrder oldResupplyOrder, ResupplyOrder newResupplyOrder);
        int CreateResupplyOrder(ResupplyOrder resupplyOrder);
        bool DeleteResupplyOrderByID(int resupplyOrderID);
        bool EditResupplyOrderStatus(int resupplyOrderID, string oldStatus, string newStatus);
    }
}
