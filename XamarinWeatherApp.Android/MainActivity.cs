using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Prism.Ioc;
using Prism;
using Xamarin.Forms;
using XamarinWeatherApp.Styling;
using Android.Content.Res;
using Android.Support.V7.App;

namespace XamarinWeatherApp.Droid
{
    [Activity(Label = "XamarinWeatherApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);

            LoadApplication(new App(new AndroidInitializer()));
            SetAppTheme();
            MessagingCenter.Subscribe<Page, Theme>(this, "ModeChanged", callback: OnModeChanged);

        }

        private void OnModeChanged(Page arg1, Theme theme)
        {
            if (theme == XamarinWeatherApp.Theme.Light)
            {
                Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightNo);
            }
            else
            {
                Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightYes);
            }
            SetTheme(theme);
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
                {
                    App.Current.Resources = new DarkTheme();
                }
            }
            else
            {
                if (App.AppTheme != XamarinWeatherApp.Theme.Dark)
                {
                    App.Current.Resources = new LightTheme();
                }                
            }
            App.AppTheme = mode;
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry) { }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}