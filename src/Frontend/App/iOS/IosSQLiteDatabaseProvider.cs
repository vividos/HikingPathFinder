using HikingPathFinder.App.Database;
using SQLite.Net.Interop;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.StyleCop.CSharp.NamingRules",
    "SA1300:ElementMustBeginWithUpperCaseLetter",
    Scope = "namespace",
    Target = "HikingPathFinder.App.iOS",
    Justification = "iOS is a proper name")]

namespace HikingPathFinder.App.iOS
{
    /// <summary>
    /// SQLite database provider for iOS
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Sonar Code Smell",
        "S101:Types should be named in camel case",
        Justification = "SQLite is a proper name")]
    internal class IosSQLiteDatabaseProvider : ISQLiteDatabaseProvider
    {
        /// <summary>
        /// Returns iOS platform instance of SQLite
        /// </summary>
        /// <returns>SQLite platform instance</returns>
        public ISQLitePlatform GetPlatform()
        {
            return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
        }
    }
}
