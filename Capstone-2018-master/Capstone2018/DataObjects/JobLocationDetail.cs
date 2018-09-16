using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Details for a Job location. Contains a list of JobLocationAttributes
    /// </summary>
    public class JobLocationDetail
    {
        public JobLocation JobLocation { get; set; }
        public List<JobLocationAttribute> JobLocationAttributes { get; set; }

        public Customer Customer { get; set; }
    }
}
