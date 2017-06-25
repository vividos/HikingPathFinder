using HikingPathFinder.App.Database.Model;
using HikingPathFinder.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HikingPathFinder.App.Database
{
    /// <summary>
    /// Updates a database to its latest version, in order to be usable by the app.
    /// </summary>
    public class DatabaseUpdater
    {
        /// <summary>
        /// Number of latest database version
        /// </summary>
        private const int LatestDatabaseVersion = 1;

        /// <summary>
        /// Database to update
        /// </summary>
        private readonly Database database;

        /// <summary>
        /// Dictionary with all database update functions, indexed by database version
        /// </summary>
        private readonly Dictionary<int, Action> dictUpdates;

        /// <summary>
        /// Creates a new database updater, but doesn't modify the database yet
        /// </summary>
        /// <param name="database">database to update</param>
        public DatabaseUpdater(Database database)
        {
            this.database = database;

            this.dictUpdates = new Dictionary<int, Action>
            {
                { 1, this.UpdateToVersion1 }
            };
        }

        /// <summary>
        /// Updates database to the latest version
        /// </summary>
        public void UpdateToLatest()
        {
            int databaseVersion = this.GetCurrentDatabaseVersion();

            while (databaseVersion < LatestDatabaseVersion)
            {
                // try to find an update to the next version
                int nextVersion = databaseVersion + 1;

                if (this.dictUpdates.ContainsKey(nextVersion))
                {
                    this.dictUpdates[nextVersion]();
                }
                else
                {
                    // there is no update to the next database version; maybe
                    Debug.Assert(
                        false,
                        "there is no update to upgrade database to version number " + nextVersion.ToString());
                    break;
                }

                databaseVersion = this.GetCurrentDatabaseVersion();
            }
        }

        /// <summary>
        /// Returns the current database version number
        /// </summary>
        /// <returns>database version number</returns>
        private int GetCurrentDatabaseVersion()
        {
            var connection = this.database.GetConnection();

            var databaseInfo = connection.Table<DatabaseInfo>().FirstOrDefault();

            return databaseInfo == null ? 0 : databaseInfo.Version;
        }

        /// <summary>
        /// Sets new database version number
        /// </summary>
        /// <param name="versionNumber">version number to set</param>
        private void SetNewDatabaseVersion(int versionNumber)
        {
            var connection = this.database.GetConnection();

            connection.Update(new DatabaseInfo(versionNumber));
        }

        #region Version update methods
        /// <summary>
        /// Updates to version 1 of the database; only contains new table AppInfo
        /// </summary>
        private void UpdateToVersion1()
        {
            var connection = this.database.GetConnection();

            // initial: tables
            connection.CreateTable<AppInfo>();
            connection.CreateTable<UserSettings>();
            connection.CreateTable<PhotoRef>();
            connection.CreateTable<Location>();
            connection.CreateTable<PrePlannedTour>();
            connection.CreateTable<StaticPageInfo>();

            this.SetNewDatabaseVersion(1);
        }
        #endregion
    }
}
