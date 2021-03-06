﻿using HikingPathFinder.App.Logic;
using HikingPathFinder.Model;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
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
        /// Logging instance
        /// </summary>
        private static Common.Logging.ILog log = App.GetLogger<ExploreMapPage>();

        /// <summary>
        /// Geo locator to use for position updates
        /// </summary>
        private readonly IGeolocator geolocator;

        /// <summary>
        /// Current user settings object
        /// </summary>
        private UserSettings userSettings;

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
        /// Task completion source to signal that the web page has been loaded
        /// </summary>
        private TaskCompletionSource<bool> taskCompletionSourcePageLoaded;

        /// <summary>
        /// List of locations on the map
        /// </summary>
        private List<Location> locationList;

        /// <summary>
        /// Creates a new maps page to explore
        /// </summary>
        public ExploreMapPage()
        {
            this.zoomToMyPosition = false;
            this.geolocator = Plugin.Geolocator.CrossGeolocator.Current;

            Task.Factory.StartNew(this.InitLayoutAsync);
        }

        /// <summary>
        /// Initializes layout by loading map html into web view
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task InitLayoutAsync()
        {
            this.Title = "Explore Map";

            App.RunOnUiThread(() => this.SetupToolbar());

            await this.LoadUserSettingsAsync();

            await this.SetupWebViewAsync(this.userSettings.ShowMapIn3D);

            var appInfo = await this.LoadDataAsync();

            this.CreateMapView(appInfo);
        }

        /// <summary>
        /// Loads user settings
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task LoadUserSettingsAsync()
        {
            var dataService = DependencyService.Get<DataService>();
            this.userSettings = await dataService.GetUserSettingsAsync(CancellationToken.None);

            var platform = DependencyService.Get<IPlatform>();
            if (!platform.IsSupportedWebViewWebGL &&
                this.userSettings.ShowMapIn3D)
            {
                this.userSettings.ShowMapIn3D = false;
                await dataService.StoreUserSettingsAsync(this.userSettings, CancellationToken.None);
            }
        }

        /// <summary>
        /// Loads data; async method
        /// </summary>
        /// <returns>app info object</returns>
        private async Task<AppInfo> LoadDataAsync()
        {
            var dataService = DependencyService.Get<DataService>();

            var appInfo = await dataService.GetAppInfoAsync(CancellationToken.None);
            this.locationList = await dataService.GetLocationListAsync(CancellationToken.None);

            return appInfo;
        }

        /// <summary>
        /// Sets up WebView control
        /// </summary>
        /// <param name="showMapIn3D">indicates if map is shown in 3D (true) or 2D (false)</param>
        /// <returns>task to wait on</returns>
        private async Task SetupWebViewAsync(bool showMapIn3D)
        {
            this.taskCompletionSourcePageLoaded = new TaskCompletionSource<bool>();

            var platform = DependencyService.Get<IPlatform>();

            string htmlText = platform.LoadTextAsset(showMapIn3D ? "map/map3D.html" : "map/map.html");

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

            this.mapView.AddLocationToTour +=
                async (locationId) => await this.OnMapView_AddLocationToTour(locationId);

            this.mapView.SetStartStopLocation +=
                async (setStartLocation, locationId)  => await this.OnMapView_SetStartStopLocation(setStartLocation, locationId);

            this.mapView.NavigateToLocation += this.OnMapView_NavigateToLocation;

            this.Content = this.webView;

            await this.taskCompletionSourcePageLoaded.Task;
        }

        /// <summary>
        /// Creates the map view and adds locations
        /// </summary>
        /// <param name="appInfo">app info object to use for initialising</param>
        private void CreateMapView(AppInfo appInfo)
        {
            this.mapView.Create(appInfo.AreaRectangle, 14);
            this.mapView.AddLocationList(this.locationList);
        }

        /// <summary>
        /// Called when location is added to tour
        /// </summary>
        /// <param name="locationId">location id of location to add to tour</param>
        /// <returns>task to wait on</returns>
        private async Task OnMapView_AddLocationToTour(string locationId)
        {
            Location location = this.FindLocationById(locationId);

            if (location == null)
            {
                log.Error("couldn't find location with id=" + locationId);
                return;
            }

            if (this.userSettings.CurrentPlanTourParameters == null)
            {
                this.userSettings.CurrentPlanTourParameters = new PlanTourParameters();
            }

            await this.StoreUserSettingsAsync();
        }

        /// <summary>
        /// Called when start or stop location has been set
        /// </summary>
        /// <param name="setStartLocation">true when start location, false when stop location</param>
        /// <param name="locationId">location ID to use</param>
        /// <returns>task to wait on</returns>
        private async Task OnMapView_SetStartStopLocation(bool setStartLocation, string locationId)
        {
            Location location = this.FindLocationById(locationId);

            if (location == null)
            {
                log.Error("couldn't find location with id=" + locationId);
                return;
            }

            if (this.userSettings.CurrentPlanTourParameters == null)
            {
                this.userSettings.CurrentPlanTourParameters = new PlanTourParameters();
            }

            var locationRef = LocationRef.FromLocation(location);

            if (setStartLocation)
            {
                this.userSettings.CurrentPlanTourParameters.StartLocation = locationRef;
            }
            else
            {
                this.userSettings.CurrentPlanTourParameters.EndLocation = locationRef;
            }

            await this.StoreUserSettingsAsync();
        }

        /// <summary>
        /// Called when user clicked on the "Navigate here" link in the pin description on the
        /// map.
        /// </summary>
        /// <param name="locationId">location id of location to navigate to</param>
        private void OnMapView_NavigateToLocation(string locationId)
        {
            Location location = this.FindLocationById(locationId);

            if (location == null)
            {
                log.Error("couldn't find location with id=" + locationId);
                return;
            }

            Plugin.ExternalMaps.CrossExternalMaps.Current.NavigateTo(
                location.Name,
                location.MapLocation.Latitude,
                location.MapLocation.Longitude,
                Plugin.ExternalMaps.Abstractions.NavigationType.Driving);
        }

        /// <summary>
        /// Finds a location by given location id.
        /// </summary>
        /// <param name="locationId">location id to use</param>
        /// <returns>found location, or null when no location could be found</returns>
        private Location FindLocationById(string locationId)
        {
            if (this.locationList == null)
            {
                return null;
            }

            return this.locationList.Find(location => location.Id == locationId);
        }

        /// <summary>
        /// Called when web view navigates to a new URL
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnNavigating_WebView(object sender, WebNavigatingEventArgs args)
        {
            if (args.NavigationEvent == WebNavigationEvent.NewPage &&
                args.Url.StartsWith("http"))
            {
                Device.OpenUri(new Uri(args.Url));
                args.Cancel = true;
            }
        }

        /// <summary>
        /// Called when navigation to current page has finished
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnNavigated_WebView(object sender, WebNavigatedEventArgs args)
        {
            this.taskCompletionSourcePageLoaded.SetResult(true);
        }

        /// <summary>
        /// Sets up toolbar for this page
        /// </summary>
        private void SetupToolbar()
        {
            var platform = DependencyService.Get<IPlatform>();

            bool isSupported3DMap = platform.IsSupportedWebViewWebGL;

            if (isSupported3DMap)
            {
                this.AddSwitch3DToolbarButton();
            }

            this.AddShowTourLocationsToolbarButton();
            this.AddLocateMeToolbarButton();
        }

        /// <summary>
        /// Adds a "switch 2D / 3D" button to the toolbar
        /// </summary>
        private void AddSwitch3DToolbarButton()
        {
            ToolbarItem showTourLocationsButton = new ToolbarItem(
                "Switch between 2D and 3D map",
                "switch_2d_3d_map.png",
                async () => await this.OnClicked_ToolbarButtonSwitch3DToolbarButton(),
                ToolbarItemOrder.Primary);

            showTourLocationsButton.AutomationId = "ShowTourLocations";
            ToolbarItems.Add(showTourLocationsButton);
        }

        /// <summary>
        /// Switches between 2D and 3D map view
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task OnClicked_ToolbarButtonSwitch3DToolbarButton()
        {
            this.userSettings.ShowMapIn3D = !this.userSettings.ShowMapIn3D;

            await this.StoreUserSettingsAsync();

            await this.SetupWebViewAsync(this.userSettings.ShowMapIn3D);

            var appInfo = await this.LoadDataAsync();

            this.CreateMapView(appInfo);
        }

        /// <summary>
        /// Stores current user settings using data service
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task StoreUserSettingsAsync()
        {
            var dataService = DependencyService.Get<DataService>();
            await dataService.StoreUserSettingsAsync(this.userSettings, CancellationToken.None);
        }

        /// <summary>
        /// Adds a "show tour location" button to the toolbar
        /// </summary>
        private void AddShowTourLocationsToolbarButton()
        {
            ToolbarItem showTourLocationsButton = new ToolbarItem(
                "Show tour locations",
                "tour_locations.png",
                async () => await this.OnClicked_ToolbarButtonShowTourLocations(),
                ToolbarItemOrder.Primary);

            showTourLocationsButton.AutomationId = "ShowTourLocations";
            ToolbarItems.Add(showTourLocationsButton);
        }

        /// <summary>
        /// Called when toolbar button "show tour locations" was clicked
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task OnClicked_ToolbarButtonShowTourLocations()
        {
            var page = new TourLocationListPopupPage();

            await Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
        }

        /// <summary>
        /// Adds a "locate me" button to the toolbar
        /// </summary>
        private void AddLocateMeToolbarButton()
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
                position = await this.geolocator.GetPositionAsync(timeout: TimeSpan.FromMilliseconds(100), includeHeading: false);
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

            TimeSpan minimumTimeForUpdate = TimeSpan.FromSeconds(5);
            const double MinimumDistanceForUpdateInMeters = 2;

            Task.Run(async () =>
            {
                await this.geolocator.StartListeningAsync(
                    minimumTimeForUpdate,
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
    }
}
