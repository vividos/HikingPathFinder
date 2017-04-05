using System;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Event args for when network connectivity has changed
    /// </summary>
    public class NetworkConnectivityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Indicates if network connectivity is online
        /// </summary>
        public bool IsOnline { get; }
    }
}
