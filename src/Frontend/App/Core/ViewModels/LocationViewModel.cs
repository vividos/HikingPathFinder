using HikingPathFinder.Model;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a location; can be used as base class
    /// </summary>
    public class LocationViewModel
    {
        /// <summary>
        /// Location to display
        /// </summary>
        public readonly Location Location;

        /// <summary>
        /// Image path of image to display for location; based on location type
        /// </summary>
        public string ImagePath
        {
            get
            {
                // TODO get image source depending on location type
                return "Assets/Location/Summit.png";
            }
        }

        /// <summary>
        /// Name and altitude to display
        /// </summary>
        public string Name
        {
            get
            {
                return string.Format("{0} ({1} m)", this.Location.Name, (int)this.Location.Elevation);
            }
        }

        /// <summary>
        /// Creates new location view model
        /// </summary>
        /// <param name="location">location to use</param>
        public LocationViewModel(Location location)
        {
            this.Location = location;
        }
    }
}
