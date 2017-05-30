using HikingPathFinder.Model;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace HikingPathFinder.App.Converter
{
    /// <summary>
    /// Converts Photo object to ImageSource
    /// </summary>
    public class PhotoToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts Photo object to an ImageSource to display in a view
        /// </summary>
        /// <param name="value">Photo object to convert</param>
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

            var photo = value as Photo;
            if (photo == null)
            {
                throw new NotSupportedException("converting objects other than Photo instances are not supported");
            }

            byte[] imageData = photo.JPEGData;
            if (imageData == null)
            {
                return null;
            }

            return ImageSource.FromStream(() => new MemoryStream(imageData));
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
