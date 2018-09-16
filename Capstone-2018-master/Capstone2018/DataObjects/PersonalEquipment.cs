using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Reuben Cassell
    /// Created 4-27-2018
    /// 
    /// A personal equipment item 
    /// </summary>
    public class PersonalEquipment
    {
        // The ID of the PersonalEquipment item
        public int PersonalEquipmentID { get; set; }

        // The type of PersonalEquipment
        public string PersonalEquipmentType { get; set; }

        // The Name of the PersonalEquipment item
        public string Name { get; set; }

        // Description of the PersonalEquipment item
        public string Description { get; set; }

        // The Status of the PersonalEquipment item
        public string PersonalEquipmentStatus { get; set; }

        // The PersonalEquipment item is assigned or not 
        public bool Assigned { get; set; }
    }
}
