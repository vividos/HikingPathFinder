using HikingPathFinder.Model;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a single Location in an auto complete view
    /// </summary>
    public class LocationAutoCompleteViewModel
    {
        /// <summary>
        /// Location to use in the view model
        /// </summary>
        private readonly Location location;

        /// <summary>
        /// Returns the name of the location object
        /// </summary>
        public string Name
        {
            get { return this.location.Name; }
        }

        /// <summary>
        /// Returns the location object
        /// </summary>
        public Location Location
        {
            get { return this.location; }
        }

        /// <summary>
        /// Creates a new view model from given location
        /// </summary>
        /// <param name="location">location to use</param>
        public LocationAutoCompleteViewModel(Location location)
        {
            this.location = location;
        }

        /// <summary>
        /// Converts view model to string; this is used in AutoCompleteView both for filtering
        /// locations and to format selected location.
        /// </summary>
        /// <returns>name of location</returns>
        public override string ToString()
        {
            return this.location.Name;
        }
    }
}
