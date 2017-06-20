/**
 * Creates a new instance of MapView
 * @constructor
 * @param {object} options Options to use for initializing map view
 */
function MapView(options) {

    console.log("creating new map view");

    this.options = options || {
        id: 'map',
        initialRectangle: [{ latitude: 47.77, longitude: 11.73 }, { latitude: 47.57, longitude: 12.04 }],
        initialZoomLevel: 14,
        callback: {}
    };

    if (this.options.callback === undefined)
        this.options.callback = callAction;

    var northWest = L.latLng(this.options.initialRectangle[0].latitude, this.options.initialRectangle[0].longitude);
    var southEast = L.latLng(this.options.initialRectangle[1].latitude, this.options.initialRectangle[1].longitude);
    var bounds = L.latLngBounds(northWest, southEast);
    var initialPosition = bounds.getCenter();

    this.map = L.map(this.options.id).setView(initialPosition, this.options.initialZoomLevel);

    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(this.map);

    this.myPositionMarker = L.marker([0, 0]);
    this.myPositionMarker.bindPopup('My Position');
}

/**
 * Updates the "my location" marker on the map
 * @param {object} options Options to use for updating my location. The following object can be used:
 * { latitude: 123.45678, longitude: 9.87654, zoomTo: true }
 */
MapView.prototype.updateMyLocation = function (options) {

    console.log("updating my position: lat=" + options.latitude + ", long=" + options.longitude);

    this.myPositionMarker.setLatLng([options.latitude, options.longitude]);
    this.myPositionMarker.addTo(this.map);
    this.myPositionMarker.update();

    if (options.zoomTo) {
        console.log("also zooming to my position");
        this.map.panTo([options.latitude, options.longitude]);
    }
};


/**
 * Zooms to given location
 * @param {object} options Options to use for zooming. The following object can be used:
 * { latitude: 123.45678, longitude: 9.87654 }
 */
MapView.prototype.zoomToLocation = function (options) {

    console.log("zooming to: lat=" + options.latitude + ", long=" + options.longitude);
    this.map.panTo([options.latitude, options.longitude]);
};

/**
 * Adds list of locations to the map, as marker pins
 * @param {array} locationList An array of location, each with the following object layout:
 * { id:"location-id", name:"Location Name", type:"LocationType", latitude: 123.45678, longitude: 9.87654, elevation:1234 }
 */
MapView.prototype.addLocationList = function (locationList) {

    console.log("adding location list, with " + locationList.length + " entries");

    for (var index in locationList) {

        var location = locationList[index];

        var text = '<h2>' + location.name + ' ' + location.elevation + 'm</h2>' +
            '<p>Location type: ' + location.type + '<br/>' +
            '<img height="32em" width="32em" src="images/add_to_tour.svg" style="vertical-align:middle" />' +
            '<a href="javascript:map.onAddLocationToTour(\'' + location.id + '\');">Add to tour</a> - ' +
            '<img height="32em" width="32em" src="images/route_to_location.svg" style="vertical-align:middle" />' +
            '<a href="javascript:map.onNavigateToLocation(\'' + location.id + '\');">Navigate here</a></p>';

        L.marker([location.latitude, location.longitude]).addTo(this.map).bindPopup(text);
    }
};

/**
 * Adds list of tracks to the map
 * @param {array} listOfTracks An array of tracks
 */

MapView.prototype.addTracksList = function (listOfTracks) {

    console.log("adding list of tracks, with " + listOfTracks.length + " entries");

    // TODO implement
};

/**
 * Called by the marker pin link, in order to add a location to the tour list.
 * @param {string} locationId Location ID of location to add to tour
 */
MapView.prototype.onAddLocationToTour = function (locationId) {

    console.log("location is added to tour: id=" + locationId);

    if (this.options.callback !== undefined)
        this.options.callback('onAddLocationToTour', locationId);
};

/**
 * Called by the marker pin link, in order to start navigating to the location.
 * @param {string} locationId Location ID of location to navigate to
 */
MapView.prototype.onNavigateToLocation = function (locationId) {

    console.log("navigation to location started: id=" + locationId);

    if (this.options.callback !== undefined)
        this.options.callback('onNavigateToLocation', locationId);
};
