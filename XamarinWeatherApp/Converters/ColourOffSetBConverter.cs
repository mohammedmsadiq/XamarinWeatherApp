using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinWeatherApp.Converters
{
    public class ColourOffSetBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return Color.FromHex("#FC5038");
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

