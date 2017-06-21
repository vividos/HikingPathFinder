using HikingPathFinder.Model;
using System.Collections.Generic;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a single tour segment list entry. A tour segment consists of a location and
    /// a following description text on how to get to the next location. The last tour segment
    /// contains no description text, only the end location.
    /// </summary>
    public class TourSegmentListEntryViewModel
    {
        /// <summary>
        /// The location to display in this tour segment
        /// </summary>
        public Location Location
        {
            get { return this.location; }
        }

        /// <summary>
        /// The location position, as printable text
        /// </summary>
        public string LocationPosition
        {
            get
            {
                return this.location.MapLocation.ToString();
            }
        }

        /// <summary>
        /// Indicates if the location is a start location
        /// </summary>
        public bool IsStartLocation { get; private set; }

        /// <summary>
        /// Indicates if there is a list of segments or not (e.g. at the end location, there is no
        /// segment list.
        /// </summary>
        public bool HasSegmentList { get; private set; }

        /// <summary>
        /// Backing store for the tour segment location
        /// </summary>
        private readonly Location location;

        /// <summary>
        /// A list of segments to display
        /// </summary>
        private readonly List<Segment> segmentList;

        /// <summary>
        /// Creates a new view model for a tour segment list entry
        /// </summary>
        /// <param name="location">location to display</param>
        /// <param name="segmentList">list of segments to display</param>
        /// <param name="isStartLocation">indicates if the location is the start location</param>
        public TourSegmentListEntryViewModel(Location location, List<Segment> segmentList, bool isStartLocation)
        {
            this.location = location;
            this.segmentList = segmentList;
            this.IsStartLocation = isStartLocation;
        }
    }
}
