using HikingPathFinder.Model;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a single Location in an auto complete view
    /// </summary>
    public class LocationAutoCompleteViewModel : LocationViewModel
    {
        /// <summary>
        /// Creates a new view model from given location
        /// </summary>
        /// <param name="location">location to use</param>
        public LocationAutoCompleteViewModel(Location location)
            : base(location)
        {
        }

        /// <summary>
        /// Converts view model to string; this is used in AutoCompleteView both for filtering
        /// locations and to format selected location.
        /// </summary>
        /// <returns>name of location</returns>
        public override string ToString()
        {
            return this.Location.Name;
        }
    }
}
