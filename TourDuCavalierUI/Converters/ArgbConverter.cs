using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using Color = TourDuCavalierLib.ViewModel.Color;

namespace TourDuCavalierUI.Converters
{
    public class ArgbConverter : IValueConverter
    {
        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            return new SolidColorBrush(System.Windows.Media.Color.FromRgb(color.R, color.G, color.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}