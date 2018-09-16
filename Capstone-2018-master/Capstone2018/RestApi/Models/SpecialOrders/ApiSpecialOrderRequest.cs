using System;

namespace RestApi.Models.SpecialOrders
{
    /// <summary>
    /// A model for the Special Order request view
    /// </summary>
    public class ApiSpecialOrderRequest
    {
        /// <summary>
        /// The ID of the vendor who requested the special order
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public int VendorId { get; set; }

        /// <summary>
        /// The date of the Special Order request
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Constructor for serialization
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public ApiSpecialOrderRequest()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="vendorId"></param>
        /// <param name="date"></param>
        public ApiSpecialOrderRequest(int vendorId, DateTime? date)
        {
            VendorId = vendorId;
            Date = date;
        }
    }
}