using HikingPathFinder.App.Database;
using HikingPathFinder.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App.Logic
{
    /// <summary>
    /// Data services that fetches data, either from local database or from network.
    /// </summary>
    public class DataService
    {
        /// <summary>
        /// Filename of database to use/create
        /// </summary>
        private const string DefaultDatabaseFilename = "HikingPathFinder.db";

        /// <summary>
        /// Database to read and store objects in a local database
        /// </summary>
        private readonly HikingPathFinder.App.Database.Database database;

        /// <summary>
        /// Network service to retrieve data from remote server using available network
        /// </summary>
        private readonly INetworkService networkService;

        /// <summary>
        /// Creates a new data service object
        /// </summary>
        public DataService()
        {
            this.networkService = ServiceLocator.Current.GetInstance<INetworkService>();
            this.database = this.OpenDatabase();
        }

        /// <summary>
        /// Opens database that is used for data storage
        /// </summary>
        /// <returns>opened (or newly created) database</returns>
        private HikingPathFinder.App.Database.Database OpenDatabase()
        {
            var platform = ServiceLocator.Current.GetInstance<IPlatform>();

            string databaseFilename = platform.PathCombine(platform.AppDataFolder, DataService.DefaultDatabaseFilename);

            var localDatabase = new HikingPathFinder.App.Database.Database(databaseFilename);

            var updater = new DatabaseUpdater(localDatabase);
            updater.UpdateToLatest();

            return localDatabase;
        }

        /// <summary>
        /// Starts initialisation of data service; initialisation ensures that all needed objects
        /// were retrieved over the network.
        /// </summary>
        /// <returns>true when initialisation was successful, false when not</returns>
        public async Task<bool> Init()
        {
            var appInfo = await this.GetAppInfoAsync(CancellationToken.None);

            return appInfo != null;
        }

        #region Object access methods
        /// <summary>
        /// Returns current app info object
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>app info object</returns>
        public async Task<AppInfo> GetAppInfoAsync(CancellationToken token)
        {
            var connection = this.database.GetConnection();

            var appInfo = connection.Table<AppInfo>().FirstOrDefault();

            if (appInfo != null)
            {
                return appInfo;
            }

            var appConfig = await this.LoadAppConfigAsync(token);
            return appConfig.Info;
        }

        /// <summary>
        /// Returns list of pre-planned tours
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>list of pre-planned tours</returns>
        public async Task<List<PrePlannedTour>> GetPrePlannedToursListAsync(CancellationToken token)
        {
            // ensure that app config has been loaded
            await this.GetAppInfoAsync(token);

            var connection = this.database.GetConnection();

            var tableQuery = connection.Table<PrePlannedTour>();

            var prePlannedToursList = new List<PrePlannedTour>(tableQuery);
            return prePlannedToursList;
        }

        /// <summary>
        /// Returns list of locations
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>list of locations</returns>
        public async Task<List<Location>> GetLocationListAsync(CancellationToken token)
        {
            // ensure that app config has been loaded
            await this.GetAppInfoAsync(token);

            var connection = this.database.GetConnection();

            var tableQuery = connection.Table<Location>();

            var locationList = new List<Location>(tableQuery);
            return locationList;
        }
        #endregion

        /// <summary>
        /// Loads AppConfig object from network and stores the properties in the database, for
        /// further access.
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>app config object</returns>
        private async Task<AppConfig> LoadAppConfigAsync(CancellationToken token)
        {
            AppConfig appConfig = await this.networkService.GetAppConfigAsync(token);
            if (appConfig != null)
            {
                this.StoreAppConfig(appConfig);
            }

            return appConfig;
        }

        /// <summary>
        /// Stores AppConfig properties in database
        /// </summary>
        /// <param name="appConfig">app config object</param>
        private void StoreAppConfig(AppConfig appConfig)
        {
            var connection = this.database.GetConnection();
            connection.InsertOrReplace(appConfig.Info);

            connection.InsertAll(appConfig.PrePlannedToursList);

            connection.InsertAll(appConfig.StartEndLocationList);
            connection.InsertAll(appConfig.TourLocationList);

            connection.InsertAll(appConfig.StaticPageInfoList);
        }
    }
}
