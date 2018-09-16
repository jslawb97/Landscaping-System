using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class EmployeeCertificationDetail
    {
        public EmployeeCertification EmployeeCertification { get; set; }
        public Employee Employee { get; set; }
        public Certification Certification { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
    }
}
