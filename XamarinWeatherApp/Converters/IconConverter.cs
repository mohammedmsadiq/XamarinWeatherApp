using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinWeatherApp.Styling;

namespace XamarinWeatherApp.Converters
{
    public class IconConverter : IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value.ToString();

            if (result == "clear-day")
            {
                return IconToFont.ClearDay;
            }
            else if (result == "clear-night")
            {
                return IconToFont.ClearNight;
            }
            else if (result == "rain")
            {
                return IconToFont.rain;
            }
            else if (result == "snow")
            {
                return IconToFont.snow;
            }
            else if (result == "sleet")
            {
                return IconToFont.sleet;
            }
            else if (result == "wind")
            {
                return IconToFont.wind;
            }
            else if (result == "fog")
            {
                return IconToFont.fog;
            }
            else if (result == "cloudy")
            {
                return IconToFont.cloudy;
            }
            else if (result == "partly-cloudy-day")
            {
                return IconToFont.DartlyCloudyDay;
            }
            else if (result == "partly-cloudy-night")
            {
                return IconToFont.DartlyCloudynight;
            }
            else if (result == "hail")
            {
                return IconToFont.hail;
            }
            else if (result == "thunderstorm")
            {
                return IconToFont.thunderstorm;
            }
            else if (result == "tornado")
            {
                return IconToFont.tornado;
            }
            else
            {
                return IconToFont.Error;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

