namespace HikingPathFinder.Model
{
    /// <summary>
    /// A point on a map, in WGS84 decimal coordinates. Negative values are
    /// left of the GMT line and below the equator.
    /// </summary>
    public class MapPoint
    {
        /// <summary>
        /// Creates a new map point
        /// </summary>
        /// <param name="latitude">latitude in decimal degrees</param>
        /// <param name="longitude">longitude in decimal degrees</param>
        public MapPoint(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Latitude, from north (+90.0) to south (-90.0), 0.0 at equator line, e.g. 48.137155
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude, from west to east, 0.0 at Greenwich line; e.g. 11.575416
        /// </summary>
        public double Longitude { get; set; }
    }
}
