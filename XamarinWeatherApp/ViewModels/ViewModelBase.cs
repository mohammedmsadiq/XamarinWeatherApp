﻿using System;
using System.Threading.Tasks;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace XamarinWeatherApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IPageLifecycleAware, IApplicationLifecycleAware, IConfirmNavigationAsync,
        IConfirmNavigation, IActiveAware
    {
        protected INavigationService NavigationService { get; }
        protected IPageDialogService DialogService { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            this.NavigationService = navigationService;
            this.DialogService = dialogService;
        }


        #region IActiveAware
        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;
        #endregion IActiveAware


        #region IConfirmNavigation
        public virtual bool CanNavigate(INavigationParameters parameters) => true;
        #endregion IConfirmNavigation


        #region IConfirmNavigationAsync
        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters) => Task.FromResult(CanNavigate(parameters));
        #endregion IConfirmNavigationAsync


        #region IApplicationLifecycleAware
        public void OnResume() { }

        public void OnSleep() { }
        #endregion IApplicationLifecycleAware


        #region IPageLifecycleAwar
        public void OnAppearing() { }

        public void OnDisappearing() { }
        #endregion IPageLifecycleAware


        #region IDestructible
        public void Destroy() { }
        #endregion IDestructible

        #region INavigationAware
        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters) { }
        #endregion INavigationAware

        #region ExecuteAsyncTask
        protected async Task ExecuteAction(Action action)
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = true; });

            try
            {
                action();
            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex);
            }

            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });
        }

        protected async Task ExecuteAsyncTask(Func<Task> actionDelegate)
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = true; });

            try
            {
                await ExecuteAsyncTaskWithNoLoading(actionDelegate);
            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex);
            }

            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });

        }

        protected async Task ExecuteAsyncTaskWithNoLoading(Func<Task> actionDelegate)
        {
            try
            {
                await actionDelegate();
            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex);
            }
        }

        protected async Task ShowErrorMessage(Exception ex)
        {
            //Dialog service, show error. 
            await DialogService.DisplayAlertAsync("Error", "Unable to Receive Data", "OK");
        }
        #endregion ExecuteAsyncTask
    }
}