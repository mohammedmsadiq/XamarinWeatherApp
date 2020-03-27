using System.Collections.Generic;

namespace XamarinWeatherApp.Models
{
    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum3> data { get; set; }
    }
}