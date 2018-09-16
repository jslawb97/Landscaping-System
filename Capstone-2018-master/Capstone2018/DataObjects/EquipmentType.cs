using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/13
    /// 
    /// Class for the creation of EquipmentType objects
    /// </summary>
    public class EquipmentType
    {
        public string EquipmentTypeID { get; set; }
        public int? InspectionChecklistID { get; set; }
        public int? PrepChecklistID { get; set; }
        public bool Active { get; set; }
    }
}
