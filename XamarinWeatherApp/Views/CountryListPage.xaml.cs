using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinWeatherApp.Views
{
    public partial class CountryListPage : ContentPage
    {
        public CountryListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await Navi.FadeTo(0);
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
            await Navi.FadeTo(1, 500, Easing.SinIn);
            base.OnAppearing();
        }
    }
}
