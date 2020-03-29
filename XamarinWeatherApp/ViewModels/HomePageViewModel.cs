﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private int offSetA;
        private int offSetB;
        private ObservableCollection<ForecastModel> item;
        private Location userLocation;
        private double deviceLatitude;
        private double deviceLongitude;
        private string timeZoneInfo;
        private int offSet;
        private string townCityName;
        private string currentTemp;
        private string currentSummary;
        private string lottieImage;
        private string country;

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.WeatherService = weatherService;
            this.Title = "Main Page";
            this.TextForLabel = "This is some text";
            Item = new ObservableCollection<ForecastModel>();
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
                    Country = placemark.CountryName.Replace(" ", string.Empty) + ".jpg";
                    Debug.WriteLine(TownCityName);
                    Debug.WriteLine(Country);

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
                this.ExecuteAsyncTask(async () =>
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
                        }
                    }
                    else
                    {
                        DialogService.DisplayAlertAsync("Error", "Unable to get Locaiton Details of device please review your setting to allow location", "OK");
                    }
                });
            }
        }

        public ObservableCollection<ForecastModel> Item
        {
            get => this.item;
            set => SetProperty(ref this.item, value);
        }

        public string TextForLabel
        {
            get => this.textForLabel;
            set => SetProperty(ref this.textForLabel, value);
        }

        public int OffSetA
        {
            get
            {
                return offSetA;
            }
            set
            {
                this.SetProperty(ref this.offSetA, value);
            }
        }

        public int OffSetB
        {
            get
            {
                return offSetB;
            }
            set
            {
                this.SetProperty(ref this.offSetB, value);
            }
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

        public string LottieImage
        {
            get => this.lottieImage;
            set => SetProperty(ref this.lottieImage, value);
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

        public string Country
        {
            get => this.country;
            set => SetProperty(ref this.country, value);
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

    }
}