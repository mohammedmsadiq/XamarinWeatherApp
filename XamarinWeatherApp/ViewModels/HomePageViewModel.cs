using System;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace XamarinWeatherApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private string textForLabel;
        private int offSetA;
        private int offSetB;

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            this.Title = "Main Page";
            this.TextForLabel = "This is some text";
            this.OffSetA = 1;
            this.OffSetB = 1;
        }




        public string TextForLabel
        {
            get => this.textForLabel;
            set => SetProperty(ref this.textForLabel, value);
        }

        public int OffSetA
        {
            get => this.offSetA;
            set => SetProperty(ref this.offSetA, value);
        }

        public int OffSetB
        {
            get => this.offSetB;
            set => SetProperty(ref this.offSetB, value);
        }
    }
}
