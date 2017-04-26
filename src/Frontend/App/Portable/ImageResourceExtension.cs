using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HikingPathFinder.App
{
    /// <summary>
    /// A xaml markup extension to refer to images in the same assembly, stored using the
    /// EmbeddedResource build action. See:
    /// https://developer.xamarin.com/guides/xamarin-forms/user-interface/images/
    /// Note that the class was slightly modified to support resource path notation.
    /// </summary>
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        /// <summary>
        /// Source string; the path name to use
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Called by xaml engine to provide a value
        /// </summary>
        /// <param name="serviceProvider">service provider; unused</param>
        /// <returns>provided object</returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Source == null)
            {
                return null;
            }

            string resourceName = GetResourceNameFromPath(this.Source);

            var imageSource = ImageSource.FromResource(resourceName);

            return imageSource;
        }

        /// <summary>
        /// Converts a resource path to a resource name, e.g. from "Assets/image.png" to
        /// "{AssemblyName}.Assets.image.png".
        /// </summary>
        /// <param name="resourcePath">resource path to convert</param>
        /// <returns>resource name</returns>
        private static string GetResourceNameFromPath(string resourcePath)
        {
            resourcePath = resourcePath.Replace('/', '.');
            resourcePath = AssemblyInfo.ResourceAssemblyPath + "." + resourcePath;

            return resourcePath;
        }
    }
}
