using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Sam Dramstad 
    /// Created on 2018/04/06
    /// 
    /// Object for a task/equipment data object.
    /// 
    /// Jacob Conley
    /// Updated: 2018/04/20
    /// 
    /// Made properties and added equipmentID
    /// </summary>
    public class TaskEquipment
    {
        public int EmployeeID { get; set; }

        public int JobID { get; set; }

        public int EquipmentID { get; set; }

    }
}
