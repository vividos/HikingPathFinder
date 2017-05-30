using System;

namespace HikingPathFinder.App.Models
{
    /// <summary>
    /// Menu entry for navigation drawer menu
    /// </summary>
    public class MenuEntry
    {
        /// <summary>
        /// Title of menu entry
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Icon source (.png filename)
        /// </summary>
        public string IconSource { get; set; }

        /// <summary>
        /// Type of page to open when menu entry is selected
        /// </summary>
        public Type PageType { get; set; }
    }
}
