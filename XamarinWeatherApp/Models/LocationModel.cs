using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinWeatherApp.Models
{
    public class LocationModel 
    {
        public string powerdBy { get; set; }
        public List<GeoModel> Results { get; set; }
    }   
}