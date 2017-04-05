namespace HikingPathFinder.Model
{
    /// <summary>
    /// General application infos
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// Name of the site that the app is managing hiking for
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Name of the area where the app can be used
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// The map rectangle of the area the app is used
        /// </summary>
        public MapRectangle AreaRectangle { get; set; }

        /// <summary>
        /// The title of the static pages menu entry
        /// </summary>
        public string StaticPagesTitle { get; set; }

        /// <summary>
        /// The license of the content distributed with the app
        /// </summary>
        public string License { get; set; }
    }
}
