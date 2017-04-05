using Foundation;
using System;
using System.IO;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.StyleCop.CSharp.NamingRules",
    "SA1300:ElementMustBeginWithUpperCaseLetter",
    Scope = "namespace",
    Target = "HikingPathFinder.App.iOS",
    Justification = "iOS is a proper name")]

namespace HikingPathFinder.App.iOS
{
    /// <summary>
    /// Platform implementation for iOS
    /// </summary>
    internal class IosPlatform : IPlatform
    {
        /// <summary>
        /// Property containing the app version number
        /// </summary>
        public string AppVersionNumber
        {
            get
            {
                return NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"] as NSString;
            }
        }

        /// <summary>
        /// Property containing the app data folder. This folder is backed up by iTunes.
        /// See https://developer.xamarin.com/guides/ios/application_fundamentals/working_with_the_file_system/
        /// </summary>
        public string AppDataFolder
        {
            get
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string libraryPath = Path.Combine(myDocumentsPath, "..", "Library");

                return libraryPath;
            }
        }

        /// <summary>
        /// Property containing the cache data folder. This folder is cleaned by iOS in low memory
        /// situations.
        /// See https://developer.xamarin.com/guides/ios/application_fundamentals/working_with_the_file_system/
        /// </summary>
        public string CacheDataFolder
        {
            get
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string cachePath = Path.Combine(myDocumentsPath, "..", "Library", "Caches");

                return cachePath;
            }
        }

        /// <summary>
        /// Property containing the web view base path for iOS
        /// </summary>
        public string WebViewBasePath
        {
            get
            {
                return NSBundle.MainBundle.ResourcePath + "/";
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
        /// Loads a text asset from iOS assets; assets must be stored in the Resources folder and
        /// marked with build type BundleResource.
        /// </summary>
        /// <param name="assetPath">relative asset path</param>
        /// <returns>loaded text asset</returns>
        public string LoadTextAsset(string assetPath)
        {
            return File.ReadAllText(Path.Combine(NSBundle.MainBundle.BundlePath, assetPath));
        }
    }
}
