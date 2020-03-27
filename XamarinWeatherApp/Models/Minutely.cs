using System.Collections.Generic;

namespace XamarinWeatherApp.Models
{
    public class Minutely
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum> data { get; set; }
    }
}