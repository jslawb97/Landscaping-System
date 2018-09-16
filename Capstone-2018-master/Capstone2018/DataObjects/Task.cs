using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Task
    {
        public int TaskID { get; set; }
        public int TaskTypeID { get; set; }
        public int ServiceItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }


    }
}
