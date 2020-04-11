using System.Data;
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
            //SQLiteConnection conn = new SQLiteConnection(StorageHelper.GetLocalFilePath());
            //conn.CreateTable<FavoriteLocationDataModel>();
            //var result = conn.Table<FavoriteLocationDataModel>().ToList();
            //conn.Close();

            base.OnAppearing();
        }

        private async Task GoBackAction()
        {
            await NavigationService.NavigateAsync("HomePage");
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}