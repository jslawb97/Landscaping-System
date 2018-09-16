using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// 2018/04/20
    /// 
    /// Added QtyReceived
    /// </summary>
    public class ResupplyOrderLine
    {
        public int ResupplyOrderLineID { get; set; }
        public int ResupplyOrderID { get; set; }
        public int SupplyItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int QtyReceived { get; set; }
    }
}
