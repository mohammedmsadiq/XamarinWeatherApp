using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinWeatherApp.Interfaces;

namespace XamarinWeatherApp.Helpers
{
    public static class StorageHelper
    {
        public static List<string> GetSpecialFolders()
        {
            return Xamarin.Forms.DependencyService.Get<IFileHelper>().GetSpecialFolders();
        }

        public static string GetLocalFilePath()
        {
            return Xamarin.Forms.DependencyService.Get<IFileHelper>().GetLocalFilePath("MoWeather.db3");
        }
    }
}

