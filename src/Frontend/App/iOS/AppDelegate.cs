using Foundation;
using GalaSoft.MvvmLight.Ioc;
using HikingPathFinder.App.Database;
using HockeyApp.iOS;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.StyleCop.CSharp.NamingRules",
    "SA1300:ElementMustBeginWithUpperCaseLetter",
    Scope = "namespace",
    Target = "HikingPathFinder.App.iOS",
    Justification = "iOS is a proper name")]

namespace HikingPathFinder.App.iOS
{
    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the 
    /// User Interface of the application, as well as listening (and optionally responding) to 
    /// application events from iOS.
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// This method is invoked when the application has loaded and is ready to run. In this
        /// method you should instantiate the window, load the UI into it and then make the window
        /// visible.
        /// You have 17 seconds to return from this method, or iOS will terminate your application.
        /// </summary>
        /// <param name="app">application object</param>
        /// <param name="options">event options</param>
        /// <returns>indicates if launching has finished successfully</returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            this.InitErrorHandling();
            this.InitServiceLocator();

#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            global::Xamarin.Forms.Forms.Init();
            this.LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// Initializes error handling
        /// </summary>
        private void InitErrorHandling()
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(Constants.HockeyApp_AppId_iOS);
            manager.StartManager();

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
            Debug.WriteLine("Unhandled exception occured: " + args.ExceptionObject.ToString());
        }

        /// <summary>
        /// Called when an exception occured in a task that wasn't handled
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            Debug.WriteLine("Unhandled exception occured: " + args.Exception.ToString());
        }

        /// <summary>
        /// Initializes service locator used throughout the app; uses MvvmLight's SimpleIoc.
        /// </summary>
        private void InitServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ILogProvider, IosSerilogProvider>();
            SimpleIoc.Default.Register<ISQLiteDatabaseProvider, IosSQLiteDatabaseProvider>();
            SimpleIoc.Default.Register<IPlatform, IosPlatform>();

            HikingPathFinder.App.App.InitServiceLocator(SimpleIoc.Default);
        }
    }
}
