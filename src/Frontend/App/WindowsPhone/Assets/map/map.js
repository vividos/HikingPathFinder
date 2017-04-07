
var myPositionMarker = L.marker([0, 0]);
myPositionMarker.bindPopup('My Position');

function updateMyPosition(options) {

    console.log("updating my position: lat=" + options.latitude + ", long=" + options.longitude);

    myPositionMarker.setLatLng(options.latitude, options.longitude);
    myPositionMarker.addTo(map);

    if (options.zoomTo) {
        console.log("also zooming to my position");
        myPositionMarker.zoomTo();
    }
}

function zoomToLocation(options) {

    console.log("zooming to: lat=" + options.latitude + ", long=" + options.longitude);
    map.zoomTo(options.latitude, options.longitude);
}
