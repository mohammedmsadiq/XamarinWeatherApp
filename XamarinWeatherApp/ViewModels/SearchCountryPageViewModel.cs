using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SQLite;
using Xamarin.Forms;
using XamarinWeatherApp.DataModel;
using XamarinWeatherApp.Helpers;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.ViewModels
{
    public class SearchCountryPageViewModel : ViewModelBase
    {
        protected readonly ILocationService LocationService;
        protected readonly IWeatherService WeatherService;
        ObservableCollection<GeoModel> data;

        public SearchCountryPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ILocationService locationService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.LocationService = locationService;
            this.WeatherService = weatherService;
            this.GoBackCommand = new DelegateCommand(async () => { await this.GoBackAction(); });
            this.Data = new ObservableCollection<GeoModel>();
        }

        public DelegateCommand<GeoModel> ItemSelected => new DelegateCommand<GeoModel>(async (Param) => await this.ItemSelectedAction(Param));

        private async Task ItemSelectedAction(GeoModel item)
        {
            var result = await this.WeatherService.GetForecast(item.Latitude, item.Longitude);

            FavoriteLocationForecastDataModel post = new FavoriteLocationForecastDataModel()
            {
                LocationName = item.name,
                DateAdded = DateTime.Now,
                latitude = item.Latitude,
                longitude = item.Latitude,
                timezone = result.timezone,
                icon = result.currently.icon,
                summary = result.currently.summary,
                time = result.currently.time,
                offset = result.offset,
                temperature = result.currently.temperature,
                apparentTemperature = result.currently.apparentTemperature,
                dewPoint = result.currently.dewPoint,
                humidity = result.currently.humidity,
                cloudCover = result.currently.cloudCover
            };

            using (SQLiteConnection postConn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
            {
                //delete table
                postConn.CreateTable<FavoriteLocationForecastDataModel>();
                int rows = postConn.InsertOrReplace(post);

                Debug.WriteLine("postConn Added " + item.name + " to DB");
            }

            await NavigationService.NavigateAsync("HomePage", animated: false);
        }

        private string _searchedText;

        public string SearchedText
        {
            get
            {
                return _searchedText;
            }
            set
            {
                _searchedText = value;
                OnPropertyChanged("SearchedText");
                SearchCommandExecute(SearchedText);
            }
        }


        private void SearchCommandExecute(string Text)
        {
            var r = Text;
            this.ExecuteAsyncTask(async () =>
            {
                var result = await this.LocationService.GetLocation(Text);
                this.Data.Clear();
                foreach (var item in result.Results)
                {
                    var itemToAdd = new GeoModel
                    {
                        name = item.name,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        tzs = item.tzs,
                        tz = item.tz
                    };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.Data.Add(itemToAdd);
                    });
                }
            });
        }

        public ObservableCollection<GeoModel> Data { get; set; }

        private async Task GoBackAction()
        {
            await NavigationService.NavigateAsync("HomePage", animated: false);
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}