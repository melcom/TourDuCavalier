using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using TourDuCavalierLib.ViewModel;


namespace TourDuCavalierUIApp.Converters
{
    public class ArgbConverter : IValueConverter
    {
        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color color = (Color)value;
            return new SolidColorBrush(Windows.UI.Color.FromArgb(255, color.R, color.G, color.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        #endregion
    }
}