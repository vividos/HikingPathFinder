using HikingPathFinder.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// MapView control; the control is actually implemented as JavsScript code, but can be
    /// controlled using this class. JavaScript code is generated for function calls, and callback
    /// functions are called from JavaScript to C#.
    /// </summary>
    public class MapView
    {
        /// <summary>
        /// Web view where MapView control is used
        /// </summary>
        private readonly WebView webView;

        /// <summary>
        /// Creates a new MapView C# object
        /// </summary>
        /// <param name="webView">web view to use</param>
        public MapView(WebView webView)
        {
            this.webView = webView;
        }

        /// <summary>
        /// Creates the MapView JavaScript object; this must be called before any other methods.
        /// </summary>
        /// <param name="areaRectangle">area rectangle to be used for map view</param>
        /// <param name="initialZoomLevel">initial zoom level, in 2D zoom level steps</param>
        public void Create(MapRectangle areaRectangle, int initialZoomLevel)
        {
            var initialPosition = new MapPoint(
                areaRectangle.NorthWest.Latitude + ((areaRectangle.SouthEast.Latitude - areaRectangle.NorthWest.Latitude) / 2),
                areaRectangle.NorthWest.Longitude + ((areaRectangle.SouthEast.Longitude - areaRectangle.NorthWest.Longitude) / 2));

            string js = string.Format(
                "map = new MapView({{id: 'map', initialPosition: [{0}, {1}], initialZoomLevel: {2}}});",
                initialPosition.Latitude.ToString(CultureInfo.InvariantCulture),
                initialPosition.Longitude.ToString(CultureInfo.InvariantCulture),
                initialZoomLevel);

            this.RunJavaScript(js);
        }

        /// <summary>
        /// Zooms to given location
        /// </summary>
        /// <param name="position">position to zoom to</param>
        public void ZoomToLocation(MapPoint position)
        {
            string js = string.Format(
                "map.zoomToLocation({{latitude: {0}, longitude: {1}}});",
                position.Latitude.ToString(CultureInfo.InvariantCulture),
                position.Longitude.ToString(CultureInfo.InvariantCulture));

            this.RunJavaScript(js);
        }

        /// <summary>
        /// Updates the "my location" pin in the map
        /// </summary>
        /// <param name="position">new position to use</param>
        /// <param name="zoomToLocation">indicates if view should also zoom to the location</param>
        public void UpdateMyLocation(MapPoint position, bool zoomToLocation)
        {
            string js = string.Format(
                "map.updateMyLocation({{latitude: {0}, longitude: {1}, zoomTo: {2}}});",
                position.Latitude.ToString(CultureInfo.InvariantCulture),
                position.Longitude.ToString(CultureInfo.InvariantCulture),
                zoomToLocation ? "true" : "false");

            this.RunJavaScript(js);
        }

        /// <summary>
        /// Adds a list of locations to the map, to be displayed as pins.
        /// </summary>
        /// <param name="locationList">list of locations to add</param>
        public void AddLocationList(List<Location> locationList)
        {
            var jsonLocationList =
                from location in locationList
                select new
                {
                    id = location.Id,
                    name = location.Name,
                    type = location.Type.ToString(),
                    latitude = location.MapLocation.Latitude,
                    longitude = location.MapLocation.Longitude,
                    elevation = (int)location.Elevation
                };

            string js = string.Format(
                "map.addLocationList({0});",
                JsonConvert.SerializeObject(jsonLocationList));

            this.RunJavaScript(js);
        }

        /// <summary>
        /// Runs JavaScript code, in main thread
        /// </summary>
        /// <param name="js">javascript code snippet</param>
        private void RunJavaScript(string js)
        {
            Debug.WriteLine("run js: " + js);

            Device.BeginInvokeOnMainThread(() => this.webView.Eval(js));
        }
    }
}
