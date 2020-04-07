using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using System.ComponentModel;
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
                await Task.Delay(5000);
                ThemeSwitch.IsPlaying = true;
            }
            else
            {
                ThemeSwitch.Animation = "darkmodeoff.json";
                await Task.Delay(5000);
                ThemeSwitch.IsPlaying = true;
            }
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Theme themeRequested = App.AppTheme == Theme.Light ? Theme.Dark : Theme.Light;
            MessagingCenter.Send<Page, Theme>(this, "ModeChanged", themeRequested);
        }
    }
}