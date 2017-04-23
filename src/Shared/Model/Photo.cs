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

        /// <summary>
        /// Returns a printable representation of this object
        /// </summary>
        /// <returns>printable text</returns>
        public override string ToString()
        {
            return string.Format(
                "{0}, Data={1} bytes",
                this.Ref.ToString(),
                this.JPEGData.Length);
        }
    }
}
