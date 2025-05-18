using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Gymplanner.Wizard
{
    /// <summary>
    /// IMultiValueConverter: [0]=CurrentStep, [1]=ThisStep
    /// </summary>
    public class StepToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return Brushes.Gray;

            var currentStep = values[0].ToString();
            var step = values[1].ToString();

            return currentStep == step ? Brushes.Green : Brushes.Gray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
