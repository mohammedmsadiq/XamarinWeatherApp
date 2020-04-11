using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SQLite;
using XamarinWeatherApp.DataModel;
using XamarinWeatherApp.Helpers;

namespace XamarinWeatherApp.ViewModels
{
    public class CountryListPageViewModel : ViewModelBase
    {
        public CountryListPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            this.GoBackCommand = new DelegateCommand(async () => { await this.GoBackAction(); });
        }

        public override void OnAppearing()
        {
            using (SQLiteConnection conn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
            {
                conn.CreateTable<FavoriteLocationDataModel>();
                var list = conn.Table<FavoriteLocationDataModel>().ToList();
                Debug.WriteLine("Database Count = " + list.Count());
            }


            base.OnAppearing();
        }

        private async Task GoBackAction()
        {
            await NavigationService.NavigateAsync("HomePage");
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}