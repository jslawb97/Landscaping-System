using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/04/05
    /// 
    /// TaskEquipmentDetail object class
    /// </summary>
    /// <remarks>
    /// Noah Davison
    /// Modified 2018/04/13
    /// 
    /// Added TaskID
    /// </remarks>
    public class TaskEquipmentDetail
    {
        public string TaskName { get; set; }
        public int HoursOfWork { get; set; }
        public int TaskTypeEquipmentNeedID { get; set; }
        public int EquipmentAssignedCount { get; set; }
        public int TaskID { get; set; }

        public int? EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public int TaskEquipmentID { get; set; }
    }
}
