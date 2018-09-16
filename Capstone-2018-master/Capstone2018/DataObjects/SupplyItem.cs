using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/02/01
    /// 
    /// Class for the creation of Supply Item objects
    /// </summary>
    public class SupplyItem
    {
        public int SupplyItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int QuantityInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public SupplyItem()
        {

        }
    }
}
