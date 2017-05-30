using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace HikingPathFinder.App.Converter
{
    /// <summary>
    /// Converts resource path of image enbedded with build action EmbeddedResource to an
    /// ImageSource. The path is relative to the project, e.g. Assets/image.png.
    /// </summary>
    public class ResourcePathToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts resource path to an ImageSource object to display in a view
        /// </summary>
        /// <param name="value">resource path to convert</param>
        /// <param name="targetType">target type; must be ImageSource</param>
        /// <param name="parameter">parameter to use; unused</param>
        /// <param name="culture">specific culture to use; unused</param>
        /// <returns>converted value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Debug.Assert(targetType == typeof(ImageSource), "target type must be ImageSource");

            var resourcePath = value as string;

            if (string.IsNullOrEmpty(resourcePath))
            {
                return null;
            }

            string resourceName = GetResourceNameFromPath(resourcePath);

            //// troubleshooting code:
            ////var assembly = typeof(ResourcePathToImageSourceConverter).GetTypeInfo().Assembly;
            ////foreach (var resource in assembly.GetManifestResourceNames())
            ////{
            ////    System.Diagnostics.Debug.WriteLine("found resource: " + resource);
            ////}

            return ImageSource.FromResource(resourceName);
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

        /// <summary>
        /// Converts back; not implemented
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter to use; unused</param>
        /// <param name="culture">specific culture to use; unused</param>
        /// <returns>converted value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
