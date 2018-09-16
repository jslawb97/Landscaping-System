using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/01
    /// </summary>
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public string EquipmentTypeID { get; set; }
        public string Name { get; set; }
        public int MakeModelID { get; set; }
        public DateTime DatePurchased { get; set; }
        public DateTime? DateLastRepaired { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime? WarrantyUntil { get; set; }
        public string EquipmentStatusID { get; set; }
        public string EquipmentDetails { get; set; }
        public bool Active { get; set; }
    }
}
