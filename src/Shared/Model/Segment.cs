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
        /// The description for this segment, in reverse walking direction
        /// </summary>
        public string ReverseDescription { get; set; }

        /// <summary>
        /// Indicates in which way the segment should be used when displaying
        /// to the user; when true, the ReverseDescription should be used, the
        /// MapStart and MapEnd points have to be swapped, the ReverseDuration
        /// is to be used, and and the TrackPointsList array has to be
        /// reversed.
        /// </summary>
        public bool UseReverseSegment { get; set; }

        /// <summary>
        /// Start of segment on map
        /// </summary>
        public MapPoint MapStart { get; set; }

        /// <summary>
        /// End of segment on map
        /// </summary>
        public MapPoint MapEnd { get; set; }

        /// <summary>
        /// Duration of segment in 100% walking speed
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Duration of segment, when going in reverse direction
        /// </summary>
        public TimeSpan ReverseDuration { get; set; }

        /// <summary>
        /// Segment length in km
        /// </summary>
        public double SegmentLengthInKm { get; set; }

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
