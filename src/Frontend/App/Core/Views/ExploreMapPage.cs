using HikingPathFinder.App.Logic;
using HikingPathFinder.Model;
using Microsoft.Practices.ServiceLocation;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Page showing a map to explore, with pins for locations to use for tour planning.
    /// </summary>
    public class ExploreMapPage : ContentPage
    {
        /// <summary>
        /// Geo locator to use for position updates
        /// </summary>
        private readonly IGeolocator geolocator;

        /// <summary>
        /// Web view that displays the map
        /// </summary>
        private WebView webView;

        /// <summary>
        /// Indicates if the next position update should also zoom to my position
        /// </summary>
        private bool zoomToMyPosition;

        /// <summary>
        /// Map view control on C# side
        /// </summary>
        private MapView mapView;

        /// <summary>
        /// Event that gets signaled when the web page has been loaded
        /// </summary>
        private ManualResetEvent eventLoaded = new ManualResetEvent(false);

        /// <summary>
        /// Creates a new maps page to explore
        /// </summary>
        public ExploreMapPage()
        {
            this.zoomToMyPosition = false;
            this.geolocator = Plugin.Geolocator.CrossGeolocator.Current;

            this.InitLayout();
            Task.Factory.StartNew(this.LoadDataAsync);
        }

        /// <summary>
        /// Initializes layout by loading map html into web view
        /// </summary>
        private void InitLayout()
        {
            this.Title = "Explore Map";

            this.SetupWebView();
            this.SetupToolbar();

            this.Content = this.webView;
        }

        /// <summary>
        /// Sets up WebView control
        /// </summary>
        private void SetupWebView()
        {
            var platform = ServiceLocator.Current.GetInstance<IPlatform>();

            string htmlText = platform.LoadTextAsset("map/map.html");

            var htmlSource = new HtmlWebViewSource
            {
                Html = htmlText,
                BaseUrl = platform.WebViewBasePath + "map/"
            };

            this.webView = new WebView
            {
                Source = htmlSource,

                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            this.webView.AutomationId = "ExploreMapWebView";

            this.webView.Navigating += this.OnNavigating_WebView;
            this.webView.Navigated += this.OnNavigated_WebView;

            this.mapView = new MapView(this.webView);
        }

        /// <summary>
        /// Called when web view navigates to a new URL
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnNavigating_WebView(object sender, WebNavigatingEventArgs args)
        {
            if (args.NavigationEvent == WebNavigationEvent.NewPage)
            {
                Device.OpenUri(new Uri(args.Url));
                args.Cancel = true;
            }
        }

        /// <summary>
        /// Called when navigation to current page has ended
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnNavigated_WebView(object sender, WebNavigatedEventArgs args)
        {
            this.eventLoaded.Set();
        }

        /// <summary>
        /// Sets up toolbar for this page
        /// </summary>
        private void SetupToolbar()
        {
            ToolbarItem locateMeButton = new ToolbarItem(
                "Locate me",
                "my_location.png",
                async () => await this.OnClicked_ToolbarButtonLocateMe(),
                ToolbarItemOrder.Primary);

            locateMeButton.AutomationId = "LocateMe";

            ToolbarItems.Add(locateMeButton);
        }

        /// <summary>
        /// Called when toolbar button "locate me" was clicked
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task OnClicked_ToolbarButtonLocateMe()
        {
            if (!await this.CheckPermissionAsync())
            {
                return;
            }

            Position position = null;
            try
            {
                position = await this.geolocator.GetPositionAsync(timeoutMilliseconds: 10, includeHeading: false);
            }
            catch (Exception ex)
            {
                // no position service activated, or timeout reached
                Debug.WriteLine(ex.ToString());

                // zoom at next update
                this.zoomToMyPosition = true;

                return;
            }

            if (position != null &&
                Math.Abs(position.Latitude) < 1e5 &&
                Math.Abs(position.Longitude) < 1e5 &&
                this.mapView != null)
            {
                this.mapView.ZoomToLocation(
                    new MapPoint(position.Latitude, position.Longitude));
            }
            else
            {
                // zoom at next update
                this.zoomToMyPosition = true;
            }
        }

        /// <summary>
        /// Checks for permission to use geolocator. See
        /// https://github.com/jamesmontemagno/PermissionsPlugin
        /// </summary>
        /// <returns>true when everything is ok, false when permission wasn't given</returns>
        private async Task<bool> CheckPermissionAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await this.DisplayAlert(
                            "Hiking Path Finder",
                            "The location permission is needed in order to locate your position on the map",
                            "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });

                    status = results[Permission.Location];
                }

                return status == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Called when page is appearing; start position updates
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            const int MinimumTimeForUpdateInSeconds = 5;
            const int MinimumDistanceForUpdateInMeters = 2;

            Task.Run(async () =>
            {
                await this.geolocator.StartListeningAsync(
                    MinimumTimeForUpdateInSeconds,
                    MinimumDistanceForUpdateInMeters,
                    includeHeading: false);
            });

            this.geolocator.PositionChanged += this.OnPositionChanged;
        }

        /// <summary>
        /// Called when form is disappearing; stop position updates
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.geolocator.PositionChanged -= this.OnPositionChanged;

            Task.Run(async () =>
            {
                await this.geolocator.StopListeningAsync();
            });
        }

        /// <summary>
        /// Called when position has changed
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args, including position</param>
        private void OnPositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs args)
        {
            var position = args.Position;

            bool zoomToPosition = this.zoomToMyPosition;

            this.zoomToMyPosition = false;

            if (this.mapView != null)
            {
                this.mapView.UpdateMyLocation(
                    new MapPoint(position.Latitude, position.Longitude), zoomToPosition);
            }
        }

        /// <summary>
        /// Loads data; async method
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task LoadDataAsync()
        {
            var dataService = ServiceLocator.Current.GetInstance<DataService>();

            var appInfo = await dataService.GetAppInfoAsync(CancellationToken.None);
            var locationList = await dataService.GetLocationListAsync(CancellationToken.None);

            // wait for map to be loaded, before sending JavaScript code
            this.eventLoaded.WaitOne();

            this.mapView.Create(appInfo.AreaRectangle, 14);
            this.mapView.AddLocationList(locationList);
        }
    }
}
