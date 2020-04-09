using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms.Xaml;
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
            await NavigationService.NavigateAsync("HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SearchCountryPage, SearchCountryPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryListPage, CountryListPageViewModel>();

            containerRegistry.RegisterSingleton<IWeatherService, WeatherService>();
            containerRegistry.RegisterSingleton<ILocationService, LocationService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public enum Theme
    {
        Light,
        Dark
    }
}