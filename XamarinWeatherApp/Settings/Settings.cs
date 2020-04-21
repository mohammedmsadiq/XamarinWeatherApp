using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace XamarinWeatherApp.Settings
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static bool IsCelsius
        {
            get => AppSettings.GetValueOrDefault(nameof(IsCelsius), true);
            set => AppSettings.AddOrUpdateValue(nameof(IsCelsius), value);
        }

        public static bool IsMPH
        {
            get => AppSettings.GetValueOrDefault(nameof(IsMPH), true);
            set => AppSettings.AddOrUpdateValue(nameof(IsMPH), value);
        }

        public static bool HasFavLocationsSaved
        {
            get => AppSettings.GetValueOrDefault(nameof(HasFavLocationsSaved), false);
            set => AppSettings.AddOrUpdateValue(nameof(HasFavLocationsSaved), value);
        }

        public static bool IsDefaultGridVewVisible 
        {
            get => AppSettings.GetValueOrDefault(nameof(IsDefaultGridVewVisible), true);
            set => AppSettings.AddOrUpdateValue(nameof(IsDefaultGridVewVisible), value);
        }

    }
}