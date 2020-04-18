using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace XamarinWeatherApp.ViewModels
{
    public class SplashScreenPageViewModel : ViewModelBase
    {

        public SplashScreenPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        public async Task GoToHome()
        {
            await NavigationService.NavigateAsync("HomePage", animated: false);
        }
    }
}