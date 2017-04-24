using HikingPathFinder.Model;

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
        }
    }
}
