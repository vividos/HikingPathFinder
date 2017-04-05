using HikingPathFinder.App.Database.Model;
using HikingPathFinder.Model;
using Microsoft.Practices.ServiceLocation;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HikingPathFinder.App.Database
{
    /// <summary>
    /// Database access class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1063:ImplementIDisposableCorrectly",
        Justification = "No need to implement full IDisposable pattern here")]
    public class Database : IDisposable
    {
        /// <summary>
        /// SQLite platform to use for this database
        /// </summary>
        private readonly ISQLitePlatform sqlitePlatform;

        /// <summary>
        /// Database connection per thread
        /// </summary>
        private readonly ThreadLocal<SQLiteConnection> databaseConnection = new ThreadLocal<SQLiteConnection>();

        /// <summary>
        /// Database filename
        /// </summary>
        private readonly string databaseFilename;

        /// <summary>
        /// Type mappings for more complex types in data model
        /// </summary>
        private readonly Dictionary<Type, string> extraTypeMappings = new Dictionary<Type, string>();

        /// <summary>
        /// Blob serializer for types that can't be serialized directly using SQLite.Net; uses
        /// JSON format.
        /// </summary>
        private readonly DataModelJsonSerializer blobSerializer = new DataModelJsonSerializer();

        /// <summary>
        /// Opens an existing SQLite database
        /// </summary>
        /// <param name="databaseFilename">full database filename</param>
        public Database(string databaseFilename)
        {
            this.CreateExtraTypeMappings();

            this.databaseFilename = databaseFilename;

            var provider = ServiceLocator.Current.GetInstance<ISQLiteDatabaseProvider>();

            this.sqlitePlatform = provider.GetPlatform();

            var platform = ServiceLocator.Current.GetInstance<IPlatform>();
            if (!platform.FileExists(databaseFilename))
            {
                this.CreateDatabase();
            }
        }

        /// <summary>
        /// Createsa list of extra type mappings for the table serializer; together with the blob
        /// serializer this manages to serialize some .NET types into JSON strings and store them
        /// in the database easily.
        /// </summary>
        private void CreateExtraTypeMappings()
        {
            this.extraTypeMappings.Add(typeof(MapRectangle), "string");
            this.extraTypeMappings.Add(typeof(MapPoint), "string");
        }

        /// <summary>
        /// Returns a database connection; connections are managed by thread
        /// </summary>
        /// <returns>connection object</returns>
        public SQLiteConnection GetConnection()
        {
            if (!this.databaseConnection.IsValueCreated)
            {
                SQLiteConnection connection = this.CreateNewConnection();
                this.databaseConnection.Value = connection;
            }

            return this.databaseConnection.Value;
        }

        /// <summary>
        /// Creates new database connection
        /// </summary>
        /// <returns>newly created database connection</returns>
        private SQLiteConnection CreateNewConnection()
        {
            return new SQLite.Net.SQLiteConnection(
                this.sqlitePlatform,
                this.databaseFilename,
                storeDateTimeAsTicks: false,
                serializer: this.blobSerializer,
                extraTypeMappings: this.extraTypeMappings);
        }

        /// <summary>
        /// Creates new database and sets database version to 0
        /// </summary>
        private void CreateDatabase()
        {
            var connection = this.GetConnection();

            connection.CreateTable<DatabaseInfo>();

            var databaseInfo = new DatabaseInfo(0);
            connection.Insert(databaseInfo);
        }

        /// <summary>
        /// Disposes of managed resouces in this object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1063:ImplementIDisposableCorrectly",
            Justification = "No need to implement full IDisposable pattern here")]
        public void Dispose()
        {
            this.databaseConnection.Dispose();
        }
    }
}
