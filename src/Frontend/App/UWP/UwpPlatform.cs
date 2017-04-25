using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace HikingPathFinder.App.UWP
{
    /// <summary>
    /// Platform implementation for UWP
    /// </summary>
    internal class UwpPlatform : IPlatform
    {
        /// <summary>
        /// Property containing the app version number
        /// </summary>
        public string AppVersionNumber
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;

                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        /// <summary>
        /// Property containing the local folder
        /// </summary>
        public string AppDataFolder
        {
            get
            {
                return ApplicationData.Current.LocalFolder.Path;
            }
        }

        /// <summary>
        /// Property containing the local cache folder
        /// </summary>
        public string CacheDataFolder
        {
            get
            {
                return ApplicationData.Current.LocalCacheFolder.Path;
            }
        }

        /// <summary>
        /// Property containing the web view base path for Windows Phone. This project stores the
        /// resources in the Assets folder, so the base path automatically contains this path.
        /// </summary>
        public string WebViewBasePath
        {
            get
            {
                return "ms-appx:///Assets/";
            }
        }

        /// <summary>
        /// Returns if a file with given filename exists in the file system.
        /// </summary>
        /// <param name="filename">filename to check</param>
        /// <returns>true when file exists, false when not</returns>
        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        /// <summary>
        /// Combines two path parts, e.g. folder name and file name
        /// </summary>
        /// <param name="path1">path part 1</param>
        /// <param name="path2">path part 2</param>
        /// <returns>combined path</returns>
        public string PathCombine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        /// <summary>
        /// Loads a text asset from Windows Phone assets
        /// </summary>
        /// <param name="assetPath">relative asset path</param>
        /// <returns>loaded text asset</returns>
        public string LoadTextAsset(string assetPath)
        {
            string fullPath = Path.Combine(this.WebViewBasePath, assetPath);
            return LoadTextAssetAsync(fullPath).Result;
        }

        /// <summary>
        /// Loads a text asset from Windows Phone assets; async version
        /// </summary>
        /// <param name="assetPath">asset path to use</param>
        /// <returns>task with text as result</returns>
        private static async Task<string> LoadTextAssetAsync(string assetPath)
        {
            StorageFile file =
                await StorageFile.GetFileFromApplicationUriAsync(new Uri(assetPath));

            string contents = await FileIO.ReadTextAsync(file);
            return contents;
        }
    }
}
