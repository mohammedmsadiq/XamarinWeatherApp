using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using XamarinWeatherApp.Interfaces;
using XamarinWeatherApp.Models;

namespace XamarinWeatherApp.ViewModels
{
    public class SearchCountryPageViewModel : ViewModelBase
    {
        protected readonly ILocationService LocationService;
        ObservableCollection<GeoModel> data;

        public SearchCountryPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ILocationService locationService) : base(navigationService, dialogService)
        {
            this.LocationService = locationService;
            this.GoBackCommand = new DelegateCommand(async () => { await this.GoBackAction(); });
            this.Data = new ObservableCollection<GeoModel>();

        }

        private string _searchedText;

        public string SearchedText
        {
            get
            {
                return _searchedText;
            }
            set
            {
                _searchedText = value;
                OnPropertyChanged("SearchedText");
                SearchCommandExecute(SearchedText);
            }
        }

        private void SearchCommandExecute(string Text)
        {
            var r = Text;
            this.ExecuteAsyncTask(async () =>
            {
                var result = await this.LocationService.GetLocation(Text);
                this.Data.Clear();
                foreach (var item in result.Results)
                {
                    var itemToAdd = new GeoModel
                    {
                        name = item.name
                    };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.Data.Add(itemToAdd);
                    });
                }
            });           
        }

        public ObservableCollection<GeoModel> Data { get; set; }





        private async Task GoBackAction()
        {
            await NavigationService.GoBackAsync(animated: false);
        }

        public DelegateCommand GoBackCommand { get; private set; }
    }
}