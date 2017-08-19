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

    var svgIcon = this.createSvgIcon({
        svgPath: 'images/map-marker.svg',
        iconSize: [32, 32],
        iconAnchor: [15, 32],
        popupAnchor: [2, -32],
        markerColor: 'green'
    });

    var text = '<h2><img height="48em" width="48em" src="images/map-marker.svg" style="vertical-align:middle" />My Position</h2>';

    this.myPositionMarker = L.marker([0, 0], {
        icon: svgIcon
    });
    this.myPositionMarker.bindPopup(text);
}

/**
 * Updates the "my location" marker on the map
 * @param {object} options Options to use for updating my location. The following object can be used:
 * { latitude: 123.45678, longitude: 9.87654, zoomTo: true }
 */
MapView.prototype.updateMyLocation = function (options) {

    if (this.myLocationMarker === null)
        return;

    console.log("updating my location: lat=" + options.latitude + ", long=" + options.longitude);

    this.myPositionMarker.setLatLng([options.latitude, options.longitude]);
    this.myPositionMarker.addTo(this.map);

    var text = '<h2><img height="48em" width="48em" src="images/map-marker.svg" style="vertical-align:middle" />My Position</h2>' +
        '<div>Latitude: ' + options.latitude + ', Longitude: ' + options.longitude + '</div>';
    this.myPositionMarker.bindPopup(text);

    this.myPositionMarker.update();

    if (options.zoomTo) {
        console.log("also zooming to my location");
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

        var text = '<h2><img height="48em" width="48em" src="' + this.imageUrlFromLocationType(location.type) + '" style="vertical-align:middle" />' +
            location.name + ' ' + location.elevation + 'm</h2>' +
            '<img height="32em" width="32em" src="images/map-marker-plus.svg" style="vertical-align:middle" />' +
            '<a href="javascript:map.onSetStartStopLocation(true, \'' + location.id + '\');">Add as start point</a> - ' +
            '<img height="32em" width="32em" src="images/map-marker-plus.svg" style="vertical-align:middle" />' +
            '<a href="javascript:map.onSetStartStopLocation(false, \'' + location.id + '\');">Add as end point</a><br/>';

        text += '<img height="32em" width="32em" src="images/playlist-plus.svg" style="vertical-align:middle" />' +
            '<a href="javascript:map.onAddLocationToTour(\'' + location.id + '\');">Add as tour location</a> - ';

        if (!location.isTourLocation)
            text += '<img height="32em" width="32em" src="images/navigation.svg" style="vertical-align:middle" />' +
                '<a href="javascript:map.onNavigateToLocation(\'' + location.id + '\');">Navigate here</a></p>';

        var svgIcon = this.createSvgIcon({
            svgPath: this.imageUrlFromLocationType(location.type),
            iconSize: [32, 32],
            iconAnchor: [15, 32],
            popupAnchor: [2, -32],
            markerColor: 'blue'
        });

        L.marker([location.latitude, location.longitude], { icon: svgIcon }).addTo(this.map).bindPopup(text);
    }
};

/**
 * Creates a marker that contains an external SVG icon as marker icon using L.VectorMarkers.
 * @param {string} options additional VectorMarkers options, apart from svgPath
 * @returns icon div to display as marker
 */
MapView.prototype.createSvgIcon = function (options) {
    var icon = L.VectorMarkers.icon(options);

    // modify the _createInner method in order to return a different icon
    icon.__proto__._createInner = function () {

        var img = document.createElement('img');
        var options = this.options;

        img.src = options.svgPath;

        img.style.color = options.iconColor;
        // change icon to white from black
        img.style['-webkit-filter'] = 'invert(100%)';
        img.style.position = 'absolute';
        img.style.top = '5px';
        img.style.left = '9.4px';

        if (options.iconSize) {
            img.style.width = options.iconSize[0] / 2.5 + 'px';
        }

        return img;
    };

    return icon;
};

/**
 * Returns a relative image Url for given location type
 * @param {string} locationType location type
 * @returns relative image Url
 */
MapView.prototype.imageUrlFromLocationType = function (locationType) {

    switch (locationType) {
        case 'Summit': return 'images/mountain-15.svg';
        //case 'Pass': return '';
        case 'Lake': return 'images/water-15.svg';
        case 'Bridge': return 'images/bridge.svg';
        case 'Viewpoint': return 'images/attraction-15.svg';
        case 'AlpineHut': return 'images/home-15.svg';
        case 'Restaurant': return 'images/restaurant-15.svg';
        case 'Church': return 'images/church.svg';
        case 'Castle': return 'images/castle.svg';
        //case 'Cave': return '';
        case 'Information': return 'images/information-outline.svg';
        case 'PublicTransportBus': return 'images/bus.svg';
        case 'PublicTransportTrain': return 'images/train.svg';
        case 'Parking': return 'images/parking.svg';
        //case 'ViaFerrata': return '';
        //case 'Paragliding': return '';
        case 'CableCar': return 'images/aerialway-15.svg';
        default: return 'images/map-marker.svg';
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
 * Called by the marker pin link, in order to set start or stop location.
 * @param {bool} setStartLocation true when start location should be set, false when end location
 *                                should be set.
 * @param {string} locationId Location ID of location to set
 */
MapView.prototype.onSetStartStopLocation = function (setStartLocation, locationId) {

    console.log("set start or stop location: id=" + locationId);

    if (this.options.callback !== undefined)
        this.options.callback('onSetStartStopLocation', {
            setStartLocation: setStartLocation,
            locationId: locationId
        });
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
