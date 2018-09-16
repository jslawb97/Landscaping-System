using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestApi.Models.Resupply;
using RestApi.Models.SpecialOrders;

namespace RestApi.Controllers
{
    /// <summary>
    /// Shows demo request forms and data pages with API response data
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public class DemoController : Controller
    {
        /// <summary>
        /// Shows a welcome page with links to demos
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <returns>The API Home page</returns>
        public async Task<ActionResult> SupplyRequest()
        {
            return await Task.Run(() =>
                View(new ApiResupplyRequest {VendorId = 1000000, Date = DateTime.Parse("2018-06-16T00:00:00")}));
        }

        /// <summary>
        /// Invalid HTTP request type. Redirects to Supply Request
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <returns>Redirects to Supply Request</returns>
        [HttpGet]
        public ActionResult SupplyData()
        {
            return RedirectToActionPermanent("SupplyRequest");
        }

        /// <summary>
        /// Shows a welcome page with links to demos
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <returns>The API Home page</returns>
        [HttpPost]
        public async Task<ActionResult> SupplyData(ApiResupplyRequest apiRequest)
        {
            var apiResponse = ResupplyReport.GetOrders(apiRequest.VendorId, apiRequest.Date);
            return await Task.Run(() => View(apiResponse));
        }


        /// <summary>
        /// Shows a view with a Special Order report request form
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <returns>The Special Order report request form</returns>
        public async Task<ActionResult> SpecialOrderRequest()
        {
            return await Task.Run(() => View(new ApiSpecialOrderRequest {VendorId = 0}));
        }

        /// <summary>
        /// Invalid HTTP request type. Redirects to Special Order Request
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <returns>Redirects to Supply Request</returns>
        [HttpGet]
        public ActionResult SpecialOrderData()
        {
            return RedirectToActionPermanent("SpecialOrderRequest");
        }

        /// <summary>
        /// Shows Special Order data page
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <returns>Special Order data view</returns>
        [HttpPost]
        public async Task<ActionResult> SpecialOrderData(ApiSpecialOrderRequest apiRequest)
        {
            var apiResponse = SpecialOrderReport.GetOrders(apiRequest.VendorId, apiRequest.Date);
            return await Task.Run(() => View(apiResponse));
        }
    }
}