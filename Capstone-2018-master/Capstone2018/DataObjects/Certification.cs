using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    ///	Weston Olund
    /// Created 2018/01/26
    /// 
    /// Class for certifications
    /// </summary>
    public class Certification
    {
        public int CertificationID { get; set; }
        public string CertificationName { get; set; }
        public string CertificationDescription { get; set; }
        public bool Active { get; set; }
    }
}
