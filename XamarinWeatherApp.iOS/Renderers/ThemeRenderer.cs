using System;
using Intents;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinWeatherApp.Styling;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XamarinWeatherApp.iOS.Renderers.ThemeRenderer))]
namespace XamarinWeatherApp.iOS.Renderers
{
    public class ThemeRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        //protected override void OnElementChanged(VisualElementChangedEventArgs e)
        //{
        //    base.OnElementChanged(e);
        //    if (e.OldElement != null || Element == null)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        SetTheme();
        //    }
        //    catch (Exception ex) { }
        //}

        //public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        //{
        //    base.TraitCollectionDidChange(previousTraitCollection);
        //    Console.WriteLine($"TraitCollectionDidChange: {TraitCollection.UserInterfaceStyle} != {previousTraitCollection.UserInterfaceStyle}");

        //    if (TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
        //    {
        //        SetTheme();
        //    }
        //}

        //private void SetTheme()
        //{
        //    if (TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark)
        //    {
        //        if (App.AppTheme == XamarinWeatherApp.Theme.Dark)
        //        {
        //            return;
        //        }

        //        App.Current.Resources = new DarkTheme();
        //        App.AppTheme = XamarinWeatherApp.Theme.Dark;
        //    }
        //    else
        //    {
        //        if (App.AppTheme != XamarinWeatherApp.Theme.Dark)
        //        {
        //            return;
        //        }

        //        App.Current.Resources = new LightTheme();
        //        App.AppTheme = XamarinWeatherApp.Theme.Light;
        //    }
        //}
    }
}

