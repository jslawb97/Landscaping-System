using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Reuben Cassell
    /// Created 2/27/2018
    /// 
    /// A order for special items
    /// </summary>
    public class SpecialOrder
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        public int SpecialOrderID { get; set; }

        /// <summary>
        /// The ID of the employee who made the order
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// The ID of the job the order is needed for
        /// </summary>
        public int? JobID { get; set; }

        /// <summary>
        /// The status of the order
        /// </summary>
        public string SupplyStatusID { get; set; }

        /// <summary>
        /// The date the order was made
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The vendor the order is being made to 
        /// </summary>
        public int? VendorID { get; set; }
    }
}
