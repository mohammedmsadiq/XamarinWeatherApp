using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    public class CountryListPageViewModel : ViewModelBase
    {
        private ObservableCollection<FavoriteLocationModel> dBData;
        private ObservableCollection<ForecastModel> locationData;
        protected readonly IWeatherService WeatherService;

        public CountryListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IWeatherService weatherService) : base(navigationService, dialogService)
        {
            this.WeatherService = weatherService;
            this.GoBackCommand = new DelegateCommand(async () => { await this.GoBackAction(); });
            DBData = new ObservableCollection<FavoriteLocationModel>();
            LocationData = new ObservableCollection<ForecastModel>();

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
                                timezone = result.timezone,
                                icon = result.currently.icon,
                                summary = result.currently.summary
                            };
                            conn.Close(); 
                            using (SQLiteConnection postConn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
                            {
                                //delete table
                                //postConn.DropTable<FavoriteLocationForecastDataModel>();
                                postConn.CreateTable<FavoriteLocationForecastDataModel>();
                                int row = postConn.Insert(post);
                                Debug.WriteLine(itemToAdd.LocationName + " Added to DB");
                            }
                        });
                    });
                }
            }
            Debug.WriteLine("Database Count = " + list.Count());
            base.OnAppearing();
        }

        public ObservableCollection<FavoriteLocationModel> DBData
        {
            get => this.dBData;
            set => SetProperty(ref this.dBData, value);
        }

        public ObservableCollection<ForecastModel> LocationData
        {
            get => this.locationData;
            set => SetProperty(ref this.locationData, value);
        }

        private async Task GoBackAction()
        {
            await NavigationService.NavigateAsync("HomePage");
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}