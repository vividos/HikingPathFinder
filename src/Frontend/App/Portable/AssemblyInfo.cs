namespace HikingPathFinder.App
{
    /// <summary>
    /// Infos about this assembly
    /// </summary>
    internal static class AssemblyInfo
    {
        /// <summary>
        /// Backing store for ResourceAssemblyPath
        /// </summary>
        private static string resourceAssemblyPath;

        /// <summary>
        /// Returns the resource assembly path of this assembly
        /// </summary>
        public static string ResourceAssemblyPath
        {
            get
            {
                // lazy evaluation
                if (resourceAssemblyPath == null)
                {
                    string assemblyPath = typeof(AssemblyInfo).FullName;

                    // remove class name
                    resourceAssemblyPath = assemblyPath.Substring(0, assemblyPath.LastIndexOf('.'));
                }

                return resourceAssemblyPath;
            }
        }
    }
}
