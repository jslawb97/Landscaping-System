using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class EmployeePersonalEquipment
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2018
        /// 
        /// ID of the Assigned Personal Equipment
        /// </summary>
        public int PersonalEquipmentID { get; set; }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/30/2018
        /// 
        /// ID of the Employee assigned the equipment
        /// </summary>
        public int EmployeeID { get; set; }
    }
}
