namespace HikingPathFinder.App
{
    /// <summary>
    /// Interface to platform specific function implementation
    /// </summary>
    public interface IPlatform
    {
        /// <summary>
        /// Property containing the app version number
        /// </summary>
        string AppVersionNumber { get; }

        /// <summary>
        /// Property containing the folder where the app can place its data files.
        /// </summary>
        string AppDataFolder { get; }

        /// <summary>
        /// Property containing the folder where the app can place cache files. The cache folder
        /// can always be cleard without impact the app.
        /// </summary>
        string CacheDataFolder { get; }

        /// <summary>
        /// Property containing the web view base path for the platform.
        /// </summary>
        string WebViewBasePath { get; }

        /// <summary>
        /// Returns if a file with given filename exists in the file system.
        /// </summary>
        /// <param name="filename">filename to check</param>
        /// <returns>true when file exists, false else</returns>
        bool FileExists(string filename);

        /// <summary>
        /// Combines two path parts, e.g. folder name and file name
        /// </summary>
        /// <param name="path1">path part 1</param>
        /// <param name="path2">path part 2</param>
        /// <returns>combined path</returns>
        string PathCombine(string path1, string path2);

        /// <summary>
        /// Loads a text asset from platform project
        /// </summary>
        /// <param name="assetPath">relative asset path</param>
        /// <returns>loaded text asset</returns>
        string LoadTextAsset(string assetPath);
    }
}
