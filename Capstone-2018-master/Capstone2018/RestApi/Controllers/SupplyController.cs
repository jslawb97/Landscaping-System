using System;
using System.Web.Http;
using RestApi.Models;
using RestApi.Models.Resupply;

namespace RestApi.Controllers
{
    /// <summary>
    /// Provides a set of functions that retreive supply related data.
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </remarks>
    [RoutePrefix("api/Supply")]
    public class SupplyController : ApiController
    {
        /// <summary>
        /// An text representation of each supply order for a given vendor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="vendorId">The ID of the vendor to retrieve data for</param>
        /// <param name="date">(Optional) The date on which data is being requested for</param>
        /// <returns></returns>
        [HttpPost, Route("{vendorId}/GetOrders")]
        public ApiResponse<ApiResupplyOrders> GetOrders([FromUri] int vendorId, [FromUri] DateTime? date = null)
        {
            return ResupplyReport.GetOrders(vendorId, date);
        }
    }
}