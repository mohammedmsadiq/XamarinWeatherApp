using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microcharts;
using Prism.Navigation;
using Prism.Services;
using Unity.Injection;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;
using XamarinWeatherApp.Styling;

namespace XamarinWeatherApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        protected readonly IWeatherService WeatherService;
        private string textForLabel;
        private ObservableCollection<Datum2> hourlyData;
        private ObservableCollection<Datum3> dailyData;
        private Location userLocation;
        private double deviceLatitude;
        private double deviceLongitude;
        private string timeZoneInfo;
        private int offSet;
        private string townCityName;
        private string currentTemp;
        private string currentSummary;
        private string hourlySummary;
        private string hourlyIcon;
        private string currentIcon;
        private string humidityPerc;
        private string cloudCoverPerc;

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.WeatherService = weatherService;
            this.Title = "Main Page";
            this.TextForLabel = "This is some text";
            HourlyData = new ObservableCollection<Datum2>();
            DailyData = new ObservableCollection<Datum3>();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            this.ExecuteAsyncTask(async () =>
            {
                await FindUserLocation();
            });
        }

        async Task FindUserLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                userLocation = await Geolocation.GetLastKnownLocationAsync();
                Debug.WriteLine(userLocation?.ToString() ?? "GetLastKnownLocation no location");
                userLocation = await Geolocation.GetLocationAsync(request);
                Debug.WriteLine(userLocation?.ToString() ?? "GetLocation no location");

                //Get Name of City/Town
                var placemarks = await Geocoding.GetPlacemarksAsync(userLocation.Latitude, userLocation.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    TownCityName = placemark.Locality.ToString();
                    Debug.WriteLine(TownCityName);

                }

                await this.loadData();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Debug.WriteLine(fnsEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Debug.WriteLine(pEx);
            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine(ex);
            }
        }

        private async Task loadData()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                await this.ExecuteAsyncTask(async () =>
                 {
                     if (userLocation != null)
                     {
                         var result = await this.WeatherService.GetForecast(userLocation.Latitude, userLocation.Longitude);
                         if (result != null)
                         {
                             DeviceLatitude = result.latitude;
                             DeviceLongitude = result.longitude;
                             TimeZoneInfo = result.timezone;
                             OffSet = result.offset;
                             CurrentTemp = Math.Round(UnitConverters.FahrenheitToCelsius(result.currently.temperature)).ToString();
                             CurrentSummary = result.currently.summary;
                             HumidityPerc = result.currently.HumidityPerc;
                             CloudCoverPerc = result.currently.CloudCoverPerc;
                             CurrentIcon = result.currently.icon.ToString();

                             HourlyData.Clear();
                             foreach (var item in result.hourly.data)
                             {
                                 var itemToAdd = new Datum2
                                 {
                                     time = item.time,
                                     HrTime = item.HrTime,
                                     summary = item.summary,
                                     icon = item.icon,
                                     precipIntensity = item.precipIntensity,
                                     precipProbability = item.precipProbability,
                                     temperature = Math.Round(UnitConverters.FahrenheitToCelsius(item.temperature)),
                                     STemperature = item.STemperature,
                                     apparentTemperature = item.apparentTemperature,
                                     dewPoint = item.dewPoint,
                                     humidity = item.humidity,
                                     pressure = item.pressure,
                                     windSpeed = item.windSpeed,
                                     windGust = item.windGust,
                                     windBearing = item.windBearing,
                                     cloudCover = item.cloudCover,
                                     uvIndex = item.uvIndex,
                                     visibility = item.visibility,
                                     ozone = item.ozone,
                                     precipType = item.precipType
                                 };
                                 Device.BeginInvokeOnMainThread(() =>
                                 {
                                     this.HourlyData.Add(itemToAdd);
                                 });
                             }

                             DailyData.Clear();
                             foreach (var dailyItem in result.daily.data)
                             {
                                 var dailyItemToAdd = new Datum3
                                 {
                                     time = dailyItem.time,
                                     DailyTime = dailyItem.DailyTime,
                                     summary = dailyItem.summary,
                                     icon = dailyItem.icon,
                                     sunriseTime = dailyItem.sunriseTime,
                                     sunsetTime = dailyItem.sunsetTime,
                                     LocalSunriseTime = dailyItem.LocalSunriseTime,
                                     LocalSunsetTime = dailyItem.LocalSunsetTime,
                                     moonPhase = dailyItem.moonPhase,
                                     precipIntensity = dailyItem.precipIntensity,
                                     precipIntensityMax = dailyItem.precipIntensityMax,
                                     precipIntensityMaxTime = dailyItem.precipIntensityMaxTime,
                                     precipProbability = dailyItem.precipProbability,
                                     precipType = dailyItem.precipType,
                                     temperatureHigh = Math.Round(UnitConverters.FahrenheitToCelsius(dailyItem.temperatureHigh)),
                                     temperatureHighTime = dailyItem.temperatureHighTime,
                                     temperatureLow = Math.Round(UnitConverters.FahrenheitToCelsius(dailyItem.temperatureLow)),
                                     temperatureLowTime = dailyItem.temperatureLowTime,
                                     apparentTemperatureHigh = dailyItem.apparentTemperatureHigh,
                                     apparentTemperatureHighTime = dailyItem.apparentTemperatureHighTime,
                                     apparentTemperatureLow = dailyItem.apparentTemperatureLow,
                                     apparentTemperatureLowTime = dailyItem.apparentTemperatureLowTime,
                                     dewPoint = dailyItem.dewPoint,
                                     humidity = dailyItem.humidity,
                                     pressure = dailyItem.pressure,
                                     windSpeed = dailyItem.windSpeed,
                                     windGust = dailyItem.windGust,
                                     windGustTime = dailyItem.windGustTime,
                                     windBearing = dailyItem.windBearing,
                                     cloudCover = dailyItem.cloudCover,
                                     uvIndex = dailyItem.uvIndex,
                                     uvIndexTime = dailyItem.uvIndexTime,
                                     visibility = dailyItem.visibility,
                                     ozone = dailyItem.ozone,
                                     temperatureMin = dailyItem.temperatureMin,
                                     temperatureMinTime = dailyItem.temperatureMinTime,
                                     temperatureMax = dailyItem.temperatureMax,
                                     temperatureMaxTime = dailyItem.temperatureMaxTime,
                                     apparentTemperatureMin = dailyItem.apparentTemperatureMin,
                                     apparentTemperatureMinTime = dailyItem.apparentTemperatureMinTime,
                                     apparentTemperatureMax = dailyItem.apparentTemperatureMax,
                                     apparentTemperatureMaxTime = dailyItem.apparentTemperatureMaxTime
                                 };
                                 Device.BeginInvokeOnMainThread(() =>
                                 {
                                     this.DailyData.Add(dailyItemToAdd);
                                 });
                             }
                         }
                     }
                     else
                     {
                         DialogService.DisplayAlertAsync("Error", "Unable to get Locaiton Details of device please review your setting to allow location", "OK");
                     }
                 });
            }
        }

        public ObservableCollection<Datum2> HourlyData
        {
            get => this.hourlyData;
            set => SetProperty(ref this.hourlyData, value);
        }

        public ObservableCollection<Datum3> DailyData
        {
            get => this.dailyData;
            set => SetProperty(ref this.dailyData, value);
        }

        public string TextForLabel
        {
            get => this.textForLabel;
            set => SetProperty(ref this.textForLabel, value);
        }

        public string CurrentTime
        {
            get => DateTime.Now.ToString("ddd dd, hh:mm tt");
        }

        //properties
        public double DeviceLatitude
        {
            get => this.deviceLatitude;
            set => SetProperty(ref this.deviceLatitude, value);
        }

        public double DeviceLongitude
        {
            get => this.deviceLongitude;
            set => SetProperty(ref this.deviceLongitude, value);
        }

        public string TownCityName
        {
            get => this.townCityName;
            set => SetProperty(ref this.townCityName, value);
        }

        public string CurrentIcon
        {
            get
            {
                var result = currentIcon;
                if (result == "clear-day")
                {
                    return IconToFont.ClearDay;
                }
                else if (result == "clear-night")
                {
                    return IconToFont.ClearNight;
                }
                else if (result == "rain")
                {
                    return IconToFont.rain;
                }
                else if (result == "snow")
                {
                    return IconToFont.snow;
                }
                else if (result == "sleet")
                {
                    return IconToFont.sleet;
                }
                else if (result == "wind")
                {
                    return IconToFont.wind;
                }
                else if (result == "fog")
                {
                    return IconToFont.fog;
                }
                else if (result == "cloudy")
                {
                    return IconToFont.cloudy;
                }
                else if (result == "partly-cloudy-day")
                {
                    return IconToFont.DartlyCloudyDay;
                }
                else if (result == "partly-cloudy-night")
                {
                    return IconToFont.DartlyCloudynight;
                }
                else if (result == "hail")
                {
                    return IconToFont.hail;
                }
                else if (result == "thunderstorm")
                {
                    return IconToFont.thunderstorm;
                }
                else if (result == "tornado")
                {
                    return IconToFont.tornado;
                }
                else
                {
                    return IconToFont.Error;
                }
            }
            set
            {
                this.SetProperty(ref this.currentIcon, value);
            }
        }

        public string TimeZoneInfo
        {
            get => this.timeZoneInfo;
            set => SetProperty(ref this.timeZoneInfo, value);
        }

        public string CurrentSummary
        {
            get => this.currentSummary;
            set => SetProperty(ref this.currentSummary, value);
        }

        public string HourlySummary
        {
            get => this.hourlySummary;
            set => SetProperty(ref this.hourlySummary, value);
        }

        public string HourlyIcon
        {
            get => this.hourlyIcon;
            set => SetProperty(ref this.hourlyIcon, value);
        }

        public int OffSet
        {
            get => this.offSet;
            set => SetProperty(ref this.offSet, value);
        }

        public string CurrentTemp
        {
            get => this.currentTemp;
            set => SetProperty(ref this.currentTemp, value);
        }

        public string HumidityPerc
        {
            get => this.humidityPerc;
            set => SetProperty(ref this.humidityPerc, value);
        }

        public string CloudCoverPerc
        {
            get => this.cloudCoverPerc;
            set => SetProperty(ref this.cloudCoverPerc, value);
        }
    }
}