using Xamarin.Forms;

namespace XamarinWeatherApp.Views
{
    public partial class SearchCountryPage : ContentPage
    {
        private double _pageHeight;

        public SearchCountryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                await cakeDetail.TranslateTo(0, 100, 500, Easing.SinOut);
                Navi.Margin = new Thickness(30, 45, 30, 0);
                BackButton.IsVisible = true;
            }
            else
            {
                await cakeDetail.TranslateTo(0, 50, 500, Easing.SinOut);
                Navi.Margin = new Thickness(0, 15, 0, 0);
                BackButton.IsVisible = false;
            }
            base.OnAppearing();
        }       
    }
}