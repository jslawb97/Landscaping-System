using System;
using System.Runtime.Serialization;
using DataObjects;
using Logic;

namespace RestApi.Models.SpecialOrders
{
    /// <summary>
    /// Keeps track of SpecialOrder info used in API reporting
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public class ApiSpecialOrder
    {
        /// <summary>
        /// The SpecialOrder ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public int SpecialOrderId { get; set; }

        /// <summary>
        /// The Vendor ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public int VendorId { get; set; }

        /// <summary>
        /// The SpecialOrder Date
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        [DataMember]
        public DateTime? Date { get; set; }

        /// <summary>
        /// The SupplyStatus ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public string SupplyStatusId { get; set; }

        /// <summary>
        /// API related data about the vendor who requested this SpecialOrder
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public ApiVendor Vendor { get; set; }

        /// <summary>
        /// Constructor for serialization
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public ApiSpecialOrder()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="order">The SpecialOrder to retrieve API related data for</param>
        public ApiSpecialOrder(SpecialOrder order)
        {
            SpecialOrderId = order.SpecialOrderID;
            VendorId = order.EmployeeID;
            Date = order.Date;
            SupplyStatusId = order.SupplyStatusID;
            Vendor = order.VendorID == null
                ? null
                : new ApiVendor(new VendorManager().RetrieveVendorByID(order.VendorID));
        }
    }
}