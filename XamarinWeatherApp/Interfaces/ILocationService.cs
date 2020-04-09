using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.Interfaces
{
    public interface ILocationService 
    {
        Task<LocationModel> GetLocation(string Location);
    }
}

