using System;
using System.Collections.Generic;

namespace HikingPathFinder.Model
{
    /// <summary>
    /// A single tour that was planned ahead.
    /// </summary>
    public class Tour
    {
        /// <summary>
        /// Start location
        /// </summary>
        public Location StartLocation { get; set; }

        /// <summary>
        /// End location
        /// </summary>
        public Location EndLocation { get; set; }

        /// <summary>
        /// Track length, in km
        /// </summary>
        public double TrackLengthInKm { get; set; }

        /// <summary>
        /// Duration of tour, calculated at planning
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// A list of locations in order
        /// </summary>
        public List<Location> LocationList { get; set; }

        /// <summary>
        /// A list of segments in order, containing the segment description
        /// </summary>
        public List<Segment> SegmentList { get; set; }
    }
}
