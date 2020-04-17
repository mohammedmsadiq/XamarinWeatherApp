using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinWeatherApp.Views
{
    public partial class HomePage : ContentPage
    {      
        public HomePage()
        {
            InitializeComponent();
            DailyList.ItemTapped += (sender, e) =>
            {
                if (e.Item == null)
                {
                    return;
                }

                if (sender is Xamarin.Forms.ListView lv)
                {
                    lv.SelectedItem = null;
                }
            };
        }

        private double pageHeight;

        protected override void OnSizeAllocated(double width, double height)
        {
            pageHeight = height;
            //Navi.FadeTo(0);
            DetailSection.TranslationY = pageHeight;
            base.OnSizeAllocated(width, height);
        }

        protected override async void OnAppearing()
        {
            //await Navi.FadeTo(0);
            await Task.Delay(Constants.Constants.AnimationDelay);
            await DetailSection.TranslateTo(0, 0, 500, Easing.SinOut);            
            //await Navi.FadeTo(1, 500, Easing.SinIn);
            base.OnAppearing();
        }
    }
}