using GalaSoft.MvvmLight.Ioc;
using HikingPathFinder.App.Database;
using Microsoft.HockeyApp;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HikingPathFinder.App.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitErrorHandling();
            this.InitServiceLocator();

            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Initializes service locator used throughout the app; uses MvvmLight's SimpleIoc.
        /// </summary>
        private void InitServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ILogProvider, UwpSerilogProvider>();
            SimpleIoc.Default.Register<ISQLiteDatabaseProvider, UwpSQLiteDatabaseProvider>();
            SimpleIoc.Default.Register<IPlatform, UwpPlatform>();

            HikingPathFinder.App.App.InitServiceLocator(SimpleIoc.Default);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                // Initialization is required due to an error when compiling in release mode.
                // Details: https://developer.xamarin.com/guides/xamarin-forms/platform-features/windows/installation/universal/#Troubleshooting
                Xamarin.Forms.Forms.Init(args, Rg.Plugins.Popup.Windows.Popup.GetExtraAssemblies());

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    ////TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), args.Arguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="args">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs args)
        {
            throw new Exception("Failed to load Page " + args.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="args">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs args)
        {
            var deferral = args.SuspendingOperation.GetDeferral();

            ////TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
        /// Initializes error handling
        /// </summary>
        private void InitErrorHandling()
        {
            Microsoft.HockeyApp.HockeyClient.Current.Configure(Constants.HockeyApp_AppId_Uwp);

            this.UnhandledException += this.OnUnhandledException;
            TaskScheduler.UnobservedTaskException += this.OnUnobservedTaskException;
        }

        /// <summary>
        /// Called when an exception occured that was unhandled by any code
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Debug.WriteLine("Unhandled exception occured: " + args.Exception.ToString());
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
    }
}
