using System;
using Android.Content;
using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinWeatherApp.Styling;
using static Android.Content.Res.Resources;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XamarinWeatherApp.Droid.Renderers.ThemeRenderer))]
namespace XamarinWeatherApp.Droid.Renderers
{
    public class ThemeRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        public ThemeRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            SetAppTheme();
        }

        void SetAppTheme()
        {
            if (Resources.Configuration.UiMode.HasFlag(UiMode.NightYes))
            {
                SetTheme(XamarinWeatherApp.Theme.Dark);
            }
            else
            {
                SetTheme(XamarinWeatherApp.Theme.Light);
            }
        }

        void SetTheme(Theme mode)
        {
            if (mode == XamarinWeatherApp.Theme.Dark)
            {
                if (App.AppTheme == XamarinWeatherApp.Theme.Dark)
                    return;

                App.Current.Resources = new DarkTheme();

                App.AppTheme = XamarinWeatherApp.Theme.Dark;
            }
            else
            {
                if (App.AppTheme != XamarinWeatherApp.Theme.Dark)
                    return;
                App.Current.Resources = new LightTheme();
                App.AppTheme = XamarinWeatherApp.Theme.Light;
            }
        }

    }
}

