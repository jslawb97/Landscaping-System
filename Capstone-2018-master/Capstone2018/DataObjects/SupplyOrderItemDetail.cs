using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplyOrderItemDetail
    {
        public string Name { get; set; }
        public SupplyOrderItem OrderItem { get; set; }
    }
}
