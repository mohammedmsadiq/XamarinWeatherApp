using System.Collections.Generic;

namespace XamarinWeatherApp.Models
{
    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum2> data { get; set; }
    }
}