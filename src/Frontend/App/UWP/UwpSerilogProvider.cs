using Common.Logging;
using Serilog;
using System.IO;
using Windows.Storage;

namespace HikingPathFinder.App.UWP
{
    /// <summary>
    /// Log provider for Serilog logging, on UWP. This class uses the NuGet packages
    /// Common.Logging, Serilog, and Serilog.Sinks.RollingFile.
    /// </summary>
    internal class UwpSerilogProvider : ILogProvider
    {
        /// <summary>
        /// Output template text for logging
        /// </summary>
        private static string outputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}";

        /// <summary>
        /// Creates a new log provider
        /// </summary>
        public UwpSerilogProvider()
        {
            string logPath = ApplicationData.Current.LocalFolder.Path;

            // instead of
            // .WriteTo.RollingFile(
            // you can also use
            // .WriteTo.Async(a => a.RollingFile(
            // to get async writing to file; use Serilog.Sinks.Async NuGet package.
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(
                    Path.Combine(logPath, "Log-{Date}.txt"),
                    outputTemplate: outputTemplate)
                .CreateLogger();

            Log.Logger = logger;
        }

        /// <summary>
        /// Returns a logger for given type
        /// </summary>
        /// <typeparam name="T">type of class that wants to log</typeparam>
        /// <returns>logger instance</returns>
        public ILog GetLogger<T>()
        {
            return LogManager.GetLogger<T>();
        }

        /// <summary>
        /// Cleans up logging; only needed when Async() is used above
        /// </summary>
        public void Cleanup()
        {
            Serilog.Log.CloseAndFlush();
        }
    }
}
