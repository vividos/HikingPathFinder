namespace HikingPathFinder.Model
{
    /// <summary>
    /// A pre-planned tour that also contains a specific name and a short description.
    /// </summary>
    public class PrePlannedTour
    {
        /// <summary>
        /// Given tour name
        /// </summary>
        public string TourName { get; set; }

        /// <summary>
        /// A short description, containing a tour summary
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The actual tour
        /// </summary>
        public Tour Tour { get; set; }
    }
}
