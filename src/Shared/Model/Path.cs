namespace HikingPathFinder.Model
{
    /// <summary>
    /// A path is a connection between two locations and has a segment and (maybe) a reverse
    /// segment, if the terrain allows it.
    /// </summary>
    public class Path
    {
        /// <summary>
        /// Path name; may be empty
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The segment describing the path in the normal walking direction
        /// </summary>
        public Segment Segment { get; set; }

        /// <summary>
        /// The segment describing the path in the reverse walking direction; may be null in rare
        /// conditions (e.g. when path only allows walking in one direction).
        /// </summary>
        public Segment ReverseSegment { get; set; }
    }
}
