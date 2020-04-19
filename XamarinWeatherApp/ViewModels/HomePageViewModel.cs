using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        protected readonly IWeatherService WeatherService;
        private string textForLabel;
        private ObservableCollection<Datum2> hourlyData;
        private ObservableCollection<Datum3> dailyData;
        private Location userLocation;
        private Location lastKnownLocation;
        private Location currentLocation;
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
        private bool isCelsius;
        private ForecastModel result;

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.WeatherService = weatherService;
            this.Title = "Main Page";
            this.TextForLabel = "This is some text";
            this.IsCelsius = Settings.Settings.IsCelsius;
            this.SearchCountryCommand = new DelegateCommand(async () => { await this.SearchCountryAction(); });
            this.CountryListCommand = new DelegateCommand(async () => { await this.CountryListAction(); });
            HourlyData = new ObservableCollection<Datum2>();
            DailyData = new ObservableCollection<Datum3>();
        }

        private async Task CountryListAction()
        {
            await NavigationService.NavigateAsync("CountryListPage", animated: false);
        }

        private async Task SearchCountryAction()
        {
            await NavigationService.NavigateAsync("SearchCountryPage", animated: false);
        }

        public DelegateCommand SearchCountryCommand { get; private set; }
        public DelegateCommand CountryListCommand { get; private set; }

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
                lastKnownLocation = await Geolocation.GetLastKnownLocationAsync();
                Debug.WriteLine(lastKnownLocation?.ToString() ?? "GetLastKnownLocation no location");

                currentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));
                Debug.WriteLine(currentLocation?.ToString() ?? "GetLocation no location");

                userLocation = currentLocation != null ? currentLocation : lastKnownLocation;

                //Get Name of City/Town
                var placemarks = await Geocoding.GetPlacemarksAsync(userLocation.Latitude, userLocation.Longitude);

                if (placemarks == null)
                {
                    DialogService.DisplayAlertAsync("Error", "Unable to get Locaiton Details of device please review your setting to allow location", "OK");
                }

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    if (placemark.Locality != null)
                    {
                        TownCityName = placemark.Locality.ToString();
                    }
                    else
                    {
                        TownCityName = placemark.CountryName.ToString();
                    }

                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Debug.WriteLine(geocodeAddress);
                    Debug.WriteLine(TownCityName);
                    await this.loadData();
                }
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
                         Result = await this.WeatherService.GetForecast(userLocation.Latitude, userLocation.Longitude);
                         this.todayData();
                     }
                 });
            }
        }

        private void todayData()
        {
            if (Result != null)
            {
                DeviceLatitude = Result.latitude;
                DeviceLongitude = Result.longitude;
                TimeZoneInfo = Result.timezone;
                OffSet = Result.offset;
                CurrentTemp = IsCelsius == true ? Math.Round(UnitConverters.FahrenheitToCelsius(Result.currently.temperature)).ToString() : Math.Round(Result.currently.temperature).ToString();
                CurrentSummary = Result.currently.summary;
                HumidityPerc = Result.currently.HumidityPerc;
                CloudCoverPerc = Result.currently.CloudCoverPerc;
                CurrentIcon = Result.currently.icon.Replace("-", string.Empty);
                this.hourlyList();
                this.dailyList();
            }
        }

        private void dailyList()
        {
            this.DailyData.Clear();
            foreach (var dailyItem in Result.daily.data)
            {
                var dailyItemToAdd = new Datum3
                {
                    time = dailyItem.time,
                    DailyTime = dailyItem.DailyTime,
                    summary = dailyItem.summary,
                    icon = dailyItem.icon + ".png",
                    ImageIcon = dailyItem.ImageIcon,
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
                    temperatureHigh = IsCelsius == true ? Math.Round(UnitConverters.FahrenheitToCelsius(dailyItem.temperatureHigh)) : Math.Round(dailyItem.temperatureHigh),
                    temperatureHighTime = dailyItem.temperatureHighTime,
                    temperatureLow = IsCelsius == true ? Math.Round(UnitConverters.FahrenheitToCelsius(dailyItem.temperatureLow)) : Math.Round(dailyItem.temperatureLow),
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

        private void hourlyList()
        {
            this.HourlyData.Clear();
            foreach (var item in Result.hourly.data)
            {
                var itemToAdd = new Datum2
                {
                    time = item.time,
                    HrTime = item.HrTime,
                    summary = item.summary,
                    icon = item.icon + ".png",
                    ImageIcon = item.ImageIcon,
                    precipIntensity = item.precipIntensity,
                    precipProbability = item.precipProbability,
                    temperature = IsCelsius == true ? Math.Round(UnitConverters.FahrenheitToCelsius(item.temperature)) : Math.Round(item.temperature),
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
        }
        public ForecastModel Result
        {
            get => this.result;
            set => SetProperty(ref this.result, value);
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
                var r = currentIcon + ".png";
                return r;
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

        public bool IsCelsius
        {
            get
            {
                return isCelsius;
            }
            set
            {
                this.SetProperty(ref this.isCelsius, value, isToogledCelsius);
            }
        }

        public bool IsFavButtonVisible
        {
            get
            {
                var r = Settings.Settings.HasFavLocationsSaved;
                return r;
            }
        }

        private async void isToogledCelsius()
        {
            if (IsCelsius != true)
            {
                Settings.Settings.IsCelsius = false;
            }
            else
            {
                Settings.Settings.IsCelsius = true;
            }
            this.loadData();
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