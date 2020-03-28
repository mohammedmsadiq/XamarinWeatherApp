using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private string textForLabel;
        private int offSetA;
        private int offSetB;
        private ObservableCollection<ForecastModel> item;
        private Location userLocation;

        protected readonly IWeatherService WeatherService;


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

                await this.loadData();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Debug.WriteLine(fnsEx);
                await DialogService.DisplayAlertAsync("Error", "FeatureNotSupportedException" + fnsEx, "OK");

            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Debug.WriteLine(pEx);
                await DialogService.DisplayAlertAsync("Error", "PermissionException" + pEx, "OK");

            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine(ex);
                await DialogService.DisplayAlertAsync("Error", "ex", "OK");

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
            get
            {
                return this.item;
            }

            set
            {
                this.SetProperty(ref this.item, value);
            }
        }

        public string TextForLabel
        {
            get => this.textForLabel;
            set => SetProperty(ref this.textForLabel, value);
        }

        public int OffSetA
        {
            get => this.offSetA;
            set => SetProperty(ref this.offSetA, value);
        }

        public int OffSetB
        {
            get => this.offSetB;
            set => SetProperty(ref this.offSetB, value);
        }
    }
}
