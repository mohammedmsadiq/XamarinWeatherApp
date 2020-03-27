using System.Collections.ObjectModel;
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
        private string timeStamp;
        private ObservableCollection<ForecastModel> item;

        IWeatherService WeatherService;


        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.WeatherService = weatherService;
            this.Title = "Main Page";
            this.TextForLabel = "This is some text";
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            this.loadData();
        }

        private async void loadData()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                this.ExecuteAsyncTask(async () =>
                {
                    var result = await this.WeatherService.GetForecast(location.Latitude, location.Longitude);

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
