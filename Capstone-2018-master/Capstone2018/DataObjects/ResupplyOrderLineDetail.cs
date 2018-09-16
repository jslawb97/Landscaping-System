using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ResupplyOrderLineDetail : ResupplyOrderLine
    {
        public string NameOfItem { get; set; }
        public string VendorName { get; set; }
        public int VendorID { get; set; }

    }
}
