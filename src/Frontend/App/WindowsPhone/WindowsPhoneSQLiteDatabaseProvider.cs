using HikingPathFinder.App.Database;
using SQLite.Net.Interop;

namespace HikingPathFinder.App.WindowsPhone
{
    /// <summary>
    /// SQLite database provider for Windows Phone 8
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Sonar Code Smell",
        "S101:Types should be named in camel case",
        Justification = "SQLite is a proper name")]
    internal class WindowsPhoneSQLiteDatabaseProvider : ISQLiteDatabaseProvider
    {
        /// <summary>
        /// Returns Windows Phone platform instance of SQLite
        /// </summary>
        /// <returns>SQLite platform instance</returns>
        public ISQLitePlatform GetPlatform()
        {
            return new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        }
    }
}
