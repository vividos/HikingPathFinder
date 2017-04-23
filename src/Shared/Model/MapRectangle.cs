namespace HikingPathFinder.Model
{
    /// <summary>
    /// A rectangle on the map
    /// </summary>
    public class MapRectangle
    {
        /// <summary>
        /// The north-west map point of the map rectangle
        /// </summary>
        public MapPoint NorthWest { get; set; }

        /// <summary>
        /// The south-east map point of the map rectangle
        /// </summary>
        public MapPoint SouthEast { get; set; }

        /// <summary>
        /// Returns a printable representation of this object
        /// </summary>
        /// <returns>printable text</returns>
        public override string ToString()
        {
            return string.Format(
                "NorthWest=({0}), SouthEast=({1})",
                this.NorthWest.ToString(),
                this.SouthEast.ToString());
        }
    }
}
