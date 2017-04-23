using System.Collections.Generic;

namespace HikingPathFinder.Model
{
    /// <summary>
    /// Infos about a static page, including content
    /// </summary>
    public class StaticPageInfo
    {
        /// <summary>
        /// Static page ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Heading for page
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Markdown-formatted content of page
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// List of photos referenced in the content
        /// </summary>
        public List<PhotoRef> PhotoList { get; set; }
    }
}
