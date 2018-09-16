using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Represents an attribute of a specific job location
    /// </summary>
    public class JobLocationAttribute
    {
        public int JobLocationID { get; set; }
        public string JobLocationAttributeTypeID { get; set; }
        public int Value { get; set; }
    }
}
