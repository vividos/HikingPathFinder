/**
 * Creates a new instance of MapView
 * @constructor
 * @param {object} options Options to use for initializing map view
 */
function MapView(options) {

    console.log("creating new 3D map view");

    this.options = options || {
        id: 'map',
        initialRectangle: [{ latitude: 47.77, longitude: 11.73 }, { latitude: 47.57, longitude: 12.04 }],
        initialZoomLevel: 14,
        callback: {}
    };

    if (this.options.callback === undefined)
        this.options.callback = callAction;

    console.log("#1 osm");
    var imageryProvider = Cesium.createOpenStreetMapImageryProvider({
        url: 'https://a.tile.openstreetmap.org/',
        maximumLevel: 18
    });

    console.log("#2 terrain");
    var terrainProvider = new Cesium.CesiumTerrainProvider({
        url: 'https://assets.agi.com/stk-terrain/v1/tilesets/world/tiles',
        requestWaterMask: false,
        requestVertexNormals: true
    });

    console.log("#3 clock");
    var today = new Date();
    today.setHours(10, 0, 0, 0);

    var fixedTime = Cesium.JulianDate.fromDate(today);

    var clock = new Cesium.Clock({
        startTime: fixedTime,
        endTime: fixedTime,
        currentTime: fixedTime,
        clockRange: Cesium.ClockRange.CLAMPED
    });

    console.log("#4 viewer");
    this.viewer = new Cesium.Viewer(this.options.id, {
        imageryProvider: imageryProvider,
        terrainProvider: terrainProvider,
        clock: clock,
        baseLayerPicker: false,
        sceneModePicker: false,
        animation: false,
        geocoder: false,
        homeButton: false,
        timeline: false,
        skyAtmosphere: false,
        skyBox: false
    });

    this.viewer.scene.globe.enableLighting = true;
    this.viewer.scene.globe.depthTestAgainstTerrain = true;

    console.log("#5 setView");
    var west = this.options.initialRectangle[0]['longitude'];
    var north = this.options.initialRectangle[0]['latitude'];

    var east = this.options.initialRectangle[1]['longitude'];
    var south = this.options.initialRectangle[1]['latitude'];

    var initialRectangle = Cesium.Rectangle.fromDegrees(west, south, east, north);

    this.viewer.camera.setView({
        destination: initialRectangle,
        orientation: {
            heading: Cesium.Math.toRadians(0), // north
            pitch: Cesium.Math.toRadians(-35),
            roll: 0.0
        }
    });

    console.log("#6 my location");
    this.pinBuilder = new Cesium.PinBuilder();

    this.myLocationMarker = null;

    var pinPromise = this.pinBuilder.fromMakiIconId('marker', Cesium.Color.RED, 48);

    Cesium.when(pinPromise, function (canvas) {

        this.myLocationMarker = this.viewer.entities.add({
            name: 'My Position',
            show: false,
            position: Cesium.Cartesian3.fromDegrees(0.0, 0.0),
            billboard: {
                image: canvas.toDataURL(),
                //image: this.pinBuilder.fromText('?', Cesium.Color.BLUE, 48).toDataURL(),
                verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
                heightReference: Cesium.HeightReference.CLAMP_TO_GROUND
            }
        });
    });

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

    this.myLocationMarker.show = true;
    this.myLocationMarker.position = Cesium.Cartesian3.fromDegrees(options.longitude, options.latitude);

    var text = '<h2><img height="48em" width="48em" src="images/map-marker.svg" style="vertical-align:middle" />My Position</h2>' +
        '<div>Latitude: ' + options.latitude + ', Longitude: ' + options.longitude + '</div>';

    this.myLocationMarker.description = text;

    this.viewer.trackedEntity = this.myLocationMarker;

    if (options.zoomTo) {
        console.log("also zooming to my location");
        this.map.zoomToLocation(options);
    }
};


/**
 * Zooms to given location
 * @param {object} options Options to use for zooming. The following object can be used:
 * { latitude: 123.45678, longitude: 9.87654 }
 */
MapView.prototype.zoomToLocation = function (options) {

    console.log("zooming to: lat=" + options.latitude + ", long=" + options.longitude);

    var cameraPos = Cesium.Cartesian3.fromDegrees(options.longitude, options.latitude, 7000.0);

    this.viewer.scene.camera.flyTo({
        destination: cameraPos,
        orientation: {
            heading: Cesium.Math.toRadians(0), // north
            pitch: Cesium.Math.toRadians(-35),
            roll: 0.0
        }
    });
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

        var pinPromise = this.pinBuilder.fromMakiIconId(this.locationTypeToMakiIconId(location.type), Cesium.Color.RED, 48);

        Cesium.when(pinPromise, function (canvas) {
            this.viewer.entities.add({
                name: location.name + ' ' + location.elevation + 'm',
                description: text,
                position: Cesium.Cartesian3.fromDegrees(location.longitude, location.latitude),
                billboard: {
                    image: this.pinBuilder.fromText('?', Cesium.Color.BLUE, 48).toDataURL(),
                    verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
                    heightReference: Cesium.HeightReference.CLAMP_TO_GROUND
                }
            });
        });
    }
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
