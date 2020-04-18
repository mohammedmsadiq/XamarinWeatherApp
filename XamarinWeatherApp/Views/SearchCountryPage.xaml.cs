using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinWeatherApp.Views
{
    public partial class SearchCountryPage : ContentPage
    {
        public SearchCountryPage()
        {
            InitializeComponent();

        }
        private double pageHeight;

        protected override void OnSizeAllocated(double width, double height)
        {
            pageHeight = height;
            DetailSection.TranslationY = pageHeight;
            base.OnSizeAllocated(width, height);
        }

        protected override async void OnAppearing()
        {
            await Task.Delay(Constants.Constants.AnimationDelay);
            await DetailSection.TranslateTo(0, 0, 500, Easing.SinOut);
            base.OnAppearing();
        }
    }
}