using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Reuben Cassell
    /// Created 2/23/2018
    /// 
    /// Data Object for a Special Order Line
    /// 
    /// Zachary Hall
    /// 2018/04/20
    /// 
    /// Added QtyReceived
    /// </summary>
    public class SpecialOrderLine
    {
        // The ID of the Special Order Line
        public int SpecialOrderLineID { get; set; }

        // The ID of the Special Order the line belongs to 
        public int SpecialOrderID { get; set; }

        // The ID of the Special Item belonging to the line
        public int SpecialOrderItemID { get; set; }

        // The quantity of the Special Item belonging to the line
        public int Quantity { get; set; }

        // The amount received
        public int QtyReceived { get; set; }
    }
}
