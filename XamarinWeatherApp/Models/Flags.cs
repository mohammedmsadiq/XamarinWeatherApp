using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinWeatherApp.Models
{
    public class Flags
    {
        public List<string> sources { get; set; }
        [JsonProperty(PropertyName = "__invalid_name__nearest-station")]
        public double invalidNameNearestStation { get; set; }
        public string units { get; set; }
    }
}