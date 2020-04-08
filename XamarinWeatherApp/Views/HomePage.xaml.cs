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

        protected override void OnAppearing()
        {
            this.setImage();
            base.OnAppearing();
        }

        private async Task setImage()
        {
            if (App.AppTheme == Theme.Light)
            {
                ThemeSwitch.Animation = "darkmodeon.json";
                if (Device.RuntimePlatform == Device.iOS)
                {
                    ThemeSwitch.HorizontalOptions = LayoutOptions.FillAndExpand;
                    ThemeSwitch.VerticalOptions = LayoutOptions.FillAndExpand;
                }
                else
                {
                    ThemeSwitch.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    ThemeSwitch.VerticalOptions = LayoutOptions.EndAndExpand;
                }
                await Task.Delay(5000);
                ThemeSwitch.IsPlaying = true;
            }
            else
            {
                ThemeSwitch.Animation = "darkmodeoff.json";
                if (Device.RuntimePlatform == Device.iOS)
                {
                    ThemeSwitch.HorizontalOptions = LayoutOptions.FillAndExpand;
                    ThemeSwitch.VerticalOptions = LayoutOptions.FillAndExpand;
                }
                else
                {
                    ThemeSwitch.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    ThemeSwitch.VerticalOptions = LayoutOptions.EndAndExpand;
                }
                await Task.Delay(5000);
                ThemeSwitch.IsPlaying = true;
            }
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Theme themeRequested = App.AppTheme == Theme.Light ? Theme.Dark : Theme.Light;
            MessagingCenter.Send<Page, Theme>(this, "ModeChanged", themeRequested);
            setImage();
        }
    }
}