using System;
using Xamarin.Essentials;

namespace XamarinWeatherApp.Models
{
    public class Datum2
    {
        private string hrTime;
        private string sTemperature;

        public double time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
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
                var timeSpan = TimeSpan.FromSeconds((double)time);
                var localDateTime = new DateTime(timeSpan.Ticks).ToLocalTime();
                hrTime = localDateTime.ToString("HH");
                return hrTime;
            }
            set { }

        }

    }
}