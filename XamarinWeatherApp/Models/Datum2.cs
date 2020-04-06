using System;
using Xamarin.Essentials;
using XamarinWeatherApp.Styling;

namespace XamarinWeatherApp.Models
{
    public class Datum2
    {
        private string hrTime;
        private string sTemperature;
        private string imageIcon;

        public string icon { get; set; }
        public double time { get; set; }
        public string summary { get; set; }
        public double precipIntensity { get; set; }
        public double precipProbability { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get; set; }
        public double dewPoint { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public int windBearing { get; set; }
        public double cloudCover { get; set; }
        public int uvIndex { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
        public string precipType { get; set; }

        public string ImageIcon
        {
            get
            {
                string str = icon.Replace("-", string.Empty);
                imageIcon = str;
                return imageIcon;
            }
            set { }
        }

        public string STemperature
        {
            get
            {
                double result = temperature;
                sTemperature = result.ToString() + "°";
                return sTemperature;
            }
            set { }
        }

        public string HrTime
        {
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(time * TimeSpan.TicksPerSecond);
                DateTime r = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
                var localDateTime = r.ToLocalTime();
                hrTime = localDateTime.ToString("HH");
                return hrTime;
            }
            set { }
        }
    }
}