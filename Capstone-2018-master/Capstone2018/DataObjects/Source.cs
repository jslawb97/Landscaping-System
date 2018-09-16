using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/02/19
    /// </summary>
    public class Source
    {
        public int SourceID { get; set; }
        public int SupplyItemID { get; set; }
        public int SpecialOrderItemID { get; set; }
        public int VendorID { get; set; }
        public int MinimumOrderQTY { get; set; }
        public decimal PriceEach { get; set; }
        public int LeadTime { get; set; }
        public bool Active { get; set; }
    }
}
