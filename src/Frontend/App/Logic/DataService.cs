using HikingPathFinder.App.Database;
using HikingPathFinder.Model;
using Microsoft.Practices.ServiceLocation;
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
        private readonly NetworkService networkService = new NetworkService();

        /// <summary>
        /// Creates a new data service object
        /// </summary>
        public DataService()
        {
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

            if (appInfo == null)
            {
                appInfo = await this.LoadAppInfoAsync(token);
            }

            return appInfo;
        }
        #endregion

        /// <summary>
        /// Loads AppInfo object from network and stores it in database
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>app info object</returns>
        private async Task<AppInfo> LoadAppInfoAsync(CancellationToken token)
        {
            AppInfo appInfo = await this.networkService.GetAppInfoAsync(token);
            if (appInfo != null)
            {
                var connection = this.database.GetConnection();
                connection.InsertOrReplace(appInfo);
            }

            return appInfo;
        }
    }
}
