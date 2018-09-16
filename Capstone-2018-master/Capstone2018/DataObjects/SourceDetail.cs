using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SourceDetail : Source
    {
        public string VendorName { get; set; }
        public string SupplyItemName { get; set; }
        public string SpecialItemName { get; set; }
    }
}
