using HikingPathFinder.Model;
using Xamarin.Forms;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a single location displayed in the tour summary
    /// </summary>
    public class TourSummaryLocationViewModel : LocationViewModel
    {
        /// <summary>
        /// Command called when user clicked on the item; this is used to jump to the tour
        /// location in the list.
        /// </summary>
        public Command ItemClicked { get; private set; }

        /// <summary>
        /// Creates a new view model instance for location
        /// </summary>
        /// <param name="location">location to display</param>
        public TourSummaryLocationViewModel(Location location)
            : base(location)
        {
            this.ItemClicked = new Command(() =>
            {
                // TODO jump to tour location in segment list
            });
        }
    }
}
