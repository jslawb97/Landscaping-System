using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Created by Zachary Hall
    /// 3/27/2018
    /// 
    /// Detail object for TaskTypeEmployeeNeed records
    /// </summary>
    public class TaskTypeEmployeeNeedDetail
    {
        public TaskType TaskType { get; set; }
        public TaskTypeEmployeeNeed TaskTypeEmployeeNeed { get; set; }
    }
}
