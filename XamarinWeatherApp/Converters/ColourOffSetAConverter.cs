using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinWeatherApp.Converters
{
    public class ColourOffSetAConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return Color.FromHex("#810E64"); ;
                default:
                    return "Transparent";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
