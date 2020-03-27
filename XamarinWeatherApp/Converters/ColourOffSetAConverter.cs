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
                    return Color.FromHex("#810E64");
                case 2:
                    return Color.FromHex("#3E65A4");
                case 3:
                    return Color.FromHex("#52A4DB");
                case 4:
                    return Color.FromHex("#5393B2");
                case 5:
                    return Color.FromHex("#BC8DB8");
                case 6:
                    return Color.FromHex("#777B86");
                case 7:
                    return Color.FromHex("#122158");
                case 8:
                    return Color.FromHex("#B17139");
                case 9:
                    return Color.FromHex("#34323A");
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
