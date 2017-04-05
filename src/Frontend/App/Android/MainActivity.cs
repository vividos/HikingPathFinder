using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using GalaSoft.MvvmLight.Ioc;
using HikingPathFinder.App.Database;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms.Platform.Android;
using System.Threading.Tasks;

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// Main activity for the Android app
    /// </summary>
    [Activity(Label = "Hiking Path Finder",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        /// <summary>
        /// Called in the activity lifecycle when the activity is about to be created. This starts
        /// the Xamarin.Forms based app
        /// </summary>
        /// <param name="bundle">bundle parameter; unused</param>
        protected override void OnCreate(Bundle bundle)
        {
            this.InitErrorHandling();
            this.InitServiceLocator();

            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }

        /// <summary>
        /// Initializes error handling
        /// </summary>
        private void InitErrorHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
            TaskScheduler.UnobservedTaskException += this.OnUnobservedTaskException;
        }

        /// <summary>
        /// Called when an exception occured that was unhandled by any code
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Unhandled exception occured: " + args.ExceptionObject.ToString());
        }

        /// <summary>
        /// Called when an exception occured in a task that wasn't handled
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Unhandled exception occured: " + args.Exception.ToString());
        }

        /// <summary>
        /// Initializes service locator used throughout the app; uses MvvmLight's SimpleIoc.
        /// </summary>
        private void InitServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ISQLiteDatabaseProvider, AndroidSQLiteDatabaseProvider>();
            SimpleIoc.Default.Register<IPlatform, AndroidPlatform>();

            App.InitServiceLocator(SimpleIoc.Default);
        }
    }
}
