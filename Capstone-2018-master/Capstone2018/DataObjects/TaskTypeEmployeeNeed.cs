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
    /// The need for employee hours of a specific task type
    /// </summary>
    public class TaskTypeEmployeeNeed
    {
        public int TaskTypeID { get; set; }
        public int HoursOfWork { get; set; }
        public bool Active { get; set; }
    }
}
