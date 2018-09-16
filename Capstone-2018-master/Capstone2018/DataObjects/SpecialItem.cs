using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/01/31
    /// 
    /// Class for creating SpecialItem objects with set data fields
    /// </summary>
    public class SpecialItem
    {
        public int SpecialOrderItemID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
