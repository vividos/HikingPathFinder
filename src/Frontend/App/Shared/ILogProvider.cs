using Common.Logging;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Interface to provide logging using Common.Logging
    /// </summary>
    public interface ILogProvider
    {
        /// <summary>
        /// Returns a Common.Logging ILog instance for given type
        /// </summary>
        /// <typeparam name="T">type of class that wants to log</typeparam>
        /// <returns>logging instance</returns>
        ILog GetLogger<T>();
    }
}
