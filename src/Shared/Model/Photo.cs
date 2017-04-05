namespace HikingPathFinder.Model
{
    /// <summary>
    /// Photo infos and photo data
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Reference infos for photo
        /// </summary>
        public PhotoRef Ref { get; set; }

        /// <summary>
        /// Actual JPEG data of photo
        /// </summary>
        public byte[] JPEGData { get; set; }
    }
}
