using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using XamarinWeatherApp.ViewModels;

namespace XamarinWeatherApp.Views
{
    public partial class SplashScreenPage : ContentPage
    {
        private SplashScreenPageViewModel splashScreenPageViewModel;

        public SplashScreenPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            splashScreenPageViewModel = (SplashScreenPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            CheckLocation();
            base.OnAppearing();
        }

        private async void CheckLocation()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    var r =await DisplayAlert("Need location", "Gunna need that location", "OK", "Cancel");
                    if (r == true)
                    {                       
                        await splashScreenPageViewModel.GoToHome();
                    }
                }
                status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
            }

            if (status == PermissionStatus.Granted)
            {
                await splashScreenPageViewModel.GoToHome();
            }
            else if (status != PermissionStatus.Unknown)
            {
                await DisplayAlert("Location Denied", "Can not continue, Goto settings and allow the app to request location", "OK");
                CrossPermissions.Current.OpenAppSettings();
            }
        }
    }
}
