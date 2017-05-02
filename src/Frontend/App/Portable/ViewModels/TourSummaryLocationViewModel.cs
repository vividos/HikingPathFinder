using HikingPathFinder.Model;
using Xamarin.Forms;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a single location displayed in the tour summary
    /// </summary>
    public class TourSummaryLocationViewModel
    {
        /// <summary>
        /// Location to display
        /// </summary>
        private readonly Location location;

        /// <summary>
        /// Image to display
        /// </summary>
        public string ImagePath
        {
            get
            {
                // TODO get image source depending on location type
                // return "Assets/Location/Summit.png";
                return null;
            }
        }

        /// <summary>
        /// Name and altitude to display
        /// </summary>
        public string Name
        {
            get
            {
                return string.Format("{0} ({1} m)", this.location.Name, (int)this.location.Elevation);
            }
        }

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
        {
            this.location = location;

            this.ItemClicked = new Command(() =>
            {
                // TODO jump to tour location in segment list
            });
        }
    }
}
