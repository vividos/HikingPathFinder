﻿using System.Collections.Generic;

namespace HikingPathFinder.Model
{
    /// <summary>
    /// A location that can be used for tour planning, e.g. as intermediate stops.
    /// </summary>
    public class Location
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
        /// Elevation of location, in meter above sea level
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// Location on map
        /// </summary>
        public MapPoint MapLocation { get; set; }

        /// <summary>
        /// Description of location
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if this location is a start/stop location
        /// </summary>
        public bool IsTourLocation { get; set; }

        /// <summary>
        /// Type of location
        /// </summary>
        public LocationType Type { get; set; }

        /// <summary>
        /// A list of photos associated with this location
        /// </summary>
        public List<PhotoRef> PhotoList { get; set; }

        /// <summary>
        /// Link to external internet page, for more infos about location
        /// </summary>
        public string InternetLink { get; set; }
    }
}
