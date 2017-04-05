using HikingPathFinder.Model;
using Microsoft.AspNetCore.Mvc;

namespace HikingPathFinder.WebApi.Controllers
{
    /// <summary>
    /// Web API controller to return AppInfo objects
    /// </summary>
    [Route("api/[controller]")]
    public class AppInfoController : Controller
    {
        /// <summary>
        /// GET: api/appInfo
        /// Returns app info object
        /// </summary>
        /// <returns>app info object</returns>
        [HttpGet]
        public AppInfo Get()
        {
            // return test app info
            var appInfo = new AppInfo
            {
                SiteName = "Hiking Path Finder beta site",
                AreaName = "Spitzingsee hiking area",
                AreaRectangle = new MapRectangle
                {
                    NorthWest = new MapPoint(47.77, 11.73),
                    SouthEast = new MapPoint(47.57, 12.04)
                },
                StaticPagesTitle = "Hiking tips",
                License = "Creative Commons Attribution-ShareAlike 4.0 International License (CC-BY-SA)"
            };

            return appInfo;
        }
    }
}
