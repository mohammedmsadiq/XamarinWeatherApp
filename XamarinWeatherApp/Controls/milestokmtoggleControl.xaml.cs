using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinWeatherApp.Controls
{
    public partial class milestokmtoggleControl : ContentView
    {
        public milestokmtoggleControl()
        {
            InitializeComponent();
            if (Settings.Settings.IsMPH)
            {
                Mph.TextColor = Color.White;
                kmh.TextColor = Color.LightGray;
            }
            else
            {
                Mph.TextColor = Color.LightGray;
                kmh.TextColor = Color.White;
            }
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(DegreeSwitchControl));

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            if (IsChecked)
            {
                Mph.TextColor = Color.White;
                kmh.TextColor = Color.LightGray;
            }
            else
            {
                Mph.TextColor = Color.LightGray;
                kmh.TextColor = Color.White;
            }
        }
    }
}
