namespace HikingPathFinder.Model
{
    /// <summary>
    /// Reference to location, not containing all location infos
    /// </summary>
    public class LocationRef
    {
        /// <summary>
        /// Location ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of location
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Location on map
        /// </summary>
        public MapPoint MapLocation { get; set; }

        /// <summary>
        /// Creates a new location ref from a given location
        /// </summary>
        /// <param name="location">location with full infos</param>
        /// <returns>location reference</returns>
        public static LocationRef FromLocation(Location location)
        {
            return new LocationRef
            {
                Id = location.Id,
                Name = location.Name,
                MapLocation = location.MapLocation
            };
        }

        /// <summary>
        /// Returns a printable representation of this object
        /// </summary>
        /// <returns>printable text</returns>
        public override string ToString()
        {
            return string.Format(
                "ID={0}, Name={1}, Location={2}",
                this.Id,
                this.Name,
                this.MapLocation.ToString());
        }
    }
}
