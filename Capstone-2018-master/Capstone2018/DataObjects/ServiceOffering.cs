using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ServiceOffering
    {
        /// <summary>
        /// Created: 2018/02/20
        /// 
        /// [ServiceOfferingID][ServicePackageID][Name][Description]
        /// added properties of ServiceOffering Class
        /// </summary>
        public int ServiceOfferingID { get; set; }
        public int ServicePackageID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
