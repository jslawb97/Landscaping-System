using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/04/06
    /// 
    /// Class for TaskSupply objects
    /// </summary>
    public class TaskSupply
    {
        public int TaskSupplyID { get; set; }
        public int SupplyItemID { get; set; }
        public int Quantity { get; set; }
        public int JobID { get; set; }
        public int TaskSupplyNeedID { get; set; }
    }
}
