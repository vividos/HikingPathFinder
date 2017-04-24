using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace HikingPathFinder.App.Converter
{
    /// <summary>
    /// Converter to convert an integer to a boolean value, using a greater-or-equal comparison.
    /// The value to compare is the binding value to convert, the value to compare to is the
    /// parameter passed to the converter. Use it e.g. like this:
    /// IsVisible="{Binding IntValue, Mode=OneWay, Converter={StaticResource IntToBooleanConverter}, ConverterParameter=42}"
    /// </summary>
    class IntToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts integer value to boolean by comparing value with parameter.
        /// </summary>
        /// <param name="value">value to use</param>
        /// <param name="targetType">target type to convert to</param>
        /// <param name="parameter">parameter to use, from ConverterParameter</param>
        /// <param name="culture">specific culture to use; unused</param>
        /// <returns>boolean true value when value greater-or-equal parameter, false else</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(targetType == typeof(bool), "convert target must be boolean");

            int minimumLength = System.Convert.ToInt32(parameter);
            return (int)value >= minimumLength;
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
