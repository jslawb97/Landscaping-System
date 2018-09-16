using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/02/22
    /// 
    /// Data transfer object for a Service Package
    /// </summary>
    public class ServicePackage
    {
        public int ServicePackageID { get; set; }
        public int TaskID { get; set; }
        public int ServiceItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
