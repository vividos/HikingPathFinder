using HikingPathFinder.Model;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App.Logic
{
    /// <summary>
    /// Network service that reads and modifies remote objects using a RESTful Web API.
    /// </summary>
    public class NetworkService
    {
        /// <summary>
        /// Retrieves current AppInfo object
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>app info object</returns>
        internal async Task<AppInfo> GetAppInfoAsync(CancellationToken token)
        {
            // TODO implement using actual REST service

            // TODO remove
            await Task.Delay(100);

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
