using System;
using System.Diagnostics;
using System.IO;
using Xamarin.UITest;

namespace UITest
{
    /// <summary>
    /// Initializes the app for UI testing
    /// </summary>
    public static class AppInitializer
    {
        /// <summary>
        /// Starts app for given platform
        /// </summary>
        /// <param name="platform">platform to use</param>
        /// <returns>deployed and started app</returns>
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                string apkFilename = GetAndroidApkFilename();

                Debug.Assert(
                    File.Exists(apkFilename),
                    ".apk file must have been built and deployed once to a device or emulator");

                return ConfigureApp
                    .Android
                    .ApkFile(apkFilename)
                    .Debug()
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            if (platform == Platform.iOS)
            {
                return ConfigureApp
                    .iOS
                    .Debug()
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            throw new NotImplementedException(
                "app initializer for unknown platform not implemented");
        }

        /// <summary>
        /// Returns android .apk filename of project HikingPathFinder.App.Android
        /// Note that this UITest project has a build dependency in order to have the project
        /// built when the UITest is started.
        /// </summary>
        /// <returns>full filename of the .apk file</returns>
        private static string GetAndroidApkFilename()
        {
            string assemblyPath = Path.GetDirectoryName(typeof(AppInitializer).Assembly.Location);

#if DEBUG
            string configuration = "Debug";
#else
            string configuration = "Release";
#endif
            string apkFilename = Path.Combine(
                assemblyPath,
                "..\\..\\..\\Android\\bin",
                configuration,
                "de.vividos.app.hikingpathfinder.android.beta.apk");

            return apkFilename;
        }
    }
}
