using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public string Name { get; set; }
        public string Rep { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
