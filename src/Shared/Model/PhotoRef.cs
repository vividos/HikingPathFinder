namespace HikingPathFinder.Model
{
    /// <summary>
    /// A reference to a photo, not containing the photo data itself.
    /// </summary>
    public class PhotoRef
    {
        /// <summary>
        /// A photo ID, refering to a specific photo
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Description of the photo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Author of the photo
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Photo location; may be null
        /// </summary>
        public MapPoint PhotoLocation { get; set; }
    }
}
