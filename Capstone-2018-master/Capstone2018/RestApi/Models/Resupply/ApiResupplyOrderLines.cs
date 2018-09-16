using System.Collections.Generic;
using DataObjects;

namespace RestApi.Models.Resupply
{
    /// <summary>
    /// API related information about the underlying ResupplyOrderLines
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </remarks>
    public class ApiResupplyOrderLines
    {
        /// <summary>
        /// A list of ResupplyOrders
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public List<ApiResupplyOrderLine> ResupplyOrderList { get; set; } = new List<ApiResupplyOrderLine>();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public ApiResupplyOrderLines()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="orderLines">The underlying list of ResupplyOrderLineDetails to be shown</param>
        public ApiResupplyOrderLines(List<ResupplyOrderLineDetail> orderLines)
        {
            foreach (var detail in orderLines)
            {
                ResupplyOrderList.Add(new ApiResupplyOrderLine(detail));
            }
        }
    }
}