using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace XamarinWeatherApp.ViewModels
{
    public class CountryListPageViewModel : ViewModelBase
    {
        public CountryListPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            this.GoBackCommand = new DelegateCommand(async () => { await this.GoBackAction(); });

        }

        private async Task GoBackAction()
        {
            await NavigationService.GoBackAsync(animated: false);
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}