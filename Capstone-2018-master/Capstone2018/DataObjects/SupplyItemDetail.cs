using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplyItemDetail
    {
        public SupplyItem SupplyItem { get; set; }
        public decimal PriceEach { get; set; }
        public string VendorName { get; set; }
        public int VendorID { get; set; }
        public int SourceID { get; set; }
    }
}
