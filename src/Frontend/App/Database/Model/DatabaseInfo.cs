using SQLite.Net.Attributes;

namespace HikingPathFinder.App.Database.Model
{
    /// <summary>
    /// Table class to store database infos of current database
    /// </summary>
    [Preserve]
    internal class DatabaseInfo
    {
        /// <summary>
        /// Primary key; not used, but needed, in order to do Update() on the table
        /// </summary>
        [PrimaryKey, Preserve]
        public int Id { get; internal set; }

        /// <summary>
        /// Database version number
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Default constructor; used by table deserialisation
        /// </summary>
        [Preserve]
        public DatabaseInfo()
        {
            this.Id = 42;
            this.Version = 0;
        }

        /// <summary>
        /// Creates new database info object with specific version number
        /// </summary>
        /// <param name="version">database version number</param>
        public DatabaseInfo(int version)
        {
            this.Id = 42;
            this.Version = version;
        }
    }
}
