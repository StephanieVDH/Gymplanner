using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Gymplanner.Wizard
{
    /// <summary>
    /// values[0] = IList SelectedMuscles
    /// values[1] = string this muscle name
    /// Two‐way: when unchecked, remove; when checked, add.
    /// </summary>
    public class ListContainsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 &&
                values[0] is IList list &&
                values[1] is string muscle)
            {
                return list.Contains(muscle);
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (targetTypes.Length == 2 &&
                value is bool isChecked &&
                parameter is not null)
            {
                // We ignore parameter here—list & muscle come from values[]
                // But WPF only gives us `value` and parameter in ConvertBack,
                // so we’ll return the same two values, mutating the list as side-effect.
            }
            // We won’t implement ConvertBack here because WPF MultiBinding
            // convert-back is awkward—so we’ll instead handle check/uncheck
            // via a command on the CheckBox.Click in the XAML below.
            throw new NotSupportedException();
        }
    }
}

