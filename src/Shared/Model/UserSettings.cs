namespace HikingPathFinder.Model
{
    /// <summary>
    /// User settings for the currently logged in user (or the anonymous ghost user).
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Indicates if map is shown in 3D; default: false
        /// </summary>
        public bool ShowMapIn3D { get; set; }

        /// <summary>
        /// Creates a new user settings object with default values
        /// </summary>
        public UserSettings()
        {
            this.ShowMapIn3D = false;
        }
    }
}
