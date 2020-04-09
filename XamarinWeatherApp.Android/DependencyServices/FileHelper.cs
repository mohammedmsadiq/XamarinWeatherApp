using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using XamarinWeatherApp.Droid.DependencyServices;
using XamarinWeatherApp.Interfaces;

[assembly: Dependency(typeof(FileHelper))]
namespace XamarinWeatherApp.Droid.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public List<string> GetSpecialFolders()
        {
            List<string> folders = new List<string>();

            foreach (var folder in Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                if (!string.IsNullOrEmpty(System.Environment.GetFolderPath((Environment.SpecialFolder)folder)))
                {
                    folders.Add($"{folder}={System.Environment.GetFolderPath((Environment.SpecialFolder)folder)}");
                }
            }
            return folders;
        }
    }
}

