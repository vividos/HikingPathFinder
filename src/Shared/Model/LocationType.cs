namespace HikingPathFinder.Model
{
    /// <summary>
    /// Type of location
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// Location is a summit
        /// </summary>
        Summit = 0,

        /// <summary>
        /// A mountain pass, saddle or col
        /// </summary>
        Pass = 1,

        /// <summary>
        /// A lake access point
        /// </summary>
        Lake = 2,

        /// <summary>
        /// A bridge
        /// </summary>
        Bridge = 3,

        /// <summary>
        /// A viewpoint, not being a summit or other suitable type
        /// </summary>
        Viewpoint = 4,

        /// <summary>
        /// An undefined location; should not be used
        /// </summary>
        Undefined = 999,
    }
}
