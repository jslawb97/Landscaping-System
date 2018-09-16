using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/08
    /// 
    /// Job data object
    /// </summary>
    public class Job
    {
        public int JobID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
            "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? DateScheduled { get; set; }
        public int EmployeeID { get; set; }
        public int JobLocationID { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string Comments { get; set; }
        public int CustomerID { get; set; }
        public bool Active { get; set; }
        public DateTime? DateTarget { get; set; }
    }
}
