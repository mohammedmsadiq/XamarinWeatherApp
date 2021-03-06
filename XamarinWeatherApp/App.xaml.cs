﻿using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinWeatherApp.Helpers;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Services;
using XamarinWeatherApp.ViewModels;
using XamarinWeatherApp.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinWeatherApp
{
    public partial class App : PrismApplication
    {
        public static Theme AppTheme { get; set; }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("HomePage");

            var availableFolders = Helpers.StorageHelper.GetSpecialFolders();
            var datapath = Helpers.StorageHelper.GetLocalFilePath();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SplashScreenPage, SplashScreenPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SearchCountryPage, SearchCountryPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryListPage, CountryListPageViewModel>();

            containerRegistry.RegisterSingleton<IWeatherService, WeatherService>();
            containerRegistry.RegisterSingleton<ILocationService, LocationService>();
        }

        protected override async void OnStart()
        {
            await NavigationService.NavigateAsync("SplashScreenPage");
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
            if (status != PermissionStatus.Granted)         
            {
                await NavigationService.NavigateAsync("SplashScreenPage");
            }
        }
    }

    public enum Theme
    {
        Light,
        Dark
    }
}