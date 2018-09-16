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
    public class TaskTypeEquipmentNeedDetail
    {
        public TaskTypeEquipmentNeed TaskTypeEquipmentNeed { get; set; }
        public TaskType TaskType { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}
