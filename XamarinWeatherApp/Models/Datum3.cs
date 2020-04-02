using System;

namespace XamarinWeatherApp.Models
{
    public class Datum3
    {
        private string dailyTime;
        private string sunrise;
        private DateTime localSunriseTime;
        private DateTime localSunsetTime;

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

        public string DailyTime
        {
            get
            {
                var timeSpan = TimeSpan.FromSeconds((double)time);
                var localDateTime = new DateTime(timeSpan.Ticks).ToLocalTime();
                dailyTime = localDateTime.ToString("dd/MM");
                return dailyTime;
            }
            set { }
        }

        public DateTime LocalSunriseTime
        {
            get
            {
                var timeSpan = TimeSpan.FromSeconds((double)sunriseTime);
                localSunriseTime = new DateTime(timeSpan.Ticks).ToLocalTime();
                return localSunriseTime;
            }
            set { }
        }

        public DateTime LocalSunsetTime
        {
            get
            {
                var timeSpan = TimeSpan.FromSeconds((double)sunsetTime);
                localSunsetTime = new DateTime(timeSpan.Ticks).ToLocalTime();
                return localSunsetTime;
            }
            set { }
        }
    }
}