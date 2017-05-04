using Common.Logging;
using Serilog;
using System.IO;

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// Log provider for Serilog logging, on Android. See
    /// https://forums.xamarin.com/discussion/23832/logging-with-xamarin-pcl
    /// for details. This class uses the NuGet packages Common.Logging, Serilog,
    /// Serilog.Sinks.RollingFile and Serilog.Sinks.Xamarin.
    /// </summary>
    internal class AndroidSerilogProvider : ILogProvider
    {
        /// <summary>
        /// Output template text for logging
        /// </summary>
        private static string outputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}";

        /// <summary>
        /// Creates a new log provider
        /// </summary>
        public AndroidSerilogProvider()
        {
            // instead of
            // .WriteTo.RollingFile(
            // you can also use
            // .WriteTo.Async(a => a.RollingFile(
            // to get async writing to file; use Serilog.Sinks.Async NuGet package.
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(
                    Path.Combine(global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "HikingPathFinder/Logs/Log-{Date}.txt"),
                    outputTemplate: outputTemplate)
                .WriteTo.AndroidLog()
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
