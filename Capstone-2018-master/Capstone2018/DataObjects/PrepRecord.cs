using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Badis Saidani
    /// Created 2018/02/24
    /// 
    /// Class for PrepRecord
    /// </summary>
    public class PrepRecord
    {
        public int PrepRecordID { get; set; }
        public int EquipmentID { get; set; }
        public int EmployeeID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
