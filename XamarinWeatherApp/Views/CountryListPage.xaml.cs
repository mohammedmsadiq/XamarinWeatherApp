using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace XamarinWeatherApp.Views
{
    public partial class CountryListPage : ContentPage
    {
        public CountryListPage()
        {
            InitializeComponent();
        }

        private double pageHeight;

        protected override void OnSizeAllocated(double width, double height)
        {
            Navi.FadeTo(0);
            if (Device.RuntimePlatform == Device.iOS)
            {
                Navi.HeightRequest = 80;
            }
            else
            {
                Navi.HeightRequest = 50;
            }
            pageHeight = height;
            DetailSection.TranslationY = pageHeight;
            base.OnSizeAllocated(width, height);
        }

        protected override async void OnAppearing()
        {
            await Navi.FadeTo(0);
            await Task.Delay(Constants.Constants.AnimationDelay);
            await DetailSection.TranslateTo(0, 0, 500, Easing.SinOut);
            await Navi.FadeTo(1, 500, Easing.SinIn);
            base.OnAppearing();
        }
    }
}
