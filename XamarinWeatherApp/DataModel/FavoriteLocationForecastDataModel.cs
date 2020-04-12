using System;
using SQLite;
using Xamarin.Forms;

namespace XamarinWeatherApp.DataModel
{
    public class FavoriteLocationForecastDataModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string LocationName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }
        public string icon { get; set; }
        public string summary { get; set; }
        public int time { get; set; }
        public int offset { get; set; }
        public int nearestStormDistance { get; set; }
        public int nearestStormBearing { get; set; }
        public double precipIntensity { get; set; }
        public double precipIntensityError { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
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
        public int visibility { get; set; }
        public double ozone { get; set; }
        public string HumidityPerc
        {
            get
            {
                var r = (humidity * 100) + "%";
                return r;
            }
            set { }
        }

        public string CloudCoverPerc
        {
            get
            {
                var r = (cloudCover * 100) + "%";
                return r;
            }
            set { }
        }
    }
}