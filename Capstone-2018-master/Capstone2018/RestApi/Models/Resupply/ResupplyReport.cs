using System;
using Logic;

namespace RestApi.Models.Resupply
{
    /// <summary>
    /// Contains static methods which retrieve respply report data.
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public static class ResupplyReport
    {
        /// <summary>
        /// An text representation of each supply order for a given vendor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="vendorId">The ID of the vendor to retrieve data for</param>
        /// <param name="date">(Optional) The date on which data is being requested for</param>
        /// <returns></returns>
        public static ApiResponse<ApiResupplyOrders> GetOrders(int vendorId, DateTime? date = null)
        {
            try
            {
                var resupplyOrders = new ApiResupplyOrders();

                var orderMgr = new ResupplyOrderManager();
                var allOrders = orderMgr.RetrieveResupplyOrderList();

                if (vendorId == 0)
                {
                    allOrders.ForEach(order => resupplyOrders.AddOrder(new ApiResupplyOrder(order)));
                }
                else
                {
                    allOrders.FindAll(order => order.VendorID == vendorId && (date == null || order.Date == date))
                        .ForEach(order => resupplyOrders.AddOrder(new ApiResupplyOrder(order)));
                }

                if (resupplyOrders.ResupplyOrderList.Count < 1)
                {
                    if (date == null)
                    {
                        return new ApiResponse<ApiResupplyOrders>(true, "There are no recorded orders for this vendor");
                    }

                    return new ApiResponse<ApiResupplyOrders>(true,
                        "There are no recorded orders for this vendor on the given date", resupplyOrders);
                }

                return new ApiResponse<ApiResupplyOrders>(true, "A list of ResupplyOrders is attached", resupplyOrders);
            }
            catch (Exception)
            {
                return new ApiResponse<ApiResupplyOrders>("An error occured in the API, please try again.");
            }
        }
    }
}