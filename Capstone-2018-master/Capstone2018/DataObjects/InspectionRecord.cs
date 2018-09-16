using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/03/07
    /// 
    /// Class for InspectionRecords
    /// </summary>
    public class InspectionRecord
    {
        public int InspectionRecordID { get; set; }
        public int EquipmentID { get; set; }
        public int EmployeeID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
