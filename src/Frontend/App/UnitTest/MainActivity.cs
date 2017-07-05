using Android.App;
using Android.OS;
using HikingPathFinder.App;
using HikingPathFinder.App.Android;
using HikingPathFinder.App.Database;
using System.Reflection;
using Xamarin.Android.NUnitLite;
using Xamarin.Forms;

namespace UnitTest
{
    /// <summary>
    /// Unit test main activity for Android
    /// </summary>
    [Activity(Label = "HikingPathFinder UnitTests for Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity
    {
        /// <summary>
        /// Called when activity is about to be created
        /// </summary>
        /// <param name="bundle">bundle parameter; unused</param>
        protected override void OnCreate(Bundle bundle)
        {
            this.InitDependencyService();

            // tests can be inside the main assembly
            this.AddTest(Assembly.GetExecutingAssembly());
            //// or in any reference assemblies
            //// AddTest (typeof(Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }

        /// <summary>
        /// Initializes dependency service used throughout the unit tests
        /// </summary>
        private void InitDependencyService()
        {
            DependencyService.Register<ISQLiteDatabaseProvider, AndroidSQLiteDatabaseProvider>();
            DependencyService.Register<IPlatform, AndroidPlatform>();
        }
    }
}
