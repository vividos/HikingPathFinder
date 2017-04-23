using System;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using System.Globalization;

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
        /// Creates a new maps page to explore
        /// </summary>
        public ExploreMapPage()
        {
            this.zoomToMyPosition = false;
            this.geolocator = Plugin.Geolocator.CrossGeolocator.Current;

            this.InitLayout();
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
        /// Sets up toolbar for this page
        /// </summary>
        private void SetupToolbar()
        {
            ToolbarItem locateMeButton = new ToolbarItem(
                "Locate me",
                "my_location.png",
                async () => { await this.OnClicked_ToolbarButtonLocateMe(); },
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
            var position = await this.geolocator.GetPositionAsync(timeoutMilliseconds: 1, includeHeading: false);

            if (position != null &&
                Math.Abs(position.Latitude) < 1e5 &&
                Math.Abs(position.Longitude) < 1e5)
            {
                this.ZoomToLocation(position);
            }
            else
            {
                // zoom at next update
                this.zoomToMyPosition = true;
            }
        }

        /// <summary>
        /// Zooms to given location
        /// </summary>
        /// <param name="position">position to zoom to</param>
        private void ZoomToLocation(Position position)
        {
            string js = string.Format(
                "zoomToLocation({{latitude: {0}, longitude: {1}}});",
                position.Latitude.ToString(CultureInfo.InvariantCulture),
                position.Longitude.ToString(CultureInfo.InvariantCulture));

            this.webView.Eval(js);
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

            this.UpdateMyPosition(position);
        }

        /// <summary>
        /// Updates the "my position" pin in the map
        /// </summary>
        /// <param name="position">new position to use</param>
        private void UpdateMyPosition(Plugin.Geolocator.Abstractions.Position position)
        {
            bool zoomToPosition = this.zoomToMyPosition;
            this.zoomToMyPosition = false;

            string js = string.Format(
                "updateMyPosition({{latitude: {0}, longitude: {1}, zoomTo: {2}}});",
                position.Latitude.ToString(CultureInfo.InvariantCulture),
                position.Longitude.ToString(CultureInfo.InvariantCulture),
                zoomToPosition ? "true" : "false");

            this.webView.Eval(js);
        }
    }
}
