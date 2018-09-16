using System;
using Logic;

namespace RestApi.Models.SpecialOrders
{
    /// <summary>
    /// Contains static methods which retrieve special order report data.
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public static class SpecialOrderReport
    {
        /// <summary>
        /// An text representation of each special order for a given employee
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="vendorId">The ID of the vendor to retrieve data for</param>
        /// <param name="date">(Optional) The date on which data is being requested for</param>
        /// <returns></returns>
        public static ApiResponse<ApiSpecialOrders> GetOrders(int vendorId, DateTime? date = null)
        {
            try
            {
                var specialOrders = new ApiSpecialOrders();

                var orderMgr = new SpecialOrderManager();
                var allOrders = orderMgr.RetrieveSpecialOrders();

                if (vendorId == 0)
                {
                    allOrders.ForEach(order => specialOrders.AddOrder(new ApiSpecialOrder(order)));
                }
                else
                {
                    allOrders.FindAll(order => order.VendorID == vendorId && (date == null || order.Date == date))
                        .ForEach(order => specialOrders.AddOrder(new ApiSpecialOrder(order)));
                }

                if (specialOrders.SpecialOrderList.Count < 1)
                {
                    if (date == null)
                    {
                        return new ApiResponse<ApiSpecialOrders>(true, "There are no recorded orders for this vendor");
                    }

                    return new ApiResponse<ApiSpecialOrders>(true,
                        "There are no recorded orders for this vendor on the given date", specialOrders);
                }

                return new ApiResponse<ApiSpecialOrders>(true, "A list of SpecialOrders is attached", specialOrders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ApiResponse<ApiSpecialOrders>("An error occured in the API, please try again.");
            }
        }
    }
}