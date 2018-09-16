using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestApi.Controllers
{
    /// <summary>
    /// Provides a welcome page for the CR Landscaping API
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </remarks>
    public class HomeController : Controller
    {
        /// <summary>
        /// Shows a welcome message
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// </summary>
        [HttpGet]
        public async Task<string> Api()
        {
            return await Task.Run(() => "Welcome to the CR Landscaping API");
        }

        /// <summary>
        /// Shows a welcome page with links to demos
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <returns>The API Home page</returns>
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View());
        }
    }
}