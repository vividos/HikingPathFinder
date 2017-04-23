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

        /// <summary>
        /// Returns a printable representation of this object
        /// </summary>
        /// <returns>printable text</returns>
        public override string ToString()
        {
            return string.Format("{0} at {1} m", this.Location, this.Altitude);
        }
    }
}
