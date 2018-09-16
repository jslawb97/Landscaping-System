using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// John Miller
    /// Created 2018/03/24
    /// 
    /// Details for a Task Type. Contains a list of TaskTypes.
    /// </summary>
    public class TaskTypeDetail
    {
        public TaskType TaskType { get; set; }
        public JobLocationAttribute JobLocationAttribute { get; set; }
    }
}
