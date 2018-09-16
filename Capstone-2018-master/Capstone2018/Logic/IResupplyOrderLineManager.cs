using System;
using System.Collections.Generic;
using DataObjects;
namespace Logic
{
    public interface IResupplyOrderLineManager
    {
        bool CreateResupplyOrderLine(ResupplyOrderLine resupplyOrderLine);
        List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderID(int resupplyID);
        bool EditResupplyOrderLine(ResupplyOrderLineDetail resupplyOrderLineDetail1, ResupplyOrderLineDetail resupplyOrderLineDetail2);
        bool DeleteResupplyOrderLineByResupplyOrderID(int resupplyOrderID);
        bool DeleteResupplyOrderLineByResupplyOrderLineID(int resupplyOrderLineID);
        List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(int resupplyOrderID);
        bool EditResupplyOrderLineQtyReceivedByID(int id, int oldQtyReceived, int newQtyReceived);
        bool EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(int id);
    }
}
