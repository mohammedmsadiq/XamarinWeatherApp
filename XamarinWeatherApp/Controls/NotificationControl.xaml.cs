using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XamarinWeatherApp.Styling;

namespace XamarinWeatherApp.Controls
{
    public partial class NotificationControl : PopupPage
    {
        public NotificationControl()
        {
            InitializeComponent();
        }
        public NotificationControl(string mTitle, string msg)
        {
            InitializeComponent();
            ChangecolorMsg(mTitle, msg);
        }

        private async void ChangecolorMsg(string mtitle, string msg)
        {
            if (mtitle == "S")
            {
                imgAlert.Source = "success.png";
                ParentView.BackgroundColor = Color.FromHex("#43A047");
            }
            else if (mtitle == "E")
            {
                imgAlert.Source = "error.png";
                ParentView.BackgroundColor = Color.FromHex("#F2DEDE");
            }

            LblMsg.Text = msg;
            await Task.Delay(500);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(4000);
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}