using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TaskType
    {
        public int TaskTypeID { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string JobLocationAttributeTypeID { get; set; }

        public bool Active { get; set; }

        public string Units
        {
            get
            {
                return Quantity + " " + JobLocationAttributeTypeID;
            }

        }
    }
}
