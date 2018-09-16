using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TaskTypeSupplyNeedDetail
    {
        public string ItemName { get; set; }
        public string TaskName { get; set; }
        public TaskTypeSupplyNeed Supply { get; set; }
    }
}
