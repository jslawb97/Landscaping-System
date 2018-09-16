using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// 2018/04/20
    /// 
    /// Added PriceEach
    /// </summary>
    public class SpecialOrderLineDetail
    {
        public string ItemName { get; set; }
        public SpecialOrderLine Line { get; set; }
        public decimal PriceEach { get; set; }
    }
}
