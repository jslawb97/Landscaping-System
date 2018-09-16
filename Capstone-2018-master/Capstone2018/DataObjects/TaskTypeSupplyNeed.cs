using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TaskTypeSupplyNeed
    {
        public int TaskTypeSupplyNeedID { get; set; }
        public int TaskTypeID { get; set; }
        public int SupplyItemID { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
