using HikingPathFinder.Model;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Interface to route navigation service on device
    /// </summary>
    public interface IRouteNavigation
    {
        /// <summary>
        /// Property that indicates if route navigation services are available on the device.
        /// </summary>
        bool IsNavigationSupported { get; }

        /// <summary>
        /// Starts route navigation for given description and destination
        /// </summary>
        /// <param name="description">description of point to navigate to</param>
        /// <param name="address">address to navigate to; optional, may be null</param>
        /// <param name="destinationPoint">destination map point</param>
        void StartRouteNavigation(string description, string address, MapPoint destinationPoint);
    }
}
