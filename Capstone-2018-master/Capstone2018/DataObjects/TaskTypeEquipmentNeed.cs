using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/04/04
    /// </summary>
    public class TaskTypeEquipmentNeed
    {
        public int TaskTypeID { get; set; }
        public string EquipmentTypeID { get; set; }
        public int HoursOfWork { get; set; }
        public int TaskTypeEquipmentNeedID { get; set; }
    }
}
