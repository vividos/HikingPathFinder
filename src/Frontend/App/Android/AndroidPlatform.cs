using Android.App;
using Android.Content.Res;
using Android.OS;
using System.IO;

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// Platform implementation for Android
    /// </summary>
    internal class AndroidPlatform : IPlatform
    {
        /// <summary>
        /// Property containing the app version number
        /// </summary>
        public string AppVersionNumber
        {
            get
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var version = assembly.GetName().Version;

                string versionText = string.Format(
                    "{0}.{1}.{2} (Build {3})",
                    version.Major,
                    version.Minor,
                    version.Build,
                    version.Revision);

                var packageInfo = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0);
                return versionText + " " + packageInfo.VersionName;
            }
        }

        /// <summary>
        /// Property containing the Android app data folder
        /// </summary>
        public string AppDataFolder
        {
            get
            {
                return Application.Context.FilesDir.AbsolutePath;
            }
        }

        /// <summary>
        /// Property containing the Android cache data folder
        /// </summary>
        public string CacheDataFolder
        {
            get
            {
                return Application.Context.CacheDir.AbsolutePath;
            }
        }

        /// <summary>
        /// Property containing the web view base path for Android
        /// </summary>
        public string WebViewBasePath
        {
            get
            {
                return "file:///android_asset/";
            }
        }

        /// <summary>
        /// Property containing bool value if WebGL is supported by WebView on this platform.
        /// </summary>
        public bool IsSupportedWebViewWebGL
        {
            get
            {
                // starting with Android 6 Kitkat most devices should have a graphics card that
                // supports WebGL
                return
                    Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;
            }
        }

        /// <summary>
        /// Returns if a file with given filename exists in the file system.
        /// </summary>
        /// <param name="filename">filename to check</param>
        /// <returns>true when file exists, false else</returns>
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
        /// Loads a text asset from Android Assets
        /// </summary>
        /// <param name="assetPath">relative asset path</param>
        /// <returns>loaded text asset</returns>
        public string LoadTextAsset(string assetPath)
        {
            AssetManager assets = Xamarin.Forms.Forms.Context.Assets;

            using (var stream = assets.Open(assetPath))
            using (StreamReader sr = new StreamReader(stream))
            {
                string text = sr.ReadToEnd();
                return text;
            }
        }
    }
}
