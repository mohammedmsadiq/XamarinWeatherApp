using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinWeatherApp.Converters
{
    public class BackgroundColorConverter : IMarkupExtension, IValueConverter
    {
        public bool IsStart { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int temp)
            {
                if (temp <= 9)
                {
                    return IsStart ? Color.Blue : Color.AliceBlue; //Color.FromHex("#810E64") : Color.FromHex("#FB5139");
                }
                if (temp >= 10)
                {
                    return IsStart ? Color.DarkRed : Color.Red; //Color.FromHex("#810E64") : Color.FromHex("#FB5139");
                }
            }
            return IsStart ? Color.Transparent : Color.Transparent;//Color.FromHex("#810E64") : Color.FromHex("#FB5139");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }


        //A
        //switch (value)
        //{
        //    case 1:
        //        return Color.FromHex("#810E64");
        //    case 2:
        //        return Color.FromHex("#3E65A4");
        //    case 3:
        //        return Color.FromHex("#52A4DB");
        //    case 4:
        //        return Color.FromHex("#5393B2");
        //    case 5:
        //        return Color.FromHex("#BC8DB8");
        //    case 6:
        //        return Color.FromHex("#777B86");
        //    case 7:
        //        return Color.FromHex("#122158");
        //    case 8:
        //        return Color.FromHex("#B17139");
        //    case 9:
        //        return Color.FromHex("#34323A");
        //    default:
        //        return Color.FromHex("#52A4DB"); //clear
        //}

        //b
        //switch (value)
        //    {
        //        case 1:
        //            return Color.FromHex("#FB5139");
        //        case 2:
        //            return Color.FromHex("#CEACA7");
        //        case 3:
        //            return Color.FromHex("#73BAE1");
        //        case 4:
        //            return Color.FromHex("#A5BCC9");
        //        case 5:
        //            return Color.FromHex("#5D5E90");
        //        case 6:
        //            return Color.FromHex("#ADB7BE");
        //        case 7:
        //            return Color.FromHex("#9662A2");
        //        case 8:
        //            return Color.FromHex("#DAA55F");
        //        case 9:
        //            return Color.FromHex("#B3957F");
        //        default:
        //            return Color.FromHex("#73BAE1"); //clear
        //    }
    }
}

