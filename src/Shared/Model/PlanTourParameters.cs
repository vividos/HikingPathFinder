using System.Collections.Generic;
using System.Linq;

namespace HikingPathFinder.Model
{
    /// <summary>
    /// Parameters for planning a tour
    /// </summary>
    public class PlanTourParameters
    {
        /// <summary>
        /// Selected start location; must be set
        /// </summary>
        public LocationRef StartLocation { get; set; }

        /// <summary>
        /// Selected end location; may be null, but at least one tour location must be present
        /// then.
        /// </summary>
        public LocationRef EndLocation { get; set; }

        /// <summary>
        /// List of tour locations to visit
        /// </summary>
        public List<LocationRef> TourLocationList { get; set; }

        /// <summary>
        /// Creates a new empty parameters object
        /// </summary>
        public PlanTourParameters()
        {
            this.TourLocationList = new List<LocationRef>();
        }

        /// <summary>
        /// Returns if parameters object is valid
        /// </summary>
        /// <returns>true when parameters are valid, false else</returns>
        public bool IsValid
        {
            get
            {
                if (this.StartLocation == null ||
                    this.TourLocationList == null)
                {
                    return false;
                }

                if (this.EndLocation == null &&
                    !this.TourLocationList.Any())
                {
                    return false;
                }

                return true;
            }
        }
    }
}
