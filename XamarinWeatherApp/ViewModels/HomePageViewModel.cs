using System;
using Prism.Mvvm;

namespace XamarinWeatherApp.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        public HomePageViewModel()
        {
            this.Title = "Main Page";
            this.TextForLabel = "This is some text";
        }

        private string title;
        public string Title
        {
            get => this.title;
            set => SetProperty(ref this.title, value);
        }

        private string textForLabel;
        public string TextForLabel
        {
            get => this.textForLabel;
            set => SetProperty(ref this.textForLabel, value);
        }
    }
}
