namespace HikingPathFinder.Model
{
    /// <summary>
    /// A track point, containing location and altitude
    /// </summary>
    public class TrackPoint
    {
        /// <summary>
        /// Track point location on map
        /// </summary>
        public MapPoint Location { get; set; }

        /// <summary>
        /// Altitude of track point
        /// </summary>
        public double Altitude { get; set; }
    }
}
