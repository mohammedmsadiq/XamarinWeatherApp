using System;

using Xamarin.Forms;

namespace XamarinWeatherApp.Models
{
    public class FavoriteLocationModel 
    {
        public string LocationName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string LocationTime { get; set; }
    }
}

