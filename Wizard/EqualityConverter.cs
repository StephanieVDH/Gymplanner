using System;
using System.Globalization;
using System.Windows.Data;

namespace Gymplanner.Wizard
{
    /// <summary>
    /// Returns true if values[0].ToString() == values[1].ToString(), false otherwise.
    /// Used in a MultiBinding to compare SelectedFocusId to Button.Tag (or other pairs).
    /// </summary>
    public class EqualityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length >= 2 && values[0] != null && values[1] != null)
            {
                // compare string representations
                return values[0].ToString() == values[1].ToString();
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("EqualityConverter does not support ConvertBack.");
        }
    }
}