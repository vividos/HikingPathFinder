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

        /// <summary>
        /// Returns a printable representation of this object
        /// </summary>
        /// <returns>printable text</returns>
        public override string ToString()
        {
            return string.Format(
                "ID={0}, Desc={1}, Author={2}, Location={3}",
                this.Id,
                this.Description,
                this.Author,
                this.PhotoLocation.ToString());
        }
    }
}
