using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Mike Mason
    /// Created 2018/04/05
    /// 
    /// Details for a Task Supply. Contains a list of TaskSupplies.
    /// </summary>
    public class TaskSupplyDetail
    {
        public string TaskName { get; set; }
        public string SupplyItemName { get; set; }
        public int TaskTypeSupplyNeedQuantity { get; set; }
        public int SupplyItemQuantityInStock { get; set; }
        public int TaskSupplyQuantity { get; set; }
        public int TaskSupplyTaskSupplyID { get; set; }
    }
}
