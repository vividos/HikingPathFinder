namespace HikingPathFinder.App
{
    /// <summary>
    /// Delegate that is called when the network connectivity of the device has changed.
    /// </summary>
    /// <param name="sender">sender object (the mobile network service)</param>
    /// <param name="e">event args</param>
    public delegate void OnNetworkConnectivityChanged(object sender, NetworkConnectivityChangedEventArgs e);

    /// <summary>
    /// Interface to the mobile network service of the device
    /// </summary>
    public interface IMobileNetworkService
    {
        /// <summary>
        /// Event that is sent when the network connectivity of the device has changed.
        /// </summary>
        event OnNetworkConnectivityChanged NetworkConnectivityChanged;
    }
}
