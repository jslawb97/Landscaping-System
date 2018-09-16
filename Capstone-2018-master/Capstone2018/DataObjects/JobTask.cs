using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class JobTask
    {
        public int JobTaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isDone { get; set; }
    }
}
