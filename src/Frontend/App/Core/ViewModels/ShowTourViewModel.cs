using HikingPathFinder.Model;
using System.Collections.Generic;
using System.Linq;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for showing a tour
    /// </summary>
    public class ShowTourViewModel
    {
        /// <summary>
        /// Tour to show
        /// </summary>
        private readonly Tour tour;

        /// <summary>
        /// List of locations to visit; used in tour summary
        /// </summary>
        public List<TourSummaryLocationViewModel> TourSummaryLocationList { get; private set; }

        /// <summary>
        /// Creates a new view model object for the start page
        /// </summary>
        /// <param name="tour">tour to display</param>
        public ShowTourViewModel(Tour tour)
        {
            this.tour = tour;

            this.SetupBindings();
        }

        /// <summary>
        /// Sets up bindings
        /// </summary>
        private void SetupBindings()
        {
            var tourLocationList =
                from location in this.tour.LocationList
                select new TourSummaryLocationViewModel(location);

            this.TourSummaryLocationList = tourLocationList.ToList();
        }
    }
}
