using HikingPathFinder.Model;
using Microsoft.AspNetCore.Mvc;

namespace HikingPathFinder.Backend.WebApi.Controllers
{
    /// <summary>
    /// Web API controller to return AppConfig objects
    /// </summary>
    [Route("api/[controller]")]
    public class AppConfigController : Controller
    {
        /// <summary>
        /// GET: api/appConfig
        /// Returns app config object
        /// </summary>
        /// <returns>app config object</returns>
        [HttpGet]
        public AppConfig Get()
        {
            return HikingPathFinder.DemoData.DataProvider.GetAppConfig();
        }
    }
}
