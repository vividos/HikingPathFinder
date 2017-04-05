namespace HikingPathFinder.App.Database
{
    /// <summary>
    /// Interface providing SQLite platform for PCL
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Sonar Code Smell",
        "S101:Types should be named in camel case",
        Justification = "SQLite is a proper name")]
    public interface ISQLiteDatabaseProvider
    {
        /// <summary>
        /// Returns a platform instance of SQLite to be used in database code
        /// </summary>
        /// <returns>SQLite platform instance</returns>
        SQLite.Net.Interop.ISQLitePlatform GetPlatform();
    }
}
