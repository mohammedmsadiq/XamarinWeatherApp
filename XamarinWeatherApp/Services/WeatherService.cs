using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        public ForecastModel Result { get; set; }

        private static string endPoint(double Latitude, double Longitude)
        {
            return $"https://api.darksky.net/forecast/d867cc739edb75f665ab2a5a47cdf4e1/{Latitude},{Longitude}";
        }

        HttpClient client;

        public WeatherService()
        {
            client = new HttpClient();
        }

        public async Task<ForecastModel> GetForecast(double Latitude, double Longitude)
        {
            try
            {
                var url = endPoint(Latitude, Longitude);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<ForecastModel>(result);
                }
                else
                {
                    var exception = new Exception($"Api Error: {response.RequestMessage}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Result;
        }

    }
}

