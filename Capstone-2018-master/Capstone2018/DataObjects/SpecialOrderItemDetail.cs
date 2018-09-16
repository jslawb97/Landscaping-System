using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Reuben Cassell
    /// Created 4/20/2018 blaze it
    /// 
    /// Detail for use in the Special Order
    /// </summary>
    public class SpecialOrderItemDetail
    {
        public SpecialItem SpecialItem { get; set; }
        public decimal PriceEach { get; set; }
        public string VendorName { get; set; }
        public int VendorID { get; set; }
        public int SourceID { get; set; }
    }
}
