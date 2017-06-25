using System;
using System.Collections.Generic;

namespace HikingPathFinder.Model
{
    /// <summary>
    /// App configuration, containing all objects and lists needed to use the functions of the
    /// app.
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Date/time when app config was last updated
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// General infos about the app
        /// </summary>
        public AppInfo Info { get; set; }

        /// <summary>
        /// List of locations that can be used to do tour planning
        /// </summary>
        public List<Location> LocationList { get; set; }

        /// <summary>
        /// List of static page info entries
        /// </summary>
        public List<StaticPageInfo> StaticPageInfoList { get; set; }

        /// <summary>
        /// List of available pre-planned tours
        /// </summary>
        public List<PrePlannedTour> PrePlannedToursList { get; set; }
    }
}
