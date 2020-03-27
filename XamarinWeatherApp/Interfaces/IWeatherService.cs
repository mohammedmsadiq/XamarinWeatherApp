using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.Interfaces
{
    public interface IWeatherService 
    {
        Task<ForecastModel> GetForecast(double Latitude, double Longitude);
    }
}

