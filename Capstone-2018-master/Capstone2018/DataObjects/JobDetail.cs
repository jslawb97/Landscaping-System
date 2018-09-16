using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/08
    /// 
    /// Job Detail contains the JobLocationDetail, Customer and a list of ServicePackages for a specific Job object
    /// </summary>
    public class JobDetail
    {
        public Job Job { get; set; }
        public JobLocationDetail JobLocationDetail { get; set; }
        public Customer Customer { get; set; }
        public List<ServicePackage> ServicePackages { get; set; }
    }
}
