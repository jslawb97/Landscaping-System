using System;
using System.Collections.Generic;
using DataObjects;
namespace DataAccess
{
    public interface IResupplyOrderLineAccessor
    {
        int CreateResupplyOrderLine(DataObjects.ResupplyOrderLine resupplyOrderLine);
        List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderID(int resupplyID);
        int EditResupplyOrderLine(ResupplyOrderLineDetail oldResupplyOrderLineDetail, ResupplyOrderLineDetail newResupplyOrderLineDetail);
        int DeleteResupplyOrderLineByID(int resupplyOrderLineID);
        int DeleteResupplyOrderLineByResupplyOrderID(int resupplyOrderID);
        List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(int resupplyOrderID);
        int EditResupplyOrderLineQtyReceivedByID(int id, int oldQtyReceived, int newQtyReceived);
        int EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(int id);
    }
}
