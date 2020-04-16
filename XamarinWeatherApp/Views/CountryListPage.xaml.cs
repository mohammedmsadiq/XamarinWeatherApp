using System;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using XamarinWeatherApp.DataModel;
using XamarinWeatherApp.Helpers;

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
            pageHeight = height;
            DetailSection.TranslationY = pageHeight;
            base.OnSizeAllocated(width, height);
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
                
            //int currentIndex = CoursesCarouselView.Position;
            //if (currentIndex >= 0 && currentIndex < ((HomePageViewModel)this.BindingContext).CourseItems.Count)
            //{
            //    var courseSelected = ((HomePageViewModel)this.BindingContext).CourseItems[currentIndex];
            //    ((HomePageViewModel)this.BindingContext).CourseSelectedCommand.Execute(courseSelected as Models.CourseModel);
            //}
            DisplayAlert("Success", "Deleted item", "OK");
        }

        protected override async void OnAppearing()
        {
            await Navi.FadeTo(0);
            await Task.Delay(Constants.Constants.AnimationDelay);
            await DetailSection.TranslateTo(0, 0, 500, Easing.SinOut);
            await Navi.FadeTo(1, 500, Easing.SinIn);
            base.OnAppearing();
        }

        void SwipeView_SwipeStarted(System.Object sender, Xamarin.Forms.SwipeStartedEventArgs e)
        {
        }
    }
}
