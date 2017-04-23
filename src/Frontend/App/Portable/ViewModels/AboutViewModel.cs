using HikingPathFinder.App.Logic;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// ViewModel for about page
    /// </summary>
    public class AboutViewModel
    {
        /// <summary>
        /// Heading text
        /// </summary>
        public string Heading { get; private set; }

        /// <summary>
        /// Site name
        /// </summary>
        public string SiteName { get; private set; }

        /// <summary>
        /// Version number of app
        /// </summary>
        public string VersionNumber { get; private set; }

        /// <summary>
        /// Main text of about page
        /// </summary>
        public string MainText { get; private set; }

        /// <summary>
        /// Command to execute when the visit website button has been clicked
        /// </summary>
        public Command VisitWebsiteCommand { get; private set; }

        /// <summary>
        /// Creates a new view model object for about page
        /// </summary>
        public AboutViewModel()
        {
            this.SetupBindings();
        }

        /// <summary>
        /// Initializes layout
        /// </summary>
        private void SetupBindings()
        {
            var platform = ServiceLocator.Current.GetInstance<IPlatform>();

            this.Heading = "Hiking Path Finder";

            var dataService = ServiceLocator.Current.GetInstance<DataService>();

            this.SiteName = string.Empty;
            Task.Run(async () =>
            {
                var appInfo = await dataService.GetAppInfoAsync(CancellationToken.None);
                this.SiteName = appInfo.SiteName;
            });

            this.VersionNumber = "Version " + platform.AppVersionNumber;

            this.MainText = @"About Hiking Path Finder
Hiking Path Finder is an app to plan hiking trips and plan new routes to
locations in a hiking area.

Hiking Path Finder uses the following libraries:
- GalaSoft MvvmLight
- SQLite.Net";

            this.VisitWebsiteCommand = new Command(() =>
            {
                Device.OpenUri(new Uri("https://github.com/vividos/HikingPathFinder"));
            });
        }
    }
}
