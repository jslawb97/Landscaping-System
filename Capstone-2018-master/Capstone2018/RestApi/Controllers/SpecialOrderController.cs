using System;
using System.Web.Http;
using RestApi.Models;
using RestApi.Models.SpecialOrders;

namespace RestApi.Controllers
{
    /// <summary>
    /// Provides a set of functions that retreive special order related data.
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    [RoutePrefix("api/SpecialOrders")]
    public class SpecialOrderController : ApiController
    {
        /// <summary>
        /// An text representation of each special order for a given vendor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="vendorId">The ID of the vendor to retrieve data for</param>
        /// <param name="date">(Optional) The date on which data is being requested for</param>
        /// <returns></returns>
        [HttpPost, Route("{vendorId}/GetOrders")]
        public ApiResponse<ApiSpecialOrders> GetOrders([FromUri] int vendorId, [FromUri] DateTime? date = null)
        {
            return SpecialOrderReport.GetOrders(vendorId, date);
        }
    }
}