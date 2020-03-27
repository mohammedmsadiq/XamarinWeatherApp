using System;

using Xamarin.Forms;

namespace XamarinWeatherApp.Converters
{
    public class BackgroundColorConverter : ContentPage
    {
        public BackgroundColorConverter()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

