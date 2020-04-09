using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinWeatherApp.Interfaces
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);

        List<string> GetSpecialFolders();
    }
}

