using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// 2018/02/19
    /// 
    /// Class for PaymentTypes
    /// </summary>
    public class PaymentType
    {
        public string PaymentTypeID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
