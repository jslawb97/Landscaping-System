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
    /// Class for MakeModels
    /// </summary>
    public class MakeModel
    {
        public int MakeModelID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool Active { get; set; }
        public int? MaintenanceChecklistID { get; set; }
    }
}
