using System;

namespace RestApi.Models.Resupply
{
    /// <summary>
    /// A model for the Supply request view
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public class ApiResupplyRequest
    {
        /// <summary>
        /// The vendor ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public int VendorId { get; set; }

        /// <summary>
        /// The date of the Supply Order request
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public DateTime? Date { get; set; }

        /// <summary>
        /// An empty constructor for serialization
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public ApiResupplyRequest()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public ApiResupplyRequest(int vendorId, DateTime? date)
        {
            VendorId = vendorId;
            Date = date;
        }
    }
}