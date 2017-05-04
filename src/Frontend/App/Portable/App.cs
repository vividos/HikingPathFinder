using Common.Logging;
using GalaSoft.MvvmLight.Ioc;
using HikingPathFinder.App.Logic;
using HikingPathFinder.App.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Xamarin.Forms application class
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Navigation service that can be used to navigate to any pages from anywhere in the app.
        /// </summary>
        public static NavigationService Navigation { get; private set; }

        /// <summary>
        /// Data service instance; retrieves and stores data for use in the app
        /// </summary>
        private readonly DataService dataService;

        /// <summary>
        /// Returns logging instace for given type
        /// </summary>
        /// <typeparam name="T">type of class that wants to log</typeparam>
        /// <returns>logging instance</returns>
        public static ILog GetLogger<T>()
        {
            var logProvider = DependencyService.Get<ILogProvider>();
            return logProvider.GetLogger<T>();
        }

        /// <summary>
        /// Creates a new application class
        /// </summary>
        public App()
        {
            this.dataService = ServiceLocator.Current.GetInstance<DataService>();

            this.StartDataService();

            this.InitLayout();
        }

        /// <summary>
        /// Starts initializing data service
        /// </summary>
        private void StartDataService()
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await this.dataService.Init();
                }
                catch (System.Exception ex)
                {
                    await this.MainPage.DisplayAlert(
                        "Error",
                        "The app couldn't load initial data\n" + ex.ToString(),
                        "OK");
                }
            });
        }

        /// <summary>
        /// Initializes app layout
        /// </summary>
        private void InitLayout()
        {
            var rootPage = new RootPage();

            Navigation = new NavigationService(rootPage);

            this.MainPage = rootPage;
        }

        /// <summary>
        /// Initialize service locator by registering all classes that are needed throughout the
        /// project.
        /// </summary>
        /// <param name="simpleIoc">IoC container to use</param>
        public static void InitServiceLocator(ISimpleIoc simpleIoc)
        {
            simpleIoc.Register<DataService>();
        }

        /// <summary>
        /// Runs action on the UI thread
        /// </summary>
        /// <param name="action">action to run</param>
        public static void RunOnUiThread(Action action)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(action);
        }

        /// <summary>
        /// Runs action on the UI thread and waits for completion; async version
        /// </summary>
        /// <param name="action">action to run</param>
        /// <returns>task to wait on for completion</returns>
        public static Task RunOnUiThreadAsync(Action action)
        {
            var tcs = new TaskCompletionSource<object>();

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return tcs.Task;
        }

        #region App lifecycle methods
        /// <summary>
        /// Called when app is started
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when app is put to sleep
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when app is resumed
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
