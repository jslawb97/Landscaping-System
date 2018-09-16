using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TaskDetail
    {
        public DataObjects.Task Task { get; set; }

        public ServiceItem ServiceItem { get; set; }

        public TaskType TaskType { get; set; }
    }
}
