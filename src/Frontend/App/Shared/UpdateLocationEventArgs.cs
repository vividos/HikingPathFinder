using HikingPathFinder.Model;
using System;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Event args for the UpdateLocation event
    /// </summary>
    public class UpdateLocationEventArgs : EventArgs
    {
        /// <summary>
        /// Updated location
        /// </summary>
        public MapPoint Location { get; }

        /// <summary>
        /// Time when location was determined
        /// </summary>
        public DateTime Time { get; }
    }
}
