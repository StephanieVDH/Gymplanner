using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Gymplanner.Wizard
{
    public class StepToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Questions step) return null;

            string file = step switch
            {
                Questions.Level => "icon-level.png",
                Questions.Goal => "icon-goal.png",
                Questions.Sessions => "icon-sessions.png",
                Questions.Duration => "icon-duration.png",
                Questions.Focus => "icon-focus.png",
                _ => "icon-default.png"
            };

            // assumes these live in /Images/ with Build Action = Resource
            return new BitmapImage(new Uri($"/Images/{file}", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
