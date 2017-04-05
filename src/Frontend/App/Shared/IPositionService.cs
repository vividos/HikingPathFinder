using HikingPathFinder.Model;
using System;
using System.Threading.Tasks;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Delegate of a function that is called when updated location infos are available
    /// </summary>
    /// <param name="sender">sender object (the position service)</param>
    /// <param name="e">event args</param>
    public delegate void OnUpdateLocation(object sender, UpdateLocationEventArgs e);

    /// <summary>
    /// Specifies states of position service
    /// </summary>
    public enum PositionServiceState
    {
        /// <summary>
        /// Service state is unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Position service is off, or device in flight mode
        /// </summary>
        Off = 1,

        /// <summary>
        /// Position is searched
        /// </summary>
        Searching = 2,

        /// <summary>
        /// Position fix is available
        /// </summary>
        Fix = 3,
    }

    /// <summary>
    /// Source of position
    /// </summary>
    public enum PositionSource
    {
        /// <summary>
        /// There is no source, or it is unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// No position was discovered yet
        /// </summary>
        None = 1,

        /// <summary>
        /// Position is determined from GPS, GLONASS, etc.
        /// </summary>
        GlobalPositionSystem = 2,

        /// <summary>
        /// Position is determined from mobile phone network
        /// </summary>
        MobileNetwork = 3,
    }

    /// <summary>
    /// Interface to a position service on the device
    /// </summary>
    public interface IPositionService
    {
        /// <summary>
        /// Event that can be subscribed to, in order to get location updates
        /// </summary>
        event OnUpdateLocation UpdateLocation;

        /// <summary>
        /// Returns last known position
        /// </summary>
        /// <returns>tuple containing position as map point, and date/time of position</returns>
        Task<Tuple<MapPoint, DateTime>> GetLastKnownPosition();

        /// <summary>
        /// Returns current state of position service
        /// </summary>
        PositionServiceState CurrentState { get; }

        /// <summary>
        /// Returns source of current position
        /// </summary>
        PositionSource CurrentPositionSource { get; }

        /// <summary>
        /// Starts location updates; this calls UpdateLocation event with the last known position
        /// </summary>
        void StartLocationUpdate();

        /// <summary>
        /// Stops location updates
        /// </summary>
        void StopLocationUpdate();

        /// <summary>
        /// Checks if location services are available for this device. Depending on the platform,
        /// this may ask to switch on position service, or shows an alert box on how to switch on
        /// position service.
        /// </summary>
        /// <returns>true when location service was switched on, false when not</returns>
        Task<bool> CheckLocationServiceAsync();
    }
}
