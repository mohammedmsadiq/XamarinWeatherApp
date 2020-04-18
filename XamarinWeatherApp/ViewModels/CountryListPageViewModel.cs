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
            this.ExecuteAsyncTask(async () =>
            {
                await ReadData();
                LoadFavLocations();
            });
        }

        private async Task ReadData()
        {
            SQLiteConnection conn = new SQLiteConnection(StorageHelper.GetLocalFilePath());
            conn.CreateTable<FavoriteLocationForecastDataModel>();
            DBData.Clear();
            var list = conn.Table<FavoriteLocationForecastDataModel>().ToList();
            Debug.WriteLine("listConn Count = " + list.Count());
            if (list != null)
            {
                foreach (var item in list)
                {
                    var itemToAdd = new FavoriteLocationModel
                    {
                        LocationName = item.LocationName,
                        Longitude = item.longitude,
                        Latitude = item.latitude,
                    };
                    this.DBData.Add(itemToAdd);

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
                        summary = result.currently.summary,
                    };
                    conn.Close();
                    using (SQLiteConnection postConn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
                    {
                        //delete table
                        postConn.CreateTable<FavoriteLocationForecastDataModel>();
                        int row = postConn.InsertOrReplace(post);
                        Debug.WriteLine("postConn updated " + itemToAdd.LocationName + " to DB");
                    }
                }
            }
        }

        public async Task LoadFavLocations()
        {
            using (SQLiteConnection readConn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
            {
                readConn.CreateTable<FavoriteLocationForecastDataModel>();
                var favorites = readConn.Table<FavoriteLocationForecastDataModel>().ToList();
                Debug.WriteLine("ReadConn Count = " + favorites.Count());
                this.FavoriteLocationData.Clear();
                foreach (var fav in favorites)
                {
                    var favToAdd = new FavoriteLocationForecastDataModel
                    {
                        LocationName = fav.LocationName,
                        icon = fav.icon + ".png",
                        ImageIcon = fav.ImageIcon,
                        temperature = Settings.Settings.IsCelsius ? Math.Round(UnitConverters.FahrenheitToCelsius(fav.temperature)) : Math.Round(fav.temperature),
                        LocalTime = fav.LocalTime,
                        DateAdded = fav.DateAdded.ToLocalTime()
                    };
                    this.FavoriteLocationData.Add(favToAdd);
                }
            }
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
            await NavigationService.NavigateAsync("HomePage", animated: false);
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}