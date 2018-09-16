using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/13
    /// 
    /// Class for the creation of EmployeeCertification objects
    /// </summary>
    /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
    public class EmployeeCertification
    {
        public int CertificationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }
}
