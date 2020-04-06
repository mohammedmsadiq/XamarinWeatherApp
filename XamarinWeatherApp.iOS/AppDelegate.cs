using Foundation;
using Lottie.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using XamarinWeatherApp.Styling;

namespace XamarinWeatherApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            LoadApplication(new App(new iOSInitializer()));
            MessagingCenter.Subscribe<Page, Theme>(this, "ModeChanged", callback: OnModeChanged);
            AnimationViewRenderer.Init();

            return base.FinishedLaunching(app, options);
        }

        private void OnModeChanged(Page page, Theme theme)
        {
            SetTheme(theme);
        }

        void SetTheme(Theme mode)
        {
            Window.OverrideUserInterfaceStyle = mode == Theme.Dark ? UIUserInterfaceStyle.Dark : UIUserInterfaceStyle.Light;

            if (mode == XamarinWeatherApp.Theme.Dark)
            {
                if (App.AppTheme == XamarinWeatherApp.Theme.Dark)
                    return;
                App.Current.Resources = new DarkTheme();
            }
            else
            {
                if (App.AppTheme != XamarinWeatherApp.Theme.Dark)
                    return;
                App.Current.Resources = new LightTheme();
            }
            App.AppTheme = mode;
        }
    }
}
