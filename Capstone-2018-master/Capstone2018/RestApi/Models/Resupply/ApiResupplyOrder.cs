using System;
using System.Runtime.Serialization;
using DataObjects;
using Logic;

namespace RestApi.Models.Resupply
{
    /// <summary>
    /// Keeps track of Resupply info used in API reporting
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </summary>
    public class ApiResupplyOrder
    {
        /// <summary>
        /// The ResupplyOrder ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public int ResupplyOrderId { get; set; }

        /// <summary>
        /// The Employee ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The ResupplyOrder Date
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        [DataMember]
        public DateTime? Date { get; set; }

        /// <summary>
        /// The SupplyStatus ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public string SupplyStatusId { get; set; }

        /// <summary>
        /// API related data about the vendor who requested this ResupplyOrder
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public ApiVendor Vendor { get; set; }

        /// <summary>
        /// Detailed information about this ResupplyOrder
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public ApiResupplyOrderLines ResupplyOrderLine { get; set; }

        /// <summary>
        /// Empty constructor for JSON serialization
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public ApiResupplyOrder()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="order">The ResupplyOrder to retrieve API related data for</param>
        public ApiResupplyOrder(ResupplyOrder order)
        {
            ResupplyOrderId = order.ResupplyOrderID;
            EmployeeId = order.EmployeeID;
            Date = order.Date;
            SupplyStatusId = order.SupplyStatusID;
            Vendor = new ApiVendor(new VendorManager().RetrieveVendorByID(order.VendorID));

            try
            {
                var orderLineList = new ResupplyOrderLineManager()
                    .RetrieveResupplyOrderLineDetailListByResupplyOrderID(ResupplyOrderId);

                ResupplyOrderLine = new ApiResupplyOrderLines(orderLineList);
            }
            catch (ApplicationException ae)
            {
                if (ae.InnerException != null && !ae.InnerException.Message.Equals("No data found"))
                {
                    throw;
                }

                ResupplyOrderLine = new ApiResupplyOrderLines();
            }
        }
    }
}