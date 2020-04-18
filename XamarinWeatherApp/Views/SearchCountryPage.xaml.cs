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
            //Navi.FadeTo(0);
            pageHeight = height;
            //DetailSection.TranslationY = pageHeight;
            base.OnSizeAllocated(width, height);
        }

        protected override async void OnAppearing()
        {
            //await Navi.FadeTo(0);
            await Task.Delay(Constants.Constants.AnimationDelay);
            //await DetailSection.TranslateTo(0, 0, 500, Easing.SinOut);
            //await Navi.FadeTo(1, 500, Easing.SinIn);
            base.OnAppearing();
        }
    }
}