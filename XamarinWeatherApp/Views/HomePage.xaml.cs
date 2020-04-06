using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using System.ComponentModel;


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

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Theme themeRequested = App.AppTheme == Theme.Light ? Theme.Dark : Theme.Light;
            MessagingCenter.Send<Page, Theme>(this, "ModeChanged", themeRequested);
        }
    }
}