using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinWeatherApp.DataModel;
using XamarinWeatherApp.Helpers;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.ViewModels
{
    public class CountryListPageViewModel : ViewModelBase
    {
        private ObservableCollection<FavoriteLocationModel> dBData;
        private ObservableCollection<FavoriteLocationForecastDataModel> favoriteLocationData;
        protected readonly IWeatherService WeatherService;

        public CountryListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.WeatherService = weatherService;
            this.GoBackCommand = new DelegateCommand(async () => { await this.GoBackAction(); });
            DBData = new ObservableCollection<FavoriteLocationModel>();
            FavoriteLocationData = new ObservableCollection<FavoriteLocationForecastDataModel>();
        }

        public override void OnAppearing()
        {
            SQLiteConnection conn = new SQLiteConnection(StorageHelper.GetLocalFilePath());
            conn.CreateTable<FavoriteLocationDataModel>();
            var list = conn.Table<FavoriteLocationDataModel>().ToList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    var itemToAdd = new FavoriteLocationModel
                    {
                        LocationName = item.LocationName,
                        Longitude = item.Longitude,
                        Latitude = item.Latitude
                    };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.DBData.Add(itemToAdd);
                        this.ExecuteAsyncTask(async () =>
                        {
                            var result = await this.WeatherService.GetForecast(itemToAdd.Latitude, itemToAdd.Longitude);
                            FavoriteLocationForecastDataModel post = new FavoriteLocationForecastDataModel()
                            {
                                LocationName = itemToAdd.LocationName,
                                latitude = result.latitude,
                                longitude = result.longitude,
                                offset = result.offset,
                                temperature = result.currently.temperature,
                                time = result.currently.time,
                                timezone = result.timezone,
                                icon = result.currently.icon,
                                summary = result.currently.summary
                            };
                            conn.Close();
                            SQLiteConnection postConn = new SQLiteConnection(StorageHelper.GetLocalFilePath());
                            //delete table
                            postConn.CreateTable<FavoriteLocationForecastDataModel>();
                            int row = postConn.Insert(post);
                            Debug.WriteLine(itemToAdd.LocationName + " Added to DB");
                        });
                    });
                }
            }
            Debug.WriteLine("Database Count = " + list.Count());

            using (SQLiteConnection postConn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
            {
                conn.CreateTable<FavoriteLocationForecastDataModel>();
                var favorites = conn.Table<FavoriteLocationForecastDataModel>().ToList();
                this.FavoriteLocationData.Clear();
                foreach (var fav in favorites)
                {
                    var favToAdd = new FavoriteLocationForecastDataModel
                    {
                        LocationName = fav.LocationName,
                        icon = fav.icon + ".png",
                        ImageIcon = fav.ImageIcon,
                        temperature = Math.Round(UnitConverters.FahrenheitToCelsius(fav.temperature)),
                        LocalTime = fav.LocalTime
                    };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.FavoriteLocationData.Add(favToAdd);
                    });
                }
            }
            base.OnAppearing();
        }

        public ObservableCollection<FavoriteLocationModel> DBData
        {
            get => this.dBData;
            set => SetProperty(ref this.dBData, value);
        }

        public ObservableCollection<FavoriteLocationForecastDataModel> FavoriteLocationData
        {
            get => this.favoriteLocationData;
            set => SetProperty(ref this.favoriteLocationData, value);
        }

        private async Task GoBackAction()
        {
            await NavigationService.NavigateAsync("HomePage");
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}