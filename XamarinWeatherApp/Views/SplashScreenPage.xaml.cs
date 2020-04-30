using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Xamarin.Essentials;
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
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
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
