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
    /// Job Location data object
    /// </summary>
    public class JobLocation
    {
        public int JobLocationID { get; set; }
        public int CustomerID { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Comments { get; set; }
        public bool Active { get; set; }

        public string AddressDisplay
        {
            get
            {
                return Street + ", " + City + ", " + State + ", " + ZipCode;
            }
        }
    }
}
