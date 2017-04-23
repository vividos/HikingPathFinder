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
        /// General infos about the app
        /// </summary>
        public AppInfo Info { get; set; }

        /// <summary>
        /// List of start and end locations
        /// </summary>
        public List<Location> StartEndLocationList { get; set; }

        /// <summary>
        /// List of available tour locations
        /// </summary>
        public List<Location> TourLocationList { get; set; }

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
