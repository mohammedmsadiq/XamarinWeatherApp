using System;
using SQLite;
using Xamarin.Forms;

namespace XamarinWeatherApp.DataModel
{
    public class FavoriteLocationDataModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }
        public string LocationName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string LocationTime { get; set; }
    }
}