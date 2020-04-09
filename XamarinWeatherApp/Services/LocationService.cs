using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.Services
{
    public class LocationService : ILocationService
    {
        public LocationModel Result { get; set; }
        private const string host = "devru-latitude-longitude-find-v1.p.rapidapi.com";
        private const string key = "2c833b4d14msh50b2baaeb81c043p13dbfcjsn8df972f4a5ac";

        private static string endPoint(string Location)
        {
            return $"https://devru-latitude-longitude-find-v1.p.rapidapi.com/latlon.php?location={Location}";
        }

        HttpClient client;

        public LocationService()
        {
            client = new HttpClient();
        }

        public async Task<LocationModel> GetLocation(string Location)
        {
            try
            {
                var url = endPoint(Location);

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("x-rapidapi-host", host);
                request.Headers.Add("x-rapidapi-key", key);

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<LocationModel>(result);
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

