using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/04/02
    /// 
    /// Class for InspectionRecordDetails
    /// </summary>
    public class InspectionRecordDetail : InspectionRecord
    {
        public string EquipmentName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeFirstName { get; set; }
    }
}
