using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/04
    /// 
    /// Class for MaintenanceChecklists
    /// </summary>
    public class MaintenanceChecklist
    {
        public int MaintenanceChecklistID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
		
        public override string ToString()
        {
                return MaintenanceChecklistID.ToString();
        }

    }
}
