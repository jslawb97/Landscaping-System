using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Brady Feller
    /// Created: 2018/02/05
    /// 
    /// A class that is able to reference multiple objects at once if needed
    /// </summary>
    public class EquipmentTypeDetail
    {
        public EquipmentType EquipmentType { get; set; }
        public InspectionChecklist InspectionChecklist { get; set; }
        public PrepChecklist PrepChecklist { get; set; }
    }
}
