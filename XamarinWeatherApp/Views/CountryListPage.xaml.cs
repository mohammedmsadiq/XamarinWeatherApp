using System;
using System.Threading.Tasks;
using SQLite;
using Unity.Injection;
using Xamarin.Forms;
using XamarinWeatherApp.DataModel;
using XamarinWeatherApp.Helpers;

namespace XamarinWeatherApp.Views
{
    public partial class CountryListPage : ContentPage
    {
        FavoriteLocationForecastDataModel selectedItem;
        public CountryListPage()
        {
            InitializeComponent();
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

        void OnDelete(System.Object sender, System.EventArgs e)
        {
            var r = ((MenuItem)sender).CommandParameter as FavoriteLocationForecastDataModel;

            var t = r.LocationName;

            using (SQLiteConnection Conn = new SQLiteConnection(StorageHelper.GetLocalFilePath()))
            {
                Conn.CreateTable<FavoriteLocationForecastDataModel>();
                int rows = Conn.Delete(r);

                //if (rows > 0)
                //{
                //    DisplayAlert("Success", "Deleted", "OK");
                //}
                //else
                //{
                //    DisplayAlert("Failed", "Not Deleted", "OK");
                //}
            }
        }
    }
}