using HikingPathFinder.App.Database;
using SQLite.Net.Interop;

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// SQLite database provider for Android
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Sonar Code Smell",
        "S101:Types should be named in camel case",
        Justification = "SQLite is a proper name")]
    internal class AndroidSQLiteDatabaseProvider : ISQLiteDatabaseProvider
    {
        /// <summary>
        /// Returns Android platform instance of SQLite
        /// </summary>
        /// <returns>SQLite platform instance</returns>
        public ISQLitePlatform GetPlatform()
        {
            return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
        }
    }
}
