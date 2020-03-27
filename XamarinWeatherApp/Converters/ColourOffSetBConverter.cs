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
                    return Color.FromHex("#810E64");
                case 2:
                    return Color.FromHex("#CEACA7");
                case 3:
                    return Color.FromHex("#73BAE1");
                case 4:
                    return Color.FromHex("#A5BCC9");
                case 5:
                    return Color.FromHex("#5D5E90");
                case 6:
                    return Color.FromHex("#ADB7BE");
                case 7:
                    return Color.FromHex("#9662A2");
                case 8:
                    return Color.FromHex("#DAA55F");
                case 9:
                    return Color.FromHex("#B3957F");
                default:
                    return "Transparent";
            }
        }

        //1=Hot
        //2=Storm
        //3=Clear
        //4=Cloudy
        //5=fog
        //6=sleet
        //7=night-clear
        //8=sandstorm
        //9=tornado

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

