using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinWeatherApp.Converters
{
    public class UnixTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = TimeSpan.FromSeconds((double)value);
            var localDateTime = new DateTime(timeSpan.Ticks).ToLocalTime();
            return localDateTime.ToString("HH");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

