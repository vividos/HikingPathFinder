using HikingPathFinder.Model;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for the tour summary section
    /// </summary>
    public class TourSummaryViewModel
    {
        /// <summary>
        /// Tour to show summary for
        /// </summary>
        private readonly Tour tour;

        /// <summary>
        /// Tour name to show in tour summary
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Tour distance
        /// </summary>
        public string Distance
        {
            get
            {
                return string.Format("{0:0.0} km", this.tour.TrackLengthInKm);
            }
        }

        /// <summary>
        /// Tour duration to show in tour summary
        /// </summary>
        public string Duration
        {
            get
            {
                return this.tour.Duration.ToString(
                    this.tour.Duration.Days > 0 ? @"d\.hh\:mm" : @"h\:mm");
            }
        }

        /// <summary>
        /// Tour altitude up
        /// </summary>
        public string AltitudeUp
        {
            get
            {
                return string.Format("{0} m", this.tour.AltitudeUpInMeters);
            }
        }

        /// <summary>
        /// Tour altitude down
        /// </summary>
        public string AltitudeDown
        {
            get
            {
                return string.Format("{0} m", this.tour.AltitudeDownInMeters);
            }
        }

        /// <summary>
        /// Creates a new view model object for the tour summary
        /// </summary>
        /// <param name="tour">tour to display</param>
        public TourSummaryViewModel(Tour tour)
        {
            this.Name = "Unnamed tour";
            this.tour = tour;
        }
    }
}
