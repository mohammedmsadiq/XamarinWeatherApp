using System;
using Xamarin.Essentials;

namespace XamarinWeatherApp.Models
{
    public class Datum3
    {
        private string dailyTime;
        private string localSunriseTime;
        private string localSunsetTime;
        private string imageIcon;
        private string localWindSpeed;

        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; }
        public double moonPhase { get; set; }
        public double precipIntensity { get; set; }
        public double precipIntensityMax { get; set; }
        public int precipIntensityMaxTime { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperatureHigh { get; set; }
        public int temperatureHighTime { get; set; }
        public double temperatureLow { get; set; }
        public int temperatureLowTime { get; set; }
        public double apparentTemperatureHigh { get; set; }
        public int apparentTemperatureHighTime { get; set; }
        public double apparentTemperatureLow { get; set; }
        public int apparentTemperatureLowTime { get; set; }
        public double dewPoint { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public int windGustTime { get; set; }
        public int windBearing { get; set; }
        public double cloudCover { get; set; }
        public int uvIndex { get; set; }
        public int uvIndexTime { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
        public double temperatureMin { get; set; }
        public int temperatureMinTime { get; set; }
        public double temperatureMax { get; set; }
        public int temperatureMaxTime { get; set; }
        public double apparentTemperatureMin { get; set; }
        public int apparentTemperatureMinTime { get; set; }
        public double apparentTemperatureMax { get; set; }
        public int apparentTemperatureMaxTime { get; set; }

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

        public string LocalWindSpeed
        {
            get
            {
                //MPS to MPH 
                double mph = (windSpeed * 2.236936);
                var r = Settings.Settings.IsMPH == true ? Math.Round(mph) : Math.Round(UnitConverters.MilesToKilometers(mph));
                localWindSpeed = r.ToString();
                return localWindSpeed;
            }
            set { }
        }

        public string DailyTime
        {
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(time * TimeSpan.TicksPerSecond);
                DateTime r = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
                var localDateTime = r.ToLocalTime();
                dailyTime = localDateTime.ToString("dddd");
                return dailyTime;
            }
            set { }
        }

        public string LocalSunriseTime
        {
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(sunriseTime * TimeSpan.TicksPerSecond);
                DateTime r = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
                var localDateTime = r.ToLocalTime();
                localSunriseTime = localDateTime.ToString("h:mm tt");
                return localSunriseTime;
            }
            set { }
        }

        public string LocalSunsetTime
        {
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(sunsetTime * TimeSpan.TicksPerSecond);
                DateTime r = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
                var localDateTime = r.ToLocalTime();
                localSunsetTime = localDateTime.ToString("h:mm tt");
                return localSunsetTime;
            }
            set { }
        }
    }
}