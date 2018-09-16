using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplyOrderItem
    {
        public int SupplyOrderLineID { get; set; }
        public int SupplyItemID { get; set; }
        public int Quantity { get; set; }
        public int SupplyOrderID { get; set; }
        public int QuantityReceived { get; set; }
    }
}
