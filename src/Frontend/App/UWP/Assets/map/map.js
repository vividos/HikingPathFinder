
var myPositionMarker = L.marker([0, 0]);
myPositionMarker.bindPopup('My Position');

function updateMyPosition(options) {

    console.log("updating my position: lat=" + options.latitude + ", long=" + options.longitude);

    myPositionMarker.setLatLng([options.latitude, options.longitude]);
    myPositionMarker.addTo(map);
    myPositionMarker.update();

    if (options.zoomTo) {
        console.log("also zooming to my position");
        map.panTo([options.latitude, options.longitude]);
    }
}

function zoomToLocation(options) {

    console.log("zooming to: lat=" + options.latitude + ", long=" + options.longitude);
    map.panTo([options.latitude, options.longitude]);
}
