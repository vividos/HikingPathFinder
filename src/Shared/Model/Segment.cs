using System;
using System.Collections.Generic;

namespace HikingPathFinder.Model
{
    /// <summary>
    /// Segment of a tour; can be used in normal direction and in reverse
    /// direction, e.g. when hiking back from a summit.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Segment name, e.g. path name; may be empty
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description for this segment, in normal walking direction
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Rating of the segment, space separated; e.g. alpine hiking or skiing ratings
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Start location of segment
        /// </summary>
        public LocationRef LocationStart { get; set; }

        /// <summary>
        /// End location of segment
        /// </summary>
        public LocationRef LocationEnd { get; set; }

        /// <summary>
        /// Segment length, in km
        /// </summary>
        public double SegmentLengthInKm { get; set; }

        /// <summary>
        /// Duration of segment
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Altitude difference walking up, in meters
        /// </summary>
        public int AltitudeUpInMeters { get; set; }

        /// <summary>
        /// Altitude difference walking down, in meters
        /// </summary>
        public int AltitudeDownInMeters { get; set; }

        /// <summary>
        /// List of track points describing the segment (in normal direction)
        /// </summary>
        public List<TrackPoint> TrackPointsList { get; set; }

        /// <summary>
        /// List of photos relevant for this segment
        /// </summary>
        public List<PhotoRef> PhotoList { get; set; }
    }
}
