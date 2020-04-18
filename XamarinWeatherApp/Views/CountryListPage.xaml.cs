using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using SQLite;
using Unity.Injection;
using Xamarin.Forms;
using XamarinWeatherApp.Controls;
using XamarinWeatherApp.DataModel;
using XamarinWeatherApp.Helpers;
using XamarinWeatherApp.ViewModels;

namespace XamarinWeatherApp.Views
{
    public partial class CountryListPage : ContentPage
    {
        private CountryListPageViewModel countryListPageViewModel;

        public CountryListPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            countryListPageViewModel = (CountryListPageViewModel)BindingContext;
        }

        private double pageHeight;

        protected override void OnSizeAllocated(double width, double height)
        {
            //Navi.FadeTo(0);
            pageHeight = height;
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

        async void OnDelete(System.Object sender, System.EventArgs e)
        {
            var selectedItem = ((MenuItem)sender).CommandParameter as FavoriteLocationForecastDataModel;
            using (SQLiteConnection Conn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
            {
                Conn.CreateTable<FavoriteLocationForecastDataModel>();
                int rows = Conn.Delete(selectedItem);

                if (rows > 0)
                {
                    await PopupNavigation.Instance.PushAsync(new NotificationControl("S", "Success!, Your selection was deleted"));
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new NotificationControl("E", "Error!, Your selection was not removed"));
                }                
            }
            await countryListPageViewModel.LoadFavLocations();
        }
    }
}